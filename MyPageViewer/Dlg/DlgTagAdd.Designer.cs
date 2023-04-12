namespace MyPageViewer.Dlg
{
    partial class DlgTagAdd
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
            label1 = new Label();
            btOk = new Button();
            button2 = new Button();
            cbTag = new ComboBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 23);
            label1.Name = "label1";
            label1.Size = new Size(32, 17);
            label1.TabIndex = 0;
            label1.Text = "标签";
            // 
            // btOk
            // 
            btOk.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btOk.Location = new Point(312, 23);
            btOk.Name = "btOk";
            btOk.Size = new Size(75, 23);
            btOk.TabIndex = 1;
            btOk.Text = "确定";
            btOk.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button2.DialogResult = DialogResult.Cancel;
            button2.Location = new Point(312, 66);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 2;
            button2.Text = "取消";
            button2.UseVisualStyleBackColor = true;
            // 
            // cbTag
            // 
            cbTag.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cbTag.FormattingEnabled = true;
            cbTag.Location = new Point(23, 45);
            cbTag.Name = "cbTag";
            cbTag.Size = new Size(257, 25);
            cbTag.TabIndex = 0;
            // 
            // DlgTagAdd
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 116);
            Controls.Add(cbTag);
            Controls.Add(button2);
            Controls.Add(btOk);
            Controls.Add(label1);
            MaximizeBox = false;
            MaximumSize = new Size(1024, 155);
            MinimizeBox = false;
            MinimumSize = new Size(415, 155);
            Name = "DlgTagAdd";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "添加标签";
            Load += DlgUrlEdit_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btOk;
        private Button button2;
        private ComboBox cbTag;
    }
}