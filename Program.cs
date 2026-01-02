namespace VideoAdRemover;

static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// <summary>
    /// 应用程序的主入口点。
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
        // 检查命令行参数
        if (args.Length > 0)
        {
            // 如果有命令行参数，执行命令行模式
            ExecuteCommandLine(args);
            return;
        }
        
        // 正常启动GUI界面
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }
    
    /// <summary>
    /// 执行命令行操作
    /// </summary>
    /// <param name="args">命令行参数</param>
    static void ExecuteCommandLine(string[] args)
    {
        Console.WriteLine("视频广告识别工具 - 命令行模式");
        Console.WriteLine("====================================");
        
        try
        {
            // 根据参数数量执行不同操作
            if (args.Length == 3 && args[0].Equals("compare", StringComparison.OrdinalIgnoreCase))
            {
                // 比较两个图片的相似度
                string imagePath1 = args[1];
                string imagePath2 = args[2];
                TestAdDetector.TestCompareImages(imagePath1, imagePath2);
            }
            else if (args.Length == 4 && args[0].Equals("detect", StringComparison.OrdinalIgnoreCase))
            {
                // 检测视频中的广告
                string videoPath = args[1];
                double adStart = double.Parse(args[2]);
                double adEnd = double.Parse(args[3]);
                TestAdDetector.Test(videoPath, adStart, adEnd);
            }
            else if (args.Length == 1 && args[0].Equals("merge-test", StringComparison.OrdinalIgnoreCase))
            {
                // 运行合并广告片段测试
                TestAdDetector.TestMergeOverlappingSegments();
            }
            else if (args.Length == 1 && args[0].Equals("--test", StringComparison.OrdinalIgnoreCase))
            {
                // 运行合并广告片段测试（简写形式）
                TestAdDetector.TestMergeOverlappingSegments();
            }
            else if (args.Length == 1 && args[0].Equals("merge-simple", StringComparison.OrdinalIgnoreCase))
            {
                // 运行简单的合并测试
                MergeTestSimple.RunSimpleTest();
            }
            else
            {
                // 显示帮助信息
                ShowHelp();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"执行命令时发生错误: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
            Console.WriteLine();
            ShowHelp();
        }
        
        Console.WriteLine();
        Console.WriteLine("按任意键退出...");
        Console.ReadKey();
    }
    
    /// <summary>
    /// 显示命令行帮助信息
    /// </summary>
    static void ShowHelp()
    {
        Console.WriteLine("使用方法:");
        Console.WriteLine();
        Console.WriteLine("1. 比较两个图片的相似度:");
        Console.WriteLine("   VideoAdRemover.exe compare <图片1路径> <图片2路径>");
        Console.WriteLine();
        Console.WriteLine("2. 检测视频中的广告:");
        Console.WriteLine("   VideoAdRemover.exe detect <视频路径> <广告开始时间(秒)> <广告结束时间(秒)>");
        Console.WriteLine();
        Console.WriteLine("3. 测试合并广告片段功能:");
        Console.WriteLine("   VideoAdRemover.exe merge-test");
        Console.WriteLine("   或");
        Console.WriteLine("   VideoAdRemover.exe --test");
        Console.WriteLine("4. 运行简单的合并测试:");
        Console.WriteLine("   VideoAdRemover.exe merge-simple");
        Console.WriteLine();
        Console.WriteLine("4. 启动图形界面:");
        Console.WriteLine("   VideoAdRemover.exe");
        Console.WriteLine();
    }
}