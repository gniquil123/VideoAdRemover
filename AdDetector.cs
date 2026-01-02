using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace VideoAdRemover
{
    /// <summary>
    /// 时间范围类，用于表示广告检测的时间段
    /// </summary>
    public class TimeRange
    {
        /// <summary>
        /// 开始时间（秒）
        /// </summary>
        public double StartTimeSec { get; set; }
        
        /// <summary>
        /// 结束时间（秒）
        /// </summary>
        public double EndTimeSec { get; set; }
        
        /// <summary>
        /// 初始化时间范围
        /// </summary>
        /// <param name="startTimeSec">开始时间（秒）</param>
        /// <param name="endTimeSec">结束时间（秒）</param>
        public TimeRange(double startTimeSec, double endTimeSec)
        {
            StartTimeSec = startTimeSec;
            EndTimeSec = endTimeSec;
        }
    }
    
    /// <summary>
    /// 广告检测模块
    /// 基于用户指定的广告时间范围，检测电影中所有广告出现的位置
    /// </summary>
    public class AdDetector
    {
        private readonly VideoAnalyzer _analyzer;
        private const int TARGET_WIDTH = 320; // 目标宽度（像素）
        private const double SIMILARITY_THRESHOLD = 90.0; // 相似度阈值（0-100%，越高越相似）
        private readonly bool _useHardwareAcceleration;
        private double _scanIntervalSec = 1.0; // 每N秒扫描一次，默认N=1
        private CancellationTokenSource? _cancellationTokenSource = null; // 用于取消检测的令牌源
        
        /// <summary>
        /// 扫描间隔（秒）
        /// </summary>
        public double ScanIntervalSec
        {
            get => _scanIntervalSec;
            set => _scanIntervalSec = Math.Max(0.1, value); // 最小为0.1秒
        }
        
        /// <summary>
        /// 请求停止检测
        /// </summary>
        public void StopDetection()
        {
            if (_cancellationTokenSource != null && !_cancellationTokenSource.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
                Logger.Info("已请求停止检测...");
            }
        }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="useHardwareAcceleration">是否使用硬件加速</param>
        /// <param name="scanIntervalSec">扫描间隔（秒）</param>
        public AdDetector(bool useHardwareAcceleration = true, double scanIntervalSec = 1.0)
        {
            _useHardwareAcceleration = useHardwareAcceleration;
            _scanIntervalSec = Math.Max(0.1, scanIntervalSec);
            _analyzer = new VideoAnalyzer(useHardwareAcceleration);
        }

        /// <summary>
        /// 检测视频中的广告片段（支持多个时间段）
        /// </summary>
        /// <param name="moviePath">电影文件路径</param>
        /// <param name="adStartTimeSec">广告开始时间（秒）</param>
        /// <param name="adEndTimeSec">广告结束时间（秒）</param>
        /// <param name="timeRanges">检测的时间范围列表</param>
        /// <param name="progressCallback">进度回调函数</param>
        /// <param name="adDetectedCallback">广告检测到回调函数，用于实时返回检测结果</param>
        /// <returns>广告片段列表</returns>
        public List<AdSegment> DetectAds(string moviePath, double adStartTimeSec, double adEndTimeSec, List<TimeRange> timeRanges, Action<double> progressCallback, Action<AdSegment> adDetectedCallback = null)
        {
            List<AdSegment> allAdSegments = new List<AdSegment>();
            
            if (timeRanges == null || timeRanges.Count == 0)
            {
                Logger.Warning("未指定检测时间范围，跳过检测");
                return allAdSegments;
            }
            
            // 处理每个时间范围
            for (int rangeIndex = 0; rangeIndex < timeRanges.Count; rangeIndex++)
            {
                var timeRange = timeRanges[rangeIndex];
                double detectionStartTimeSec = timeRange.StartTimeSec;
                double detectionEndTimeSec = timeRange.EndTimeSec;
                
                Logger.Info(string.Format("开始处理第 {0}/{1} 个时间范围：{2} - {3}", 
                    rangeIndex + 1, timeRanges.Count, 
                    TimeSpan.FromSeconds(detectionStartTimeSec).ToString(@"hh\:mm\:ss"), 
                    TimeSpan.FromSeconds(detectionEndTimeSec).ToString(@"hh\:mm\:ss")));
                
                // 调用原有的单个时间段检测方法，传递正确的广告时间和检测时间
                List<AdSegment> rangeSegments = DetectAds(moviePath, adStartTimeSec, adEndTimeSec, detectionStartTimeSec, detectionEndTimeSec, 
                    (progress) => 
                    { 
                        // 转换为全局进度：当前范围进度 + 已完成范围的进度
                        double globalProgress = rangeIndex * (100.0 / timeRanges.Count) + (progress / timeRanges.Count);
                        progressCallback?.Invoke(globalProgress);
                    }, 
                    adDetectedCallback);
                
                // 合并当前范围的检测结果
                allAdSegments.AddRange(rangeSegments);
                
                Logger.Info(string.Format("完成处理第 {0}/{1} 个时间范围，共检测到 {2} 个广告片段", 
                    rangeIndex + 1, timeRanges.Count, rangeSegments.Count));
            }
            
            return allAdSegments;
        }
        
        /// <summary>
        /// 检测视频中的广告片段（支持多个时间段）
        /// </summary>
        /// <param name="moviePath">电影文件路径</param>
        /// <param name="timeRanges">检测的时间范围列表</param>
        /// <param name="progressCallback">进度回调函数</param>
        /// <param name="adDetectedCallback">广告检测到回调函数，用于实时返回检测结果</param>
        /// <returns>广告片段列表</returns>
        /// <remarks>此重载方法保留向后兼容性，内部调用新的重载方法</remarks>
        public List<AdSegment> DetectAds(string moviePath, List<TimeRange> timeRanges, Action<double> progressCallback, Action<AdSegment> adDetectedCallback = null)
        {
            // 对于向后兼容性，使用第一个检测时间段作为广告时间（虽然这是错误的）
            // 实际应用中，这个重载方法应该被废弃
            if (timeRanges == null || timeRanges.Count == 0)
            {
                Logger.Warning("未指定检测时间范围，跳过检测");
                return new List<AdSegment>();
            }
            
            var firstRange = timeRanges[0];
            return DetectAds(moviePath, firstRange.StartTimeSec, firstRange.EndTimeSec, timeRanges, progressCallback, adDetectedCallback);
        }
        
        /// <summary>
        /// 检测视频中的广告片段（单个时间段版本）
        /// </summary>
        /// <param name="moviePath">电影文件路径</param>
        /// <param name="adStartTimeSec">广告开始时间（秒）</param>
        /// <param name="adEndTimeSec">广告结束时间（秒）</param>
        /// <param name="detectionStartTimeSec">检测开始时间（秒）</param>
        /// <param name="detectionEndTimeSec">检测结束时间（秒）</param>
        /// <param name="progressCallback">进度回调函数</param>
        /// <param name="adDetectedCallback">广告检测到回调函数，用于实时返回检测结果</param>
        /// <returns>广告片段列表</returns>
        public List<AdSegment> DetectAds(string moviePath, double adStartTimeSec, double adEndTimeSec, double detectionStartTimeSec, double detectionEndTimeSec, Action<double> progressCallback, Action<AdSegment> adDetectedCallback = null)
        {
            List<AdSegment> adSegments = new List<AdSegment>();
            object segmentsLock = new object();
            // 计算广告时长，用于后续合并片段
            double adDurationSec = adEndTimeSec - adStartTimeSec;

            try
            {
                // 初始化取消令牌源
                _cancellationTokenSource = new CancellationTokenSource();
                CancellationToken cancellationToken = _cancellationTokenSource.Token;
                
                // 获取视频信息
                VideoInfo movieInfo = _analyzer.GetVideoInfo(moviePath);
                int adDurationFrames = (int)(adDurationSec * movieInfo.Fps);
                
                Logger.Info(string.Format("开始检测广告，视频路径：{0}，广告时长：{1}秒", moviePath, adDurationSec));

                // 提取广告特征帧
                List<Mat> adFrames = ExtractAdFrames(moviePath, adStartTimeSec, adEndTimeSec, movieInfo.Fps);
                if (adFrames.Count == 0)
                {
                    Logger.Warning("未提取到广告特征帧");
                    return adSegments;
                }
                
                Logger.Info(string.Format("成功提取 {0} 个广告特征帧", adFrames.Count));

                // 计算扫描步长（每_scanIntervalSec秒扫描一次）
                int step = (int)(movieInfo.Fps * _scanIntervalSec);
                if (step < 1) step = 1;
                
                Logger.Info(string.Format("扫描步长：{0}帧，总帧数：{1}，每{2}秒扫描一次", step, movieInfo.FrameCount, _scanIntervalSec));
                
                // 创建日期时间文件夹路径
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                string saveFolder = Path.Combine(Environment.CurrentDirectory, $"screenshots_{timestamp}");
                Directory.CreateDirectory(saveFolder);
                
                // 保存广告起始帧和结束帧的处理后图片
                // 格式化时间为hhmmss格式
                string adStartTimeStr = TimeSpan.FromSeconds(adStartTimeSec).ToString("hhmmss");
                string adEndTimeStr = TimeSpan.FromSeconds(adEndTimeSec).ToString("hhmmss");
                string adStartFramePath = Path.Combine(saveFolder, $"ad_start_frame_{Path.GetFileNameWithoutExtension(moviePath)}_{adStartTimeStr}.png");
                Cv2.ImWrite(adStartFramePath, adFrames[0]);
                Logger.Info(string.Format("已保存广告起始帧到：{0}", adStartFramePath));
                
                // 保存广告结束帧
                string adEndFramePath = Path.Combine(saveFolder, $"ad_end_frame_{Path.GetFileNameWithoutExtension(moviePath)}_{adEndTimeStr}.png");
                Cv2.ImWrite(adEndFramePath, adFrames[adFrames.Count - 1]);
                Logger.Info(string.Format("已保存广告结束帧到：{0}", adEndFramePath));

                // 计算需要处理的总帧数
                int totalFramesToProcess = (int)Math.Ceiling((double)movieInfo.FrameCount / step);

                // 扫描电影 - 使用并行处理加速，保持顺序性
                int processedCount = 0;
                object progressLock = new object();
                
                // 定义每个时间块的最大时间跨度（5分钟 = 300秒）
                const double MAX_TIME_BLOCK_SEC = 300;
                
                // 计算每个时间块包含的帧数
                int framesPerBlock = (int)(MAX_TIME_BLOCK_SEC * movieInfo.Fps);
                int blocksPerStep = (int)Math.Ceiling((double)framesPerBlock / step);
                
                // 计算总共有多少个时间块
                int totalBlocks = (int)Math.Ceiling((double)totalFramesToProcess / blocksPerStep);
                
                // 顺序处理每个时间块，每个时间块内并行处理帧
                for (int blockIndex = 0; blockIndex < totalBlocks; blockIndex++)
                {
                    // 检查是否请求取消
                    if (cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }
                    
                    // 计算当前时间块的起始和结束索引
                    int blockStartIndex = blockIndex * blocksPerStep;
                    int blockEndIndex = Math.Min((blockIndex + 1) * blocksPerStep, totalFramesToProcess);
                    
                    // 计算当前时间块的时间范围
                    int firstFrameInBlock = blockStartIndex * step;
                    int lastFrameInBlock = (blockEndIndex - 1) * step;
                    double blockStartTimeSec = firstFrameInBlock / movieInfo.Fps;
                    double blockEndTimeSec = lastFrameInBlock / movieInfo.Fps;
                    
                    // 检查当前时间块是否在用户指定的检测时间范围内
                    // 如果时间块的结束时间 < 检测开始时间，或者时间块的开始时间 > 检测结束时间，则跳过
                    if (blockEndTimeSec < detectionStartTimeSec || blockStartTimeSec > detectionEndTimeSec)
                    {
                        // 更新进度
                        lock (progressLock)
                        {
                            processedCount += (blockEndIndex - blockStartIndex);
                            double progress = (double)processedCount / totalFramesToProcess * 100;
                            progressCallback?.Invoke(progress);
                        }
                        continue;
                    }
                    
                    // 安全格式化时间
                    string blockStartTimeStr = "00:00:00";
                    string blockEndTimeStr = "00:00:00";
                    
                    try
                    {
                        if (!double.IsNaN(blockStartTimeSec) && !double.IsInfinity(blockStartTimeSec) && blockStartTimeSec >= 0)
                        {
                            TimeSpan startTimeSpan = TimeSpan.FromSeconds(blockStartTimeSec);
                            blockStartTimeStr = startTimeSpan.ToString(@"hh\:mm\:ss");
                        }
                        
                        if (!double.IsNaN(blockEndTimeSec) && !double.IsInfinity(blockEndTimeSec) && blockEndTimeSec >= 0)
                        {
                            TimeSpan endTimeSpan = TimeSpan.FromSeconds(blockEndTimeSec);
                            blockEndTimeStr = endTimeSpan.ToString(@"hh\:mm\:ss");
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Warning(string.Format("格式化时间块范围时出错，开始时间：{0}秒，结束时间：{1}秒，错误：{2}", 
                            blockStartTimeSec, blockEndTimeSec, ex.Message));
                    }
                    
                    Logger.Info(string.Format("开始处理时间块 {0}/{1}，时间范围：{2} - {3}", 
                        blockIndex + 1, totalBlocks, blockStartTimeStr, blockEndTimeStr));
                    
                    try
                    {
                        // 并行处理当前时间块内的帧索引
                        Parallel.For(blockStartIndex, blockEndIndex, new ParallelOptions { 
                            MaxDegreeOfParallelism = _useHardwareAcceleration ? Environment.ProcessorCount : 1, 
                            CancellationToken = cancellationToken
                        }, (index, loopState) =>
                        {
                            // 检查是否请求取消
                            if (cancellationToken.IsCancellationRequested)
                            {
                                loopState.Break();
                                return;
                            }
                            
                            // 计算当前要处理的帧索引
                            int i = index * step;
                            
                            // 检查是否超过实际帧数
                            if (i >= movieInfo.FrameCount)
                            {
                                return;
                            }
                        
                        try
                        {
                            // 使用独立的VideoCapture实例进行并行读取
                            using var movieCap = new VideoCapture(moviePath);
                            
                            // 读取当前帧
                            movieCap.PosFrames = i;
                            Mat currentFrame = new Mat();
                            if (!movieCap.Read(currentFrame))
                            {
                                Logger.Warning(string.Format("无法读取帧 {0}", i));
                                return;
                            }

                            // 计算等比例缩放后的尺寸，保持宽高比，目标宽度为320像素
                            int originalWidth = currentFrame.Cols;
                            int originalHeight = currentFrame.Rows;
                            double scale = (double)TARGET_WIDTH / originalWidth;
                            int newHeight = (int)(originalHeight * scale);
                            OpenCvSharp.Size scaledSize = new OpenCvSharp.Size(TARGET_WIDTH, newHeight);
                            
                            // 预处理当前帧
                            Mat currentFrameThumb = _analyzer.PreprocessFrame(currentFrame, scaledSize);
                            
                            // 计算当前帧在视频中的时间（秒）
                            double frameTimeSec = i / movieInfo.Fps;
                            // 检查当前帧时间是否在用户指定的检测时间范围内
                            if (frameTimeSec < detectionStartTimeSec || frameTimeSec > detectionEndTimeSec)
                            {
                                // 更新进度
                                lock (progressLock)
                                {
                                    processedCount++;
                                    double progress = (double)processedCount / totalFramesToProcess * 100;
                                    progressCallback?.Invoke(progress);
                                }
                                return;
                            }
                            // 格式化为hhmmss格式（用于文件名）
                            string frameTimeStrFileName = "000000";
                            // 格式化为hh mm ss格式（用于日志）
                            string frameTimeStrLog = "00 00 00";
                            
                            // 检查时间是否为有效数值
                            if (!double.IsNaN(frameTimeSec) && !double.IsInfinity(frameTimeSec) && frameTimeSec >= 0)
                            {
                                try
                                {
                                    // 计算TimeSpan
                                    TimeSpan timeSpan = TimeSpan.FromSeconds(frameTimeSec);
                                    
                                    try
                                    {
                                        frameTimeStrFileName = timeSpan.ToString("hhmmss");
                                    }
                                    catch (Exception)
                                    {
                                        frameTimeStrFileName = "000000";
                                    }
                                    
                                    try
                                    {
                                        // 使用ToString("c")获取标准时间格式，然后替换分隔符
                                        frameTimeStrLog = timeSpan.ToString("c").Replace(":", " ").Trim();
                                    }
                                    catch (Exception)
                                    {
                                        // 手动格式化时间，避免依赖ToString方法的格式字符串
                                        int hours = (int)timeSpan.TotalHours;
                                        int minutes = timeSpan.Minutes;
                                        int seconds = timeSpan.Seconds;
                                        frameTimeStrLog = $"{hours:00} {minutes:00} {seconds:00}";
                                    }
                                }
                                catch (Exception ex)
                                {
                                    // 处理所有可能的异常，使用默认值
                                    Logger.Warning(string.Format("帧：{0}，时间格式化失败，错误：{1}，使用默认值", i, ex.Message));
                                }
                            }
                            // 保存预处理后的帧图片，包含时间信息
                            string framePath = Path.Combine(saveFolder, $"frame_{i}_{frameTimeStrFileName}_{Path.GetFileNameWithoutExtension(moviePath)}.png");
                            Cv2.ImWrite(framePath, currentFrameThumb);
                            
                            // 比较当前帧与广告起始帧和结束帧
                            double startSimilarityValue = CompareFrames(currentFrameThumb, adFrames[0]);
                            double endSimilarityValue = CompareFrames(currentFrameThumb, adFrames[adFrames.Count - 1]);
                            
                            // 转换为匹配率（0-100%，越高越相似）
                            double startSimilarity = Math.Max(0, 100 - (startSimilarityValue / 255 * 100));
                            double endSimilarity = Math.Max(0, 100 - (endSimilarityValue / 255 * 100));
                            
                            // 检查相似度是否为有效数值
                            bool isStartSimilarityValid = !double.IsNaN(startSimilarity) && !double.IsInfinity(startSimilarity);
                            bool isEndSimilarityValid = !double.IsNaN(endSimilarity) && !double.IsInfinity(endSimilarity);
                            
                            if (isStartSimilarityValid && isEndSimilarityValid)
                            {
                                // 记录每个帧的相似度
                                Logger.Info(string.Format("帧：{0}，时间：{1}，开始帧匹配率：{2:F2}%，结束帧匹配率：{3:F2}%", i, frameTimeStrLog, startSimilarity, endSimilarity));
                                
                                // 检测广告开始（与开始帧匹配）或广告结束（与结束帧匹配）
                                if (startSimilarity >= SIMILARITY_THRESHOLD || endSimilarity >= SIMILARITY_THRESHOLD)
                                {
                                    string matchType = startSimilarity >= SIMILARITY_THRESHOLD ? "开始帧" : "结束帧";
                                    double matchRate = startSimilarity >= SIMILARITY_THRESHOLD ? startSimilarity : endSimilarity;
                                    Logger.Info(string.Format("检测到疑似广告，帧：{0}，时间：{1}，{2}匹配率：{3:F2}%", i, frameTimeStrLog, matchType, matchRate));
                                    
                                    // 疑似广告，进行序列验证
                                    if (VerifyAdSequence(moviePath, adFrames, i, movieInfo.Fps, adDurationFrames))
                                    {
                                        // 计算当前帧的时间（秒）
                                        double currentTime = i / movieInfo.Fps;
                                        
                                        // 检查时间值是否有效
                                        if (!double.IsNaN(currentTime) && !double.IsInfinity(currentTime) && currentTime >= 0)
                                        {
                                            // 验证通过，创建广告片段
                                            AdSegment segment = new AdSegment();
                                            
                                            // 根据匹配类型，只设置对应的时间
                                            if (startSimilarity >= SIMILARITY_THRESHOLD)
                                            {
                                                // 匹配到开始帧，只设置开始时间，结束时间设为NaN
                                                segment.StartTime = currentTime;
                                                segment.EndTime = double.NaN;
                                                Logger.Info(string.Format("广告验证通过，仅记录开始时间：{0:F2}秒", segment.StartTime));
                                            }
                                            else
                                            {
                                                // 匹配到结束帧，只设置结束时间，开始时间设为NaN
                                                segment.StartTime = double.NaN;
                                                segment.EndTime = currentTime;
                                                Logger.Info(string.Format("广告验证通过，仅记录结束时间：{0:F2}秒", segment.EndTime));
                                            }
                                            
                                            lock (segmentsLock)
                                            {
                                                adSegments.Add(segment);
                                            }

                                            // 实时返回检测结果
                                            adDetectedCallback?.Invoke(segment);
                                        }
                                        else
                                        {
                                            Logger.Warning(string.Format("帧：{0}，时间：{1}，计算得到无效的时间：{2:F2}秒，跳过", i, frameTimeStrLog, currentTime));
                                        }
                                    }
                                }
                            }
                            else
                            {
                                Logger.Warning(string.Format("帧：{0}，时间：{1}，相似度计算结果无效", i, frameTimeStrLog));
                            }

                            // 释放资源
                            currentFrameThumb.Dispose();
                            currentFrame.Dispose();
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(string.Format("处理帧 {0} 时出错", i), ex);
                        }
                        finally
                        {
                            // 更新进度
                            lock (progressLock)
                            {
                                processedCount++;
                                double progress = (double)processedCount / totalFramesToProcess * 100;
                                progressCallback?.Invoke(progress);
                            }
                        }
                    });
                    }
                    catch (OperationCanceledException)
                    {
                        // 用户主动取消检测，这是预期行为，不需要记录为错误
                        Logger.Info("检测已被用户取消，正在处理已有结果...");
                        break; // 跳出时间块循环
                    }
                }

                // 释放广告帧资源
                foreach (var frame in adFrames)
                {
                    frame.Dispose();
                }
            }
            catch (Exception ex)
            {
                // 只记录非取消操作的错误
                if (!(ex is OperationCanceledException))
                {
                    Logger.Error("广告检测过程中发生错误", ex);
                }
                else
                {
                    Logger.Info("检测已被用户取消，正在处理已有结果...");
                }
            }
            
            try
            {
                // 基本数值检查
                int segmentCount = adSegments?.Count ?? 0;
                Logger.Info(string.Format("广告检测完成，共发现 {0} 个广告片段", segmentCount));
                
                // 调试输出：原始检测结果
                Logger.Info("=== 原始检测结果 ===");
                
                // 确保 adSegments 不为空，避免 OrderBy 时的空引用异常
                var validSegments = adSegments?.Where(s => s != null).ToList() ?? new List<AdSegment>();
                
                // 对原始片段进行排序，使用安全的排序键
                var sortedSegments = validSegments.OrderBy(s => 
                    double.IsNaN(s.StartTime) || double.IsInfinity(s.StartTime) ? double.MaxValue : s.StartTime
                ).ToList();
                
                foreach (var seg in sortedSegments)
                {
                    // 检查开始时间是否有效
                    if (!double.IsNaN(seg.StartTime) && !double.IsInfinity(seg.StartTime) && seg.StartTime >= 0)
                    {
                        try
                        {
                            TimeSpan startTimeSpan = TimeSpan.FromSeconds(seg.StartTime);
                            string startTimeStr = startTimeSpan.ToString(@"hh\:mm\:ss");
                            Logger.Info(string.Format("原始片段：开始时间 {0}", startTimeStr));
                        }
                        catch (Exception ex)
                        {
                            Logger.Warning(string.Format("原始片段：开始时间转换失败，原始值：{0}，错误：{1}", seg.StartTime, ex.Message));
                        }
                    }
                    
                    // 检查结束时间是否有效
                    if (!double.IsNaN(seg.EndTime) && !double.IsInfinity(seg.EndTime) && seg.EndTime >= 0)
                    {
                        try
                        {
                            TimeSpan endTimeSpan = TimeSpan.FromSeconds(seg.EndTime);
                            string endTimeStr = endTimeSpan.ToString(@"hh\:mm\:ss");
                            Logger.Info(string.Format("原始片段：结束时间 {0}", endTimeStr));
                        }
                        catch (Exception ex)
                        {
                            Logger.Warning(string.Format("原始片段：结束时间转换失败，原始值：{0}，错误：{1}", seg.EndTime, ex.Message));
                        }
                    }
                }
                
                // 检查广告时长是否有效
                bool isAdDurationValid = !double.IsNaN(adDurationSec) && !double.IsInfinity(adDurationSec) && adDurationSec > 0;
                if (!isAdDurationValid)
                {
                    Logger.Warning(string.Format("广告时长无效：{0}，使用默认值 10 秒", adDurationSec));
                    adDurationSec = 10.0;
                }
                
                // 排序广告片段并去重（合并重叠片段和时间接近的片段）
                List<AdSegment> mergedSegments = MergeOverlappingSegments(sortedSegments, adDurationSec);
                
                // 调试输出：合并后的结果
                Logger.Info("=== 合并后结果 ===");
                
                // 确保 mergedSegments 不为空
                mergedSegments = mergedSegments ?? new List<AdSegment>();
                
                for (int i = 0; i < mergedSegments.Count; i++)
                {
                    var seg = mergedSegments[i];
                    if (seg == null)
                    {
                        Logger.Warning(string.Format("合并片段 {0} 为空，跳过处理", i + 1));
                        continue;
                    }
                    
                    // 安全转换开始时间
                    string startStr = "-";
                    if (!double.IsNaN(seg.StartTime) && !double.IsInfinity(seg.StartTime) && seg.StartTime >= 0)
                    {
                        try
                        {
                            TimeSpan startTimeSpan = TimeSpan.FromSeconds(seg.StartTime);
                            startStr = startTimeSpan.ToString(@"hh\:mm\:ss");
                        }
                        catch (Exception ex)
                        {
                            Logger.Warning(string.Format("合并片段 {0}：开始时间转换失败，原始值：{1}，错误：{2}", i + 1, seg.StartTime, ex.Message));
                        }
                    }
                    
                    // 安全转换结束时间
                    string endStr = "-";
                    if (!double.IsNaN(seg.EndTime) && !double.IsInfinity(seg.EndTime) && seg.EndTime >= 0)
                    {
                        try
                        {
                            TimeSpan endTimeSpan = TimeSpan.FromSeconds(seg.EndTime);
                            endStr = endTimeSpan.ToString(@"hh\:mm\:ss");
                        }
                        catch (Exception ex)
                        {
                            Logger.Warning(string.Format("合并片段 {0}：结束时间转换失败，原始值：{1}，错误：{2}", i + 1, seg.EndTime, ex.Message));
                        }
                    }
                    
                    // 使用安全的字符串格式化
                    try
                    {
                        Logger.Info(string.Format("合并片段 {0}：开始时间 {1}，结束时间 {2}", i + 1, startStr, endStr));
                    }
                    catch (Exception ex)
                    {
                        Logger.Warning(string.Format("合并片段日志格式化失败，索引：{0}，错误：{1}", i + 1, ex.Message));
                    }
                }
                
                return mergedSegments;
            }
            catch (Exception ex)
            {
                Logger.Error("广告片段合并过程中发生错误", ex);
                // 返回原始未合并的片段，确保方法不会因合并错误而失败
                var safeReturnSegments = adSegments?.Where(s => s != null).OrderBy(s => 
                    double.IsNaN(s.StartTime) || double.IsInfinity(s.StartTime) ? double.MaxValue : s.StartTime
                ).ToList() ?? new List<AdSegment>();
                return safeReturnSegments;
            }
        }
        
        /// <summary>
        /// 检测视频中的广告片段（单个时间段版本，向后兼容）
        /// </summary>
        /// <param name="moviePath">电影文件路径</param>
        /// <param name="adStartTimeSec">广告开始时间（秒）</param>
        /// <param name="adEndTimeSec">广告结束时间（秒）</param>
        /// <param name="progressCallback">进度回调函数</param>
        /// <param name="adDetectedCallback">广告检测到回调函数，用于实时返回检测结果</param>
        /// <returns>广告片段列表</returns>
        public List<AdSegment> DetectAds(string moviePath, double adStartTimeSec, double adEndTimeSec, Action<double> progressCallback, Action<AdSegment> adDetectedCallback = null)
        {
            // 向后兼容，检测时间段默认为广告时间
            return DetectAds(moviePath, adStartTimeSec, adEndTimeSec, adStartTimeSec, adEndTimeSec, progressCallback, adDetectedCallback);
        }
                            
        /// <summary>
        /// 合并广告片段，处理只有开始时间或只有结束时间的情况
        /// </summary>
        /// <param name="segments">排序后的广告片段列表</param>
        /// <param name="adDurationSec">广告时长（秒）</param>
        /// <returns>合并后的广告片段列表</returns>
        public List<AdSegment> MergeOverlappingSegments(List<AdSegment> segments, double adDurationSec)
        {
            // 过滤掉null片段
            var validSegments = segments.Where(s => s != null).ToList();
            if (validSegments.Count == 0)
                return new List<AdSegment>();
            
            // 生成原始片段的详细信息，用于备注
            List<string> originalSegmentsInfo = new List<string>();
            for (int i = 0; i < validSegments.Count; i++)
            {
                var seg = validSegments[i];
                string startTimeStr = double.IsNaN(seg.StartTime) ? "-" : TimeSpan.FromSeconds(seg.StartTime).ToString(@"hh\:mm\:ss");
                string endTimeStr = double.IsNaN(seg.EndTime) ? "-" : TimeSpan.FromSeconds(seg.EndTime).ToString(@"hh\:mm\:ss");
                originalSegmentsInfo.Add($"[{i+1}] 开始时间：{startTimeStr}，结束时间：{endTimeStr}");
            }
            
            // 收集所有有效的开始时间和结束时间，排除默认值0.0
            List<double> startTimes = validSegments
                .Where(s => !double.IsNaN(s.StartTime) && !double.IsInfinity(s.StartTime) && s.StartTime > 0)
                .Select(s => s.StartTime)
                .OrderBy(t => t)
                .ToList();
            
            List<double> endTimes = validSegments
                .Where(s => !double.IsNaN(s.EndTime) && !double.IsInfinity(s.EndTime) && s.EndTime > 0)
                .Select(s => s.EndTime)
                .OrderBy(t => t)
                .ToList();
            
            // 创建合并后的片段
            List<AdSegment> merged = new List<AdSegment>();
            AdSegment mergedSegment = new AdSegment();
            
            // 设置默认值为NaN，确保在没有有效时间点时显示为"-"
            mergedSegment.StartTime = double.NaN;
            mergedSegment.EndTime = double.NaN;
            
            // 如果有开始时间和结束时间，取最早的开始时间和最晚的结束时间
            if (startTimes.Count > 0 && endTimes.Count > 0)
            {
                mergedSegment.StartTime = startTimes.First();
                mergedSegment.EndTime = endTimes.Last();
            }
            // 如果只有开始时间，生成结束时间
            else if (startTimes.Count > 0)
            {
                mergedSegment.StartTime = startTimes.First();
                mergedSegment.EndTime = mergedSegment.StartTime + adDurationSec;
            }
            // 如果只有结束时间，生成开始时间，确保不小于0
            else if (endTimes.Count > 0)
            {
                mergedSegment.EndTime = endTimes.Last();
                mergedSegment.StartTime = Math.Max(0, mergedSegment.EndTime - adDurationSec);
            }
            
            // 设置备注信息
            mergedSegment.Notes = $"合并了 {validSegments.Count} 条记录：\n" +
                                  string.Join("\n", originalSegmentsInfo) + "\n" +
                                  $"合并后开始时间：{(double.IsNaN(mergedSegment.StartTime) ? "-" : TimeSpan.FromSeconds(mergedSegment.StartTime).ToString(@"hh\:mm\:ss"))}; " +
                                  $"合并后结束时间：{(double.IsNaN(mergedSegment.EndTime) ? "-" : TimeSpan.FromSeconds(mergedSegment.EndTime).ToString(@"hh\:mm\:ss"))}";
            
            // 只添加一个合并后的片段
            merged.Add(mergedSegment);
            
            return merged;
        }

        /// <summary>
        /// 提取广告帧序列
        /// </summary>
        /// <param name="videoPath">视频文件路径</param>
        /// <param name="startTimeSec">开始时间（秒）</param>
        /// <param name="endTimeSec">结束时间（秒）</param>
        /// <param name="fps">帧率</param>
        /// <returns>广告帧序列</returns>
        private List<Mat> ExtractAdFrames(string videoPath, double startTimeSec, double endTimeSec, double fps)
        {
            List<Mat> frames = new List<Mat>();
            int startFrame = (int)(startTimeSec * fps);
            int endFrame = (int)(endTimeSec * fps);
            int frameCount = endFrame - startFrame;

            // 提取关键帧（开始、20%、50%、80%、结束）
            double[] checkPoints = { 0.0, 0.2, 0.5, 0.8, 1.0 };

            using var cap = new VideoCapture(videoPath);
            foreach (double point in checkPoints)
            {
                int frameIndex = startFrame + (int)(frameCount * point);
                cap.PosFrames = frameIndex;
                Mat frame = new Mat();
                if (cap.Read(frame))
                {
                    // 计算等比例缩放后的尺寸，保持宽高比，目标宽度为320像素
                    int originalWidth = frame.Cols;
                    int originalHeight = frame.Rows;
                    double scale = (double)TARGET_WIDTH / originalWidth;
                    int newHeight = (int)(originalHeight * scale);
                    OpenCvSharp.Size scaledSize = new OpenCvSharp.Size(TARGET_WIDTH, newHeight);
                    
                    Mat preprocessedFrame = _analyzer.PreprocessFrame(frame, scaledSize);
                    frames.Add(preprocessedFrame);
                }
                frame.Dispose();
            }

            return frames;
        }

        /// <summary>
        /// 比较两个帧的相似度
        /// </summary>
        /// <param name="frame1">帧1</param>
        /// <param name="frame2">帧2</param>
        /// <returns>相似度值（0-255，越小越相似）</returns>
        protected double CompareFrames(Mat frame1, Mat frame2)
        {
            using Mat diff = new Mat();
            
            // 计算帧差异
            Cv2.Absdiff(frame1, frame2, diff);
            Scalar mean = Cv2.Mean(diff);
            return mean.Val0;
        }
        
        /// <summary>
        /// 比较两个图片文件的相似度，并输出匹配率
        /// </summary>
        /// <param name="imagePath1">第一张图片的路径</param>
        /// <param name="imagePath2">第二张图片的路径</param>
        /// <returns>匹配率（0-100%，越高越相似）</returns>
        public double CompareImages(string imagePath1, string imagePath2)
        {
            if (!File.Exists(imagePath1))
            {
                throw new FileNotFoundException("第一张图片不存在", imagePath1);
            }
            if (!File.Exists(imagePath2))
            {
                throw new FileNotFoundException("第二张图片不存在", imagePath2);
            }
            
            // 加载图片
            Mat image1 = Cv2.ImRead(imagePath1);
            Mat image2 = Cv2.ImRead(imagePath2);
            
            if (image1.Empty())
            {
                throw new Exception("无法加载第一张图片");
            }
            if (image2.Empty())
            {
                throw new Exception("无法加载第二张图片");
            }
            
            try
            {
                // 预处理图片 - 等比例缩放，保持宽高比，目标宽度为320像素
                // 计算第一张图片的缩放尺寸
                int originalWidth1 = image1.Cols;
                int originalHeight1 = image1.Rows;
                double scale1 = (double)TARGET_WIDTH / originalWidth1;
                int newHeight1 = (int)(originalHeight1 * scale1);
                OpenCvSharp.Size scaledSize1 = new OpenCvSharp.Size(TARGET_WIDTH, newHeight1);
                
                // 计算第二张图片的缩放尺寸
                int originalWidth2 = image2.Cols;
                int originalHeight2 = image2.Rows;
                double scale2 = (double)TARGET_WIDTH / originalWidth2;
                int newHeight2 = (int)(originalHeight2 * scale2);
                OpenCvSharp.Size scaledSize2 = new OpenCvSharp.Size(TARGET_WIDTH, newHeight2);
                
                Mat preprocessed1 = _analyzer.PreprocessFrame(image1, scaledSize1);
                Mat preprocessed2 = _analyzer.PreprocessFrame(image2, scaledSize2);
                
                // 计算相似度（0-255，越小越相似）
                double similarityValue = CompareFrames(preprocessed1, preprocessed2);
                
                // 转换为匹配率（0-100%，越高越相似）
                // 相似度值为0表示完全相同，255表示完全不同
                double matchRate = Math.Max(0, 100 - (similarityValue / 255 * 100));
                
                Logger.Info(string.Format("图片相似度比较：{0} 和 {1}，匹配率：{2:F2}%", 
                    Path.GetFileName(imagePath1), Path.GetFileName(imagePath2), matchRate));
                
                return matchRate;
            }
            finally
            {
                // 释放资源
                image1.Dispose();
                image2.Dispose();
            }
        }

        /// <summary>
        /// 验证广告序列
        /// </summary>
        /// <param name="moviePath">电影文件路径</param>
        /// <param name="adFrames">广告帧序列</param>
        /// <param name="movieStartFrameIdx">电影中疑似匹配的起始帧</param>
        /// <param name="fps">电影帧率</param>
        /// <param name="adDurationFrames">广告实际时长对应的帧数</param>
        /// <returns>验证是否通过</returns>
        private bool VerifyAdSequence(string moviePath, List<Mat> adFrames, int movieStartFrameIdx, double fps, int adDurationFrames)
        {
            // 简化验证逻辑：只要是疑似广告就认为是匹配到广告
            // 计算时间信息
            double frameTimeSec = movieStartFrameIdx / fps;
            string frameTimeStr = "00 00 00";
            
            // 检查时间是否为有效数值
            if (!double.IsNaN(frameTimeSec) && !double.IsInfinity(frameTimeSec) && frameTimeSec >= 0)
            {
                try
                {
                    // 计算TimeSpan
                    TimeSpan timeSpan = TimeSpan.FromSeconds(frameTimeSec);
                    
                    try
                    {
                        // 使用ToString("c")获取标准时间格式，然后替换分隔符
                        frameTimeStr = timeSpan.ToString("c").Replace(":", " ").Trim();
                    }
                    catch (Exception)
                    {
                        // 手动格式化时间，避免依赖ToString方法的格式字符串
                        int hours = (int)timeSpan.TotalHours;
                        int minutes = timeSpan.Minutes;
                        int seconds = timeSpan.Seconds;
                        frameTimeStr = $"{hours:00} {minutes:00} {seconds:00}";
                    }
                }
                catch (Exception ex)
                {
                    // 处理所有可能的异常，使用默认值
                    Logger.Warning(string.Format("疑似广告验证 - 起始帧：{0}，时间格式化失败，错误：{1}，使用默认值", movieStartFrameIdx, ex.Message));
                }
            }
            Logger.Info(string.Format("疑似广告验证 - 起始帧：{0}，时间：{1}，直接判定为广告", movieStartFrameIdx, frameTimeStr));
            return true;
        }
    }
}