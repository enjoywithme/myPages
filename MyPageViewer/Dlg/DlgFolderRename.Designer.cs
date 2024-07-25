namespace MyPageViewer.Dlg
{
    partial class DlgFolderRename
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
            lbNodePath = new Label();
            label1 = new Label();
            tbNewName = new TextBox();
            btOk = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // lbNodePath
            // 
            lbNodePath.Location = new Point(27, 25);
            lbNodePath.Name = "lbNodePath";
            lbNodePath.Size = new Size(491, 39);
            lbNodePath.TabIndex = 0;
            lbNodePath.Text = "文件夹路径：";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 86);
            label1.Name = "label1";
            label1.Size = new Size(56, 17);
            label1.TabIndex = 1;
            label1.Text = "新名称：";
            // 
            // tbNewName
            // 
            tbNewName.Location = new Point(92, 87);
            tbNewName.Name = "tbNewName";
            tbNewName.Size = new Size(393, 23);
            tbNewName.TabIndex = 2;
            // 
            // btOk
            // 
            btOk.Location = new Point(302, 147);
            btOk.Name = "btOk";
            btOk.Size = new Size(75, 23);
            btOk.TabIndex = 3;
            btOk.Text = "确定(&O)";
            btOk.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.DialogResult = DialogResult.Cancel;
            button2.Location = new Point(410, 147);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 3;
            button2.Text = "取消(&C)";
            button2.UseVisualStyleBackColor = true;
            // 
            // DlgFolderRename
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(518, 211);
            Controls.Add(button2);
            Controls.Add(btOk);
            Controls.Add(tbNewName);
            Controls.Add(label1);
            Controls.Add(lbNodePath);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DlgFolderRename";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "重命名文件夹";
            Load += DlgFolderRename_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbNodePath;
        private Label label1;
        private TextBox tbNewName;
        private Button btOk;
        private Button button2;
    }
}