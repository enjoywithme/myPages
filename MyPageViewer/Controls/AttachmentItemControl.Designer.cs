namespace MyPageViewer.Controls
{
    partial class AttachmentItemControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            btDelete = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(129, 47);
            label1.TabIndex = 0;
            label1.Text = "label1";
            // 
            // btDelete
            // 
            btDelete.Location = new Point(138, 15);
            btDelete.Name = "btDelete";
            btDelete.Size = new Size(35, 23);
            btDelete.TabIndex = 1;
            btDelete.Text = "X";
            btDelete.UseVisualStyleBackColor = true;
            // 
            // AttachmentItemControl
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDark;
            Controls.Add(btDelete);
            Controls.Add(label1);
            Name = "AttachmentItemControl";
            Size = new Size(176, 47);
            Load += AttachmentItemControl_Load;
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Button btDelete;
    }
}
