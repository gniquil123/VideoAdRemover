using System;
using System.Collections.Generic;

namespace VideoAdRemover
{
    /// <summary>
    /// 广告片段合并命令行测试工具
    /// </summary>
    public static class MergeTestConsole
    {
        /// <summary>
    /// 运行交互式合并测试
    /// </summary>
        public static void RunInteractiveTest()
        {
            Console.WriteLine("广告片段合并测试工具");
            Console.WriteLine("=====================");
            Console.WriteLine("这个工具允许你设置合并前的广告片段，测试合并结果是否正确");
            Console.WriteLine();
            
            // 创建AdDetector实例
            AdDetector detector = new AdDetector(false);
            
            while (true)
            {
                try
                {
                    Console.WriteLine("1. 运行预设测试用例");
                    Console.WriteLine("2. 自定义测试数据");
                    Console.WriteLine("3. 退出测试");
                    Console.Write("请选择操作 (1-3): ");
                    
                    string choice = Console.ReadLine();
                    Console.WriteLine();
                    
                    switch (choice)
                    {
                        case "1":
                            RunPresetTests(detector);
                            break;
                        case "2":
                            RunCustomTest(detector);
                            break;
                        case "3":
                            Console.WriteLine("退出测试工具");
                            return;
                        default:
                            Console.WriteLine("无效的选择，请重新输入");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"测试过程中发生错误: {ex.Message}");
                }
                finally
                {
                    Console.WriteLine();
                    Console.WriteLine("按任意键继续...");
                    Console.ReadKey(true);
                    Console.WriteLine();
                }
            }
        }
        
        /// <summary>
        /// 运行预设测试用例
        /// </summary>
        private static void RunPresetTests(AdDetector detector)
        {
            Console.WriteLine("=== 预设测试用例 ===");
            
            // 测试用例1：用户描述的场景 - 6个结果合并为1个
            Console.WriteLine("测试用例1：用户描述的6条结果");
            List<AdSegment> test1 = new List<AdSegment>
            {
                new AdSegment { StartTime = 478, EndTime = double.NaN }, // 00:07:58
                new AdSegment { StartTime = 479, EndTime = double.NaN }, // 00:07:59
                new AdSegment { StartTime = 480, EndTime = double.NaN }, // 00:08:00
                new AdSegment { StartTime = double.NaN, EndTime = 491 }, // 00:08:11
                new AdSegment { StartTime = double.NaN, EndTime = 492 }, // 00:08:12
                new AdSegment { StartTime = double.NaN, EndTime = 493 }  // 00:08:13
            };
            TestMerge(detector, test1, 15.0);
            
            // 测试用例2：只有开始时间的情况
            Console.WriteLine("\n测试用例2：只有开始时间的情况");
            List<AdSegment> test2 = new List<AdSegment>
            {
                new AdSegment { StartTime = 100, EndTime = double.NaN },
                new AdSegment { StartTime = 101, EndTime = double.NaN },
                new AdSegment { StartTime = 102, EndTime = double.NaN }
            };
            TestMerge(detector, test2, 10.0);
            
            // 测试用例3：只有结束时间的情况
            Console.WriteLine("\n测试用例3：只有结束时间的情况");
            List<AdSegment> test3 = new List<AdSegment>
            {
                new AdSegment { StartTime = double.NaN, EndTime = 200 },
                new AdSegment { StartTime = double.NaN, EndTime = 201 },
                new AdSegment { StartTime = double.NaN, EndTime = 202 }
            };
            TestMerge(detector, test3, 10.0);
            
            // 测试用例4：打乱顺序的情况
            Console.WriteLine("\n测试用例4：打乱顺序的情况");
            List<AdSegment> test4 = new List<AdSegment>
            {
                new AdSegment { StartTime = double.NaN, EndTime = 493 },  // 00:08:13 (放在前面)
                new AdSegment { StartTime = 478, EndTime = double.NaN }, // 00:07:58
                new AdSegment { StartTime = double.NaN, EndTime = 491 }, // 00:08:11
                new AdSegment { StartTime = 480, EndTime = double.NaN }, // 00:08:00
                new AdSegment { StartTime = double.NaN, EndTime = 492 }, // 00:08:12
                new AdSegment { StartTime = 479, EndTime = double.NaN }  // 00:07:59 (放在最后)
            };
            TestMerge(detector, test4, 15.0);
        }
        
