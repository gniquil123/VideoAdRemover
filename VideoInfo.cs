using System;

namespace VideoAdRemover
{
    /// <summary>
    /// 视频信息类
    /// </summary>
    public class VideoInfo
    {
        /// <summary>
        /// 视频宽度
        /// </summary>
        public int Width { get; set; }
        
        /// <summary>
        /// 视频高度
        /// </summary>
        public int Height { get; set; }
        
        /// <summary>
        /// 帧率
        /// </summary>
        public double Fps { get; set; }
        
        /// <summary>
        /// 总帧数
        /// </summary>
        public int FrameCount { get; set; }
        
        /// <summary>
        /// 视频时长（秒）
        /// </summary>
        public double Duration { get; set; }
    }
}