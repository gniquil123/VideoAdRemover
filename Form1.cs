using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VideoAdRemover
{
    public partial class Form1 : Form
    {
        private List<AdSegment> _detectedAdSegments = new List<AdSegment>();
        private AdDetector _adDetector;
        private VideoEditor _videoEditor = new VideoEditor();
        private bool _isStopDetectRequested = false;
        private List<TimeRange> _detectionTimeRanges = new List<TimeRange>(); // 存储多个检测时间段
        
        // 检测结果编辑控件声明
        private Label lblEditStartTime;
        private NumericUpDown nudEditStartHour;
        private NumericUpDown nudEditStartMinute;
        private NumericUpDown nudEditStartSecond;
        private Label lblEditStartHour;
        private Label lblEditStartMinute;
        private Label lblEditStartSecond;
        private Label lblEditEndTime;
        private NumericUpDown nudEditEndHour;
        private NumericUpDown nudEditEndMinute;
        private NumericUpDown nudEditEndSecond;
        private Label lblEditEndHour;
        private Label lblEditEndMinute;
        private Label lblEditEndSecond;
        private TextBox txtEditNotes;
        private Label lblEditNotes;
        private Button btnAddSegment;
        private Button btnUpdateSegment;
        private Button btnDeleteSegment;
        private Button btnClearEdit;

        public Form1()
        {
            InitializeComponent();
            this.chkDarkTheme.CheckedChanged += new System.EventHandler(this.chkDarkTheme_CheckedChanged);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            
            // 加载配置
            LoadConfig();
            
            // 初始化广告检测器，使用配置中的扫描间隔
            _adDetector = new AdDetector(scanIntervalSec: ConfigManager.ScanIntervalSec);
            
            // 初始应用主题
            ApplyTheme(chkDarkTheme.Checked);
        }

        /// <summary>
        /// 主题切换复选框状态改变事件
        /// </summary>
        private void chkDarkTheme_CheckedChanged(object sender, EventArgs e)
        {
            ApplyTheme(chkDarkTheme.Checked);
        }

        /// <summary>
        /// 应用主题
        /// </summary>
        /// <param name="isDarkTheme">是否应用黑暗主题</param>
        private void ApplyTheme(bool isDarkTheme)
        {
            // 主题颜色定义
            Color backgroundColor = isDarkTheme ? Color.FromArgb(30, 30, 30) : Color.White;
            Color foregroundColor = isDarkTheme ? Color.White : Color.Black;
            Color groupBoxColor = isDarkTheme ? Color.FromArgb(45, 45, 45) : Color.White;
            Color buttonColor = isDarkTheme ? Color.FromArgb(60, 60, 60) : Color.White;
            Color textboxColor = isDarkTheme ? Color.FromArgb(60, 60, 60) : Color.White;
            Color listViewColor = isDarkTheme ? Color.FromArgb(45, 45, 45) : Color.White;
            Color listViewHeaderColor = isDarkTheme ? Color.FromArgb(60, 60, 60) : Color.LightGray;
            Color progressBarColor = isDarkTheme ? Color.FromArgb(0, 120, 215) : Color.FromArgb(0, 120, 215);

            // 应用到Form
            this.BackColor = backgroundColor;
            this.ForeColor = foregroundColor;

            // 应用到Label
            lblTitle.ForeColor = foregroundColor;
            label1.ForeColor = foregroundColor;
            // 应用到新的时分秒Label控件
            labelStartHour.ForeColor = foregroundColor;
            labelStartMinute.ForeColor = foregroundColor;
            labelStartSecond.ForeColor = foregroundColor;
            labelEndHour.ForeColor = foregroundColor;
            labelEndMinute.ForeColor = foregroundColor;
            labelEndSecond.ForeColor = foregroundColor;
            lblProgressText.ForeColor = foregroundColor;
            // 应用到图片比较标签
            lblImage1.ForeColor = foregroundColor;
            lblImage2.ForeColor = foregroundColor;
            lblCompareResult.ForeColor = foregroundColor;

            // 应用到GroupBox
            ApplyGroupBoxTheme(grpMovieFile, groupBoxColor, foregroundColor);
            ApplyGroupBoxTheme(grpAdTime, groupBoxColor, foregroundColor);
            ApplyGroupBoxTheme(grpDetectResult, groupBoxColor, foregroundColor);
            ApplyGroupBoxTheme(grpActions, groupBoxColor, foregroundColor);
            ApplyGroupBoxTheme(grpProgress, groupBoxColor, foregroundColor);
            ApplyGroupBoxTheme(grpImageCompare, groupBoxColor, foregroundColor);

            // 应用到Button
            ApplyButtonTheme(btnBrowseMovie, buttonColor, foregroundColor);
            //ApplyButtonTheme(btnPreviewMovie, buttonColor, foregroundColor);
            //ApplyButtonTheme(btnPlayAdPreview, buttonColor, foregroundColor);
            //ApplyButtonTheme(btnPauseAdPreview, buttonColor, foregroundColor);
            //ApplyButtonTheme(btnStopAdPreview, buttonColor, foregroundColor);
            ApplyButtonTheme(btnDetectAds, buttonColor, foregroundColor);
            ApplyButtonTheme(btnStopDetect, buttonColor, foregroundColor);
            ApplyButtonTheme(btnRemoveAds, buttonColor, foregroundColor);
            ApplyButtonTheme(btnTestMerge, buttonColor, foregroundColor);
            //ApplyButtonTheme(btnExit, buttonColor, foregroundColor);
            ApplyButtonTheme(btnBrowseImage1, buttonColor, foregroundColor);
            ApplyButtonTheme(btnBrowseImage2, buttonColor, foregroundColor);
            ApplyButtonTheme(btnCompareImages, buttonColor, foregroundColor);

            // 应用到TextBox
            txtMoviePath.BackColor = textboxColor;
            txtMoviePath.ForeColor = foregroundColor;
            txtMoviePath.BorderStyle = BorderStyle.FixedSingle;
            txtImagePath1.BackColor = textboxColor;
            txtImagePath1.ForeColor = foregroundColor;
            txtImagePath1.BorderStyle = BorderStyle.FixedSingle;
            txtImagePath2.BackColor = textboxColor;
            txtImagePath2.ForeColor = foregroundColor;
            txtImagePath2.BorderStyle = BorderStyle.FixedSingle;

            // 应用到时分秒NumericUpDown控件
            ApplyNumericUpDownTheme(nudAdStartHour, textboxColor, foregroundColor);
            ApplyNumericUpDownTheme(nudAdStartMinute, textboxColor, foregroundColor);
            ApplyNumericUpDownTheme(nudAdStartSecond, textboxColor, foregroundColor);
            ApplyNumericUpDownTheme(nudAdEndHour, textboxColor, foregroundColor);
            ApplyNumericUpDownTheme(nudAdEndMinute, textboxColor, foregroundColor);
            ApplyNumericUpDownTheme(nudAdEndSecond, textboxColor, foregroundColor);
            
            // 应用到扫描间隔控件
            lblScanInterval.ForeColor = foregroundColor;
            ApplyNumericUpDownTheme(nudScanInterval, textboxColor, foregroundColor);

            // 应用到ListView
            lvAdSegments.BackColor = listViewColor;
            lvAdSegments.ForeColor = foregroundColor;
            lvAdSegments.GridLines = true;
            lvAdSegments.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            
            // 应用到检测时间段控件
            ApplyGroupBoxTheme(grpDetectionRanges, groupBoxColor, foregroundColor);
            ApplyButtonTheme(btnAddRange, buttonColor, foregroundColor);
            ApplyButtonTheme(btnDeleteRange, buttonColor, foregroundColor);
            ApplyButtonTheme(btnClearRanges, buttonColor, foregroundColor);
            lvDetectionRanges.BackColor = listViewColor;
            lvDetectionRanges.ForeColor = foregroundColor;
            lvDetectionRanges.GridLines = true;
            
            // 应用到检测结果编辑区域的标签和控件
            lblEditStartTime.ForeColor = foregroundColor;
            lblEditEndTime.ForeColor = foregroundColor;
            lblEditStartHour.ForeColor = foregroundColor;
            lblEditStartMinute.ForeColor = foregroundColor;
            lblEditStartSecond.ForeColor = foregroundColor;
            lblEditEndHour.ForeColor = foregroundColor;
            lblEditEndMinute.ForeColor = foregroundColor;
            lblEditEndSecond.ForeColor = foregroundColor;
            lblEditNotes.ForeColor = foregroundColor;
            
            // 应用到检测结果编辑区域的按钮
            ApplyButtonTheme(btnAddSegment, buttonColor, foregroundColor);
            ApplyButtonTheme(btnUpdateSegment, buttonColor, foregroundColor);
            ApplyButtonTheme(btnDeleteSegment, buttonColor, foregroundColor);
            ApplyButtonTheme(btnClearEdit, buttonColor, foregroundColor);
            
            // 应用到检测结果编辑区域的文本框和数字选择器
            txtEditNotes.BackColor = textboxColor;
            txtEditNotes.ForeColor = foregroundColor;
            txtEditNotes.BorderStyle = BorderStyle.FixedSingle;
            ApplyNumericUpDownTheme(nudEditStartHour, textboxColor, foregroundColor);
            ApplyNumericUpDownTheme(nudEditStartMinute, textboxColor, foregroundColor);
            ApplyNumericUpDownTheme(nudEditStartSecond, textboxColor, foregroundColor);
            ApplyNumericUpDownTheme(nudEditEndHour, textboxColor, foregroundColor);
            ApplyNumericUpDownTheme(nudEditEndMinute, textboxColor, foregroundColor);
            ApplyNumericUpDownTheme(nudEditEndSecond, textboxColor, foregroundColor);
            
            // 应用到检测时间段时间控件
            lblRangeTimeLabel.ForeColor = foregroundColor;
            lblRangeStartHour.ForeColor = foregroundColor;
            lblRangeStartMinute.ForeColor = foregroundColor;
            lblRangeStartSecond.ForeColor = foregroundColor;
            lblRangeEndHour.ForeColor = foregroundColor;
            lblRangeEndMinute.ForeColor = foregroundColor;
            lblRangeEndSecond.ForeColor = foregroundColor;
            ApplyNumericUpDownTheme(nudRangeStartHour, textboxColor, foregroundColor);
            ApplyNumericUpDownTheme(nudRangeStartMinute, textboxColor, foregroundColor);
            ApplyNumericUpDownTheme(nudRangeStartSecond, textboxColor, foregroundColor);
            ApplyNumericUpDownTheme(nudRangeEndHour, textboxColor, foregroundColor);
            ApplyNumericUpDownTheme(nudRangeEndMinute, textboxColor, foregroundColor);
            ApplyNumericUpDownTheme(nudRangeEndSecond, textboxColor, foregroundColor);
            
            // ListView表头样式在Windows Forms中无法直接通过ColumnHeader属性设置
            // 这里仅确保ListView本身的样式正确

            // 应用到ProgressBar
            pbProgress.ForeColor = progressBarColor;
        }

        /// <summary>
        /// 应用GroupBox主题
        /// </summary>
        /// <param name="groupBox">GroupBox控件</param>
        /// <param name="backgroundColor">背景色</param>
        /// <param name="foregroundColor">前景色</param>
        private void ApplyGroupBoxTheme(GroupBox groupBox, Color backgroundColor, Color foregroundColor)
        {
            groupBox.BackColor = backgroundColor;
            groupBox.ForeColor = foregroundColor;
        }

        /// <summary>
        /// 应用Button主题
        /// </summary>
        /// <param name="button">Button控件</param>
        /// <param name="backgroundColor">背景色</param>
        /// <param name="foregroundColor">前景色</param>
        private void ApplyButtonTheme(Button button, Color backgroundColor, Color foregroundColor)
        {
            button.BackColor = backgroundColor;
            button.ForeColor = foregroundColor;
            button.FlatStyle = FlatStyle.Flat;
        }

        /// <summary>
        /// 应用NumericUpDown主题
        /// </summary>
        /// <param name="numericUpDown">NumericUpDown控件</param>
        /// <param name="backgroundColor">背景色</param>
        /// <param name="foregroundColor">前景色</param>
        private void ApplyNumericUpDownTheme(NumericUpDown numericUpDown, Color backgroundColor, Color foregroundColor)
        {
            numericUpDown.BackColor = backgroundColor;
            numericUpDown.ForeColor = foregroundColor;
            numericUpDown.BorderStyle = BorderStyle.FixedSingle;
        }

        /// <summary>
        /// 将时分秒转换为总秒数
        /// </summary>
        /// <param name="hour">小时</param>
        /// <param name="minute">分钟</param>
        /// <param name="second">秒</param>
        /// <returns>总秒数</returns>
        private double TimeToSeconds(int hour, int minute, int second)
        {
            return hour * 3600 + minute * 60 + second;
        }
        
        /// <summary>
        /// 加载配置到界面
        /// </summary>
        private void LoadConfig()
        {
            try
            {
                // 加载上次使用的电影路径
                txtMoviePath.Text = ConfigManager.LastMoviePath;
                
                // 加载广告开始时间
                nudAdStartHour.Value = ConfigManager.AdStartHour;
                nudAdStartMinute.Value = ConfigManager.AdStartMinute;
                nudAdStartSecond.Value = ConfigManager.AdStartSecond;
                
                // 加载广告结束时间
                nudAdEndHour.Value = ConfigManager.AdEndHour;
                nudAdEndMinute.Value = ConfigManager.AdEndMinute;
                nudAdEndSecond.Value = ConfigManager.AdEndSecond;
                
                // 加载主题设置
                chkDarkTheme.Checked = ConfigManager.DarkTheme;
                
                // 加载扫描间隔设置
                nudScanInterval.Value = (decimal)ConfigManager.ScanIntervalSec;
                
                Logger.Info("配置已加载");
            }
            catch (Exception ex)
            {
                Logger.Error("加载配置失败", ex);
            }
        }
        
        /// <summary>
        /// 保存界面配置
        /// </summary>
        private void SaveConfig()
        {
            try
            {
                // 保存上次使用的电影路径
                ConfigManager.LastMoviePath = txtMoviePath.Text;
                
                // 保存广告开始时间
                ConfigManager.AdStartHour = (int)nudAdStartHour.Value;
                ConfigManager.AdStartMinute = (int)nudAdStartMinute.Value;
                ConfigManager.AdStartSecond = (int)nudAdStartSecond.Value;
                
                // 保存广告结束时间
                ConfigManager.AdEndHour = (int)nudAdEndHour.Value;
                ConfigManager.AdEndMinute = (int)nudAdEndMinute.Value;
                ConfigManager.AdEndSecond = (int)nudAdEndSecond.Value;
                
                // 保存主题设置
                ConfigManager.DarkTheme = chkDarkTheme.Checked;
                
                // 保存扫描间隔设置
                ConfigManager.ScanIntervalSec = (int)nudScanInterval.Value;
                
                // 保存配置到文件
                ConfigManager.SaveConfig();
            }
            catch (Exception ex)
            {
                Logger.Error("保存配置失败", ex);
            }
        }
        
        /// <summary>
        /// 添加广告片段到列表视图（实时显示）
        /// </summary>
        /// <param name="segment">广告片段</param>
        private void AddAdSegmentToView(AdSegment segment)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<AdSegment>(AddAdSegmentToView), segment);
                return;
            }
            
            int index = _detectedAdSegments.Count + 1;
            ListViewItem item = new ListViewItem(index.ToString());
            
            // 安全格式化开始时间
            string startTimeStr = "-";
            try
            {
                if (!double.IsNaN(segment.StartTime) && !double.IsInfinity(segment.StartTime) && segment.StartTime >= 0)
                {
                    int totalSeconds = (int)segment.StartTime;
                    int hours = totalSeconds / 3600;
                    int minutes = (totalSeconds % 3600) / 60;
                    int seconds = totalSeconds % 60;
                    startTimeStr = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
                }
            }
            catch (Exception ex)
            {
                Logger.Warning(string.Format("格式化开始时间失败：{0}秒，错误：{1}，显示为未设置", segment.StartTime, ex.Message));
            }
            
            // 安全格式化结束时间
            string endTimeStr = "-";
            try
            {
                if (!double.IsNaN(segment.EndTime) && !double.IsInfinity(segment.EndTime) && segment.EndTime >= 0)
                {
                    int totalSeconds = (int)segment.EndTime;
                    int hours = totalSeconds / 3600;
                    int minutes = (totalSeconds % 3600) / 60;
                    int seconds = totalSeconds % 60;
                    endTimeStr = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
                }
            }
            catch (Exception ex)
            {
                Logger.Warning(string.Format("格式化结束时间失败：{0}秒，错误：{1}，显示为未设置", segment.EndTime, ex.Message));
            }
            
            // 备注信息
            string notes = segment.Notes ?? "";
            
            item.SubItems.Add(startTimeStr);
            item.SubItems.Add(endTimeStr);
            item.SubItems.Add(notes);
            item.Tag = segment;
            lvAdSegments.Items.Add(item);
        }

        /// <summary>
        /// 获取开始时间总秒数
        /// </summary>
        /// <returns>开始时间总秒数</returns>
        private double GetAdStartTime()
        {
            int hour = (int)nudAdStartHour.Value;
            int minute = (int)nudAdStartMinute.Value;
            int second = (int)nudAdStartSecond.Value;
            return TimeToSeconds(hour, minute, second);
        }

        /// <summary>
        /// 获取结束时间总秒数
        /// </summary>
        /// <returns>结束时间总秒数</returns>
        private double GetAdEndTime()
        {
            int hour = (int)nudAdEndHour.Value;
            int minute = (int)nudAdEndMinute.Value;
            int second = (int)nudAdEndSecond.Value;
            return TimeToSeconds(hour, minute, second);
        }

        /// <summary>
        /// 浏览电影文件按钮点击事件
        /// </summary>
        private void btnBrowseMovie_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtMoviePath.Text = openFileDialog1.FileName;
            }
        }

        /// <summary>
        /// 预览电影按钮点击事件
        /// </summary>
        private void btnPreviewMovie_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMoviePath.Text))
            {
                MessageBox.Show("请先选择电影文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 使用系统默认播放器打开视频
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = txtMoviePath.Text,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"预览失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 播放广告预览按钮点击事件
        /// </summary>
        private void btnPlayAdPreview_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMoviePath.Text))
            {
                MessageBox.Show("请先选择电影文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double adStart = GetAdStartTime();
            double adEnd = GetAdEndTime();

            if (adEnd <= adStart)
            {
                MessageBox.Show("广告结束时间必须大于开始时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 暂时不实现广告预览功能，使用消息提示
                MessageBox.Show($"广告预览功能开发中，广告时间范围：{adStart} - {adEnd}秒", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"广告预览失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 暂停广告预览按钮点击事件
        /// </summary>
        private void btnPauseAdPreview_Click(object sender, EventArgs e)
        {
            // 暂时不实现暂停功能
            MessageBox.Show("广告预览暂停功能开发中", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 停止广告预览按钮点击事件
        /// </summary>
        private void btnStopAdPreview_Click(object sender, EventArgs e)
        {
            // 暂时不实现停止功能
            MessageBox.Show("广告预览停止功能开发中", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 检测广告按钮点击事件
        /// </summary>
        private async void btnDetectAds_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMoviePath.Text))
            {
                MessageBox.Show("请先选择电影文件", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double adStart = GetAdStartTime();
            double adEnd = GetAdEndTime();

            if (adEnd <= adStart)
            {
                MessageBox.Show("广告结束时间必须大于开始时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 重置停止检测标志
                _isStopDetectRequested = false;
                
                // 清空之前的检测结果
                _detectedAdSegments.Clear();
                lvAdSegments.Items.Clear();

                // 禁用检测按钮，启用停止按钮
                btnDetectAds.Enabled = false;
                btnStopDetect.Enabled = true;
                btnRemoveAds.Enabled = false;

                // 初始化进度
                pbProgress.Value = 0;
                lblProgressText.Text = "开始检测广告...";

                // 设置扫描间隔
                _adDetector.ScanIntervalSec = (double)nudScanInterval.Value;
                
                // 异步检测广告
                List<AdSegment> finalSegments = await Task.Run(() =>
                {
                    // 如果没有添加任何时间段，则使用当前设置的开始和结束时间作为默认时间段
                    List<TimeRange> timeRangesToUse = _detectionTimeRanges;
                    if (timeRangesToUse.Count == 0)
                    {
                        timeRangesToUse = new List<TimeRange>
                        {
                            new TimeRange(adStart, adEnd)
                        };
                    }

                    return _adDetector.DetectAds(
                        txtMoviePath.Text,
                        adStart,
                        adEnd,
                        timeRangesToUse,
                        UpdateProgress,
                        segment =>
                        {
                            // 实时显示到界面
                            AddAdSegmentToView(segment);
                        }
                    );
                });

                // 更新检测结果列表
                _detectedAdSegments.Clear();
                _detectedAdSegments.AddRange(finalSegments);

                // 清空视图并重新显示最终结果，使用正确的序号
                lvAdSegments.Items.Clear();
                ShowDetectResults();

                // 更新进度
                pbProgress.Value = 100;
                lblProgressText.Text = $"检测完成，共发现 {finalSegments.Count} 个广告片段";

                // 启用移除广告按钮
                btnRemoveAds.Enabled = finalSegments.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"检测广告失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblProgressText.Text = "检测失败";
            }
            finally
            {
                // 重置停止检测标志
                _isStopDetectRequested = false;
                
                // 启用检测按钮，禁用停止按钮
                btnDetectAds.Enabled = true;
                btnStopDetect.Enabled = false;
            }
        }
        
        /// <summary>
        /// 停止检测按钮点击事件
        /// </summary>
        private void btnStopDetect_Click(object sender, EventArgs e)
        {
            // 调用广告检测器的停止方法
            _adDetector.StopDetection();
            
            // 更新UI提示
            lblProgressText.Text = "正在停止检测...";
            
            // 禁用停止按钮，防止重复点击
            btnStopDetect.Enabled = false;
        }

        /// <summary>
        /// 移除广告按钮点击事件
        /// </summary>
        private async void btnRemoveAds_Click(object sender, EventArgs e)
        {
            if (_detectedAdSegments.Count == 0)
            {
                MessageBox.Show("没有可移除的广告片段", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 确认是否开始移除
            DialogResult result = MessageBox.Show(
                $"共发现 {_detectedAdSegments.Count} 个广告片段，是否开始移除？\n" +
                "移除过程可能需要较长时间，请耐心等待。",
                "确认",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                // 禁用按钮，防止重复点击
                btnDetectAds.Enabled = false;
                btnRemoveAds.Enabled = false;

                // 初始化进度
                pbProgress.Value = 0;
                lblProgressText.Text = "开始移除广告...";

                // 生成输出文件路径
                string inputPath = txtMoviePath.Text;
                string outputDir = Path.GetDirectoryName(inputPath);
                string outputFileName = $"{Path.GetFileNameWithoutExtension(inputPath)}_无广告{Path.GetExtension(inputPath)}";
                string outputPath = Path.Combine(outputDir, outputFileName);

                // 异步移除广告
                await Task.Run(() =>
                {
                    _videoEditor.RemoveAds(
                        inputPath,
                        outputPath,
                        _detectedAdSegments,
                        UpdateProgress
                    );
                });

                // 更新进度
                pbProgress.Value = 100;
                lblProgressText.Text = "广告移除完成";

                // 显示成功消息
                MessageBox.Show($"广告移除完成！\n输出文件：{outputPath}", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"移除广告失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblProgressText.Text = "移除失败";
            }
            finally
            {
                // 启用按钮
                btnDetectAds.Enabled = true;
                btnRemoveAds.Enabled = _detectedAdSegments.Count > 0;
            }
        }

        /// <summary>
        /// 退出按钮点击事件
        /// </summary>
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        /// <summary>
        /// 窗体关闭事件，用于保存配置
        /// </summary>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveConfig();
        }

        /// <summary>
        /// 更新进度条
        /// </summary>
        /// <param name="progress">进度值（0-100）</param>
        private void UpdateProgress(double progress)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<double>(UpdateProgress), progress);
                return;
            }

            pbProgress.Value = (int)Math.Min(progress, 100);
            lblProgressText.Text = $"处理中... {progress:F1}%";
        }

        /// <summary>
        /// 浏览图片1按钮点击事件
        /// </summary>
        private void btnBrowseImage1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "图片文件|*.jpg;*.jpeg;*.png;*.bmp|所有文件|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtImagePath1.Text = openFileDialog.FileName;
                }
            }
        }

        /// <summary>
        /// 浏览图片2按钮点击事件
        /// </summary>
        private void btnBrowseImage2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "图片文件|*.jpg;*.jpeg;*.png;*.bmp|所有文件|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtImagePath2.Text = openFileDialog.FileName;
                }
            }
        }

        /// <summary>
        /// 比较图片相似度按钮点击事件
        /// </summary>
        private void btnCompareImages_Click(object sender, EventArgs e)
        {
            try
            {
                string imagePath1 = txtImagePath1.Text;
                string imagePath2 = txtImagePath2.Text;

                if (string.IsNullOrEmpty(imagePath1) || string.IsNullOrEmpty(imagePath2))
                {
                    MessageBox.Show("请先选择两张图片", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!File.Exists(imagePath1))
                {
                    MessageBox.Show("第一张图片不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!File.Exists(imagePath2))
                {
                    MessageBox.Show("第二张图片不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 禁用按钮，防止重复点击
                btnCompareImages.Enabled = false;
                lblCompareResult.Text = "正在比较...";

                // 异步比较图片相似度
                Task.Run(() =>
                {
                    double matchRate = _adDetector.CompareImages(imagePath1, imagePath2);
                    
                    // 更新UI
                    this.Invoke(new Action(() =>
                    {
                        lblCompareResult.Text = $"比较结果：{matchRate:F2}%";
                        btnCompareImages.Enabled = true;
                    }));
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"比较图片相似度失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnCompareImages.Enabled = true;
                lblCompareResult.Text = "比较结果：--%";
            }
        }

        /// <summary>
        /// 显示检测结果
        /// </summary>
        private void ShowDetectResults()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(ShowDetectResults));
                return;
            }

            lvAdSegments.Items.Clear();

            if (_detectedAdSegments.Count == 0)
            {
                return;
            }

            for (int i = 0; i < _detectedAdSegments.Count; i++)
            {
                AdSegment segment = _detectedAdSegments[i];
                ListViewItem item = new ListViewItem((i + 1).ToString());
                
                // 安全格式化开始时间
                string startTimeStr = "-";
                try
                {
                    if (!double.IsNaN(segment.StartTime) && !double.IsInfinity(segment.StartTime) && segment.StartTime >= 0)
                    {
                        startTimeStr = TimeSpan.FromSeconds(segment.StartTime).ToString(@"hh\:mm\:ss");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Warning(string.Format("格式化广告片段开始时间失败：{0}秒，错误：{1}", segment.StartTime, ex.Message));
                }
                
                // 安全格式化结束时间
                string endTimeStr = "-";
                try
                {
                    if (!double.IsNaN(segment.EndTime) && !double.IsInfinity(segment.EndTime) && segment.EndTime >= 0)
                    {
                        endTimeStr = TimeSpan.FromSeconds(segment.EndTime).ToString(@"hh\:mm\:ss");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Warning(string.Format("格式化广告片段结束时间失败：{0}秒，错误：{1}", segment.EndTime, ex.Message));
                }
                
                // 备注信息
                string notes = segment.Notes ?? "";
                
                item.SubItems.Add(startTimeStr);
                item.SubItems.Add(endTimeStr);
                item.SubItems.Add(notes);
                item.Tag = segment;
                lvAdSegments.Items.Add(item);
            }
        }
        
        /// <summary>
        /// 获取检测时间段开始时间总秒数
        /// </summary>
        /// <returns>开始时间总秒数</returns>
        private double GetRangeStartTime()
        {
            int hour = (int)nudRangeStartHour.Value;
            int minute = (int)nudRangeStartMinute.Value;
            int second = (int)nudRangeStartSecond.Value;
            return TimeToSeconds(hour, minute, second);
        }

        /// <summary>
        /// 获取检测时间段结束时间总秒数
        /// </summary>
        /// <returns>结束时间总秒数</returns>
        private double GetRangeEndTime()
        {
            int hour = (int)nudRangeEndHour.Value;
            int minute = (int)nudRangeEndMinute.Value;
            int second = (int)nudRangeEndSecond.Value;
            return TimeToSeconds(hour, minute, second);
        }

        /// <summary>
        /// 添加检测时间段按钮点击事件
        /// </summary>
        private void btnAddRange_Click(object sender, EventArgs e)
        {
            double rangeStart = GetRangeStartTime();
            double rangeEnd = GetRangeEndTime();

            if (rangeEnd <= rangeStart)
            {
                MessageBox.Show("时间段结束时间必须大于开始时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 创建时间范围对象
            TimeRange timeRange = new TimeRange(rangeStart, rangeEnd);
            _detectionTimeRanges.Add(timeRange);

            // 更新列表视图
            UpdateDetectionRangesListView();
        }

        /// <summary>
        /// 删除选中检测时间段按钮点击事件
        /// </summary>
        private void btnDeleteRange_Click(object sender, EventArgs e)
        {
            if (lvDetectionRanges.SelectedItems.Count > 0)
            {
                int selectedIndex = int.Parse(lvDetectionRanges.SelectedItems[0].SubItems[0].Text) - 1;
                _detectionTimeRanges.RemoveAt(selectedIndex);
                UpdateDetectionRangesListView();
            }
            else
            {
                MessageBox.Show("请先选择要删除的时间段", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 清空所有检测时间段按钮点击事件
        /// </summary>
        private void btnClearRanges_Click(object sender, EventArgs e)
        {
            if (_detectionTimeRanges.Count > 0)
            {
                DialogResult result = MessageBox.Show("确定要清空所有检测时间段吗？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    _detectionTimeRanges.Clear();
                    UpdateDetectionRangesListView();
                }
            }
        }

        /// <summary>
        /// 更新检测时间段列表视图
        /// </summary>
        private void UpdateDetectionRangesListView()
        {
            lvDetectionRanges.Items.Clear();

            for (int i = 0; i < _detectionTimeRanges.Count; i++)
            {
                TimeRange range = _detectionTimeRanges[i];
                ListViewItem item = new ListViewItem((i + 1).ToString());

                // 格式化开始时间
                string startTimeStr = "-";
                try
                {
                    if (!double.IsNaN(range.StartTimeSec) && !double.IsInfinity(range.StartTimeSec) && range.StartTimeSec >= 0)
                    {
                        startTimeStr = TimeSpan.FromSeconds(range.StartTimeSec).ToString(@"hh\:mm\:ss");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Warning(string.Format("格式化检测时间段开始时间失败：{0}秒，错误：{1}", range.StartTimeSec, ex.Message));
                }

                // 格式化结束时间
                string endTimeStr = "-";
                try
                {
                    if (!double.IsNaN(range.EndTimeSec) && !double.IsInfinity(range.EndTimeSec) && range.EndTimeSec >= 0)
                    {
                        endTimeStr = TimeSpan.FromSeconds(range.EndTimeSec).ToString(@"hh\:mm\:ss");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Warning(string.Format("格式化检测时间段结束时间失败：{0}秒，错误：{1}", range.EndTimeSec, ex.Message));
                }

                item.SubItems.Add(startTimeStr);
                item.SubItems.Add(endTimeStr);
                lvDetectionRanges.Items.Add(item);
            }
        }

        /// <summary>
        /// 测试合并功能按钮点击事件
        /// </summary>
        private void btnTestMerge_Click(object sender, EventArgs e)
        {
            // 打开合并测试GUI窗口
            MergeTestForm testForm = new MergeTestForm();
            testForm.ShowDialog();
        }
        
        /// <summary>
        /// 检测结果列表选中项变化事件
        /// </summary>
        private void lvAdSegments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvAdSegments.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvAdSegments.SelectedItems[0];
                AdSegment segment = selectedItem.Tag as AdSegment;
                if (segment != null)
                {
                    // 将广告片段信息填充到编辑控件中
                    if (!double.IsNaN(segment.StartTime) && !double.IsInfinity(segment.StartTime))
                    {
                        TimeSpan startTimeSpan = TimeSpan.FromSeconds(segment.StartTime);
                        nudEditStartHour.Value = startTimeSpan.Hours;
                        nudEditStartMinute.Value = startTimeSpan.Minutes;
                        nudEditStartSecond.Value = startTimeSpan.Seconds;
                    }
                    else
                    {
                        nudEditStartHour.Value = 0;
                        nudEditStartMinute.Value = 0;
                        nudEditStartSecond.Value = 0;
                    }
                    
                    if (!double.IsNaN(segment.EndTime) && !double.IsInfinity(segment.EndTime))
                    {
                        TimeSpan endTimeSpan = TimeSpan.FromSeconds(segment.EndTime);
                        nudEditEndHour.Value = endTimeSpan.Hours;
                        nudEditEndMinute.Value = endTimeSpan.Minutes;
                        nudEditEndSecond.Value = endTimeSpan.Seconds;
                    }
                    else
                    {
                        nudEditEndHour.Value = 0;
                        nudEditEndMinute.Value = 30;
                        nudEditEndSecond.Value = 0;
                    }
                    
                    txtEditNotes.Text = segment.Notes ?? string.Empty;
                }
            }
        }
        
        /// <summary>
        /// 添加广告片段按钮点击事件
        /// </summary>
        private void btnAddSegment_Click(object sender, EventArgs e)
        {
            // 创建新的广告片段
            AdSegment newSegment = new AdSegment();
            
            // 设置开始时间
            int startHour = (int)nudEditStartHour.Value;
            int startMinute = (int)nudEditStartMinute.Value;
            int startSecond = (int)nudEditStartSecond.Value;
            newSegment.StartTime = TimeToSeconds(startHour, startMinute, startSecond);
            
            // 设置结束时间
            int endHour = (int)nudEditEndHour.Value;
            int endMinute = (int)nudEditEndMinute.Value;
            int endSecond = (int)nudEditEndSecond.Value;
            newSegment.EndTime = TimeToSeconds(endHour, endMinute, endSecond);
            
            // 设置备注
            newSegment.Notes = txtEditNotes.Text;
            
            // 验证时间范围
            if (newSegment.StartTime >= newSegment.EndTime)
            {
                MessageBox.Show("结束时间必须大于开始时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // 添加到列表
            _detectedAdSegments.Add(newSegment);
            
            // 更新显示
            ShowDetectResults();
            
            // 清空编辑控件
            ClearEditControls();
            
            Logger.Info($"添加了新的广告片段：{newSegment.StartTime} - {newSegment.EndTime}秒");
        }
        
        /// <summary>
        /// 更新广告片段按钮点击事件
        /// </summary>
        private void btnUpdateSegment_Click(object sender, EventArgs e)
        {
            if (lvAdSegments.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择要更新的广告片段", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            ListViewItem selectedItem = lvAdSegments.SelectedItems[0];
            AdSegment segment = selectedItem.Tag as AdSegment;
            if (segment != null)
            {
                // 更新开始时间
                int startHour = (int)nudEditStartHour.Value;
                int startMinute = (int)nudEditStartMinute.Value;
                int startSecond = (int)nudEditStartSecond.Value;
                segment.StartTime = TimeToSeconds(startHour, startMinute, startSecond);
                
                // 更新结束时间
                int endHour = (int)nudEditEndHour.Value;
                int endMinute = (int)nudEditEndMinute.Value;
                int endSecond = (int)nudEditEndSecond.Value;
                segment.EndTime = TimeToSeconds(endHour, endMinute, endSecond);
                
                // 更新备注
                segment.Notes = txtEditNotes.Text;
                
                // 验证时间范围
                if (segment.StartTime >= segment.EndTime)
                {
                    MessageBox.Show("结束时间必须大于开始时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                // 更新显示
                ShowDetectResults();
                
                // 清空编辑控件
                ClearEditControls();
                
                Logger.Info($"更新了广告片段：{segment.StartTime} - {segment.EndTime}秒");
            }
        }
        
        /// <summary>
        /// 删除广告片段按钮点击事件
        /// </summary>
        private void btnDeleteSegment_Click(object sender, EventArgs e)
        {
            if (lvAdSegments.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择要删除的广告片段", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            DialogResult result = MessageBox.Show("确定要删除选中的广告片段吗？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                ListViewItem selectedItem = lvAdSegments.SelectedItems[0];
                AdSegment segment = selectedItem.Tag as AdSegment;
                if (segment != null)
                {
                    // 从列表中删除
                    _detectedAdSegments.Remove(segment);
                    
                    // 更新显示
                    ShowDetectResults();
                    
                    // 清空编辑控件
                    ClearEditControls();
                    
                    Logger.Info($"删除了广告片段：{segment.StartTime} - {segment.EndTime}秒");
                }
            }
        }
        
        /// <summary>
        /// 清空编辑控件按钮点击事件
        /// </summary>
        private void btnClearEdit_Click(object sender, EventArgs e)
        {
            ClearEditControls();
        }
        
        /// <summary>
        /// 清空编辑控件
        /// </summary>
        private void ClearEditControls()
        {
            // 清空时间控件
            nudEditStartHour.Value = 0;
            nudEditStartMinute.Value = 0;
            nudEditStartSecond.Value = 0;
            nudEditEndHour.Value = 0;
            nudEditEndMinute.Value = 30;
            nudEditEndSecond.Value = 0;
            
            // 清空备注
            txtEditNotes.Text = string.Empty;
            
            // 取消选中
            lvAdSegments.SelectedItems.Clear();
        }
    }
}