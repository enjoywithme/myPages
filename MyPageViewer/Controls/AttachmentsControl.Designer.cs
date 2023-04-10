namespace MyPageViewer.Controls
{
    partial class AttachmentsControl
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
            btAdd = new Button();
            button1 = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            btRefresh = new Button();
            SuspendLayout();
            // 
            // btAdd
            // 
            btAdd.BackColor = SystemColors.ButtonHighlight;
            btAdd.Location = new Point(34, 39);
            btAdd.Name = "btAdd";
            btAdd.Size = new Size(75, 23);
            btAdd.TabIndex = 0;
            btAdd.Text = "添加附件";
            btAdd.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            button1.BackColor = Color.CornflowerBlue;
            button1.Location = new Point(139, 39);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "下载全部";
            button1.UseVisualStyleBackColor = false;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Location = new Point(12, 85);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(267, 390);
            flowLayoutPanel1.TabIndex = 1;
            // 
            // btRefresh
            // 
            btRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btRefresh.Image = Properties.Resources.Sub_blue_rotate_cw24;
            btRefresh.Location = new Point(241, 3);
            btRefresh.Name = "btRefresh";
            btRefresh.Size = new Size(38, 33);
            btRefresh.TabIndex = 2;
            btRefresh.UseVisualStyleBackColor = true;
            btRefresh.Click += btRefresh_Click;
            // 
            // AttachmentsControl
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(btRefresh);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(button1);
            Controls.Add(btAdd);
            Name = "AttachmentsControl";
            Size = new Size(290, 484);
            Load += AttachmentsControl_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button btAdd;
        private Button button1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button btRefresh;
    }
}
