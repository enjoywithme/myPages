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
            视图VToolStripMenuItem = new ToolStripMenuItem();
            tsmiViewTree = new ToolStripMenuItem();
            tsmiViewPreviewPane = new ToolStripMenuItem();
            tsmiViewStatus = new ToolStripMenuItem();
            panelTop = new Panel();
            tbSearch = new TextBox();
            statusStrip1 = new StatusStrip();
            tsslInfo = new ToolStripStatusLabel();
            panelPreview = new Panel();
            splitter1 = new Splitter();
            panelTree = new Panel();
            naviTreeControl1 = new Controls.ExploreTreeControl();
            splitter2 = new Splitter();
            panelMiddle = new Panel();
            listView = new ListView();
            colTitle = new ColumnHeader();
            colFilePath = new ColumnHeader();
            notifyIcon1 = new NotifyIcon(components);
            menuStrip1.SuspendLayout();
            panelTop.SuspendLayout();
            statusStrip1.SuspendLayout();
            panelTree.SuspendLayout();
            panelMiddle.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { 文件FToolStripMenuItem, 视图VToolStripMenuItem });
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
            // panelTop
            // 
            panelTop.Controls.Add(tbSearch);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 25);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(939, 28);
            panelTop.TabIndex = 2;
            // 
            // tbSearch
            // 
            tbSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbSearch.Location = new Point(4, 3);
            tbSearch.Name = "tbSearch";
            tbSearch.Size = new Size(932, 23);
            tbSearch.TabIndex = 2;
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
            tsslInfo.Size = new Size(893, 17);
            tsslInfo.Spring = true;
            tsslInfo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelPreview
            // 
            panelPreview.Dock = DockStyle.Right;
            panelPreview.Location = new Point(765, 53);
            panelPreview.Name = "panelPreview";
            panelPreview.Size = new Size(174, 527);
            panelPreview.TabIndex = 4;
            panelPreview.Visible = false;
            // 
            // splitter1
            // 
            splitter1.Dock = DockStyle.Right;
            splitter1.Location = new Point(762, 53);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(3, 527);
            splitter1.TabIndex = 5;
            splitter1.TabStop = false;
            // 
            // panelTree
            // 
            panelTree.Controls.Add(naviTreeControl1);
            panelTree.Dock = DockStyle.Left;
            panelTree.Location = new Point(0, 53);
            panelTree.Name = "panelTree";
            panelTree.Size = new Size(221, 527);
            panelTree.TabIndex = 7;
            panelTree.Visible = false;
            // 
            // naviTreeControl1
            // 
            naviTreeControl1.Dock = DockStyle.Fill;
            naviTreeControl1.Location = new Point(0, 0);
            naviTreeControl1.Name = "naviTreeControl1";
            naviTreeControl1.Size = new Size(221, 527);
            naviTreeControl1.TabIndex = 0;
            // 
            // splitter2
            // 
            splitter2.Location = new Point(221, 53);
            splitter2.Name = "splitter2";
            splitter2.Size = new Size(3, 527);
            splitter2.TabIndex = 8;
            splitter2.TabStop = false;
            // 
            // panelMiddle
            // 
            panelMiddle.Controls.Add(listView);
            panelMiddle.Dock = DockStyle.Fill;
            panelMiddle.Location = new Point(224, 53);
            panelMiddle.Name = "panelMiddle";
            panelMiddle.Size = new Size(538, 527);
            panelMiddle.TabIndex = 9;
            // 
            // listView
            // 
            listView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView.Columns.AddRange(new ColumnHeader[] { colTitle, colFilePath });
            listView.FullRowSelect = true;
            listView.Location = new Point(3, 4);
            listView.Name = "listView";
            listView.Size = new Size(535, 520);
            listView.TabIndex = 0;
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
            // notifyIcon1
            // 
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "My pages";
            notifyIcon1.Visible = true;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(939, 602);
            Controls.Add(panelMiddle);
            Controls.Add(splitter2);
            Controls.Add(panelTree);
            Controls.Add(splitter1);
            Controls.Add(panelPreview);
            Controls.Add(statusStrip1);
            Controls.Add(panelTop);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "FormMain";
            Text = "My pages";
            Load += FormMain_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            panelTree.ResumeLayout(false);
            panelMiddle.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem 文件FToolStripMenuItem;
        private ToolStripMenuItem tsmiExit;
        private Panel panelTop;
        private TextBox tbSearch;
        private StatusStrip statusStrip1;
        private Panel panelPreview;
        private Splitter splitter1;
        private Panel panelTree;
        private Splitter splitter2;
        private Panel panelMiddle;
        private ListView listView;
        private ToolStripMenuItem 视图VToolStripMenuItem;
        private ToolStripMenuItem tsmiViewTree;
        private ToolStripMenuItem tsmiViewPreviewPane;
        private ToolStripMenuItem tsmiViewStatus;
        private ToolStripMenuItem tsmiOptions;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem tsmiStartIndex;
        private ColumnHeader colTitle;
        private ColumnHeader colFilePath;
        private NotifyIcon notifyIcon1;
        private Controls.ExploreTreeControl naviTreeControl1;
        private ToolStripStatusLabel tsslInfo;
    }
}