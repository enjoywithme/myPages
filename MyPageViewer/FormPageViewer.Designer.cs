using MyPageViewer.Controls;

namespace MyPageViewer
{
    partial class FormPageViewer
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPageViewer));
            statusStrip1 = new StatusStrip();
            tsAddresss = new ToolStripStatusLabel();
            textBox1 = new TextBox();
            button1 = new Button();
            btAttachment = new Button();
            panel1 = new Panel();
            panelAttachments = new AttachmentsControl();
            splitter1 = new Splitter();
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            toolTip1 = new ToolTip(components);
            statusStrip1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView).BeginInit();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { tsAddresss });
            statusStrip1.Location = new Point(0, 688);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1023, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // tsAddresss
            // 
            tsAddresss.Image = Properties.Resources.Link24;
            tsAddresss.ImageAlign = ContentAlignment.MiddleLeft;
            tsAddresss.Name = "tsAddresss";
            tsAddresss.Size = new Size(1008, 17);
            tsAddresss.Spring = true;
            tsAddresss.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(381, 23);
            textBox1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(442, 13);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // btAttachment
            // 
            btAttachment.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btAttachment.Image = Properties.Resources.Attach24;
            btAttachment.Location = new Point(973, 10);
            btAttachment.Name = "btAttachment";
            btAttachment.Size = new Size(38, 43);
            btAttachment.TabIndex = 2;
            toolTip1.SetToolTip(btAttachment, "附件");
            btAttachment.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(btAttachment);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(textBox1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1023, 62);
            panel1.TabIndex = 0;
            // 
            // panelAttachments
            // 
            panelAttachments.BorderStyle = BorderStyle.FixedSingle;
            panelAttachments.Dock = DockStyle.Right;
            panelAttachments.Document = null;
            panelAttachments.Location = new Point(792, 62);
            panelAttachments.Name = "panelAttachments";
            panelAttachments.Size = new Size(231, 626);
            panelAttachments.TabIndex = 6;
            panelAttachments.Visible = false;
            // 
            // splitter1
            // 
            splitter1.Dock = DockStyle.Right;
            splitter1.Location = new Point(789, 62);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(3, 626);
            splitter1.TabIndex = 7;
            splitter1.TabStop = false;
            // 
            // webView
            // 
            webView.AllowExternalDrop = true;
            webView.CreationProperties = null;
            webView.DefaultBackgroundColor = Color.White;
            webView.Dock = DockStyle.Fill;
            webView.Location = new Point(0, 62);
            webView.Name = "webView";
            webView.Size = new Size(789, 626);
            webView.TabIndex = 8;
            webView.ZoomFactor = 1D;
            // 
            // FormPageViewer
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1023, 710);
            Controls.Add(webView);
            Controls.Add(splitter1);
            Controls.Add(panelAttachments);
            Controls.Add(statusStrip1);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormPageViewer";
            Text = "FormPageViewer";
            WindowState = FormWindowState.Maximized;
            Load += FormPageViewer_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)webView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip statusStrip1;
        private TextBox textBox1;
        private Button button1;
        private Button btAttachment;
        private Panel panel1;
        private AttachmentsControl panelAttachments;
        private Splitter splitter1;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private ToolStripStatusLabel tsAddresss;
        private ToolTip toolTip1;
    }
}