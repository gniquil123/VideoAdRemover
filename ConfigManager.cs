using System;
using System.Configuration;
using System.IO;
using System.Xml.Linq;
using System.Xml.XPath;

namespace VideoAdRemover
{
    /// <summary>
    /// 配置管理器
    /// 用于保存和加载程序配置，包括界面输入记忆
    /// </summary>
    public class ConfigManager
    {
        private static readonly string _configFilePath;
        private static readonly XDocument _configDoc;
        
        /// <summary>
        /// 静态构造函数，初始化配置文件路径
        /// </summary>
        static ConfigManager()
        {
            // 配置文件保存在程序运行目录下
            _configFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml");
            
            // 如果配置文件不存在，创建默认配置
            if (!File.Exists(_configFilePath))
            {
                _configDoc = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("Config",
                        new XElement("LastMoviePath", string.Empty),
                        new XElement("AdStartTime", new XElement("Hour", 0), new XElement("Minute", 0), new XElement("Second", 0)),
                        new XElement("AdEndTime", new XElement("Hour", 0), new XElement("Minute", 0), new XElement("Second", 30)),
                        new XElement("DarkTheme", false)
                    )
                );
                _configDoc.Save(_configFilePath);
            }
            else
            {
                _configDoc = XDocument.Load(_configFilePath);
            }
        }
        
        /// <summary>
        /// 保存配置
        /// </summary>
        public static void SaveConfig()
        {
            try
            {
                _configDoc.Save(_configFilePath);
                Logger.Info("配置已保存");
            }
            catch (Exception ex)
            {
                Logger.Error("保存配置失败", ex);
            }
        }
        
        #region 配置项
        
        /// <summary>
        /// 上次使用的电影文件路径
        /// </summary>
        public static string LastMoviePath
        {
            get => GetConfigValue("LastMoviePath", string.Empty);
            set => SetConfigValue("LastMoviePath", value);
        }
        
        /// <summary>
        /// 广告开始时间 - 小时
        /// </summary>
        public static int AdStartHour
        {
            get => GetConfigValue("AdStartTime/Hour", 0);
            set => SetConfigValue("AdStartTime/Hour", value);
        }
        
        /// <summary>
        /// 广告开始时间 - 分钟
        /// </summary>
        public static int AdStartMinute
        {
            get => GetConfigValue("AdStartTime/Minute", 0);
            set => SetConfigValue("AdStartTime/Minute", value);
        }
        
        /// <summary>
        /// 广告开始时间 - 秒
        /// </summary>
        public static int AdStartSecond
        {
            get => GetConfigValue("AdStartTime/Second", 0);
            set => SetConfigValue("AdStartTime/Second", value);
        }
        
        /// <summary>
        /// 广告结束时间 - 小时
        /// </summary>
        public static int AdEndHour
        {
            get => GetConfigValue("AdEndTime/Hour", 0);
            set => SetConfigValue("AdEndTime/Hour", value);
        }
        
        /// <summary>
        /// 广告结束时间 - 分钟
        /// </summary>
        public static int AdEndMinute
        {
            get => GetConfigValue("AdEndTime/Minute", 0);
            set => SetConfigValue("AdEndTime/Minute", value);
        }
        
        /// <summary>
        /// 广告结束时间 - 秒
        /// </summary>
        public static int AdEndSecond
        {
            get => GetConfigValue("AdEndTime/Second", 30);
            set => SetConfigValue("AdEndTime/Second", value);
        }
        
        /// <summary>
        /// 是否使用深色主题
        /// </summary>
        public static bool DarkTheme
        {
            get => GetConfigValue("DarkTheme", false);
            set => SetConfigValue("DarkTheme", value);
        }
        
        /// <summary>
        /// 广告检测扫描间隔（秒）
        /// </summary>
        public static int ScanIntervalSec
        {
            get => GetConfigValue("ScanIntervalSec", 1);
            set => SetConfigValue("ScanIntervalSec", value);
        }
        
        #endregion
        
        #region 私有方法
        
        /// <summary>
        /// 获取配置值
        /// </summary>
        /// <param name="xpath">XPath路径</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>配置值</returns>
        private static T GetConfigValue<T>(string xpath, T defaultValue)
        {
            try
            {
                var element = _configDoc.Root?.XPathSelectElement(xpath);
                if (element != null)
                {
                    return (T)Convert.ChangeType(element.Value, typeof(T));
                }
            }
            catch (Exception ex)
            {
                Logger.Warning("获取配置 " + xpath + " 失败，使用默认值", ex);
            }
            return defaultValue;
        }
        
        /// <summary>
        /// 设置配置值
        /// </summary>
        /// <param name="xpath">XPath路径</param>
        /// <param name="value">配置值</param>
        private static void SetConfigValue<T>(string xpath, T value)
        {
            try
            {
                var element = _configDoc.Root?.XPathSelectElement(xpath);
                if (element != null)
                {
                    element.Value = value?.ToString() ?? string.Empty;
                }
                else
                {
                    // 如果元素不存在，创建它
                    var parts = xpath.Split('/');
                    var current = _configDoc.Root;
                    
                    for (int i = 0; i < parts.Length; i++)
                    {
                        var part = parts[i];
                        var child = current?.Element(part);
                        
                        if (child == null)
                        {
                            child = new XElement(part);
                            current?.Add(child);
                        }
                        
                        current = child;
                    }
                    
                    if (current != null)
                    {
                        current.Value = value?.ToString() ?? string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("设置配置 " + xpath + " 失败", ex);
            }
        }
        
        #endregion
    }
}