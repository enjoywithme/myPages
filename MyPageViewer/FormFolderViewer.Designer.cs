namespace MyPageViewer
{
    partial class FormFolderViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFolderViewer));
            toolStrip1 = new ToolStrip();
            toolStripSeparator1 = new ToolStripSeparator();
            tsbLast100Items = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            tsbLastDay1 = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            tsbLast2Days = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            tsbGotoDocFolder = new ToolStripButton();
            tsbDelete = new ToolStripButton();
            tbSearch = new TextBox();
            panelMain = new Panel();
            panelMiddle = new Panel();
            listView = new ListView();
            colTitle = new ColumnHeader();
            colName = new ColumnHeader();
            colSize = new ColumnHeader();
            colDtModified = new ColumnHeader();
            colFilePath = new ColumnHeader();
            splitterRight = new Splitter();
            splitterLeft = new Splitter();
            panelTree = new Panel();
            naviTreeControl1 = new Controls.ExploreTreeControl();
            toolStrip1.SuspendLayout();
            panelMain.SuspendLayout();
            panelMiddle.SuspendLayout();
            panelTree.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(24, 24);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripSeparator1, tsbLast100Items, toolStripSeparator2, tsbLastDay1, toolStripSeparator3, tsbLast2Days, toolStripSeparator4, tsbGotoDocFolder, tsbDelete });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(946, 31);
            toolStrip1.TabIndex = 5;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 31);
            // 
            // tsbLast100Items
            // 
            tsbLast100Items.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbLast100Items.Image = (Image)resources.GetObject("tsbLast100Items.Image");
            tsbLast100Items.ImageTransparentColor = Color.Magenta;
            tsbLast100Items.Name = "tsbLast100Items";
            tsbLast100Items.Size = new Size(69, 28);
            tsbLast100Items.Text = "最新100条";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 31);
            // 
            // tsbLastDay1
            // 
            tsbLastDay1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbLastDay1.Image = (Image)resources.GetObject("tsbLastDay1.Image");
            tsbLastDay1.ImageTransparentColor = Color.Magenta;
            tsbLastDay1.Name = "tsbLastDay1";
            tsbLastDay1.Size = new Size(55, 28);
            tsbLastDay1.Text = "最近1天";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 31);
            // 
            // tsbLast2Days
            // 
            tsbLast2Days.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsbLast2Days.Image = (Image)resources.GetObject("tsbLast2Days.Image");
            tsbLast2Days.ImageTransparentColor = Color.Magenta;
            tsbLast2Days.Name = "tsbLast2Days";
            tsbLast2Days.Size = new Size(55, 28);
            tsbLast2Days.Text = "最近2天";
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(6, 31);
            // 
            // tsbGotoDocFolder
            // 
            tsbGotoDocFolder.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbGotoDocFolder.Image = Properties.Resources.Folder_go24;
            tsbGotoDocFolder.ImageTransparentColor = Color.Magenta;
            tsbGotoDocFolder.Name = "tsbGotoDocFolder";
            tsbGotoDocFolder.Size = new Size(28, 28);
            tsbGotoDocFolder.Text = "定位目录";
            // 
            // tsbDelete
            // 
            tsbDelete.DisplayStyle = ToolStripItemDisplayStyle.Image;
            tsbDelete.Image = Properties.Resources.Cross24;
            tsbDelete.ImageTransparentColor = Color.Magenta;
            tsbDelete.Name = "tsbDelete";
            tsbDelete.Size = new Size(28, 28);
            tsbDelete.Text = "删除";
            // 
            // tbSearch
            // 
            tbSearch.Dock = DockStyle.Top;
            tbSearch.Location = new Point(0, 31);
            tbSearch.Name = "tbSearch";
            tbSearch.Size = new Size(946, 23);
            tbSearch.TabIndex = 6;
            // 
            // panelMain
            // 
            panelMain.Controls.Add(panelMiddle);
            panelMain.Controls.Add(splitterRight);
            panelMain.Controls.Add(splitterLeft);
            panelMain.Controls.Add(panelTree);
            panelMain.Dock = DockStyle.Fill;
            panelMain.Location = new Point(0, 54);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(946, 521);
            panelMain.TabIndex = 7;
            // 
            // panelMiddle
            // 
            panelMiddle.Controls.Add(listView);
            panelMiddle.Dock = DockStyle.Fill;
            panelMiddle.Location = new Point(224, 0);
            panelMiddle.Name = "panelMiddle";
            panelMiddle.Size = new Size(719, 521);
            panelMiddle.TabIndex = 12;
            // 
            // listView
            // 
            listView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView.Columns.AddRange(new ColumnHeader[] { colTitle, colName, colSize, colDtModified, colFilePath });
            listView.FullRowSelect = true;
            listView.Location = new Point(0, 4);
            listView.Name = "listView";
            listView.Size = new Size(722, 514);
            listView.TabIndex = 3;
            listView.UseCompatibleStateImageBehavior = false;
            listView.View = View.Details;
            // 
            // colTitle
            // 
            colTitle.Text = "标题";
            colTitle.Width = 330;
            // 
            // colName
            // 
            colName.Text = "名称";
            // 
            // colSize
            // 
            colSize.Text = "大小";
            colSize.Width = 90;
            // 
            // colDtModified
            // 
            colDtModified.Text = "修改时间";
            colDtModified.Width = 140;
            // 
            // colFilePath
            // 
            colFilePath.Text = "路径";
            colFilePath.Width = 300;
            // 
            // splitterRight
            // 
            splitterRight.Dock = DockStyle.Right;
            splitterRight.Location = new Point(943, 0);
            splitterRight.Name = "splitterRight";
            splitterRight.Size = new Size(3, 521);
            splitterRight.TabIndex = 11;
            splitterRight.TabStop = false;
            // 
            // splitterLeft
            // 
            splitterLeft.Location = new Point(221, 0);
            splitterLeft.Name = "splitterLeft";
            splitterLeft.Size = new Size(3, 521);
            splitterLeft.TabIndex = 9;
            splitterLeft.TabStop = false;
            // 
            // panelTree
            // 
            panelTree.Controls.Add(naviTreeControl1);
            panelTree.Dock = DockStyle.Left;
            panelTree.Location = new Point(0, 0);
            panelTree.Name = "panelTree";
            panelTree.Size = new Size(221, 521);
            panelTree.TabIndex = 8;
            panelTree.Visible = false;
            // 
            // naviTreeControl1
            // 
            naviTreeControl1.Dock = DockStyle.Fill;
            naviTreeControl1.Location = new Point(0, 0);
            naviTreeControl1.Name = "naviTreeControl1";
            naviTreeControl1.Size = new Size(221, 521);
            naviTreeControl1.TabIndex = 0;
            // 
            // FormFolderViewer
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(946, 575);
            Controls.Add(panelMain);
            Controls.Add(tbSearch);
            Controls.Add(toolStrip1);
            Name = "FormFolderViewer";
            Text = "文件夹";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panelMain.ResumeLayout(false);
            panelMiddle.ResumeLayout(false);
            panelTree.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton tsbLast100Items;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton tsbLastDay1;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton tsbLast2Days;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton tsbGotoDocFolder;
        private ToolStripButton tsbDelete;
        private TextBox tbSearch;
        private Panel panelMain;
        private Panel panelMiddle;
        private ListView listView;
        private ColumnHeader colTitle;
        private ColumnHeader colName;
        private ColumnHeader colSize;
        private ColumnHeader colDtModified;
        private ColumnHeader colFilePath;
        private Splitter splitterRight;
        private Splitter splitterLeft;
        private Panel panelTree;
        private Controls.ExploreTreeControl naviTreeControl1;
    }
}