using System;
using System.Collections.Generic;
using System.Threading;

namespace VideoAdRemover
{
    /// <summary>
    /// 广告检测测试类
    /// </summary>
    public class TestAdDetector
    {
        /// <summary>
        /// 测试广告检测功能
        /// </summary>
        /// <param name="videoPath">视频文件路径</param>
        /// <param name="adStartTimeSec">广告开始时间（秒）</param>
        /// <param name="adEndTimeSec">广告结束时间（秒）</param>
        public static void Test(string videoPath, double adStartTimeSec, double adEndTimeSec)
        {
            Console.WriteLine("开始测试广告检测功能...");
            Console.WriteLine($"视频路径: {videoPath}");
            Console.WriteLine($"广告时间范围: {adStartTimeSec} - {adEndTimeSec} 秒");
            
            try
            {
                // 创建广告检测器实例
                AdDetector detector = new AdDetector(useHardwareAcceleration: true, scanIntervalSec: 0.5);
                
                // 检测广告
                List<AdSegment> adSegments = detector.DetectAds(
                    videoPath,
                    adStartTimeSec,
                    adEndTimeSec,
                    progress => Console.WriteLine($"检测进度: {progress:F2}%"),
                    segment => Console.WriteLine($"实时检测到广告: {segment}")
                );
                
                // 输出检测结果
                Console.WriteLine($"\n广告检测完成!");
                Console.WriteLine($"共检测到 {adSegments.Count} 个广告片段:");
                
                if (adSegments.Count > 0)
                {
                    for (int i = 0; i < adSegments.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {adSegments[i]}");
                    }
                }
                else
                {
                    Console.WriteLine("未检测到广告片段。");
                    Console.WriteLine("可能的原因:");
                    Console.WriteLine("1. 广告特征不明显");
                    Console.WriteLine("2. 广告时间范围设置不正确");
                    Console.WriteLine("3. 视频质量问题");
                    Console.WriteLine("4. 相似度阈值设置过高");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"测试过程中发生错误: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
        
        /// <summary>
        /// 测试图片相似度比较功能
        /// </summary>
        /// <param name="imagePath1">第一张图片的路径</param>
        /// <param name="imagePath2">第二张图片的路径</param>
        public static void TestCompareImages(string imagePath1, string imagePath2)
        {
            Console.WriteLine("开始测试图片相似度比较功能...");
            Console.WriteLine($"第一张图片: {imagePath1}");
            Console.WriteLine($"第二张图片: {imagePath2}");
            
            try
            {
                // 创建广告检测器实例
                AdDetector detector = new AdDetector(useHardwareAcceleration: true);
                
                // 比较图片相似度
                double matchRate = detector.CompareImages(imagePath1, imagePath2);
                
                // 输出结果
                Console.WriteLine($"\n图片相似度比较完成!");
                Console.WriteLine($"匹配率: {matchRate:F2}%");
                Console.WriteLine($"相似度评估: {(matchRate >= 90 ? "高度相似" : (matchRate >= 70 ? "中度相似" : (matchRate >= 50 ? "低度相似" : "不相似")))}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"测试过程中发生错误: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
        
        /// <summary>
        /// 测试合并广告片段功能
        /// </summary>
        public static void TestMergeOverlappingSegments()
        {
            Console.WriteLine("开始测试合并广告片段功能...");
            
            try
            {
                // 创建广告检测器实例
                AdDetector detector = new AdDetector(useHardwareAcceleration: true);
                
                // 测试场景1：正常情况 - 有开始时间和结束时间的片段合并
                Console.WriteLine("\n=== 测试场景1：正常情况 - 有开始时间和结束时间的片段合并 ===");
                List<AdSegment> segments1 = new List<AdSegment>
                {
                    new AdSegment { StartTime = 478, EndTime = double.NaN },
                    new AdSegment { StartTime = double.NaN, EndTime = 493 }
                };
                double adDurationSec1 = 15; // 广告时长15秒
                List<AdSegment> result1 = detector.MergeOverlappingSegments(segments1, adDurationSec1);
                PrintMergeResult(result1);
                
                // 测试场景2：只有开始时间的片段合并
                Console.WriteLine("\n=== 测试场景2：只有开始时间的片段合并 ===");
                List<AdSegment> segments2 = new List<AdSegment>
                {
                    new AdSegment { StartTime = 478, EndTime = double.NaN },
                    new AdSegment { StartTime = 480, EndTime = double.NaN },
                    new AdSegment { StartTime = 482, EndTime = double.NaN }
                };
                double adDurationSec2 = 15;
                List<AdSegment> result2 = detector.MergeOverlappingSegments(segments2, adDurationSec2);
                PrintMergeResult(result2);
                
                // 测试场景3：只有结束时间的片段合并
                Console.WriteLine("\n=== 测试场景3：只有结束时间的片段合并 ===");
                List<AdSegment> segments3 = new List<AdSegment>
                {
                    new AdSegment { StartTime = double.NaN, EndTime = 493 },
                    new AdSegment { StartTime = double.NaN, EndTime = 495 },
                    new AdSegment { StartTime = double.NaN, EndTime = 497 }
                };
                double adDurationSec3 = 15;
                List<AdSegment> result3 = detector.MergeOverlappingSegments(segments3, adDurationSec3);
                PrintMergeResult(result3);
                
                // 测试场景4：混合情况 - 既有只有开始时间的片段，也有只有结束时间的片段
                Console.WriteLine("\n=== 测试场景4：混合情况 ===");
                List<AdSegment> segments4 = new List<AdSegment>
                {
                    new AdSegment { StartTime = 478, EndTime = double.NaN },
                    new AdSegment { StartTime = 480, EndTime = double.NaN },
                    new AdSegment { StartTime = double.NaN, EndTime = 493 },
                    new AdSegment { StartTime = double.NaN, EndTime = 495 }
                };
                double adDurationSec4 = 15;
                List<AdSegment> result4 = detector.MergeOverlappingSegments(segments4, adDurationSec4);
                PrintMergeResult(result4);
                
                // 测试场景5：无效时间点的片段合并
                Console.WriteLine("\n=== 测试场景5：无效时间点的片段合并 ===");
                List<AdSegment> segments5 = new List<AdSegment>
                {
                    new AdSegment { StartTime = double.NaN, EndTime = double.NaN },
                    new AdSegment { StartTime = double.NaN, EndTime = double.NaN }
                };
                double adDurationSec5 = 15;
                List<AdSegment> result5 = detector.MergeOverlappingSegments(segments5, adDurationSec5);
                PrintMergeResult(result5);
                
                Console.WriteLine("\n所有测试场景完成!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"测试过程中发生错误: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
        
        /// <summary>
        /// 打印合并结果
        /// </summary>
        /// <param name="segments">合并后的广告片段列表</param>
        private static void PrintMergeResult(List<AdSegment> segments)
        {
            Console.WriteLine($"合并后共 {segments.Count} 个广告片段:");
            
            if (segments.Count > 0)
            {
                for (int i = 0; i < segments.Count; i++)
                {
                    AdSegment segment = segments[i];
                    string startTimeStr = double.IsNaN(segment.StartTime) ? "-" : TimeSpan.FromSeconds(segment.StartTime).ToString(@"hh\:mm\:ss");
                    string endTimeStr = double.IsNaN(segment.EndTime) ? "-" : TimeSpan.FromSeconds(segment.EndTime).ToString(@"hh\:mm\:ss");
                    Console.WriteLine($"{i + 1}. 开始时间: {startTimeStr}, 结束时间: {endTimeStr}");
                    Console.WriteLine($"   备注: {segment.Notes.Replace('\n', ' ')}");
                }
            }
            else
            {
                Console.WriteLine("未生成任何合并片段");
            }
        }
    }
}