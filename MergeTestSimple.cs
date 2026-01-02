using System;
using System.Collections.Generic;

namespace VideoAdRemover
{
    /// <summary>
    /// 简单的合并测试类，直接调用MergeOverlappingSegments方法并输出结果
    /// </summary>
    public static class MergeTestSimple
    {
        /// <summary>
        /// 运行简单的合并测试
        /// </summary>
        public static void RunSimpleTest()
        {
            Console.WriteLine("开始运行简单的合并测试...");
            
            try
            {
                // 创建广告检测器实例
                AdDetector detector = new AdDetector(useHardwareAcceleration: true);
                
                // 测试用例：用户描述的6条结果
                List<AdSegment> segments = new List<AdSegment>
                {
                    new AdSegment { StartTime = 478, EndTime = double.NaN }, // 00:07:58
                    new AdSegment { StartTime = 479, EndTime = double.NaN }, // 00:07:59
                    new AdSegment { StartTime = 480, EndTime = double.NaN }, // 00:08:00
                    new AdSegment { StartTime = double.NaN, EndTime = 491 }, // 00:08:11
                    new AdSegment { StartTime = double.NaN, EndTime = 492 }, // 00:08:12
                    new AdSegment { StartTime = double.NaN, EndTime = 493 }  // 00:08:13
                };
                
                double adDurationSec = 15; // 广告时长15秒
                
                Console.WriteLine($"\n原始片段（{segments.Count}个）：");
                for (int i = 0; i < segments.Count; i++)
                {
                    AdSegment seg = segments[i];
                    string startTimeStr = double.IsNaN(seg.StartTime) ? "-" : TimeSpan.FromSeconds(seg.StartTime).ToString(@"hh\:mm\:ss");
                    string endTimeStr = double.IsNaN(seg.EndTime) ? "-" : TimeSpan.FromSeconds(seg.EndTime).ToString(@"hh\:mm\:ss");
                    Console.WriteLine($"[{i+1}] 开始时间：{startTimeStr}，结束时间：{endTimeStr}");
                }
                
                // 执行合并
                List<AdSegment> merged = detector.MergeOverlappingSegments(segments, adDurationSec);
                
                Console.WriteLine($"\n合并结果（{merged.Count}个）：");
                for (int i = 0; i < merged.Count; i++)
                {
                    AdSegment seg = merged[i];
                    string startTimeStr = double.IsNaN(seg.StartTime) ? "-" : TimeSpan.FromSeconds(seg.StartTime).ToString(@"hh\:mm\:ss");
                    string endTimeStr = double.IsNaN(seg.EndTime) ? "-" : TimeSpan.FromSeconds(seg.EndTime).ToString(@"hh\:mm\:ss");
                    Console.WriteLine($"[{i+1}] 开始时间：{startTimeStr}，结束时间：{endTimeStr}");
                    Console.WriteLine($"   备注：{seg.Notes.Replace('\n', ' ')}");
                }
                
                Console.WriteLine("\n测试完成！");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"测试过程中发生错误: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}