        /// <summary>
        /// 运行自定义测试
        /// </summary>
        private static void RunCustomTest(AdDetector detector)
        {
            Console.WriteLine("=== 自定义测试 ===");
            
            List<AdSegment> customSegments = new List<AdSegment>();
            
            // 获取片段数量
            int segmentCount = GetValidIntInput("请输入广告片段数量 (1-20): ", 1, 20);
            
            // 获取每个片段的开始时间和结束时间
            for (int i = 0; i < segmentCount; i++)
            {
                Console.WriteLine($"\n广告片段 {i+1}：");
                
                double startTime = GetDoubleInput("开始时间（秒，输入-1表示无）: ");
                startTime = startTime == -1 ? double.NaN : startTime;
                
                double endTime = GetDoubleInput("结束时间（秒，输入-1表示无）: ");
                endTime = endTime == -1 ? double.NaN : endTime;
                
                customSegments.Add(new AdSegment { StartTime = startTime, EndTime = endTime });
            }
            
            // 获取广告时长
            double adDuration = GetValidDoubleInput("广告时长（秒，1-600）: ", 1, 600);
            
            // 执行测试
            TestMerge(detector, customSegments, adDuration);
        }
        
        /// <summary>
        /// 执行合并测试
        /// </summary>
        private static void TestMerge(AdDetector detector, List<AdSegment> segments, double adDurationSec)
        {
            // 打印测试数据
            Console.WriteLine($"广告时长：{adDurationSec}秒");
            Console.WriteLine("合并前的片段：");
            
            // 显示合并前的片段，包括序号
            for (int i = 0; i < segments.Count; i++)
            {
                var seg = segments[i];
                string startTimeStr = double.IsNaN(seg.StartTime) ? "-" : TimeSpan.FromSeconds(seg.StartTime).ToString(@"hh\:mm\:ss");
                string endTimeStr = double.IsNaN(seg.EndTime) ? "-" : TimeSpan.FromSeconds(seg.EndTime).ToString(@"hh\:mm\:ss");
                Console.WriteLine($"  [{i+1}] 开始时间：{startTimeStr}，结束时间：{endTimeStr}");
            }
            
            // 执行合并
            List<AdSegment> merged = detector.MergeOverlappingSegments(segments, adDurationSec);
            
            // 打印合并结果
            Console.WriteLine("\n合并后的片段：");
            for (int i = 0; i < merged.Count; i++)
            {
                var seg = merged[i];
                string startTimeStr = double.IsNaN(seg.StartTime) ? "-" : TimeSpan.FromSeconds(seg.StartTime).ToString(@"hh\:mm\:ss");
                string endTimeStr = double.IsNaN(seg.EndTime) ? "-" : TimeSpan.FromSeconds(seg.EndTime).ToString(@"hh\:mm\:ss");
                Console.WriteLine($"  [{i+1}] 开始时间：{startTimeStr}，结束时间：{endTimeStr}，备注：{seg.Notes}");
            }
            
            Console.WriteLine($"\n合并结果：{segments.Count}个片段 → {merged.Count}个片段");
            
            // 验证是否合并成功
            if (segments.Count > 1 && merged.Count == 1 && 
                !double.IsNaN(merged[0].StartTime) && !double.IsNaN(merged[0].EndTime))
            {
                Console.WriteLine("✅ 合并成功！多个片段已合并为一个完整的广告段");
            }
            else if (segments.Count == merged.Count)
            {
                Console.WriteLine("ℹ️  未合并任何片段，所有片段都是独立的");
            }
            else
            {
                Console.WriteLine("✅ 部分合并成功");
            }
        }
        
        /// <summary>
        /// 获取有效的整数输入
        /// </summary>
        private static int GetValidIntInput(string prompt, int minValue, int maxValue)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out result) && result >= minValue && result <= maxValue)
                {
                    break;
                }
                Console.WriteLine($"输入无效，请输入{minValue}到{maxValue}之间的整数");
            }
            return result;
        }
        
        /// <summary>
        /// 获取有效的双精度浮点数输入
        /// </summary>
        private static double GetValidDoubleInput(string prompt, double minValue, double maxValue)
        {
            double result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (double.TryParse(input, out result) && result >= minValue && result <= maxValue)
                {
                    break;
                }
                Console.WriteLine($"输入无效，请输入{minValue}到{maxValue}之间的数字");
            }
            return result;
        }
        
        /// <summary>
        /// 获取双精度浮点数输入，允许-1表示无
        /// </summary>
        private static double GetDoubleInput(string prompt)
        {
            double result;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (double.TryParse(input, out result))
                {
                    break;
                }
                Console.WriteLine("输入无效，请输入数字");
            }
            return result;
        }
    }
}