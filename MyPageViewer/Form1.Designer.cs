namespace MyPageViewer
{
    partial class Form1
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
            panelMain = new Panel();
            panelMiddle = new Panel();
            splitterRight = new Splitter();
            panelPreview = new Panel();
            splitterLeft = new Splitter();
            panelTree = new Panel();
            naviTreeControl1 = new Controls.ExploreTreeControl();
            listView = new ListView();
            colTitle = new ColumnHeader();
            colFilePath = new ColumnHeader();
            panelMain.SuspendLayout();
            panelMiddle.SuspendLayout();
            panelTree.SuspendLayout();
            SuspendLayout();
            // 
            // panelMain
            // 
            panelMain.Controls.Add(panelMiddle);
            panelMain.Controls.Add(splitterRight);
            panelMain.Controls.Add(panelPreview);
            panelMain.Controls.Add(splitterLeft);
            panelMain.Controls.Add(panelTree);
            panelMain.Location = new Point(54, 31);
            panelMain.Name = "panelMain";
            panelMain.Size = new Size(1029, 667);
            panelMain.TabIndex = 0;
            // 
            // panelMiddle
            // 
            panelMiddle.Controls.Add(listView);
            panelMiddle.Dock = DockStyle.Fill;
            panelMiddle.Location = new Point(224, 0);
            panelMiddle.Name = "panelMiddle";
            panelMiddle.Size = new Size(628, 667);
            panelMiddle.TabIndex = 12;
            // 
            // splitterRight
            // 
            splitterRight.Dock = DockStyle.Right;
            splitterRight.Location = new Point(852, 0);
            splitterRight.Name = "splitterRight";
            splitterRight.Size = new Size(3, 667);
            splitterRight.TabIndex = 11;
            splitterRight.TabStop = false;
            // 
            // panelPreview
            // 
            panelPreview.Dock = DockStyle.Right;
            panelPreview.Location = new Point(855, 0);
            panelPreview.Name = "panelPreview";
            panelPreview.Size = new Size(174, 667);
            panelPreview.TabIndex = 10;
            panelPreview.Visible = false;
            // 
            // splitterLeft
            // 
            splitterLeft.Location = new Point(221, 0);
            splitterLeft.Name = "splitterLeft";
            splitterLeft.Size = new Size(3, 667);
            splitterLeft.TabIndex = 9;
            splitterLeft.TabStop = false;
            // 
            // panelTree
            // 
            panelTree.Controls.Add(naviTreeControl1);
            panelTree.Dock = DockStyle.Left;
            panelTree.Location = new Point(0, 0);
            panelTree.Name = "panelTree";
            panelTree.Size = new Size(221, 667);
            panelTree.TabIndex = 8;
            panelTree.Visible = false;
            // 
            // naviTreeControl1
            // 
            naviTreeControl1.Dock = DockStyle.Fill;
            naviTreeControl1.Location = new Point(0, 0);
            naviTreeControl1.Name = "naviTreeControl1";
            naviTreeControl1.Size = new Size(221, 667);
            naviTreeControl1.TabIndex = 0;
            // 
            // listView
            // 
            listView.Columns.AddRange(new ColumnHeader[] { colTitle, colFilePath });
            listView.FullRowSelect = true;
            listView.Location = new Point(6, 12);
            listView.Name = "listView";
            listView.Size = new Size(481, 525);
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1217, 791);
            Controls.Add(panelMain);
            Name = "Form1";
            Text = "Form1";
            panelMain.ResumeLayout(false);
            panelMiddle.ResumeLayout(false);
            panelTree.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMain;
        private Panel panelTree;
        private Controls.ExploreTreeControl naviTreeControl1;
        private Splitter splitterLeft;
        private Splitter splitterRight;
        private Panel panelPreview;
        private Panel panelMiddle;
        private ListView listView;
        private ColumnHeader colTitle;
        private ColumnHeader colFilePath;
    }
}