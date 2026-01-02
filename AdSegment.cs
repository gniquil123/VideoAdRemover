using System;

namespace VideoAdRemover
{
    /// <summary>
    /// 广告片段类
    /// </summary>
    public class AdSegment
    {
        /// <summary>
        /// 广告开始时间（秒）
        /// </summary>
        public double StartTime { get; set; }
        
        /// <summary>
        /// 广告结束时间（秒）
        /// </summary>
        public double EndTime { get; set; }
        
        /// <summary>
        /// 备注信息，用于记录合并的时间点
        /// </summary>
        public string Notes { get; set; } = string.Empty;
        
        /// <summary>
        /// 广告持续时间（秒）
        /// </summary>
        public double Duration => EndTime - StartTime;
        
        /// <summary>
        /// 重写ToString方法，用于列表显示
        /// </summary>
        /// <returns>格式化的广告时间段字符串</returns>
        public override string ToString()
        {
            string startStr = double.IsNaN(StartTime) ? "-" : $"{TimeSpan.FromSeconds(StartTime):hh:mm:ss}";
            string endStr = double.IsNaN(EndTime) ? "-" : $"{TimeSpan.FromSeconds(EndTime):hh:mm:ss}";
            return $"{startStr} - {endStr}";
        }
    }
}