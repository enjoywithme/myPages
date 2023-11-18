namespace MyPageViewer.Controls
{
    partial class ExploreTreeControl
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExploreTreeControl));
            tableLayoutPanel1 = new TableLayoutPanel();
            label1 = new Label();
            label2 = new Label();
            cbTreeType = new ComboBox();
            treeView1 = new TreeView();
            imageList1 = new ImageList(components);
            panel1 = new Panel();
            cbFilter = new ComboBox();
            btRefresh = new Button();
            tableLayoutPanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 63F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(label1, 0, 0);
            tableLayoutPanel1.Controls.Add(label2, 0, 1);
            tableLayoutPanel1.Controls.Add(cbTreeType, 1, 0);
            tableLayoutPanel1.Controls.Add(treeView1, 0, 2);
            tableLayoutPanel1.Controls.Add(panel1, 1, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 31F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(308, 523);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(3, 3);
            label1.Margin = new Padding(3);
            label1.Name = "label1";
            label1.Size = new Size(57, 24);
            label1.TabIndex = 0;
            label1.Text = "树类型";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(3, 33);
            label2.Margin = new Padding(3);
            label2.Name = "label2";
            label2.Size = new Size(57, 25);
            label2.TabIndex = 1;
            label2.Text = "过滤";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // cbTreeType
            // 
            cbTreeType.Dock = DockStyle.Fill;
            cbTreeType.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTreeType.FormattingEnabled = true;
            cbTreeType.Items.AddRange(new object[] { "文件夹", "标签" });
            cbTreeType.Location = new Point(66, 3);
            cbTreeType.Name = "cbTreeType";
            cbTreeType.Size = new Size(239, 25);
            cbTreeType.TabIndex = 2;
            // 
            // treeView1
            // 
            tableLayoutPanel1.SetColumnSpan(treeView1, 2);
            treeView1.Dock = DockStyle.Fill;
            treeView1.ImageIndex = 0;
            treeView1.ImageList = imageList1;
            treeView1.Location = new Point(3, 64);
            treeView1.Name = "treeView1";
            treeView1.SelectedImageIndex = 1;
            treeView1.Size = new Size(302, 456);
            treeView1.TabIndex = 3;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth32Bit;
            imageList1.ImageStream = (ImageListStreamer)resources.GetObject("imageList1.ImageStream");
            imageList1.TransparentColor = Color.Transparent;
            imageList1.Images.SetKeyName(0, "Folder16.png");
            imageList1.Images.SetKeyName(1, "Folder-go16.png");
            // 
            // panel1
            // 
            panel1.Controls.Add(cbFilter);
            panel1.Controls.Add(btRefresh);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(66, 33);
            panel1.Name = "panel1";
            panel1.Size = new Size(239, 25);
            panel1.TabIndex = 4;
            // 
            // cbFilter
            // 
            cbFilter.Dock = DockStyle.Fill;
            cbFilter.FormattingEnabled = true;
            cbFilter.Location = new Point(0, 0);
            cbFilter.Margin = new Padding(0);
            cbFilter.Name = "cbFilter";
            cbFilter.Size = new Size(187, 25);
            cbFilter.TabIndex = 5;
            // 
            // btRefresh
            // 
            btRefresh.Dock = DockStyle.Right;
            btRefresh.Location = new Point(187, 0);
            btRefresh.Name = "btRefresh";
            btRefresh.Size = new Size(52, 25);
            btRefresh.TabIndex = 4;
            btRefresh.Text = "刷新";
            btRefresh.UseVisualStyleBackColor = true;
            // 
            // ExploreTreeControl
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tableLayoutPanel1);
            Name = "ExploreTreeControl";
            Size = new Size(308, 523);
            Load += ExploreTreeControl_Load;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label2;
        private ComboBox cbTreeType;
        private TreeView treeView1;
        private ImageList imageList1;
        private Panel panel1;
        private Button btRefresh;
        private ComboBox cbFilter;
    }
}
