namespace MyPageViewer.Dlg
{
    partial class DlgUrlEdit
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
            tbUrl = new TextBox();
            btOk = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 20);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 0;
            label1.Text = "链接地址";
            // 
            // tbUrl
            // 
            tbUrl.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbUrl.Location = new Point(18, 40);
            tbUrl.Name = "tbUrl";
            tbUrl.Size = new Size(386, 23);
            tbUrl.TabIndex = 1;
            // 
            // btOk
            // 
            btOk.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btOk.Location = new Point(424, 23);
            btOk.Name = "btOk";
            btOk.Size = new Size(75, 23);
            btOk.TabIndex = 2;
            btOk.Text = "确定";
            btOk.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button2.DialogResult = DialogResult.Cancel;
            button2.Location = new Point(424, 56);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 2;
            button2.Text = "取消";
            button2.UseVisualStyleBackColor = true;
            // 
            // DlgUrlEdit
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(512, 116);
            Controls.Add(button2);
            Controls.Add(btOk);
            Controls.Add(tbUrl);
            Controls.Add(label1);
            MaximizeBox = false;
            MaximumSize = new Size(1024, 155);
            MinimizeBox = false;
            MinimumSize = new Size(500, 155);
            Name = "DlgUrlEdit";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "文章源链接";
            Load += DlgUrlEdit_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox tbUrl;
        private Button btOk;
        private Button button2;
    }
}