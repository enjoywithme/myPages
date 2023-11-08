namespace Wiz2Mypage
{
    partial class MainForm
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
            tbSrcDir = new TextBox();
            label1 = new Label();
            btStart = new Button();
            progressBar1 = new CustomProgressBar();
            tbLog = new TextBox();
            btChoseDir = new Button();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            SuspendLayout();
            // 
            // tbSrcDir
            // 
            tbSrcDir.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbSrcDir.Location = new Point(26, 30);
            tbSrcDir.Name = "tbSrcDir";
            tbSrcDir.Size = new Size(507, 23);
            tbSrcDir.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 10);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 1;
            label1.Text = "开始路径";
            // 
            // btStart
            // 
            btStart.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Left;
            btStart.BackColor = Color.Turquoise;
            btStart.Location = new Point(26, 59);
            btStart.Name = "btStart";
            btStart.Size = new Size(605, 43);
            btStart.TabIndex = 2;
            btStart.Text = "开始";
            btStart.UseVisualStyleBackColor = false;
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBar1.CustomText = null;
            progressBar1.DisplayStyle = ProgressBarDisplayText.Percentage;
            progressBar1.Location = new Point(28, 116);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(605, 23);
            progressBar1.TabIndex = 3;
            // 
            // tbLog
            // 
            tbLog.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbLog.Location = new Point(28, 155);
            tbLog.Multiline = true;
            tbLog.Name = "tbLog";
            tbLog.ReadOnly = true;
            tbLog.ScrollBars = ScrollBars.Vertical;
            tbLog.Size = new Size(603, 205);
            tbLog.TabIndex = 4;
            // 
            // btChoseDir
            // 
            btChoseDir.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btChoseDir.Location = new Point(556, 30);
            btChoseDir.Name = "btChoseDir";
            btChoseDir.Size = new Size(75, 23);
            btChoseDir.TabIndex = 2;
            btChoseDir.Text = "...";
            btChoseDir.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(676, 396);
            Controls.Add(tbLog);
            Controls.Add(progressBar1);
            Controls.Add(btChoseDir);
            Controls.Add(btStart);
            Controls.Add(label1);
            Controls.Add(tbSrcDir);
            MaximizeBox = false;
            Name = "MainForm";
            Text = "为知笔记转My Page";
            Load += MainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox tbSrcDir;
        private Label label1;
        private Button btStart;
        private CustomProgressBar progressBar1;
        private TextBox tbLog;
        private Button btChoseDir;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}