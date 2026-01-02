using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VideoAdRemover
{
    /// <summary>
    /// 广告片段合并GUI测试工具
    /// </summary>
    public partial class MergeTestForm : Form
    {
        private AdDetector detector;
        private List<AdSegment> customSegments;
        
        /// <summary>
        /// 初始化合并测试窗体
        /// </summary>
        public MergeTestForm()
        {
            InitializeComponent();
            detector = new AdDetector(false);
            customSegments = new List<AdSegment>();
            InitializeCustomTestTab();
        }
        
        /// <summary>
        /// 初始化自定义测试选项卡
        /// </summary>
        private void InitializeCustomTestTab()
        {
            nudSegmentCount.Value = 1;
            nudAdDuration.Value = 15;
            UpdateCustomSegmentsList();
        }
        
        /// <summary>
        /// 运行预设测试用例1
        /// </summary>
        private void btnRunTest1_Click(object sender, EventArgs e)
        {
            // 测试用例1：用户描述的场景 - 6个结果合并为1个
            List<AdSegment> test1 = new List<AdSegment>
            {
                new AdSegment { StartTime = 478, EndTime = double.NaN }, // 00:07:58
                new AdSegment { StartTime = 479, EndTime = double.NaN }, // 00:07:59
                new AdSegment { StartTime = 480, EndTime = double.NaN }, // 00:08:00
                new AdSegment { StartTime = double.NaN, EndTime = 491 }, // 00:08:11
                new AdSegment { StartTime = double.NaN, EndTime = 492 }, // 00:08:12
                new AdSegment { StartTime = double.NaN, EndTime = 493 }  // 00:08:13
            };
            RunTest(test1, 15.0, "测试用例1：用户描述的6条结果");
        }
        
        /// <summary>
        /// 运行预设测试用例2
        /// </summary>
        private void btnRunTest2_Click(object sender, EventArgs e)
        {
            // 测试用例2：只有开始时间的情况
            List<AdSegment> test2 = new List<AdSegment>
            {
                new AdSegment { StartTime = 100, EndTime = double.NaN },
                new AdSegment { StartTime = 101, EndTime = double.NaN },
                new AdSegment { StartTime = 102, EndTime = double.NaN }
            };
            RunTest(test2, 10.0, "测试用例2：只有开始时间的情况");
        }
        
        /// <summary>
        /// 运行预设测试用例3
        /// </summary>
        private void btnRunTest3_Click(object sender, EventArgs e)
        {
            // 测试用例3：只有结束时间的情况
            List<AdSegment> test3 = new List<AdSegment>
            {
                new AdSegment { StartTime = double.NaN, EndTime = 200 },
                new AdSegment { StartTime = double.NaN, EndTime = 201 },
                new AdSegment { StartTime = double.NaN, EndTime = 202 }
            };
            RunTest(test3, 10.0, "测试用例3：只有结束时间的情况");
        }
        
        /// <summary>
        /// 运行预设测试用例4
        /// </summary>
        private void btnRunTest4_Click(object sender, EventArgs e)
        {
            // 测试用例4：打乱顺序的情况
            List<AdSegment> test4 = new List<AdSegment>
            {
                new AdSegment { StartTime = double.NaN, EndTime = 493 },  // 00:08:13 (放在前面)
                new AdSegment { StartTime = 478, EndTime = double.NaN }, // 00:07:58
                new AdSegment { StartTime = double.NaN, EndTime = 491 }, // 00:08:11
                new AdSegment { StartTime = 480, EndTime = double.NaN }, // 00:08:00
                new AdSegment { StartTime = double.NaN, EndTime = 492 }, // 00:08:12
                new AdSegment { StartTime = 479, EndTime = double.NaN }  // 00:07:59 (放在最后)
            };
            RunTest(test4, 15.0, "测试用例4：打乱顺序的情况");
        }
        
        /// <summary>
        /// 运行所有预设测试用例
        /// </summary>
        private void btnRunAllTests_Click(object sender, EventArgs e)
        {
            btnRunTest1_Click(sender, e);
            btnRunTest2_Click(sender, e);
            btnRunTest3_Click(sender, e);
            btnRunTest4_Click(sender, e);
        }
        
        /// <summary>
        /// 运行自定义测试
        /// </summary>
        private void btnRunCustomTest_Click(object sender, EventArgs e)
        {
            double adDuration = (double)nudAdDuration.Value;
            RunTest(customSegments, adDuration, "自定义测试");
        }
        
        /// <summary>
        /// 更新自定义片段列表
        /// </summary>
        private void UpdateCustomSegmentsList()
        {
            lvSegments.Items.Clear();
            
            for (int i = 0; i < customSegments.Count; i++)
            {
                var seg = customSegments[i];
                string startTimeStr = double.IsNaN(seg.StartTime) ? "-" : seg.StartTime.ToString("F1");
                string endTimeStr = double.IsNaN(seg.EndTime) ? "-" : seg.EndTime.ToString("F1");
                
                ListViewItem item = new ListViewItem((i + 1).ToString());
                item.SubItems.Add(startTimeStr);
                item.SubItems.Add(endTimeStr);
                lvSegments.Items.Add(item);
            }
        }
        
        /// <summary>
        /// 片段数量变化时更新自定义片段列表
        /// </summary>
        private void nudSegmentCount_ValueChanged(object sender, EventArgs e)
        {
            int count = (int)nudSegmentCount.Value;
            while (customSegments.Count < count)
            {
                customSegments.Add(new AdSegment());
            }
            while (customSegments.Count > count)
            {
                customSegments.RemoveAt(customSegments.Count - 1);
            }
            UpdateCustomSegmentsList();
        }
        
        /// <summary>
        /// 执行合并测试
        /// </summary>
        /// <param name="segments">要合并的广告片段列表</param>
        /// <param name="adDurationSec">广告时长（秒）</param>
        /// <param name="testName">测试名称</param>
        private void RunTest(List<AdSegment> segments, double adDurationSec, string testName)
        {
            // 显示测试名称
            txtResults.AppendText($"\n=== {testName} ===\n");
            txtResults.AppendText($"广告时长：{adDurationSec}秒\n");
            txtResults.AppendText("合并前的片段：\n");
            
            // 显示合并前的片段，包括序号
            for (int i = 0; i < segments.Count; i++)
            {
                var seg = segments[i];
                string startTimeStr = double.IsNaN(seg.StartTime) ? "-" : TimeSpan.FromSeconds(seg.StartTime).ToString(@"hh\:mm\:ss");
                string endTimeStr = double.IsNaN(seg.EndTime) ? "-" : TimeSpan.FromSeconds(seg.EndTime).ToString(@"hh\:mm\:ss");
                txtResults.AppendText($"  [{i+1}] 开始时间：{startTimeStr}，结束时间：{endTimeStr}\n");
            }
            
            // 执行合并
            List<AdSegment> merged = detector.MergeOverlappingSegments(segments, adDurationSec);
            
            // 打印合并结果
            txtResults.AppendText("\n合并后的片段：\n");
            for (int i = 0; i < merged.Count; i++)
            {
                var seg = merged[i];
                string startTimeStr = double.IsNaN(seg.StartTime) ? "-" : TimeSpan.FromSeconds(seg.StartTime).ToString(@"hh\:mm\:ss");
                string endTimeStr = double.IsNaN(seg.EndTime) ? "-" : TimeSpan.FromSeconds(seg.EndTime).ToString(@"hh\:mm\:ss");
                txtResults.AppendText($"  [{i+1}] 开始时间：{startTimeStr}，结束时间：{endTimeStr}，备注：{seg.Notes}\n");
            }
            
            txtResults.AppendText($"\n合并结果：{segments.Count}个片段 → {merged.Count}个片段\n");
            
            // 验证是否合并成功
            if (segments.Count > 1 && merged.Count == 1 && 
                !double.IsNaN(merged[0].StartTime) && !double.IsNaN(merged[0].EndTime))
            {
                txtResults.AppendText("✅ 合并成功！多个片段已合并为一个完整的广告段\n");
            }
            else if (segments.Count == merged.Count)
            {
                txtResults.AppendText("ℹ️  未合并任何片段，所有片段都是独立的\n");
            }
            else
            {
                txtResults.AppendText("✅ 部分合并成功\n");
            }
            
            txtResults.AppendText("\n----------------------------------------\n");
            txtResults.ScrollToCaret();
        }
        
        /// <summary>
        /// 清空结果文本框
        /// </summary>
        private void btnClearResults_Click(object sender, EventArgs e)
        {
            txtResults.Clear();
        }
        
        /// <summary>
        /// 双击列表项编辑片段
        /// </summary>
        private void lvSegments_DoubleClick(object sender, EventArgs e)
        {
            if (lvSegments.SelectedItems.Count > 0)
            {
                int index = lvSegments.SelectedItems[0].Index;
                var seg = customSegments[index];
                
                using (var editForm = new SegmentEditForm(seg))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        customSegments[index] = editForm.Segment;
                        UpdateCustomSegmentsList();
                    }
                }
            }
        }
        
        /// <summary>
        /// 添加片段按钮点击事件
        /// </summary>
        private void btnAddSegment_Click(object sender, EventArgs e)
        {
            nudSegmentCount.Value++;
        }
        
        /// <summary>
        /// 删除片段按钮点击事件
        /// </summary>
        private void btnRemoveSegment_Click(object sender, EventArgs e)
        {
            if (nudSegmentCount.Value > 1)
            {
                nudSegmentCount.Value--;
            }
        }
    }
    
    /// <summary>
    /// 片段编辑窗体
    /// </summary>
    public partial class SegmentEditForm : Form
    {
        /// <summary>
        /// 获取或设置要编辑的片段
        /// </summary>
        public AdSegment Segment { get; set; }
        
        /// <summary>
        /// 初始化片段编辑窗体
        /// </summary>
        /// <param name="segment">要编辑的片段</param>
        public SegmentEditForm(AdSegment segment)
        {
            InitializeComponent();
            Segment = segment;
            
            // 初始化控件值
            if (!double.IsNaN(segment.StartTime))
            {
                txtStartTime.Text = segment.StartTime.ToString("F1");
            }
            if (!double.IsNaN(segment.EndTime))
            {
                txtEndTime.Text = segment.EndTime.ToString("F1");
            }
        }
        
        /// <summary>
        /// 确定按钮点击事件
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e)
        {
            // 验证并保存输入
            if (double.TryParse(txtStartTime.Text, out double startTime))
            {
                Segment.StartTime = startTime;
            }
            else
            {
                Segment.StartTime = double.NaN;
            }
            
            if (double.TryParse(txtEndTime.Text, out double endTime))
            {
                Segment.EndTime = endTime;
            }
            else
            {
                Segment.EndTime = double.NaN;
            }
            
            DialogResult = DialogResult.OK;
            Close();
        }
        
        /// <summary>
        /// 取消按钮点击事件
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}