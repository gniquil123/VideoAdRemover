namespace VideoAdRemover;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        lblTitle = new Label();
        grpMovieFile = new GroupBox();
        txtMoviePath = new TextBox();
        btnBrowseMovie = new Button();
        grpAdTime = new GroupBox();
        lblScanInterval = new Label();
        nudScanInterval = new NumericUpDown();
        labelEndSecond = new Label();
        nudAdEndSecond = new NumericUpDown();
        labelEndMinute = new Label();
        nudAdEndMinute = new NumericUpDown();
        labelEndHour = new Label();
        nudAdEndHour = new NumericUpDown();
        labelStartSecond = new Label();
        nudAdStartSecond = new NumericUpDown();
        labelStartMinute = new Label();
        nudAdStartMinute = new NumericUpDown();
        labelStartHour = new Label();
        nudAdStartHour = new NumericUpDown();
        label1 = new Label();
        grpDetectResult = new GroupBox();
        lblEditStartTime = new Label();
        nudEditStartHour = new NumericUpDown();
        nudEditStartMinute = new NumericUpDown();
        nudEditStartSecond = new NumericUpDown();
        lblEditStartHour = new Label();
        lblEditStartMinute = new Label();
        lblEditStartSecond = new Label();
        lblEditEndTime = new Label();
        nudEditEndHour = new NumericUpDown();
        nudEditEndMinute = new NumericUpDown();
        nudEditEndSecond = new NumericUpDown();
        lblEditEndHour = new Label();
        lblEditEndMinute = new Label();
        lblEditEndSecond = new Label();
        txtEditNotes = new TextBox();
        lblEditNotes = new Label();
        btnAddSegment = new Button();
        btnUpdateSegment = new Button();
        btnDeleteSegment = new Button();
        btnClearEdit = new Button();
        lvAdSegments = new ListView();
        columnHeader1 = new ColumnHeader();
        columnHeader2 = new ColumnHeader();
        columnHeader3 = new ColumnHeader();
        columnHeader4 = new ColumnHeader();
        grpActions = new GroupBox();
        btnDetectAds = new Button();
        btnStopDetect = new Button();
        btnRemoveAds = new Button();
        btnTestMerge = new Button();
        grpProgress = new GroupBox();
        lblProgressText = new Label();
        pbProgress = new ProgressBar();
        openFileDialog1 = new OpenFileDialog();
        chkDarkTheme = new CheckBox();
        grpImageCompare = new GroupBox();
        lblCompareResult = new Label();
        btnCompareImages = new Button();
        btnBrowseImage2 = new Button();
        txtImagePath2 = new TextBox();
        btnBrowseImage1 = new Button();
        txtImagePath1 = new TextBox();
        lblImage1 = new Label();
        lblImage2 = new Label();
        openFileDialog2 = new OpenFileDialog();
        grpDetectionRanges = new GroupBox();
        lblRangeEndSecond = new Label();
        nudRangeEndSecond = new NumericUpDown();
        lblRangeEndMinute = new Label();
        nudRangeEndMinute = new NumericUpDown();
        lblRangeEndHour = new Label();
        nudRangeEndHour = new NumericUpDown();
        lblRangeStartSecond = new Label();
        nudRangeStartSecond = new NumericUpDown();
        lblRangeStartMinute = new Label();
        nudRangeStartMinute = new NumericUpDown();
        lblRangeStartHour = new Label();
        nudRangeStartHour = new NumericUpDown();
        lblRangeTimeLabel = new Label();
        btnClearRanges = new Button();
        btnDeleteRange = new Button();
        btnAddRange = new Button();
        lvDetectionRanges = new ListView();
        columnHeaderRangeIndex = new ColumnHeader();
        columnHeaderRangeStart = new ColumnHeader();
        columnHeaderRangeEnd = new ColumnHeader();
        grpMovieFile.SuspendLayout();
        grpAdTime.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudScanInterval).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudAdEndSecond).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudAdEndMinute).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudAdEndHour).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudAdStartSecond).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudAdStartMinute).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudAdStartHour).BeginInit();
        grpDetectResult.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudEditStartHour).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudEditStartMinute).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudEditStartSecond).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudEditEndHour).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudEditEndMinute).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudEditEndSecond).BeginInit();
        grpActions.SuspendLayout();
        grpProgress.SuspendLayout();
        grpImageCompare.SuspendLayout();
        grpDetectionRanges.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudRangeEndSecond).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudRangeEndMinute).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudRangeEndHour).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudRangeStartSecond).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudRangeStartMinute).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nudRangeStartHour).BeginInit();
        SuspendLayout();
        // 
        // lblTitle
        // 
        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Microsoft YaHei UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
        lblTitle.Location = new Point(15, 11);
        lblTitle.Margin = new Padding(4, 0, 4, 0);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(283, 44);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "视频广告移除工具";
        // 
        // grpMovieFile
        // 
        grpMovieFile.Controls.Add(txtMoviePath);
        grpMovieFile.Controls.Add(btnBrowseMovie);
        grpMovieFile.Location = new Point(22, 68);
        grpMovieFile.Margin = new Padding(4);
        grpMovieFile.Name = "grpMovieFile";
        grpMovieFile.Padding = new Padding(4);
        grpMovieFile.Size = new Size(948, 91);
        grpMovieFile.TabIndex = 1;
        grpMovieFile.TabStop = false;
        grpMovieFile.Text = "电影文件选择";
        // 
        // txtMoviePath
        // 
        txtMoviePath.Location = new Point(7, 31);
        txtMoviePath.Margin = new Padding(4);
        txtMoviePath.Name = "txtMoviePath";
        txtMoviePath.Size = new Size(763, 30);
        txtMoviePath.TabIndex = 2;
        // 
        // btnBrowseMovie
        // 
        btnBrowseMovie.Location = new Point(779, 28);
        btnBrowseMovie.Margin = new Padding(4);
        btnBrowseMovie.Name = "btnBrowseMovie";
        btnBrowseMovie.Size = new Size(84, 41);
        btnBrowseMovie.TabIndex = 1;
        btnBrowseMovie.Text = "浏览";
        btnBrowseMovie.UseVisualStyleBackColor = true;
        btnBrowseMovie.Click += btnBrowseMovie_Click;
        // 
        // grpAdTime
        // 
        grpAdTime.Controls.Add(lblScanInterval);
        grpAdTime.Controls.Add(nudScanInterval);
        grpAdTime.Controls.Add(labelEndSecond);
        grpAdTime.Controls.Add(nudAdEndSecond);
        grpAdTime.Controls.Add(labelEndMinute);
        grpAdTime.Controls.Add(nudAdEndMinute);
        grpAdTime.Controls.Add(labelEndHour);
        grpAdTime.Controls.Add(nudAdEndHour);
        grpAdTime.Controls.Add(labelStartSecond);
        grpAdTime.Controls.Add(nudAdStartSecond);
        grpAdTime.Controls.Add(labelStartMinute);
        grpAdTime.Controls.Add(nudAdStartMinute);
        grpAdTime.Controls.Add(labelStartHour);
        grpAdTime.Controls.Add(nudAdStartHour);
        grpAdTime.Controls.Add(label1);
        grpAdTime.Location = new Point(22, 167);
        grpAdTime.Margin = new Padding(4);
        grpAdTime.Name = "grpAdTime";
        grpAdTime.Padding = new Padding(4);
        grpAdTime.Size = new Size(948, 138);
        grpAdTime.TabIndex = 2;
        grpAdTime.TabStop = false;
        grpAdTime.Text = "广告时间设置";
        // 
        // lblScanInterval
        // 
        lblScanInterval.AutoSize = true;
        lblScanInterval.Location = new Point(649, 99);
        lblScanInterval.Margin = new Padding(4, 0, 4, 0);
        lblScanInterval.Name = "lblScanInterval";
        lblScanInterval.Size = new Size(140, 24);
        lblScanInterval.TabIndex = 8;
        lblScanInterval.Text = "扫描间隔（秒）:";
        // 
        // nudScanInterval
        // 
        nudScanInterval.DecimalPlaces = 1;
        nudScanInterval.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
        nudScanInterval.Location = new Point(797, 97);
        nudScanInterval.Margin = new Padding(4);
        nudScanInterval.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
        nudScanInterval.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
        nudScanInterval.Name = "nudScanInterval";
        nudScanInterval.Size = new Size(91, 30);
        nudScanInterval.TabIndex = 9;
        nudScanInterval.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // labelEndSecond
        // 
        labelEndSecond.AutoSize = true;
        labelEndSecond.Location = new Point(361, 85);
        labelEndSecond.Margin = new Padding(4, 0, 4, 0);
        labelEndSecond.Name = "labelEndSecond";
        labelEndSecond.Size = new Size(28, 24);
        labelEndSecond.TabIndex = 19;
        labelEndSecond.Text = "秒";
        // 
        // nudAdEndSecond
        // 
        nudAdEndSecond.Location = new Point(293, 82);
        nudAdEndSecond.Margin = new Padding(4);
        nudAdEndSecond.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
        nudAdEndSecond.Name = "nudAdEndSecond";
        nudAdEndSecond.Size = new Size(61, 30);
        nudAdEndSecond.TabIndex = 18;
        nudAdEndSecond.Value = new decimal(new int[] { 30, 0, 0, 0 });
        // 
        // labelEndMinute
        // 
        labelEndMinute.AutoSize = true;
        labelEndMinute.Location = new Point(262, 85);
        labelEndMinute.Margin = new Padding(4, 0, 4, 0);
        labelEndMinute.Name = "labelEndMinute";
        labelEndMinute.Size = new Size(28, 24);
        labelEndMinute.TabIndex = 17;
        labelEndMinute.Text = "分";
        // 
        // nudAdEndMinute
        // 
        nudAdEndMinute.Location = new Point(194, 82);
        nudAdEndMinute.Margin = new Padding(4);
        nudAdEndMinute.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
        nudAdEndMinute.Name = "nudAdEndMinute";
        nudAdEndMinute.Size = new Size(61, 30);
        nudAdEndMinute.TabIndex = 16;
        // 
        // labelEndHour
        // 
        labelEndHour.AutoSize = true;
        labelEndHour.Location = new Point(163, 85);
        labelEndHour.Margin = new Padding(4, 0, 4, 0);
        labelEndHour.Name = "labelEndHour";
        labelEndHour.Size = new Size(28, 24);
        labelEndHour.TabIndex = 15;
        labelEndHour.Text = "时";
        // 
        // nudAdEndHour
        // 
        nudAdEndHour.Location = new Point(95, 82);
        nudAdEndHour.Margin = new Padding(4);
        nudAdEndHour.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
        nudAdEndHour.Name = "nudAdEndHour";
        nudAdEndHour.Size = new Size(61, 30);
        nudAdEndHour.TabIndex = 14;
        // 
        // labelStartSecond
        // 
        labelStartSecond.AutoSize = true;
        labelStartSecond.Location = new Point(362, 47);
        labelStartSecond.Margin = new Padding(4, 0, 4, 0);
        labelStartSecond.Name = "labelStartSecond";
        labelStartSecond.Size = new Size(28, 24);
        labelStartSecond.TabIndex = 13;
        labelStartSecond.Text = "秒";
        // 
        // nudAdStartSecond
        // 
        nudAdStartSecond.Location = new Point(293, 44);
        nudAdStartSecond.Margin = new Padding(4);
        nudAdStartSecond.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
        nudAdStartSecond.Name = "nudAdStartSecond";
        nudAdStartSecond.Size = new Size(61, 30);
        nudAdStartSecond.TabIndex = 12;
        // 
        // labelStartMinute
        // 
        labelStartMinute.AutoSize = true;
        labelStartMinute.Location = new Point(263, 47);
        labelStartMinute.Margin = new Padding(4, 0, 4, 0);
        labelStartMinute.Name = "labelStartMinute";
        labelStartMinute.Size = new Size(28, 24);
        labelStartMinute.TabIndex = 11;
        labelStartMinute.Text = "分";
        // 
        // nudAdStartMinute
        // 
        nudAdStartMinute.Location = new Point(194, 44);
        nudAdStartMinute.Margin = new Padding(4);
        nudAdStartMinute.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
        nudAdStartMinute.Name = "nudAdStartMinute";
        nudAdStartMinute.Size = new Size(61, 30);
        nudAdStartMinute.TabIndex = 10;
        // 
        // labelStartHour
        // 
        labelStartHour.AutoSize = true;
        labelStartHour.Location = new Point(164, 47);
        labelStartHour.Margin = new Padding(4, 0, 4, 0);
        labelStartHour.Name = "labelStartHour";
        labelStartHour.Size = new Size(28, 24);
        labelStartHour.TabIndex = 9;
        labelStartHour.Text = "时";
        // 
        // nudAdStartHour
        // 
        nudAdStartHour.Location = new Point(95, 44);
        nudAdStartHour.Margin = new Padding(4);
        nudAdStartHour.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
        nudAdStartHour.Name = "nudAdStartHour";
        nudAdStartHour.Size = new Size(61, 30);
        nudAdStartHour.TabIndex = 1;
        // 
        // label1
        // 
        label1.AutoSize = true;
        label1.Location = new Point(7, 47);
        label1.Margin = new Padding(4, 0, 4, 0);
        label1.Name = "label1";
        label1.Size = new Size(86, 24);
        label1.TabIndex = 0;
        label1.Text = "广告时间:";
        // 
        // grpDetectResult
        // 
        grpDetectResult.Controls.Add(lblEditStartTime);
        grpDetectResult.Controls.Add(nudEditStartHour);
        grpDetectResult.Controls.Add(nudEditStartMinute);
        grpDetectResult.Controls.Add(nudEditStartSecond);
        grpDetectResult.Controls.Add(lblEditStartHour);
        grpDetectResult.Controls.Add(lblEditStartMinute);
        grpDetectResult.Controls.Add(lblEditStartSecond);
        grpDetectResult.Controls.Add(lblEditEndTime);
        grpDetectResult.Controls.Add(nudEditEndHour);
        grpDetectResult.Controls.Add(nudEditEndMinute);
        grpDetectResult.Controls.Add(nudEditEndSecond);
        grpDetectResult.Controls.Add(lblEditEndHour);
        grpDetectResult.Controls.Add(lblEditEndMinute);
        grpDetectResult.Controls.Add(lblEditEndSecond);
        grpDetectResult.Controls.Add(txtEditNotes);
        grpDetectResult.Controls.Add(lblEditNotes);
        grpDetectResult.Controls.Add(btnAddSegment);
        grpDetectResult.Controls.Add(btnUpdateSegment);
        grpDetectResult.Controls.Add(btnDeleteSegment);
        grpDetectResult.Controls.Add(btnClearEdit);
        grpDetectResult.Controls.Add(lvAdSegments);
        grpDetectResult.Location = new Point(22, 691);
        grpDetectResult.Margin = new Padding(4);
        grpDetectResult.Name = "grpDetectResult";
        grpDetectResult.Padding = new Padding(4);
        grpDetectResult.Size = new Size(948, 250);
        grpDetectResult.TabIndex = 3;
        grpDetectResult.TabStop = false;
        grpDetectResult.Text = "检测结果";
        // 
        // lblEditStartTime
        // 
        lblEditStartTime.AutoSize = true;
        lblEditStartTime.Location = new Point(7, 167);
        lblEditStartTime.Margin = new Padding(4, 0, 4, 0);
        lblEditStartTime.Name = "lblEditStartTime";
        lblEditStartTime.Size = new Size(86, 24);
        lblEditStartTime.TabIndex = 1;
        lblEditStartTime.Text = "开始时间:";
        // 
        // nudEditStartHour
        // 
        nudEditStartHour.Location = new Point(95, 164);
        nudEditStartHour.Margin = new Padding(4);
        nudEditStartHour.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
        nudEditStartHour.Name = "nudEditStartHour";
        nudEditStartHour.Size = new Size(61, 30);
        nudEditStartHour.TabIndex = 2;
        // 
        // nudEditStartMinute
        // 
        nudEditStartMinute.Location = new Point(194, 164);
        nudEditStartMinute.Margin = new Padding(4);
        nudEditStartMinute.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
        nudEditStartMinute.Name = "nudEditStartMinute";
        nudEditStartMinute.Size = new Size(61, 30);
        nudEditStartMinute.TabIndex = 4;
        // 
        // nudEditStartSecond
        // 
        nudEditStartSecond.Location = new Point(293, 164);
        nudEditStartSecond.Margin = new Padding(4);
        nudEditStartSecond.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
        nudEditStartSecond.Name = "nudEditStartSecond";
        nudEditStartSecond.Size = new Size(61, 30);
        nudEditStartSecond.TabIndex = 6;
        // 
        // lblEditStartHour
        // 
        lblEditStartHour.AutoSize = true;
        lblEditStartHour.Location = new Point(164, 167);
        lblEditStartHour.Margin = new Padding(4, 0, 4, 0);
        lblEditStartHour.Name = "lblEditStartHour";
        lblEditStartHour.Size = new Size(28, 24);
        lblEditStartHour.TabIndex = 3;
        lblEditStartHour.Text = "时";
        // 
        // lblEditStartMinute
        // 
        lblEditStartMinute.AutoSize = true;
        lblEditStartMinute.Location = new Point(263, 167);
        lblEditStartMinute.Margin = new Padding(4, 0, 4, 0);
        lblEditStartMinute.Name = "lblEditStartMinute";
        lblEditStartMinute.Size = new Size(28, 24);
        lblEditStartMinute.TabIndex = 5;
        lblEditStartMinute.Text = "分";
        // 
        // lblEditStartSecond
        // 
        lblEditStartSecond.AutoSize = true;
        lblEditStartSecond.Location = new Point(362, 167);
        lblEditStartSecond.Margin = new Padding(4, 0, 4, 0);
        lblEditStartSecond.Name = "lblEditStartSecond";
        lblEditStartSecond.Size = new Size(28, 24);
        lblEditStartSecond.TabIndex = 7;
        lblEditStartSecond.Text = "秒";
        // 
        // lblEditEndTime
        // 
        lblEditEndTime.AutoSize = true;
        lblEditEndTime.Location = new Point(7, 205);
        lblEditEndTime.Margin = new Padding(4, 0, 4, 0);
        lblEditEndTime.Name = "lblEditEndTime";
        lblEditEndTime.Size = new Size(86, 24);
        lblEditEndTime.TabIndex = 8;
        lblEditEndTime.Text = "结束时间:";
        // 
        // nudEditEndHour
        // 
        nudEditEndHour.Location = new Point(95, 202);
        nudEditEndHour.Margin = new Padding(4);
        nudEditEndHour.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
        nudEditEndHour.Name = "nudEditEndHour";
        nudEditEndHour.Size = new Size(61, 30);
        nudEditEndHour.TabIndex = 9;
        // 
        // nudEditEndMinute
        // 
        nudEditEndMinute.Location = new Point(194, 202);
        nudEditEndMinute.Margin = new Padding(4);
        nudEditEndMinute.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
        nudEditEndMinute.Name = "nudEditEndMinute";
        nudEditEndMinute.Size = new Size(61, 30);
        nudEditEndMinute.TabIndex = 11;
        nudEditEndMinute.Value = new decimal(new int[] { 30, 0, 0, 0 });
        // 
        // nudEditEndSecond
        // 
        nudEditEndSecond.Location = new Point(293, 202);
        nudEditEndSecond.Margin = new Padding(4);
        nudEditEndSecond.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
        nudEditEndSecond.Name = "nudEditEndSecond";
        nudEditEndSecond.Size = new Size(61, 30);
        nudEditEndSecond.TabIndex = 13;
        nudEditEndSecond.Value = new decimal(new int[] { 30, 0, 0, 0 });
        // 
        // lblEditEndHour
        // 
        lblEditEndHour.AutoSize = true;
        lblEditEndHour.Location = new Point(164, 205);
        lblEditEndHour.Margin = new Padding(4, 0, 4, 0);
        lblEditEndHour.Name = "lblEditEndHour";
        lblEditEndHour.Size = new Size(28, 24);
        lblEditEndHour.TabIndex = 10;
        lblEditEndHour.Text = "时";
        // 
        // lblEditEndMinute
        // 
        lblEditEndMinute.AutoSize = true;
        lblEditEndMinute.Location = new Point(263, 205);
        lblEditEndMinute.Margin = new Padding(4, 0, 4, 0);
        lblEditEndMinute.Name = "lblEditEndMinute";
        lblEditEndMinute.Size = new Size(28, 24);
        lblEditEndMinute.TabIndex = 12;
        lblEditEndMinute.Text = "分";
        // 
        // lblEditEndSecond
        // 
        lblEditEndSecond.AutoSize = true;
        lblEditEndSecond.Location = new Point(362, 205);
        lblEditEndSecond.Margin = new Padding(4, 0, 4, 0);
        lblEditEndSecond.Name = "lblEditEndSecond";
        lblEditEndSecond.Size = new Size(28, 24);
        lblEditEndSecond.TabIndex = 14;
        lblEditEndSecond.Text = "秒";
        // 
        // txtEditNotes
        // 
        txtEditNotes.Location = new Point(467, 164);
        txtEditNotes.Margin = new Padding(4);
        txtEditNotes.Name = "txtEditNotes";
        txtEditNotes.Size = new Size(200, 30);
        txtEditNotes.TabIndex = 16;
        // 
        // lblEditNotes
        // 
        lblEditNotes.AutoSize = true;
        lblEditNotes.Location = new Point(410, 167);
        lblEditNotes.Margin = new Padding(4, 0, 4, 0);
        lblEditNotes.Name = "lblEditNotes";
        lblEditNotes.Size = new Size(50, 24);
        lblEditNotes.TabIndex = 15;
        lblEditNotes.Text = "备注:";
        // 
        // btnAddSegment
        // 
        btnAddSegment.Location = new Point(675, 164);
        btnAddSegment.Margin = new Padding(4);
        btnAddSegment.Name = "btnAddSegment";
        btnAddSegment.Size = new Size(80, 30);
        btnAddSegment.TabIndex = 17;
        btnAddSegment.Text = "添加";
        btnAddSegment.UseVisualStyleBackColor = true;
        btnAddSegment.Click += btnAddSegment_Click;
        // 
        // btnUpdateSegment
        // 
        btnUpdateSegment.Location = new Point(763, 164);
        btnUpdateSegment.Margin = new Padding(4);
        btnUpdateSegment.Name = "btnUpdateSegment";
        btnUpdateSegment.Size = new Size(80, 30);
        btnUpdateSegment.TabIndex = 18;
        btnUpdateSegment.Text = "更新";
        btnUpdateSegment.UseVisualStyleBackColor = true;
        btnUpdateSegment.Click += btnUpdateSegment_Click;
        // 
        // btnDeleteSegment
        // 
        btnDeleteSegment.Location = new Point(851, 164);
        btnDeleteSegment.Margin = new Padding(4);
        btnDeleteSegment.Name = "btnDeleteSegment";
        btnDeleteSegment.Size = new Size(80, 30);
        btnDeleteSegment.TabIndex = 19;
        btnDeleteSegment.Text = "删除";
        btnDeleteSegment.UseVisualStyleBackColor = true;
        btnDeleteSegment.Click += btnDeleteSegment_Click;
        // 
        // btnClearEdit
        // 
        btnClearEdit.Location = new Point(675, 202);
        btnClearEdit.Margin = new Padding(4);
        btnClearEdit.Name = "btnClearEdit";
        btnClearEdit.Size = new Size(80, 30);
        btnClearEdit.TabIndex = 20;
        btnClearEdit.Text = "清空";
        btnClearEdit.UseVisualStyleBackColor = true;
        btnClearEdit.Click += btnClearEdit_Click;
        // 
        // lvAdSegments
        // 
        lvAdSegments.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4 });
        lvAdSegments.FullRowSelect = true;
        lvAdSegments.GridLines = true;
        lvAdSegments.Location = new Point(7, 31);
        lvAdSegments.Margin = new Padding(4);
        lvAdSegments.Name = "lvAdSegments";
        lvAdSegments.Size = new Size(933, 118);
        lvAdSegments.TabIndex = 0;
        lvAdSegments.UseCompatibleStateImageBehavior = false;
        lvAdSegments.View = View.Details;
        lvAdSegments.SelectedIndexChanged += lvAdSegments_SelectedIndexChanged;
        // 
        // columnHeader1
        // 
        columnHeader1.Text = "序号";
        columnHeader1.Width = 50;
        // 
        // columnHeader2
        // 
        columnHeader2.Text = "开始时间";
        columnHeader2.Width = 150;
        // 
        // columnHeader3
        // 
        columnHeader3.Text = "结束时间";
        columnHeader3.Width = 150;
        // 
        // columnHeader4
        // 
        columnHeader4.Text = "备注";
        columnHeader4.Width = 400;
        // 
        // grpActions
        // 
        grpActions.Controls.Add(btnDetectAds);
        grpActions.Controls.Add(btnStopDetect);
        grpActions.Controls.Add(btnRemoveAds);
        grpActions.Controls.Add(btnTestMerge);
        grpActions.Location = new Point(22, 485);
        grpActions.Margin = new Padding(4);
        grpActions.Name = "grpActions";
        grpActions.Padding = new Padding(4);
        grpActions.Size = new Size(948, 95);
        grpActions.TabIndex = 4;
        grpActions.TabStop = false;
        grpActions.Text = "操作";
        // 
        // btnDetectAds
        // 
        btnDetectAds.Location = new Point(37, 31);
        btnDetectAds.Margin = new Padding(4);
        btnDetectAds.Name = "btnDetectAds";
        btnDetectAds.Size = new Size(115, 41);
        btnDetectAds.TabIndex = 2;
        btnDetectAds.Text = "检测广告";
        btnDetectAds.UseVisualStyleBackColor = true;
        btnDetectAds.Click += btnDetectAds_Click;
        // 
        // btnStopDetect
        // 
        btnStopDetect.Enabled = false;
        btnStopDetect.Location = new Point(189, 31);
        btnStopDetect.Margin = new Padding(4);
        btnStopDetect.Name = "btnStopDetect";
        btnStopDetect.Size = new Size(115, 41);
        btnStopDetect.TabIndex = 3;
        btnStopDetect.Text = "停止检测";
        btnStopDetect.UseVisualStyleBackColor = true;
        btnStopDetect.Click += btnStopDetect_Click;
        // 
        // btnRemoveAds
        // 
        btnRemoveAds.Location = new Point(339, 31);
        btnRemoveAds.Margin = new Padding(4);
        btnRemoveAds.Name = "btnRemoveAds";
        btnRemoveAds.Size = new Size(115, 41);
        btnRemoveAds.TabIndex = 4;
        btnRemoveAds.Text = "移除广告";
        btnRemoveAds.UseVisualStyleBackColor = true;
        btnRemoveAds.Click += btnRemoveAds_Click;
        // 
        // btnTestMerge
        // 
        btnTestMerge.Location = new Point(491, 31);
        btnTestMerge.Margin = new Padding(4);
        btnTestMerge.Name = "btnTestMerge";
        btnTestMerge.Size = new Size(115, 41);
        btnTestMerge.TabIndex = 5;
        btnTestMerge.Text = "测试";
        btnTestMerge.UseVisualStyleBackColor = true;
        btnTestMerge.Click += btnTestMerge_Click;
        // 
        // grpProgress
        // 
        grpProgress.Controls.Add(lblProgressText);
        grpProgress.Controls.Add(pbProgress);
        grpProgress.Location = new Point(22, 588);
        grpProgress.Margin = new Padding(4);
        grpProgress.Name = "grpProgress";
        grpProgress.Padding = new Padding(4);
        grpProgress.Size = new Size(948, 95);
        grpProgress.TabIndex = 5;
        grpProgress.TabStop = false;
        grpProgress.Text = "进度";
        // 
        // lblProgressText
        // 
        lblProgressText.AutoSize = true;
        lblProgressText.Location = new Point(7, 61);
        lblProgressText.Margin = new Padding(4, 0, 4, 0);
        lblProgressText.Name = "lblProgressText";
        lblProgressText.Size = new Size(0, 24);
        lblProgressText.TabIndex = 1;
        // 
        // pbProgress
        // 
        pbProgress.Location = new Point(7, 31);
        pbProgress.Margin = new Padding(4);
        pbProgress.Name = "pbProgress";
        pbProgress.Size = new Size(934, 28);
        pbProgress.TabIndex = 0;
        // 
        // openFileDialog1
        // 
        openFileDialog1.FileName = "openFileDialog1";
        openFileDialog1.Filter = "视频文件|*.mp4;*.avi;*.mov;*.mkv|所有文件|*.*";
        // 
        // chkDarkTheme
        // 
        chkDarkTheme.AutoSize = true;
        chkDarkTheme.Location = new Point(794, 23);
        chkDarkTheme.Margin = new Padding(4);
        chkDarkTheme.Name = "chkDarkTheme";
        chkDarkTheme.Size = new Size(144, 28);
        chkDarkTheme.TabIndex = 6;
        chkDarkTheme.Text = "启用黑暗主题";
        chkDarkTheme.UseVisualStyleBackColor = true;
        // 
        // grpImageCompare
        // 
        grpImageCompare.Controls.Add(lblCompareResult);
        grpImageCompare.Controls.Add(btnCompareImages);
        grpImageCompare.Controls.Add(btnBrowseImage2);
        grpImageCompare.Controls.Add(txtImagePath2);
        grpImageCompare.Controls.Add(btnBrowseImage1);
        grpImageCompare.Controls.Add(txtImagePath1);
        grpImageCompare.Controls.Add(lblImage1);
        grpImageCompare.Controls.Add(lblImage2);
        grpImageCompare.Location = new Point(22, 956);
        grpImageCompare.Margin = new Padding(4);
        grpImageCompare.Name = "grpImageCompare";
        grpImageCompare.Padding = new Padding(4);
        grpImageCompare.Size = new Size(948, 120);
        grpImageCompare.TabIndex = 7;
        grpImageCompare.TabStop = false;
        grpImageCompare.Text = "图片相似度比较";
        // 
        // lblCompareResult
        // 
        lblCompareResult.AutoSize = true;
        lblCompareResult.Font = new Font("Microsoft YaHei UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point);
        lblCompareResult.Location = new Point(626, 53);
        lblCompareResult.Margin = new Padding(4, 0, 4, 0);
        lblCompareResult.Name = "lblCompareResult";
        lblCompareResult.Size = new Size(163, 30);
        lblCompareResult.TabIndex = 7;
        lblCompareResult.Text = "比较结果：--%";
        // 
        // btnCompareImages
        // 
        btnCompareImages.Location = new Point(503, 40);
        btnCompareImages.Margin = new Padding(4);
        btnCompareImages.Name = "btnCompareImages";
        btnCompareImages.Size = new Size(115, 61);
        btnCompareImages.TabIndex = 6;
        btnCompareImages.Text = "比较相似度";
        btnCompareImages.UseVisualStyleBackColor = true;
        btnCompareImages.Click += btnCompareImages_Click;
        // 
        // btnBrowseImage2
        // 
        btnBrowseImage2.Location = new Point(361, 71);
        btnBrowseImage2.Margin = new Padding(4);
        btnBrowseImage2.Name = "btnBrowseImage2";
        btnBrowseImage2.Size = new Size(95, 30);
        btnBrowseImage2.TabIndex = 5;
        btnBrowseImage2.Text = "浏览";
        btnBrowseImage2.UseVisualStyleBackColor = true;
        btnBrowseImage2.Click += btnBrowseImage2_Click;
        // 
        // txtImagePath2
        // 
        txtImagePath2.Location = new Point(93, 71);
        txtImagePath2.Margin = new Padding(4);
        txtImagePath2.Name = "txtImagePath2";
        txtImagePath2.ReadOnly = true;
        txtImagePath2.Size = new Size(260, 30);
        txtImagePath2.TabIndex = 4;
        // 
        // btnBrowseImage1
        // 
        btnBrowseImage1.Location = new Point(361, 40);
        btnBrowseImage1.Margin = new Padding(4);
        btnBrowseImage1.Name = "btnBrowseImage1";
        btnBrowseImage1.Size = new Size(95, 30);
        btnBrowseImage1.TabIndex = 3;
        btnBrowseImage1.Text = "浏览";
        btnBrowseImage1.UseVisualStyleBackColor = true;
        btnBrowseImage1.Click += btnBrowseImage1_Click;
        // 
        // txtImagePath1
        // 
        txtImagePath1.Location = new Point(93, 40);
        txtImagePath1.Margin = new Padding(4);
        txtImagePath1.Name = "txtImagePath1";
        txtImagePath1.ReadOnly = true;
        txtImagePath1.Size = new Size(260, 30);
        txtImagePath1.TabIndex = 2;
        // 
        // lblImage1
        // 
        lblImage1.AutoSize = true;
        lblImage1.Location = new Point(12, 40);
        lblImage1.Margin = new Padding(4, 0, 4, 0);
        lblImage1.Name = "lblImage1";
        lblImage1.Size = new Size(75, 24);
        lblImage1.TabIndex = 0;
        lblImage1.Text = "图片1：";
        // 
        // lblImage2
        // 
        lblImage2.AutoSize = true;
        lblImage2.Location = new Point(12, 75);
        lblImage2.Margin = new Padding(4, 0, 4, 0);
        lblImage2.Name = "lblImage2";
        lblImage2.Size = new Size(75, 24);
        lblImage2.TabIndex = 1;
        lblImage2.Text = "图片2：";
        // 
        // openFileDialog2
        // 
        openFileDialog2.FileName = "openFileDialog2";
        openFileDialog2.Filter = "图片文件|*.jpg;*.jpeg;*.png;*.bmp|所有文件|*.*";
        // 
        // grpDetectionRanges
        // 
        grpDetectionRanges.Controls.Add(lblRangeEndSecond);
        grpDetectionRanges.Controls.Add(nudRangeEndSecond);
        grpDetectionRanges.Controls.Add(lblRangeEndMinute);
        grpDetectionRanges.Controls.Add(nudRangeEndMinute);
        grpDetectionRanges.Controls.Add(lblRangeEndHour);
        grpDetectionRanges.Controls.Add(nudRangeEndHour);
        grpDetectionRanges.Controls.Add(lblRangeStartSecond);
        grpDetectionRanges.Controls.Add(nudRangeStartSecond);
        grpDetectionRanges.Controls.Add(lblRangeStartMinute);
        grpDetectionRanges.Controls.Add(nudRangeStartMinute);
        grpDetectionRanges.Controls.Add(lblRangeStartHour);
        grpDetectionRanges.Controls.Add(nudRangeStartHour);
        grpDetectionRanges.Controls.Add(lblRangeTimeLabel);
        grpDetectionRanges.Controls.Add(btnClearRanges);
        grpDetectionRanges.Controls.Add(btnDeleteRange);
        grpDetectionRanges.Controls.Add(btnAddRange);
        grpDetectionRanges.Controls.Add(lvDetectionRanges);
        grpDetectionRanges.Location = new Point(22, 313);
        grpDetectionRanges.Margin = new Padding(4);
        grpDetectionRanges.Name = "grpDetectionRanges";
        grpDetectionRanges.Padding = new Padding(4);
        grpDetectionRanges.Size = new Size(948, 164);
        grpDetectionRanges.TabIndex = 3;
        grpDetectionRanges.TabStop = false;
        grpDetectionRanges.Text = "检测时间段管理";
        // 
        // lblRangeEndSecond
        // 
        lblRangeEndSecond.AutoSize = true;
        lblRangeEndSecond.Location = new Point(361, 72);
        lblRangeEndSecond.Margin = new Padding(4, 0, 4, 0);
        lblRangeEndSecond.Name = "lblRangeEndSecond";
        lblRangeEndSecond.Size = new Size(28, 24);
        lblRangeEndSecond.TabIndex = 19;
        lblRangeEndSecond.Text = "秒";
        // 
        // nudRangeEndSecond
        // 
        nudRangeEndSecond.Location = new Point(293, 69);
        nudRangeEndSecond.Margin = new Padding(4);
        nudRangeEndSecond.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
        nudRangeEndSecond.Name = "nudRangeEndSecond";
        nudRangeEndSecond.Size = new Size(61, 30);
        nudRangeEndSecond.TabIndex = 18;
        nudRangeEndSecond.Value = new decimal(new int[] { 30, 0, 0, 0 });
        // 
        // lblRangeEndMinute
        // 
        lblRangeEndMinute.AutoSize = true;
        lblRangeEndMinute.Location = new Point(262, 72);
        lblRangeEndMinute.Margin = new Padding(4, 0, 4, 0);
        lblRangeEndMinute.Name = "lblRangeEndMinute";
        lblRangeEndMinute.Size = new Size(28, 24);
        lblRangeEndMinute.TabIndex = 17;
        lblRangeEndMinute.Text = "分";
        // 
        // nudRangeEndMinute
        // 
        nudRangeEndMinute.Location = new Point(194, 69);
        nudRangeEndMinute.Margin = new Padding(4);
        nudRangeEndMinute.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
        nudRangeEndMinute.Name = "nudRangeEndMinute";
        nudRangeEndMinute.Size = new Size(61, 30);
        nudRangeEndMinute.TabIndex = 16;
        nudRangeEndMinute.Value = new decimal(new int[] { 30, 0, 0, 0 });
        // 
        // lblRangeEndHour
        // 
        lblRangeEndHour.AutoSize = true;
        lblRangeEndHour.Location = new Point(163, 72);
        lblRangeEndHour.Margin = new Padding(4, 0, 4, 0);
        lblRangeEndHour.Name = "lblRangeEndHour";
        lblRangeEndHour.Size = new Size(28, 24);
        lblRangeEndHour.TabIndex = 15;
        lblRangeEndHour.Text = "时";
        // 
        // nudRangeEndHour
        // 
        nudRangeEndHour.Location = new Point(95, 69);
        nudRangeEndHour.Margin = new Padding(4);
        nudRangeEndHour.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
        nudRangeEndHour.Name = "nudRangeEndHour";
        nudRangeEndHour.Size = new Size(61, 30);
        nudRangeEndHour.TabIndex = 14;
        // 
        // lblRangeStartSecond
        // 
        lblRangeStartSecond.AutoSize = true;
        lblRangeStartSecond.Location = new Point(362, 34);
        lblRangeStartSecond.Margin = new Padding(4, 0, 4, 0);
        lblRangeStartSecond.Name = "lblRangeStartSecond";
        lblRangeStartSecond.Size = new Size(28, 24);
        lblRangeStartSecond.TabIndex = 13;
        lblRangeStartSecond.Text = "秒";
        // 
        // nudRangeStartSecond
        // 
        nudRangeStartSecond.Location = new Point(293, 31);
        nudRangeStartSecond.Margin = new Padding(4);
        nudRangeStartSecond.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
        nudRangeStartSecond.Name = "nudRangeStartSecond";
        nudRangeStartSecond.Size = new Size(61, 30);
        nudRangeStartSecond.TabIndex = 12;
        // 
        // lblRangeStartMinute
        // 
        lblRangeStartMinute.AutoSize = true;
        lblRangeStartMinute.Location = new Point(263, 34);
        lblRangeStartMinute.Margin = new Padding(4, 0, 4, 0);
        lblRangeStartMinute.Name = "lblRangeStartMinute";
        lblRangeStartMinute.Size = new Size(28, 24);
        lblRangeStartMinute.TabIndex = 11;
        lblRangeStartMinute.Text = "分";
        // 
        // nudRangeStartMinute
        // 
        nudRangeStartMinute.Location = new Point(194, 31);
        nudRangeStartMinute.Margin = new Padding(4);
        nudRangeStartMinute.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
        nudRangeStartMinute.Name = "nudRangeStartMinute";
        nudRangeStartMinute.Size = new Size(61, 30);
        nudRangeStartMinute.TabIndex = 10;
        // 
        // lblRangeStartHour
        // 
        lblRangeStartHour.AutoSize = true;
        lblRangeStartHour.Location = new Point(164, 34);
        lblRangeStartHour.Margin = new Padding(4, 0, 4, 0);
        lblRangeStartHour.Name = "lblRangeStartHour";
        lblRangeStartHour.Size = new Size(28, 24);
        lblRangeStartHour.TabIndex = 9;
        lblRangeStartHour.Text = "时";
        // 
        // nudRangeStartHour
        // 
        nudRangeStartHour.Location = new Point(95, 31);
        nudRangeStartHour.Margin = new Padding(4);
        nudRangeStartHour.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
        nudRangeStartHour.Name = "nudRangeStartHour";
        nudRangeStartHour.Size = new Size(61, 30);
        nudRangeStartHour.TabIndex = 1;
        // 
        // lblRangeTimeLabel
        // 
        lblRangeTimeLabel.AutoSize = true;
        lblRangeTimeLabel.Location = new Point(7, 34);
        lblRangeTimeLabel.Margin = new Padding(4, 0, 4, 0);
        lblRangeTimeLabel.Name = "lblRangeTimeLabel";
        lblRangeTimeLabel.Size = new Size(82, 24);
        lblRangeTimeLabel.TabIndex = 8;
        lblRangeTimeLabel.Text = "时间段：";
        // 
        // btnClearRanges
        // 
        btnClearRanges.Location = new Point(402, 104);
        btnClearRanges.Margin = new Padding(4);
        btnClearRanges.Name = "btnClearRanges";
        btnClearRanges.Size = new Size(107, 30);
        btnClearRanges.TabIndex = 3;
        btnClearRanges.Text = "清空所有";
        btnClearRanges.UseVisualStyleBackColor = true;
        btnClearRanges.Click += btnClearRanges_Click;
        // 
        // btnDeleteRange
        // 
        btnDeleteRange.Location = new Point(402, 66);
        btnDeleteRange.Margin = new Padding(4);
        btnDeleteRange.Name = "btnDeleteRange";
        btnDeleteRange.Size = new Size(107, 30);
        btnDeleteRange.TabIndex = 2;
        btnDeleteRange.Text = "删除选中";
        btnDeleteRange.UseVisualStyleBackColor = true;
        btnDeleteRange.Click += btnDeleteRange_Click;
        // 
        // btnAddRange
        // 
        btnAddRange.Location = new Point(402, 31);
        btnAddRange.Margin = new Padding(4);
        btnAddRange.Name = "btnAddRange";
        btnAddRange.Size = new Size(107, 30);
        btnAddRange.TabIndex = 1;
        btnAddRange.Text = "添加时间段";
        btnAddRange.UseVisualStyleBackColor = true;
        btnAddRange.Click += btnAddRange_Click;
        // 
        // lvDetectionRanges
        // 
        lvDetectionRanges.Columns.AddRange(new ColumnHeader[] { columnHeaderRangeIndex, columnHeaderRangeStart, columnHeaderRangeEnd });
        lvDetectionRanges.FullRowSelect = true;
        lvDetectionRanges.GridLines = true;
        lvDetectionRanges.Location = new Point(530, 31);
        lvDetectionRanges.Margin = new Padding(4);
        lvDetectionRanges.Name = "lvDetectionRanges";
        lvDetectionRanges.Size = new Size(371, 121);
        lvDetectionRanges.TabIndex = 0;
        lvDetectionRanges.UseCompatibleStateImageBehavior = false;
        lvDetectionRanges.View = View.Details;
        // 
        // columnHeaderRangeIndex
        // 
        columnHeaderRangeIndex.Text = "序号";
        columnHeaderRangeIndex.Width = 50;
        // 
        // columnHeaderRangeStart
        // 
        columnHeaderRangeStart.Text = "开始时间";
        columnHeaderRangeStart.Width = 120;
        // 
        // columnHeaderRangeEnd
        // 
        columnHeaderRangeEnd.Text = "结束时间";
        columnHeaderRangeEnd.Width = 120;
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(11F, 24F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(998, 1086);
        Controls.Add(grpProgress);
        Controls.Add(grpImageCompare);
        Controls.Add(grpActions);
        Controls.Add(grpDetectResult);
        Controls.Add(grpDetectionRanges);
        Controls.Add(grpAdTime);
        Controls.Add(grpMovieFile);
        Controls.Add(chkDarkTheme);
        Controls.Add(lblTitle);
        Margin = new Padding(4);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "视频广告移除工具";
        grpMovieFile.ResumeLayout(false);
        grpMovieFile.PerformLayout();
        grpAdTime.ResumeLayout(false);
        grpAdTime.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudScanInterval).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudAdEndSecond).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudAdEndMinute).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudAdEndHour).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudAdStartSecond).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudAdStartMinute).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudAdStartHour).EndInit();
        grpDetectResult.ResumeLayout(false);
        grpDetectResult.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudEditStartHour).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudEditStartMinute).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudEditStartSecond).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudEditEndHour).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudEditEndMinute).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudEditEndSecond).EndInit();
        grpActions.ResumeLayout(false);
        grpProgress.ResumeLayout(false);
        grpProgress.PerformLayout();
        grpImageCompare.ResumeLayout(false);
        grpImageCompare.PerformLayout();
        grpDetectionRanges.ResumeLayout(false);
        grpDetectionRanges.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudRangeEndSecond).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudRangeEndMinute).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudRangeEndHour).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudRangeStartSecond).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudRangeStartMinute).EndInit();
        ((System.ComponentModel.ISupportInitialize)nudRangeStartHour).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.Label lblTitle;
    private System.Windows.Forms.GroupBox grpMovieFile;
    private System.Windows.Forms.TextBox txtMoviePath;
    private System.Windows.Forms.Button btnBrowseMovie;
    private System.Windows.Forms.GroupBox grpAdTime;
    private System.Windows.Forms.Label label1;
        // 开始时间时分秒控件
        private System.Windows.Forms.NumericUpDown nudAdStartHour;
        private System.Windows.Forms.NumericUpDown nudAdStartMinute;
        private System.Windows.Forms.NumericUpDown nudAdStartSecond;
        private System.Windows.Forms.Label labelStartHour;
        private System.Windows.Forms.Label labelStartMinute;
        private System.Windows.Forms.Label labelStartSecond;
        // 结束时间时分秒控件
        private System.Windows.Forms.NumericUpDown nudAdEndHour;
        private System.Windows.Forms.NumericUpDown nudAdEndMinute;
        private System.Windows.Forms.NumericUpDown nudAdEndSecond;
        private System.Windows.Forms.Label labelEndHour;
        private System.Windows.Forms.Label labelEndMinute;
        private System.Windows.Forms.Label labelEndSecond;
        private System.Windows.Forms.GroupBox grpDetectResult;
        private System.Windows.Forms.ListView lvAdSegments;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.GroupBox grpActions;
        private System.Windows.Forms.Button btnDetectAds;
        private System.Windows.Forms.Button btnStopDetect;
        private System.Windows.Forms.Button btnRemoveAds;
        private System.Windows.Forms.Button btnTestMerge;
        private System.Windows.Forms.GroupBox grpProgress;
        private System.Windows.Forms.Label lblProgressText;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox chkDarkTheme;
        private System.Windows.Forms.Label lblScanInterval;
        private System.Windows.Forms.NumericUpDown nudScanInterval;
        // 图片比较相关控件
        private System.Windows.Forms.GroupBox grpImageCompare;
        private System.Windows.Forms.Label lblCompareResult;
        private System.Windows.Forms.Button btnCompareImages;
        private System.Windows.Forms.Button btnBrowseImage2;
        private System.Windows.Forms.TextBox txtImagePath2;
        private System.Windows.Forms.Button btnBrowseImage1;
        private System.Windows.Forms.TextBox txtImagePath1;
        private System.Windows.Forms.Label lblImage1;
        private System.Windows.Forms.Label lblImage2;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        // 检测时间段管理控件
    private System.Windows.Forms.GroupBox grpDetectionRanges;
    private System.Windows.Forms.ListView lvDetectionRanges;
    private System.Windows.Forms.ColumnHeader columnHeaderRangeIndex;
    private System.Windows.Forms.ColumnHeader columnHeaderRangeStart;
    private System.Windows.Forms.ColumnHeader columnHeaderRangeEnd;
    private System.Windows.Forms.Button btnAddRange;
    private System.Windows.Forms.Button btnDeleteRange;
    private System.Windows.Forms.Button btnClearRanges;
    // 检测时间段时间控件
    private System.Windows.Forms.Label lblRangeTimeLabel;
    private System.Windows.Forms.Label lblRangeStartHour;
    private System.Windows.Forms.Label lblRangeStartMinute;
    private System.Windows.Forms.Label lblRangeStartSecond;
    private System.Windows.Forms.NumericUpDown nudRangeStartHour;
    private System.Windows.Forms.NumericUpDown nudRangeStartMinute;
    private System.Windows.Forms.NumericUpDown nudRangeStartSecond;
    private System.Windows.Forms.Label lblRangeEndHour;
    private System.Windows.Forms.Label lblRangeEndMinute;
    private System.Windows.Forms.Label lblRangeEndSecond;
    private System.Windows.Forms.NumericUpDown nudRangeEndHour;
    private System.Windows.Forms.NumericUpDown nudRangeEndMinute;
    private System.Windows.Forms.NumericUpDown nudRangeEndSecond;
}
