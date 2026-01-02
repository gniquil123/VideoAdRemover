using System;
using System.IO;
using System.Text;

namespace VideoAdRemover
{
    /// <summary>
    /// 日志记录器
    /// 用于记录程序运行过程中的信息、警告和错误
    /// </summary>
    public class Logger
    {
        private static readonly string _logFilePath;
        private static readonly object _lockObject = new object();
        
        /// <summary>
        /// 日志级别枚举
        /// </summary>
        public enum LogLevel
        {
            Info,
            Warning,
            Error
        }
        
        /// <summary>
        /// 静态构造函数，初始化日志文件路径
        /// </summary>
        static Logger()
        {
            // 日志文件保存在程序运行目录下的Logs文件夹
            string logDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            Directory.CreateDirectory(logDir);
            _logFilePath = Path.Combine(logDir, $"VideoAdRemover_{DateTime.Now:yyyyMMdd}.log");
        }
        
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="level">日志级别</param>
        /// <param name="message">日志信息</param>
        public static void Log(LogLevel level, string message)
        {
            lock (_lockObject)
            {
                try
                {
                    // 构建日志行
                    string logLine = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [{level.ToString().ToUpper()}] {message}";
                    
                    // 写入日志文件
                    File.AppendAllLines(_logFilePath, new[] { logLine }, Encoding.UTF8);
                    
                    // 同时输出到控制台
                    Console.WriteLine(logLine);
                }
                catch (Exception ex)
                {
                    // 日志记录失败时，输出到控制台
                    Console.WriteLine($"日志记录失败: {ex.Message}");
                }
            }
        }
        
        /// <summary>
        /// 记录信息日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public static void Info(string message)
        {
            Log(LogLevel.Info, message);
        }
        
        /// <summary>
        /// 记录警告日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public static void Warning(string message)
        {
            Log(LogLevel.Warning, message);
        }
        
        /// <summary>
        /// 记录警告日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="ex">异常对象</param>
        public static void Warning(string message, Exception ex)
        {
            Log(LogLevel.Warning, $"{message}\n异常详情: {ex.Message}\n堆栈跟踪: {ex.StackTrace}");
        }
        
        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="message">日志信息</param>
        public static void Error(string message)
        {
            Log(LogLevel.Error, message);
        }
        
        /// <summary>
        /// 记录异常日志
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="ex">异常对象</param>
        public static void Error(string message, Exception ex)
        {
            Log(LogLevel.Error, $"{message}\n异常详情: {ex.Message}\n堆栈跟踪: {ex.StackTrace}");
        }
    }
}