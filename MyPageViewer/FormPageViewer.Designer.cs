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
            tssIconLink = new ToolStripStatusLabel();
            tsAddresss = new ToolStripStatusLabel();
            tsbRate = new ToolStripDropDownButton();
            tsbRate5 = new ToolStripMenuItem();
            tsbRate4 = new ToolStripMenuItem();
            tsbRate3 = new ToolStripMenuItem();
            tsbRate2 = new ToolStripMenuItem();
            tsbRate1 = new ToolStripMenuItem();
            tsbRate0 = new ToolStripMenuItem();
            tbTitle = new TextBox();
            btAttachment = new Button();
            panel1 = new Panel();
            btDelete = new Button();
            btReloadFromTemp = new Button();
            btCleanHmtl = new Button();
            btZip = new Button();
            btTags = new Button();
            panelAttachments = new AttachmentsControl();
            toolTip1 = new ToolTip(components);
            panelRight = new Panel();
            tagsControl = new TagsControl();
            splitter1 = new Splitter();
            webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            statusStrip1.SuspendLayout();
            panel1.SuspendLayout();
            panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)webView).BeginInit();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { tssIconLink, tsAddresss, tsbRate });
            statusStrip1.Location = new Point(0, 511);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(1023, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // tssIconLink
            // 
            tssIconLink.AutoToolTip = true;
            tssIconLink.DoubleClickEnabled = true;
            tssIconLink.Image = Properties.Resources.Link24;
            tssIconLink.Name = "tssIconLink";
            tssIconLink.Size = new Size(16, 17);
            tssIconLink.ToolTipText = "双击修改URL";
            // 
            // tsAddresss
            // 
            tsAddresss.AutoToolTip = true;
            tsAddresss.DoubleClickEnabled = true;
            tsAddresss.ImageAlign = ContentAlignment.MiddleLeft;
            tsAddresss.Name = "tsAddresss";
            tsAddresss.Size = new Size(889, 17);
            tsAddresss.Spring = true;
            tsAddresss.TextAlign = ContentAlignment.MiddleLeft;
            tsAddresss.ToolTipText = "双击打开URL";
            // 
            // tsbRate
            // 
            tsbRate.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbRate.DropDownItems.AddRange(new ToolStripItem[] { tsbRate5, tsbRate4, tsbRate3, tsbRate2, tsbRate1, tsbRate0 });
            tsbRate.Image = Properties.Resources.star0;
            tsbRate.ImageScaling = ToolStripItemImageScaling.None;
            tsbRate.ImageTransparentColor = Color.Magenta;
            tsbRate.Name = "tsbRate";
            tsbRate.Size = new Size(103, 20);
            tsbRate.Text = "tsbRate";
            // 
            // tsbRate5
            // 
            tsbRate5.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbRate5.Image = Properties.Resources.star5;
            tsbRate5.ImageScaling = ToolStripItemImageScaling.None;
            tsbRate5.Name = "tsbRate5";
            tsbRate5.Size = new Size(162, 22);
            tsbRate5.Tag = "5";
            tsbRate5.Text = "r5";
            // 
            // tsbRate4
            // 
            tsbRate4.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbRate4.Image = Properties.Resources.star4;
            tsbRate4.ImageScaling = ToolStripItemImageScaling.None;
            tsbRate4.Name = "tsbRate4";
            tsbRate4.Size = new Size(162, 22);
            tsbRate4.Tag = "4";
            tsbRate4.Text = "r4";
            // 
            // tsbRate3
            // 
            tsbRate3.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbRate3.Image = Properties.Resources.star3;
            tsbRate3.ImageScaling = ToolStripItemImageScaling.None;
            tsbRate3.Name = "tsbRate3";
            tsbRate3.Size = new Size(162, 22);
            tsbRate3.Tag = "3";
            tsbRate3.Text = "r3";
            // 
            // tsbRate2
            // 
            tsbRate2.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbRate2.Image = Properties.Resources.star2;
            tsbRate2.ImageScaling = ToolStripItemImageScaling.None;
            tsbRate2.Name = "tsbRate2";
            tsbRate2.Size = new Size(162, 22);
            tsbRate2.Tag = "2";
            tsbRate2.Text = "r2";
            // 
            // tsbRate1
            // 
            tsbRate1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbRate1.Image = Properties.Resources.star1;
            tsbRate1.ImageScaling = ToolStripItemImageScaling.None;
            tsbRate1.Name = "tsbRate1";
            tsbRate1.Size = new Size(162, 22);
            tsbRate1.Tag = "1";
            tsbRate1.Text = "r1";
            // 
            // tsbRate0
            // 
            tsbRate0.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbRate0.Image = Properties.Resources.star0;
            tsbRate0.ImageScaling = ToolStripItemImageScaling.None;
            tsbRate0.Name = "tsbRate0";
            tsbRate0.Size = new Size(162, 22);
            tsbRate0.Tag = "0";
            tsbRate0.Text = "r0";
            // 
            // tbTitle
            // 
            tbTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbTitle.BackColor = SystemColors.InactiveBorder;
            tbTitle.Location = new Point(12, 20);
            tbTitle.Name = "tbTitle";
            tbTitle.Size = new Size(712, 23);
            tbTitle.TabIndex = 0;
            // 
            // btAttachment
            // 
            btAttachment.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btAttachment.Image = Properties.Resources.Attach24;
            btAttachment.Location = new Point(969, 10);
            btAttachment.Name = "btAttachment";
            btAttachment.Size = new Size(38, 43);
            btAttachment.TabIndex = 2;
            toolTip1.SetToolTip(btAttachment, "附件");
            btAttachment.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            panel1.Controls.Add(btDelete);
            panel1.Controls.Add(btReloadFromTemp);
            panel1.Controls.Add(btCleanHmtl);
            panel1.Controls.Add(btZip);
            panel1.Controls.Add(btTags);
            panel1.Controls.Add(btAttachment);
            panel1.Controls.Add(tbTitle);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1023, 62);
            panel1.TabIndex = 0;
            // 
            // btDelete
            // 
            btDelete.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btDelete.Image = Properties.Resources.Bin24;
            btDelete.Location = new Point(789, 10);
            btDelete.Name = "btDelete";
            btDelete.Size = new Size(38, 43);
            btDelete.TabIndex = 2;
            toolTip1.SetToolTip(btDelete, "删除文档");
            btDelete.UseVisualStyleBackColor = true;
            // 
            // btReloadFromTemp
            // 
            btReloadFromTemp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btReloadFromTemp.Image = Properties.Resources.Sync24;
            btReloadFromTemp.Location = new Point(744, 10);
            btReloadFromTemp.Name = "btReloadFromTemp";
            btReloadFromTemp.Size = new Size(38, 43);
            btReloadFromTemp.TabIndex = 2;
            toolTip1.SetToolTip(btReloadFromTemp, "从临时目录刷新");
            btReloadFromTemp.UseVisualStyleBackColor = true;
            // 
            // btCleanHmtl
            // 
            btCleanHmtl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btCleanHmtl.Image = Properties.Resources.Broom24;
            btCleanHmtl.Location = new Point(834, 10);
            btCleanHmtl.Name = "btCleanHmtl";
            btCleanHmtl.Size = new Size(38, 43);
            btCleanHmtl.TabIndex = 2;
            toolTip1.SetToolTip(btCleanHmtl, "净化HTML");
            btCleanHmtl.UseVisualStyleBackColor = true;
            // 
            // btZip
            // 
            btZip.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btZip.Image = Properties.Resources.ZipFolder24;
            btZip.Location = new Point(879, 10);
            btZip.Name = "btZip";
            btZip.Size = new Size(38, 43);
            btZip.TabIndex = 2;
            toolTip1.SetToolTip(btZip, "重新压制");
            btZip.UseVisualStyleBackColor = true;
            // 
            // btTags
            // 
            btTags.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btTags.Image = Properties.Resources.label24;
            btTags.Location = new Point(924, 10);
            btTags.Name = "btTags";
            btTags.Size = new Size(38, 43);
            btTags.TabIndex = 2;
            toolTip1.SetToolTip(btTags, "编辑标签");
            btTags.UseVisualStyleBackColor = true;
            // 
            // panelAttachments
            // 
            panelAttachments.BorderStyle = BorderStyle.FixedSingle;
            panelAttachments.Document = null;
            panelAttachments.Location = new Point(28, 15);
            panelAttachments.Name = "panelAttachments";
            panelAttachments.Size = new Size(181, 232);
            panelAttachments.TabIndex = 6;
            panelAttachments.Visible = false;
            // 
            // panelRight
            // 
            panelRight.Controls.Add(tagsControl);
            panelRight.Controls.Add(panelAttachments);
            panelRight.Dock = DockStyle.Right;
            panelRight.Location = new Point(788, 62);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(235, 449);
            panelRight.TabIndex = 9;
            panelRight.Visible = false;
            // 
            // tagsControl
            // 
            tagsControl.BorderStyle = BorderStyle.FixedSingle;
            tagsControl.Document = null;
            tagsControl.Location = new Point(41, 253);
            tagsControl.Name = "tagsControl";
            tagsControl.Size = new Size(112, 168);
            tagsControl.TabIndex = 7;
            // 
            // splitter1
            // 
            splitter1.Dock = DockStyle.Right;
            splitter1.Location = new Point(785, 62);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(3, 449);
            splitter1.TabIndex = 10;
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
            webView.Size = new Size(785, 449);
            webView.TabIndex = 11;
            webView.ZoomFactor = 1D;
            // 
            // FormPageViewer
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1023, 533);
            Controls.Add(webView);
            Controls.Add(splitter1);
            Controls.Add(panelRight);
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
            panelRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)webView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private StatusStrip statusStrip1;
        private TextBox tbTitle;
        private Button btAttachment;
        private Panel panel1;
        private AttachmentsControl panelAttachments;
        private ToolStripStatusLabel tsAddresss;
        private ToolTip toolTip1;
        private Button btZip;
        private Button btCleanHmtl;
        private Button btReloadFromTemp;
        private ToolStripStatusLabel tssIconLink;
        private ToolStripDropDownButton tsbRate;
        private ToolStripMenuItem tsbRate1;
        private ToolStripMenuItem tsbRate0;
        private ToolStripMenuItem tsbRate5;
        private ToolStripMenuItem tsbRate4;
        private ToolStripMenuItem tsbRate3;
        private ToolStripMenuItem tsbRate2;
        private Panel panelRight;
        private Splitter splitter1;
        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private TagsControl tagsControl;
        private Button btTags;
        private Button btDelete;
    }
}