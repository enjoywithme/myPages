using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyPageViewer.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using TreeView = System.Windows.Forms.TreeView;

namespace MyPageViewer.Controls
{
    public enum ExploreTreeType
    {
        Folder = 0,
        Tag = 1
    }

    public partial class ExploreTreeControl : UserControl
    {
        public ExploreTreeType TreeType => cbTreeType.SelectedIndex == 0 ? ExploreTreeType.Folder : (ExploreTreeType)Tag;

        public event EventHandler<string> NodeChanged;
        private readonly TreeView _cacheTree = new TreeView();

        public ExploreTreeControl()
        {
            InitializeComponent();
        }
        private void ExploreTreeControl_Load(object sender, EventArgs e)
        {
            if (MyPageSettings.Instance.ScanFolders is { Count: > 0 })
            {
                LoadDirectory(MyPageSettings.Instance.ScanFolders[0]);
            }

            cbTreeType.SelectedIndex = 0;
            cbTreeType.SelectedIndexChanged += CbTreeType_SelectedIndexChanged;

            treeView1.AfterSelect += TreeView1_AfterSelect;
            cbFilter.TextUpdate += ((_, _) => DisplayTree());
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var nodePath = (string)e.Node?.Tag;
            NodeChanged?.Invoke(this, nodePath);
        }

        private void CbTreeType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void LoadDirectory(string dir)
        {
            _cacheTree.Nodes.Clear();
            var di = new DirectoryInfo(dir);
            var tds = _cacheTree.Nodes.Add(di.Name);
            tds.Tag = di.FullName;
            tds.ImageIndex = 0;
            tds.StateImageIndex = 0;
            //LoadFiles(dir, tds);
            LoadSubDirectories(dir, tds);

            DisplayTree();
        }

        private void DisplayTree()
        {
            //blocks repainting tree till all objects loaded
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();
            if (cbFilter.Text != string.Empty)
            {
                foreach (TreeNode parentNode in _cacheTree.Nodes)
                {
                    LoadFilterNode(parentNode);
                }
            }
            else
            {
                foreach (TreeNode node in this._cacheTree.Nodes)
                {
                    treeView1.Nodes.Add((TreeNode)node.Clone());
                }
            }
            //enables redrawing tree after all objects have been added
            this.treeView1.EndUpdate();
        }


        private void LoadFilterNode(TreeNode node)
        {
            if (node.Text.Contains(cbFilter.Text))
                treeView1.Nodes.Add((TreeNode)node.Clone());
            foreach (TreeNode treeNode in node.Nodes)
            {
                LoadFilterNode(treeNode);
            }
        }


        private void LoadSubDirectories(string dir, TreeNode td)
        {
            var subdirectories = Directory.GetDirectories(dir);
            foreach (var s in subdirectories)
            {
                var di = new DirectoryInfo(s);
                var tds = td.Nodes.Add(di.Name);
                tds.StateImageIndex = 0;
                tds.Tag = di.FullName;
                //LoadFiles(subdirectory, tds);
                LoadSubDirectories(s, tds); td.EnsureVisible();
            }
        }



    }
}
