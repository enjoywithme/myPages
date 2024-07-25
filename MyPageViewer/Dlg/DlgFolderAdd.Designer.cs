namespace MyPageViewer.Dlg
{
    partial class DlgFolderAdd
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
            lbParentNode = new Label();
            label2 = new Label();
            tbNewName = new TextBox();
            button2 = new Button();
            btOk = new Button();
            SuspendLayout();
            // 
            // lbParentNode
            // 
            lbParentNode.Location = new Point(32, 17);
            lbParentNode.Name = "lbParentNode";
            lbParentNode.Size = new Size(354, 38);
            lbParentNode.TabIndex = 0;
            lbParentNode.Text = "父节点：";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(33, 69);
            label2.Name = "label2";
            label2.Size = new Size(68, 17);
            label2.TabIndex = 1;
            label2.Text = "文件夹名称";
            // 
            // tbNewName
            // 
            tbNewName.Location = new Point(111, 71);
            tbNewName.Name = "tbNewName";
            tbNewName.Size = new Size(275, 23);
            tbNewName.TabIndex = 2;
            // 
            // button2
            // 
            button2.DialogResult = DialogResult.Cancel;
            button2.Location = new Point(311, 133);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 4;
            button2.Text = "取消(&C)";
            button2.UseVisualStyleBackColor = true;
            // 
            // btOk
            // 
            btOk.Location = new Point(203, 133);
            btOk.Name = "btOk";
            btOk.Size = new Size(75, 23);
            btOk.TabIndex = 5;
            btOk.Text = "确定(&O)";
            btOk.UseVisualStyleBackColor = true;
            // 
            // DlgFolderAdd
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(428, 200);
            Controls.Add(button2);
            Controls.Add(btOk);
            Controls.Add(tbNewName);
            Controls.Add(label2);
            Controls.Add(lbParentNode);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DlgFolderAdd";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "添加文件夹";
            Load += DlgFolderAdd_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbParentNode;
        private Label label2;
        private TextBox tbNewName;
        private Button button2;
        private Button btOk;
    }
}