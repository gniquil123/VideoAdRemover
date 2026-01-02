namespace VideoAdRemover
{
    partial class MergeTestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPresetTests = new System.Windows.Forms.TabPage();
            this.btnRunAllTests = new System.Windows.Forms.Button();
            this.btnRunTest4 = new System.Windows.Forms.Button();
            this.btnRunTest3 = new System.Windows.Forms.Button();
            this.btnRunTest2 = new System.Windows.Forms.Button();
            this.btnRunTest1 = new System.Windows.Forms.Button();
            this.tabCustomTest = new System.Windows.Forms.TabPage();
            this.btnRemoveSegment = new System.Windows.Forms.Button();
            this.btnAddSegment = new System.Windows.Forms.Button();
            this.btnRunCustomTest = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.nudAdDuration = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.nudSegmentCount = new System.Windows.Forms.NumericUpDown();
            this.lvSegments = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabResults = new System.Windows.Forms.TabPage();
            this.btnClearResults = new System.Windows.Forms.Button();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPresetTests.SuspendLayout();
            this.tabCustomTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSegmentCount)).BeginInit();
            this.tabResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPresetTests);
            this.tabControl1.Controls.Add(this.tabCustomTest);
            this.tabControl1.Controls.Add(this.tabResults);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 600);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPresetTests
            // 
            this.tabPresetTests.Controls.Add(this.btnRunAllTests);
            this.tabPresetTests.Controls.Add(this.btnRunTest4);
            this.tabPresetTests.Controls.Add(this.btnRunTest3);
            this.tabPresetTests.Controls.Add(this.btnRunTest2);
            this.tabPresetTests.Controls.Add(this.btnRunTest1);
            this.tabPresetTests.Location = new System.Drawing.Point(4, 22);
            this.tabPresetTests.Name = "tabPresetTests";
            this.tabPresetTests.Padding = new System.Windows.Forms.Padding(3);
            this.tabPresetTests.Size = new System.Drawing.Size(792, 574);
            this.tabPresetTests.TabIndex = 0;
            this.tabPresetTests.Text = "预设测试用例";
            this.tabPresetTests.UseVisualStyleBackColor = true;
            // 
            // btnRunAllTests
            // 
            this.btnRunAllTests.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRunAllTests.Location = new System.Drawing.Point(20, 300);
            this.btnRunAllTests.Name = "btnRunAllTests";
            this.btnRunAllTests.Size = new System.Drawing.Size(300, 40);
            this.btnRunAllTests.TabIndex = 4;
            this.btnRunAllTests.Text = "运行所有测试用例";
            this.btnRunAllTests.UseVisualStyleBackColor = true;
            this.btnRunAllTests.Click += new System.EventHandler(this.btnRunAllTests_Click);
            // 
            // btnRunTest4
            // 
            this.btnRunTest4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRunTest4.Location = new System.Drawing.Point(20, 240);
            this.btnRunTest4.Name = "btnRunTest4";
            this.btnRunTest4.Size = new System.Drawing.Size(300, 40);
            this.btnRunTest4.TabIndex = 3;
            this.btnRunTest4.Text = "测试用例4：打乱顺序的情况";
            this.btnRunTest4.UseVisualStyleBackColor = true;
            this.btnRunTest4.Click += new System.EventHandler(this.btnRunTest4_Click);
            // 
            // btnRunTest3
            // 
            this.btnRunTest3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRunTest3.Location = new System.Drawing.Point(20, 180);
            this.btnRunTest3.Name = "btnRunTest3";
            this.btnRunTest3.Size = new System.Drawing.Size(300, 40);
            this.btnRunTest3.TabIndex = 2;
            this.btnRunTest3.Text = "测试用例3：只有结束时间的情况";
            this.btnRunTest3.UseVisualStyleBackColor = true;
            this.btnRunTest3.Click += new System.EventHandler(this.btnRunTest3_Click);
            // 
            // btnRunTest2
            // 
            this.btnRunTest2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRunTest2.Location = new System.Drawing.Point(20, 120);
            this.btnRunTest2.Name = "btnRunTest2";
            this.btnRunTest2.Size = new System.Drawing.Size(300, 40);
            this.btnRunTest2.TabIndex = 1;
            this.btnRunTest2.Text = "测试用例2：只有开始时间的情况";
            this.btnRunTest2.UseVisualStyleBackColor = true;
            this.btnRunTest2.Click += new System.EventHandler(this.btnRunTest2_Click);
            // 
            // btnRunTest1
            // 
            this.btnRunTest1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRunTest1.Location = new System.Drawing.Point(20, 60);
            this.btnRunTest1.Name = "btnRunTest1";
            this.btnRunTest1.Size = new System.Drawing.Size(300, 40);
            this.btnRunTest1.TabIndex = 0;
            this.btnRunTest1.Text = "测试用例1：用户描述的6条结果";
            this.btnRunTest1.UseVisualStyleBackColor = true;
            this.btnRunTest1.Click += new System.EventHandler(this.btnRunTest1_Click);
            // 
            // tabCustomTest
            // 
            this.tabCustomTest.Controls.Add(this.btnRemoveSegment);
            this.tabCustomTest.Controls.Add(this.btnAddSegment);
            this.tabCustomTest.Controls.Add(this.btnRunCustomTest);
            this.tabCustomTest.Controls.Add(this.label2);
            this.tabCustomTest.Controls.Add(this.nudAdDuration);
            this.tabCustomTest.Controls.Add(this.label1);
            this.tabCustomTest.Controls.Add(this.nudSegmentCount);
            this.tabCustomTest.Controls.Add(this.lvSegments);
            this.tabCustomTest.Location = new System.Drawing.Point(4, 22);
            this.tabCustomTest.Name = "tabCustomTest";
            this.tabCustomTest.Padding = new System.Windows.Forms.Padding(3);
            this.tabCustomTest.Size = new System.Drawing.Size(792, 574);
            this.tabCustomTest.TabIndex = 1;
            this.tabCustomTest.Text = "自定义测试";
            this.tabCustomTest.UseVisualStyleBackColor = true;
            // 
            // btnRemoveSegment
            // 
            this.btnRemoveSegment.Location = new System.Drawing.Point(200, 20);
            this.btnRemoveSegment.Name = "btnRemoveSegment";
            this.btnRemoveSegment.Size = new System.Drawing.Size(30, 30);
            this.btnRemoveSegment.TabIndex = 7;
            this.btnRemoveSegment.Text = "-";
            this.btnRemoveSegment.UseVisualStyleBackColor = true;
            this.btnRemoveSegment.Click += new System.EventHandler(this.btnRemoveSegment_Click);
            // 
            // btnAddSegment
            // 
            this.btnAddSegment.Location = new System.Drawing.Point(240, 20);
            this.btnAddSegment.Name = "btnAddSegment";
            this.btnAddSegment.Size = new System.Drawing.Size(30, 30);
            this.btnAddSegment.TabIndex = 6;
            this.btnAddSegment.Text = "+";
            this.btnAddSegment.UseVisualStyleBackColor = true;
            this.btnAddSegment.Click += new System.EventHandler(this.btnAddSegment_Click);
            // 
            // btnRunCustomTest
            // 
            this.btnRunCustomTest.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnRunCustomTest.Location = new System.Drawing.Point(20, 490);
            this.btnRunCustomTest.Name = "btnRunCustomTest";
            this.btnRunCustomTest.Size = new System.Drawing.Size(300, 40);
            this.btnRunCustomTest.TabIndex = 5;
            this.btnRunCustomTest.Text = "运行自定义测试";
            this.btnRunCustomTest.UseVisualStyleBackColor = true;
            this.btnRunCustomTest.Click += new System.EventHandler(this.btnRunCustomTest_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "广告时长：";
            // 
            // nudAdDuration
            // 
            this.nudAdDuration.DecimalPlaces = 1;
            this.nudAdDuration.Location = new System.Drawing.Point(88, 58);
            this.nudAdDuration.Maximum = new decimal(new int[] { 600, 0, 0, 0 });
            this.nudAdDuration.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudAdDuration.Name = "nudAdDuration";
            this.nudAdDuration.Size = new System.Drawing.Size(142, 21);
            this.nudAdDuration.TabIndex = 3;
            this.nudAdDuration.Value = new decimal(new int[] { 15, 0, 0, 0 });
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "片段数量：";
            // 
            // nudSegmentCount
            // 
            this.nudSegmentCount.Location = new System.Drawing.Point(88, 33);
            this.nudSegmentCount.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            this.nudSegmentCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudSegmentCount.Name = "nudSegmentCount";
            this.nudSegmentCount.Size = new System.Drawing.Size(106, 21);
            this.nudSegmentCount.TabIndex = 1;
            this.nudSegmentCount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudSegmentCount.ValueChanged += new System.EventHandler(this.nudSegmentCount_ValueChanged);
            // 
            // lvSegments
            // 
            this.lvSegments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lvSegments.FullRowSelect = true;
            this.lvSegments.GridLines = true;
            this.lvSegments.HideSelection = false;
            this.lvSegments.Location = new System.Drawing.Point(20, 90);
            this.lvSegments.Name = "lvSegments";
            this.lvSegments.Size = new System.Drawing.Size(750, 380);
            this.lvSegments.TabIndex = 0;
            this.lvSegments.UseCompatibleStateImageBehavior = false;
            this.lvSegments.View = System.Windows.Forms.View.Details;
            this.lvSegments.DoubleClick += new System.EventHandler(this.lvSegments_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "序号";
            this.columnHeader1.Width = 60;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "开始时间（秒）";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "结束时间（秒）";
            this.columnHeader3.Width = 150;
            // 
            // tabResults
            // 
            this.tabResults.Controls.Add(this.btnClearResults);
            this.tabResults.Controls.Add(this.txtResults);
            this.tabResults.Location = new System.Drawing.Point(4, 22);
            this.tabResults.Name = "tabResults";
            this.tabResults.Padding = new System.Windows.Forms.Padding(3);
            this.tabResults.Size = new System.Drawing.Size(792, 574);
            this.tabResults.TabIndex = 2;
            this.tabResults.Text = "测试结果";
            this.tabResults.UseVisualStyleBackColor = true;
            // 
            // btnClearResults
            // 
            this.btnClearResults.Location = new System.Drawing.Point(690, 530);
            this.btnClearResults.Name = "btnClearResults";
            this.btnClearResults.Size = new System.Drawing.Size(80, 30);
            this.btnClearResults.TabIndex = 1;
            this.btnClearResults.Text = "清空结果";
            this.btnClearResults.UseVisualStyleBackColor = true;
            this.btnClearResults.Click += new System.EventHandler(this.btnClearResults_Click);
            // 
            // txtResults
            // 
            this.txtResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResults.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtResults.Location = new System.Drawing.Point(3, 3);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResults.Size = new System.Drawing.Size(786, 568);
            this.txtResults.TabIndex = 0;
            this.txtResults.WordWrap = false;
            // 
            // MergeTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.tabControl1);
            this.Name = "MergeTestForm";
            this.Text = "广告片段合并测试工具";
            this.tabControl1.ResumeLayout(false);
            this.tabPresetTests.ResumeLayout(false);
            this.tabCustomTest.ResumeLayout(false);
            this.tabCustomTest.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSegmentCount)).EndInit();
            this.tabResults.ResumeLayout(false);
            this.tabResults.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPresetTests;
        private System.Windows.Forms.TabPage tabCustomTest;
        private System.Windows.Forms.TabPage tabResults;
        private System.Windows.Forms.Button btnRunTest1;
        private System.Windows.Forms.Button btnRunTest4;
        private System.Windows.Forms.Button btnRunTest3;
        private System.Windows.Forms.Button btnRunTest2;
        private System.Windows.Forms.Button btnRunAllTests;
        private System.Windows.Forms.ListView lvSegments;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.NumericUpDown nudSegmentCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudAdDuration;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRunCustomTest;
        private System.Windows.Forms.Button btnRemoveSegment;
        private System.Windows.Forms.Button btnAddSegment;
        private System.Windows.Forms.Button btnClearResults;
        private System.Windows.Forms.TextBox txtResults;
    }
    
    partial class SegmentEditForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStartTime = new System.Windows.Forms.TextBox();
            this.txtEndTime = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(100, 120);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(80, 30);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(200, 120);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "开始时间（秒）:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "结束时间（秒）:";
            // 
            // txtStartTime
            // 
            this.txtStartTime.Location = new System.Drawing.Point(120, 25);
            this.txtStartTime.Name = "txtStartTime";
            this.txtStartTime.Size = new System.Drawing.Size(160, 21);
            this.txtStartTime.TabIndex = 0;
            // 
            // txtEndTime
            // 
            this.txtEndTime.Location = new System.Drawing.Point(120, 65);
            this.txtEndTime.Name = "txtEndTime";
            this.txtEndTime.Size = new System.Drawing.Size(160, 21);
            this.txtEndTime.TabIndex = 1;
            // 
            // SegmentEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 170);
            this.Controls.Add(this.txtEndTime);
            this.Controls.Add(this.txtStartTime);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "SegmentEditForm";
            this.Text = "编辑广告片段";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStartTime;
        private System.Windows.Forms.TextBox txtEndTime;
    }
}