using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace VideoAdRemover
{
    /// <summary>
    /// 视频编辑模块
    /// 负责移除广告片段，生成新的视频文件
    /// </summary>
    public class VideoEditor
    {
        /// <summary>
        /// 移除视频中的广告片段
        /// </summary>
        /// <param name="inputPath">输入视频路径</param>
        /// <param name="outputPath">输出视频路径</param>
        /// <param name="adSegments">广告片段列表</param>
        /// <param name="progressCallback">进度回调函数</param>
        /// <returns>操作是否成功</returns>
        public bool RemoveAds(string inputPath, string outputPath, List<AdSegment> adSegments, Action<double> progressCallback)
        {
            try
            {
                Logger.Info($"开始移除广告，输入路径：{inputPath}，输出路径：{outputPath}，广告片段数量：{adSegments.Count}");
                
                // 对广告片段按时间排序
                List<AdSegment> sortedSegments = adSegments.OrderBy(s => s.StartTime).ToList();
                
                Logger.Info("广告片段已排序");

                // 查找FFmpeg可执行文件
                string ffmpegPath = FindFFmpeg();
                if (string.IsNullOrEmpty(ffmpegPath))
                    throw new Exception("未找到FFmpeg可执行文件");

                // 使用分段提取合并法移除广告
                bool result = RemoveAdsWithSegmentMerge(ffmpegPath, inputPath, outputPath, sortedSegments, progressCallback);
                
                if (result)
                {
                    Logger.Info("广告移除成功");
                }
                else
                {
                    Logger.Error("广告移除失败");
                }
                
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error("视频编辑失败", ex);
                throw new Exception($"视频编辑失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 使用分段提取合并法移除广告片段
        /// </summary>
        /// <param name="ffmpegPath">FFmpeg可执行文件路径</param>
        /// <param name="inputPath">输入视频路径</param>
        /// <param name="outputPath">输出视频路径</param>
        /// <param name="adSegments">已排序的广告片段列表</param>
        /// <param name="progressCallback">进度回调函数</param>
        /// <returns>操作是否成功</returns>
        private bool RemoveAdsWithSegmentMerge(string ffmpegPath, string inputPath, string outputPath, List<AdSegment> adSegments, Action<double> progressCallback)
        {
            // 获取视频信息
            VideoAnalyzer analyzer = new VideoAnalyzer();
            VideoInfo videoInfo = analyzer.GetVideoInfo(inputPath);
            double videoDuration = videoInfo.Duration;

            // 生成保留片段列表
            List<(double Start, double End)> keepSegments = new List<(double, double)>();

            if (adSegments.Count == 0)
            {
                // 没有广告，直接复制
                Logger.Info("没有广告片段，直接复制视频");
                string arguments = $"-hwaccel auto -y -i \"{inputPath}\" -c copy \"{outputPath}\"";
                ExecuteFfmpegCommand(ffmpegPath, arguments, progressCallback);
                return true;
            }
            else
            {
                // 添加第一个广告之前的片段
                if (adSegments[0].StartTime > 0.1) // 允许0.1秒的误差
                {
                    keepSegments.Add((0, adSegments[0].StartTime));
                }

                // 添加广告之间的片段
                for (int i = 0; i < adSegments.Count - 1; i++)
                {
                    keepSegments.Add((adSegments[i].EndTime, adSegments[i + 1].StartTime));
                }

                // 添加最后一个广告之后的片段
                if (adSegments[^1].EndTime < videoDuration - 0.1) // 允许0.1秒的误差
                {
                    keepSegments.Add((adSegments[^1].EndTime, videoDuration));
                }
            }

            if (keepSegments.Count == 0)
            {
                throw new Exception("没有可保留的视频片段");
            }
            else if (keepSegments.Count == 1)
            {
                // 只有一个片段，直接提取
                Logger.Info("只有一个保留片段，直接提取");
                var segment = keepSegments[0];
                string arguments = $"-hwaccel auto -y -i \"{inputPath}\" -ss {segment.Start} -to {segment.End} -c copy -avoid_negative_ts 1 \"{outputPath}\"";
                ExecuteFfmpegCommand(ffmpegPath, arguments, progressCallback);
                return true;
            }
            else
            {
                // 多个保留片段，使用分段提取合并法
                Logger.Info($"有 {keepSegments.Count} 个保留片段，使用分段提取合并法");
                return MergeMultipleSegments(ffmpegPath, inputPath, outputPath, keepSegments, progressCallback);
            }
        }

        /// <summary>
        /// 合并多个视频片段
        /// </summary>
        /// <param name="ffmpegPath">FFmpeg可执行文件路径</param>
        /// <param name="inputPath">输入视频路径</param>
        /// <param name="outputPath">输出视频路径</param>
        /// <param name="keepSegments">保留片段列表</param>
        /// <param name="progressCallback">进度回调函数</param>
        /// <returns>操作是否成功</returns>
        private bool MergeMultipleSegments(string ffmpegPath, string inputPath, string outputPath, List<(double Start, double End)> keepSegments, Action<double> progressCallback)
        {
            // 生成唯一ID，用于临时文件名，避免冲突
            string uniqueId = Guid.NewGuid().ToString("N");
            string directory = Path.GetDirectoryName(outputPath) ?? Environment.CurrentDirectory;
            List<string> tempFiles = new List<string>();
            string listPath = Path.Combine(directory, $"temp_list_{uniqueId}.txt");

            try
            {
                // 1. 提取所有保留片段
                for (int i = 0; i < keepSegments.Count; i++)
                {
                    var segment = keepSegments[i];
                    string tempFilePath = Path.Combine(directory, $"temp_part{i}_{uniqueId}.mp4");
                    tempFiles.Add(tempFilePath);

                    double duration = segment.End - segment.Start;
                    Logger.Info($"正在提取片段 {i + 1}/{keepSegments.Count} ({segment.Start:F2}s - {segment.End:F2}s)...");
                    
                    // 执行提取命令
                    string arguments = $"-hwaccel auto -y -i \"{inputPath}\" -ss {segment.Start} -t {duration} -c copy -avoid_negative_ts 1 \"{tempFilePath}\"";
                    ExecuteFfmpegCommand(ffmpegPath, arguments, progress => {
                        // 提取片段进度分配
                        double segmentProgress = (double)i / keepSegments.Count * 66;
                        double currentProgress = segmentProgress + (progress / keepSegments.Count * 66);
                        progressCallback?.Invoke(currentProgress);
                    });
                }

                // 2. 创建合并列表文件
                var sb = new StringBuilder();
                foreach (string tempFile in tempFiles)
                {
                    sb.AppendLine($"file '{tempFile.Replace("'", "''")}'");
                }
                File.WriteAllText(listPath, sb.ToString());
                Logger.Info($"已创建合并列表文件: {listPath}");

                // 3. 合并视频
                Logger.Info("正在合并视频...");
                string mergeArguments = $"-hwaccel auto -y -f concat -safe 0 -i \"{listPath}\" -c copy \"{outputPath}\"";
                
                try
                {
                    // 优先使用流复制合并
                    ExecuteFfmpegCommand(ffmpegPath, mergeArguments, progress => {
                        double mergeProgress = 66 + (progress * 0.34);
                        progressCallback?.Invoke(mergeProgress);
                    });
                }
                catch (Exception ex)
                {
                    // 如果流复制失败，尝试重新编码
                    Logger.Warning($"流复制失败，尝试重新编码: {ex.Message}");
                    mergeArguments = $"-hwaccel auto -y -f concat -safe 0 -i \"{listPath}\" -c:v libx264 -c:a aac \"{outputPath}\"";
                    ExecuteFfmpegCommand(ffmpegPath, mergeArguments, progress => {
                        double mergeProgress = 66 + (progress * 0.34);
                        progressCallback?.Invoke(mergeProgress);
                    });
                }

                return true;
            }
            finally
            {
                // 清理临时文件
                Logger.Info("清理临时文件...");
                foreach (string tempFile in tempFiles)
                {
                    DeleteFileIfExists(tempFile);
                }
                DeleteFileIfExists(listPath);
            }
        }

        /// <summary>
        /// 执行FFmpeg命令
        /// </summary>
        /// <param name="ffmpegPath">FFmpeg可执行文件路径</param>
        /// <param name="arguments">命令参数</param>
        /// <param name="progressCallback">进度回调函数</param>
        private void ExecuteFfmpegCommand(string ffmpegPath, string arguments, Action<double> progressCallback)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = ffmpegPath,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process())
            {
                process.StartInfo = startInfo;
                
                // 异步读取输出流，避免缓冲区满导致阻塞
                StringBuilder outputBuilder = new StringBuilder();
                StringBuilder errorBuilder = new StringBuilder();
                
                process.OutputDataReceived += (sender, e) => 
                {
                    if (e.Data != null)
                    {
                        outputBuilder.AppendLine(e.Data);
                    }
                };
                
                process.ErrorDataReceived += (sender, e) => 
                {
                    if (e.Data != null)
                    {
                        string line = e.Data;
                        errorBuilder.AppendLine(line);
                        
                        // 解析FFmpeg进度信息
                        if (progressCallback != null && line.Contains("time="))
                        {
                            try
                            {
                                // 提取时间信息，格式如：time=00:00:05.00
                                int timeIndex = line.IndexOf("time=") + 5;
                                int nextSpaceIndex = line.IndexOf(' ', timeIndex);
                                if (nextSpaceIndex > timeIndex)
                                {
                                    string timeStr = line.Substring(timeIndex, nextSpaceIndex - timeIndex);
                                    
                                    // 将时间字符串转换为秒数
                                    if (TimeSpan.TryParse(timeStr, out TimeSpan currentTime))
                                    {
                                        // 简化进度计算，根据当前处理时间估算进度
                                        double progress = Math.Min(currentTime.TotalSeconds / 3600 * 100, 100);
                                        progressCallback(progress);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // 进度解析失败，忽略并继续
                                Logger.Warning($"进度解析失败: {ex.Message}");
                            }
                        }
                    }
                };
                
                process.Start();
                Logger.Info($"ffmpeg进程已启动，执行命令: {arguments}");
                
                // 开始异步读取
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                
                // 等待进程退出
                process.WaitForExit();
                
                if (process.ExitCode != 0)
                {
                    string errorOutput = errorBuilder.ToString();
                    Logger.Error($"ffmpeg执行失败，退出码: {process.ExitCode}");
                    Logger.Error($"ffmpeg错误输出: {errorOutput}");
                    throw new Exception($"ffmpeg执行失败: {errorOutput}\n命令: {arguments}");
                }
                else
                {
                    Logger.Info($"ffmpeg执行成功，退出码: {process.ExitCode}");
                }
            }
        }

        /// <summary>
        /// 删除文件，如果不存在则忽略
        /// </summary>
        /// <param name="path">文件路径</param>
        private void DeleteFileIfExists(string path)
        {
            if (File.Exists(path))
            {
                try 
                {
                    File.Delete(path);
                    Logger.Info($"已删除临时文件: {path}");
                } 
                catch (Exception ex)
                {
                    Logger.Warning($"删除临时文件失败: {path}, 错误: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 查找FFmpeg可执行文件
        /// </summary>
        /// <returns>FFmpeg可执行文件路径</returns>
        private string? FindFFmpeg()
        {
            // 1. 检查当前目录
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            if (!string.IsNullOrEmpty(currentDir))
            {
                string ffmpegPath = Path.Combine(currentDir, "ffmpeg.exe");
                if (File.Exists(ffmpegPath))
                    return ffmpegPath;
            }

            // 2. 检查系统环境变量PATH
            string? pathEnv = Environment.GetEnvironmentVariable("PATH");
            if (!string.IsNullOrEmpty(pathEnv))
            {
                foreach (string path in pathEnv.Split(';'))
                {
                    if (!string.IsNullOrEmpty(path))
                    {
                        string ffmpegPath = Path.Combine(path, "ffmpeg.exe");
                        if (File.Exists(ffmpegPath))
                            return ffmpegPath;
                    }
                }
            }

            // 3. 检查常见安装路径
            string[] commonPaths = {
                @"C:\Program Files\ffmpeg\bin\ffmpeg.exe",
                @"C:\Program Files (x86)\ffmpeg\bin\ffmpeg.exe",
                @"D:\ffmpeg\bin\ffmpeg.exe"
            };

            foreach (string path in commonPaths)
            {
                if (File.Exists(path))
                    return path;
            }

            return null;
        }
    }
}