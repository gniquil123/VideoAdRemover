using OpenCvSharp;
using System;
using System.Diagnostics;
using System.IO;

namespace VideoAdRemover
{
    /// <summary>
    /// 视频分析模块
    /// 负责视频帧的提取和预处理
    /// </summary>
    public class VideoAnalyzer
    {
        protected bool _useHardwareAcceleration;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="useHardwareAcceleration">是否使用硬件加速</param>
        public VideoAnalyzer(bool useHardwareAcceleration = false)
        {
            _useHardwareAcceleration = useHardwareAcceleration;
            
            // 记录硬件加速状态
            if (_useHardwareAcceleration)
            {
                Logger.Info("启用硬件加速");
            }
        }

        /// <summary>
        /// 提取视频帧
        /// </summary>
        /// <param name="videoPath">视频文件路径</param>
        /// <param name="frameIndex">帧索引</param>
        /// <returns>提取的帧</returns>
        public Mat ExtractFrame(string videoPath, int frameIndex)
        {
            using var cap = new VideoCapture(videoPath);
            if (!cap.IsOpened())
                throw new Exception("无法打开视频文件");

            cap.PosFrames = frameIndex;
            Mat frame = new Mat();
            if (!cap.Read(frame))
                throw new Exception("无法提取指定帧");

            return frame;
        }

        /// <summary>
        /// 预处理帧（缩放、灰度化、模糊）
        /// </summary>
        /// <param name="frame">原始帧</param>
        /// <param name="targetSize">目标尺寸</param>
        /// <returns>预处理后的帧</returns>
        public Mat PreprocessFrame(Mat frame, OpenCvSharp.Size targetSize)
        {
            Mat resized = new Mat();
            Mat gray = new Mat();
            Mat blurred = new Mat();

            // 缩放
            Cv2.Resize(frame, resized, targetSize);
            // 灰度化
            Cv2.CvtColor(resized, gray, ColorConversionCodes.BGR2GRAY);
            // 高斯模糊去噪
            Cv2.GaussianBlur(gray, blurred, new OpenCvSharp.Size(3, 3), 0);

            return blurred;
        }

        /// <summary>
        /// 获取视频基本信息
        /// </summary>
        /// <param name="videoPath">视频文件路径</param>
        /// <returns>视频信息对象</returns>
        public VideoInfo GetVideoInfo(string videoPath)
        {
            using var cap = new VideoCapture(videoPath);
            if (!cap.IsOpened())
                throw new Exception("无法打开视频文件");

            // 使用FFmpeg获取更准确的视频时长
            double accurateDuration = GetVideoDurationWithFFmpeg(videoPath);

            return new VideoInfo
            {
                Width = (int)cap.FrameWidth,
                Height = (int)cap.FrameHeight,
                Fps = cap.Fps,
                FrameCount = (int)cap.FrameCount,
                Duration = accurateDuration
            };
        }
        
        /// <summary>
        /// 使用FFmpeg获取准确的视频时长
        /// </summary>
        /// <param name="videoPath">视频文件路径</param>
        /// <returns>视频时长（秒）</returns>
        private double GetVideoDurationWithFFmpeg(string videoPath)
        {
            try
            {
                // 查找FFmpeg可执行文件
                string ffmpegPath = FindFFmpeg();
                if (string.IsNullOrEmpty(ffmpegPath))
                {
                    Logger.Warning("未找到FFmpeg，使用OpenCV计算时长");
                    // 回退到OpenCV计算
                    using var cap = new VideoCapture(videoPath);
                    return cap.FrameCount / cap.Fps;
                }

                // 构建FFmpeg命令获取视频时长
                string arguments = $"-i \"{videoPath}\" -show_entries format=duration -v quiet -of csv=p=0";
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = ffmpegPath,
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using Process process = new Process();
                process.StartInfo = startInfo;
                process.Start();
                
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode == 0 && double.TryParse(output.Trim(), out double duration))
                {
                    Logger.Info($"使用FFmpeg获取到视频时长：{duration}秒");
                    return duration;
                }
                else
                {
                    Logger.Warning("FFmpeg获取时长失败，使用OpenCV计算时长");
                    // 回退到OpenCV计算
                    using var cap = new VideoCapture(videoPath);
                    return cap.FrameCount / cap.Fps;
                }
            }
            catch (Exception ex)
            {
                Logger.Warning($"获取视频时长失败：{ex.Message}，使用OpenCV计算时长");
                // 回退到OpenCV计算
                using var cap = new VideoCapture(videoPath);
                return cap.FrameCount / cap.Fps;
            }
        }
        
        /// <summary>
        /// 查找FFmpeg可执行文件
        /// </summary>
        /// <returns>FFmpeg可执行文件路径</returns>
        private string? FindFFmpeg()
        {
            // 1. 检查当前目录
            string currentDir = AppContext.BaseDirectory;
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

        /// <summary>
        /// 根据时间获取帧索引
        /// </summary>
        /// <param name="timeSec">时间（秒）</param>
        /// <param name="fps">帧率</param>
        /// <returns>帧索引</returns>
        public int GetFrameIndexByTime(double timeSec, double fps)
        {
            return (int)(timeSec * fps);
        }

        /// <summary>
        /// 根据帧索引获取时间
        /// </summary>
        /// <param name="frameIndex">帧索引</param>
        /// <param name="fps">帧率</param>
        /// <returns>时间（秒）</returns>
        public double GetTimeByFrameIndex(int frameIndex, double fps)
        {
            return frameIndex / fps;
        }
    }
}