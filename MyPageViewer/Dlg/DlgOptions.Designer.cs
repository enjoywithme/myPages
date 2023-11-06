namespace MyPageViewer.Dlg
{
    partial class DlgOptions
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
            tabControl1 = new TabControl();
            tabPageIndex = new TabPage();
            cbAutoIndexUnit = new ComboBox();
            tbAutoIndexInterval = new TextBox();
            rbNoAutoScan = new RadioButton();
            rbScanInterval = new RadioButton();
            btSetWorkingDir = new Button();
            tbWorkingDir = new TextBox();
            label1 = new Label();
            tabPage1 = new TabPage();
            btRemoveScanFolder = new Button();
            btAddScanFolder = new Button();
            listScanFolders = new ListBox();
            label2 = new Label();
            btOk = new Button();
            btCancel = new Button();
            tabControl1.SuspendLayout();
            tabPageIndex.SuspendLayout();
            tabPage1.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPageIndex);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(411, 321);
            tabControl1.TabIndex = 0;
            // 
            // tabPageIndex
            // 
            tabPageIndex.Controls.Add(cbAutoIndexUnit);
            tabPageIndex.Controls.Add(tbAutoIndexInterval);
            tabPageIndex.Controls.Add(rbNoAutoScan);
            tabPageIndex.Controls.Add(rbScanInterval);
            tabPageIndex.Controls.Add(btSetWorkingDir);
            tabPageIndex.Controls.Add(tbWorkingDir);
            tabPageIndex.Controls.Add(label1);
            tabPageIndex.Location = new Point(4, 26);
            tabPageIndex.Name = "tabPageIndex";
            tabPageIndex.Padding = new Padding(3);
            tabPageIndex.Size = new Size(403, 291);
            tabPageIndex.TabIndex = 2;
            tabPageIndex.Text = "索引";
            tabPageIndex.UseVisualStyleBackColor = true;
            // 
            // cbAutoIndexUnit
            // 
            cbAutoIndexUnit.DropDownStyle = ComboBoxStyle.DropDownList;
            cbAutoIndexUnit.FormattingEnabled = true;
            cbAutoIndexUnit.Items.AddRange(new object[] { "小时", "分钟" });
            cbAutoIndexUnit.Location = new Point(189, 83);
            cbAutoIndexUnit.Name = "cbAutoIndexUnit";
            cbAutoIndexUnit.Size = new Size(47, 25);
            cbAutoIndexUnit.TabIndex = 7;
            // 
            // tbAutoIndexInterval
            // 
            tbAutoIndexInterval.Location = new Point(126, 84);
            tbAutoIndexInterval.Name = "tbAutoIndexInterval";
            tbAutoIndexInterval.Size = new Size(57, 23);
            tbAutoIndexInterval.TabIndex = 6;
            // 
            // rbNoAutoScan
            // 
            rbNoAutoScan.AutoSize = true;
            rbNoAutoScan.Location = new Point(22, 132);
            rbNoAutoScan.Name = "rbNoAutoScan";
            rbNoAutoScan.Size = new Size(86, 21);
            rbNoAutoScan.TabIndex = 5;
            rbNoAutoScan.TabStop = true;
            rbNoAutoScan.Text = "不自动索引";
            rbNoAutoScan.UseVisualStyleBackColor = true;
            // 
            // rbScanInterval
            // 
            rbScanInterval.AutoSize = true;
            rbScanInterval.Location = new Point(22, 85);
            rbScanInterval.Name = "rbScanInterval";
            rbScanInterval.Size = new Size(98, 21);
            rbScanInterval.TabIndex = 5;
            rbScanInterval.TabStop = true;
            rbScanInterval.Text = "重新索引周期";
            rbScanInterval.UseVisualStyleBackColor = true;
            // 
            // btSetWorkingDir
            // 
            btSetWorkingDir.Location = new Point(316, 31);
            btSetWorkingDir.Name = "btSetWorkingDir";
            btSetWorkingDir.Size = new Size(75, 23);
            btSetWorkingDir.TabIndex = 4;
            btSetWorkingDir.Text = "浏览(&B)...";
            btSetWorkingDir.UseVisualStyleBackColor = true;
            // 
            // tbWorkingDir
            // 
            tbWorkingDir.Location = new Point(22, 31);
            tbWorkingDir.Name = "tbWorkingDir";
            tbWorkingDir.Size = new Size(275, 23);
            tbWorkingDir.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 11);
            label1.Name = "label1";
            label1.Size = new Size(68, 17);
            label1.TabIndex = 2;
            label1.Text = "数据库路径";
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(btRemoveScanFolder);
            tabPage1.Controls.Add(btAddScanFolder);
            tabPage1.Controls.Add(listScanFolders);
            tabPage1.Controls.Add(label2);
            tabPage1.Location = new Point(4, 26);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(403, 291);
            tabPage1.TabIndex = 3;
            tabPage1.Text = "文件夹";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // btRemoveScanFolder
            // 
            btRemoveScanFolder.Location = new Point(308, 82);
            btRemoveScanFolder.Name = "btRemoveScanFolder";
            btRemoveScanFolder.Size = new Size(75, 23);
            btRemoveScanFolder.TabIndex = 6;
            btRemoveScanFolder.Text = "移除(&R)";
            btRemoveScanFolder.UseVisualStyleBackColor = true;
            // 
            // btAddScanFolder
            // 
            btAddScanFolder.Location = new Point(308, 42);
            btAddScanFolder.Name = "btAddScanFolder";
            btAddScanFolder.Size = new Size(75, 23);
            btAddScanFolder.TabIndex = 7;
            btAddScanFolder.Text = "添加(&A)...";
            btAddScanFolder.UseVisualStyleBackColor = true;
            // 
            // listScanFolders
            // 
            listScanFolders.FormattingEnabled = true;
            listScanFolders.ItemHeight = 17;
            listScanFolders.Location = new Point(27, 42);
            listScanFolders.Name = "listScanFolders";
            listScanFolders.Size = new Size(247, 140);
            listScanFolders.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(20, 16);
            label2.Name = "label2";
            label2.Size = new Size(56, 17);
            label2.TabIndex = 4;
            label2.Text = "文件夹：";
            // 
            // btOk
            // 
            btOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btOk.Location = new Point(238, 347);
            btOk.Name = "btOk";
            btOk.Size = new Size(75, 23);
            btOk.TabIndex = 1;
            btOk.Text = "确定(&O)";
            btOk.UseVisualStyleBackColor = true;
            // 
            // btCancel
            // 
            btCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btCancel.DialogResult = DialogResult.Cancel;
            btCancel.Location = new Point(344, 347);
            btCancel.Name = "btCancel";
            btCancel.Size = new Size(75, 23);
            btCancel.TabIndex = 1;
            btCancel.Text = "取消(&C)";
            btCancel.UseVisualStyleBackColor = true;
            // 
            // DlgOptions
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(435, 391);
            Controls.Add(btCancel);
            Controls.Add(btOk);
            Controls.Add(tabControl1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DlgOptions";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "选项设置";
            Load += DlgOptions_Load;
            tabControl1.ResumeLayout(false);
            tabPageIndex.ResumeLayout(false);
            tabPageIndex.PerformLayout();
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private Button btOk;
        private Button btCancel;
        private TabPage tabPageIndex;
        private Button btSetWorkingDir;
        private TextBox tbWorkingDir;
        private Label label1;
        private TabPage tabPage1;
        private Button btRemoveScanFolder;
        private Button btAddScanFolder;
        private ListBox listScanFolders;
        private Label label2;
        private ComboBox cbAutoIndexUnit;
        private TextBox tbAutoIndexInterval;
        private RadioButton rbNoAutoScan;
        private RadioButton rbScanInterval;
    }
}