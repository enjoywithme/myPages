namespace MyPageViewer
{
    partial class FormMain
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            menuStrip1 = new MenuStrip();
            文件FToolStripMenuItem = new ToolStripMenuItem();
            tsmiStartIndex = new ToolStripMenuItem();
            tsmiOptions = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripSeparator();
            tsmiExit = new ToolStripMenuItem();
            工具TToolStripMenuItem = new ToolStripMenuItem();
            tsmiPasteFromClipboard = new ToolStripMenuItem();
            视图VToolStripMenuItem = new ToolStripMenuItem();
            tsmiViewTree = new ToolStripMenuItem();
            tsmiViewPreviewPane = new ToolStripMenuItem();
            tsmiViewStatus = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            tsslInfo = new ToolStripStatusLabel();
            notifyIcon1 = new NotifyIcon(components);
            toolStrip1 = new ToolStrip();
            tsbPasteFromClipboard = new ToolStripButton();
            panelTop = new Panel();
            tbSearch = new TextBox();
            panelMain = new Panel();
            panelMiddle = new Panel();
            listView = new ListView();
            colTitle = new ColumnHeader();
            colFilePath = new ColumnHeader();
            splitterRight = new Splitter();
            panelPreview = new Panel();
            splitterLeft = new Splitter();
            panelTree = new Panel();
            naviTreeControl1 = new Controls.ExploreTreeControl();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            panelTop.SuspendLayout();
            panelMain.SuspendLayout();
            panelMiddle.SuspendLayout();
            panelTree.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { 文件FToolStripMenuItem, 工具TToolStripMenuItem, 视图VToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(939, 25);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // 文件FToolStripMenuItem
            // 
            文件FToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tsmiStartIndex, tsmiOptions, toolStripMenuItem2, tsmiExit });
            文件FToolStripMenuItem.Name = "文件FToolStripMenuItem";
            文件FToolStripMenuItem.Size = new Size(58, 21);
            文件FToolStripMenuItem.Text = "文件(&F)";
            // 
            // tsmiStartIndex
            // 
            tsmiStartIndex.Name = "tsmiStartIndex";
            tsmiStartIndex.Size = new Size(124, 22);
            tsmiStartIndex.Text = "开始索引";
            // 
            // tsmiOptions
            // 
            tsmiOptions.Name = "tsmiOptions";
            tsmiOptions.Size = new Size(124, 22);
            tsmiOptions.Text = "选项(&O)";
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(121, 6);
            // 
            // tsmiExit
            // 
            tsmiExit.Name = "tsmiExit";
            tsmiExit.Size = new Size(124, 22);
            tsmiExit.Text = "退出(&X)";
            // 
            // 工具TToolStripMenuItem
            // 
            工具TToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tsmiPasteFromClipboard });
            工具TToolStripMenuItem.Name = "工具TToolStripMenuItem";
            工具TToolStripMenuItem.Size = new Size(59, 21);
            工具TToolStripMenuItem.Text = "工具(&T)";
            // 
            // tsmiPasteFromClipboard
            // 
            tsmiPasteFromClipboard.Name = "tsmiPasteFromClipboard";
            tsmiPasteFromClipboard.Size = new Size(148, 22);
            tsmiPasteFromClipboard.Text = "从剪贴板粘贴";
            // 
            // 视图VToolStripMenuItem
            // 
            视图VToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tsmiViewTree, tsmiViewPreviewPane, tsmiViewStatus });
            视图VToolStripMenuItem.Name = "视图VToolStripMenuItem";
            视图VToolStripMenuItem.Size = new Size(60, 21);
            视图VToolStripMenuItem.Text = "视图(&V)";
            // 
            // tsmiViewTree
            // 
            tsmiViewTree.Name = "tsmiViewTree";
            tsmiViewTree.Size = new Size(127, 22);
            tsmiViewTree.Text = "浏览树(&T)";
            // 
            // tsmiViewPreviewPane
            // 
            tsmiViewPreviewPane.Name = "tsmiViewPreviewPane";
            tsmiViewPreviewPane.Size = new Size(127, 22);
            tsmiViewPreviewPane.Text = "预览(&P)";
            // 
            // tsmiViewStatus
            // 
            tsmiViewStatus.Checked = true;
            tsmiViewStatus.CheckState = CheckState.Checked;
            tsmiViewStatus.Name = "tsmiViewStatus";
            tsmiViewStatus.Size = new Size(127, 22);
            tsmiViewStatus.Text = "状态栏(&S)";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { tsslInfo });
            statusStrip1.Location = new Point(0, 580);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(939, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // tsslInfo
            // 
            tsslInfo.Name = "tsslInfo";
            tsslInfo.Size = new Size(924, 17);
            tsslInfo.Spring = true;
            tsslInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // notifyIcon1
            // 
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "My pages";
            notifyIcon1.Visible = true;
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsbPasteFromClipboard });
            toolStrip1.Location = new Point(0, 25);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(939, 25);
            toolStrip1.TabIndex = 4;
            toolStrip1.Text = "toolStrip1";
            // 
            // tsbPasteFromClipboard
            // 
            tsbPasteFromClipboard.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbPasteFromClipboard.Image = Properties.Resources.Editpaste16;
            tsbPasteFromClipboard.ImageTransparentColor = Color.Magenta;
            tsbPasteFromClipboard.Name = "tsbPasteFromClipboard";
            tsbPasteFromClipboard.Size = new Size(23, 22);
            tsbPasteFromClipboard.Text = "从剪贴板粘贴";
            // 
            // panelTop
            // 
            panelTop.Controls.Add(tbSearch);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 50);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(939, 28);
            panelTop.TabIndex = 5;
            // 
            // tbSearch
            // 
            tbSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbSearch.Location = new Point(4, 3);
            tbSearch.Name = "tbSearch";
            tbSearch.Size = new Size(1671, 23);
            tbSearch.TabIndex = 2;
            // 
            // panelMain
            // 
            panelMain.Controls.Add(panelMiddle);
            panelMain.Controls.Add(splitterRight);
            panelMain.Controls.Add(panelPreview);
            panelMain.Controls.Add(splitterLeft);
            panelMain.Controls.Add(panelTree);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 78);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(939, 502);
            panelMain.TabIndex = 6;
            // 
            // panelMiddle
            // 
            panelMiddle.Controls.Add(listView);
            panelMiddle.Dock = DockStyle.Fill;
            panelMiddle.Location = new Point(224, 0);
            panelMiddle.Name = "panelMiddle";
            panelMiddle.Size = new Size(538, 502);
            panelMiddle.TabIndex = 12;
            // 
            // listView
            // 
            listView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView.Columns.AddRange(new ColumnHeader[] { colTitle, colFilePath });
            listView.FullRowSelect = true;
            listView.Location = new Point(0, 4);
            listView.Name = "listView";
            listView.Size = new Size(538, 495);
            listView.TabIndex = 3;
            listView.UseCompatibleStateImageBehavior = false;
            listView.View = View.Details;
            // 
            // colTitle
            // 
            colTitle.Text = "名称";
            colTitle.Width = 300;
            // 
            // colFilePath
            // 
            colFilePath.Text = "路径";
            colFilePath.Width = 300;
            // 
            // splitterRight
            // 
            splitterRight.Dock = DockStyle.Right;
            splitterRight.Location = new Point(762, 0);
            splitterRight.Name = "splitterRight";
            splitterRight.Size = new Size(3, 502);
            splitterRight.TabIndex = 11;
            splitterRight.TabStop = false;
            // 
            // panelPreview
            // 
            panelPreview.Dock = DockStyle.Right;
            panelPreview.Location = new Point(765, 0);
            panelPreview.Name = "panelPreview";
            panelPreview.Size = new Size(174, 502);
            panelPreview.TabIndex = 10;
            panelPreview.Visible = false;
            // 
            // splitterLeft
            // 
            splitterLeft.Location = new Point(221, 0);
            splitterLeft.Name = "splitterLeft";
            splitterLeft.Size = new Size(3, 502);
            splitterLeft.TabIndex = 9;
            splitterLeft.TabStop = false;
            // 
            // panelTree
            // 
            panelTree.Controls.Add(naviTreeControl1);
            panelTree.Dock = DockStyle.Left;
            panelTree.Location = new Point(0, 0);
            panelTree.Name = "panelTree";
            panelTree.Size = new Size(221, 502);
            panelTree.TabIndex = 8;
            panelTree.Visible = false;
            // 
            // naviTreeControl1
            // 
            naviTreeControl1.Dock = DockStyle.Fill;
            naviTreeControl1.Location = new Point(0, 0);
            naviTreeControl1.Name = "naviTreeControl1";
            naviTreeControl1.Size = new Size(221, 502);
            naviTreeControl1.TabIndex = 0;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(939, 602);
            Controls.Add(panelMain);
            Controls.Add(panelTop);
            Controls.Add(toolStrip1);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "FormMain";
            Text = "My pages";
            Load += FormMain_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelMain.ResumeLayout(false);
            panelMiddle.ResumeLayout(false);
            panelTree.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem 文件FToolStripMenuItem;
        private ToolStripMenuItem tsmiExit;
        private StatusStrip statusStrip1;
        private ToolStripMenuItem 视图VToolStripMenuItem;
        private ToolStripMenuItem tsmiViewTree;
        private ToolStripMenuItem tsmiViewPreviewPane;
        private ToolStripMenuItem tsmiViewStatus;
        private ToolStripMenuItem tsmiOptions;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem tsmiStartIndex;
        private NotifyIcon notifyIcon1;
        private ToolStripStatusLabel tsslInfo;
        private ToolStripMenuItem 工具TToolStripMenuItem;
        private ToolStripMenuItem tsmiPasteFromClipboard;
        private ToolStrip toolStrip1;
        private ToolStripButton tsbPasteFromClipboard;
        private Panel panelTop;
        private TextBox tbSearch;
        private Panel panelMain;
        private Panel panelMiddle;
        private ListView listView;
        private ColumnHeader colTitle;
        private ColumnHeader colFilePath;
        private Splitter splitterRight;
        private Panel panelPreview;
        private Splitter splitterLeft;
        private Panel panelTree;
        private Controls.ExploreTreeControl naviTreeControl1;
    }
}