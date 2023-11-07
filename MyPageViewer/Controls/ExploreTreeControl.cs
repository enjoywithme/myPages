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
using MyPageLib;
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
        private readonly TreeView _cacheTree = new();

        public ExploreTreeControl()
        {
            InitializeComponent();
            treeView1.ShowNodeToolTips = true;
        }
        private void ExploreTreeControl_Load(object sender, EventArgs e)
        {
            LoadFolderTree();

            cbTreeType.SelectedIndex = 0;
            cbTreeType.SelectedIndexChanged += CbTreeType_SelectedIndexChanged;
            btRefresh.Click += BtRefresh_Click;

            treeView1.AfterSelect += TreeView1_AfterSelect;
            treeView1.DragEnter += TreeView1_DragEnter;
            treeView1.DragOver += TreeView1_DragOver;
            treeView1.DragDrop += TreeView1_DragDrop;
            treeView1.AllowDrop = true;
            cbFilter.TextUpdate += ((_, _) => DisplayTree());
        }

        private void BtRefresh_Click(object sender, EventArgs e)
        {
            LoadFolderTree();
        }

        private void LoadFolderTree()
        {
            treeView1.Nodes.Clear();
            if (MyPageSettings.Instance.ScanFolders is not { Count: > 0 }) return;
            foreach (var scanFolder in MyPageSettings.Instance.ScanFolders)
            {
                LoadDirectory(scanFolder);
            }
        }

        public void GotoPath(string folderPath)
        {
            TreeNode tn = null;
            foreach (TreeNode node in treeView1.Nodes)
            {
                tn = FindNodeByPath(node, folderPath);
                if (tn != null) break;

            }

            if (tn == null) return;

            treeView1.SelectedNode = tn;
        }

        private TreeNode FindNodeByPath(TreeNode treeNode, string folderPath)
        {
            var tag = (string)treeNode.Tag;
            if (folderPath.Equals(tag, StringComparison.InvariantCultureIgnoreCase)) return treeNode;
            foreach (TreeNode tn in treeNode.Nodes)
            {
                var node = FindNodeByPath(tn, folderPath);
                if (node != null) return node;
            }

            return null;
        }


        #region piz文件拖放

        private void TreeView1_DragDrop(object sender, DragEventArgs e)
        {
            var targetPoint = treeView1.PointToClient(new Point(e.X, e.Y));

            var targetNode = treeView1.GetNodeAt(targetPoint);
            if (targetNode == null || e.Data == null || targetNode.Tag == null) return;

            var destPath = (string)targetNode.Tag;
            if (!Directory.Exists(destPath)) return;

            var fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (fileList == null) return;
            var pizFiles = fileList.Where(x => Path.GetExtension(x).ToLower() == ".piz").ToList();


            var sb = new StringBuilder();

            foreach (var pizFile in pizFiles)
            {
                if (!MyPageDb.Instance.MoveFile(pizFile, Path.Combine(destPath, Path.GetFileName(pizFile)),out var message))
                {
                    sb.AppendLine($"移动文件{pizFile}错误:\r\n{message}");

                }

            }

            var msg = sb.ToString();
            if (!string.IsNullOrEmpty(msg)) { Program.ShowError(msg); }

            NodeChanged?.Invoke(this, destPath);


        }

        private void TreeView1_DragOver(object sender, DragEventArgs e)
        {
            var targetPoint = treeView1.PointToClient(new Point(e.X, e.Y));
            treeView1.SelectedNode = treeView1.GetNodeAt(targetPoint);
        }

        private void TreeView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data != null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
                if (fileList != null && fileList.Any(x => Path.GetExtension(x).ToLower() == ".piz"))
                {
                    e.Effect = DragDropEffects.Move;
                    return;
                }
            }

            e.Effect = DragDropEffects.None;
        }


        #endregion

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var nodePath = (string)e.Node?.Tag;
            NodeChanged?.Invoke(this, nodePath);
        }

        private void CbTreeType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #region 装载文件夹
        public void LoadDirectory(string dir)
        {
            _cacheTree.Nodes.Clear();
            var di = new DirectoryInfo(dir);
            var tds = _cacheTree.Nodes.Add(di.Name);
            tds.Tag = di.FullName;
            tds.ImageIndex = 0;
            tds.StateImageIndex = 0;
            tds.ToolTipText = di.FullName;
            //LoadFiles(dir, tds);
            LoadSubDirectories(dir, tds);

            DisplayTree();
        }

        private async void DisplayTree()
        {
            async Task<bool> UserKeepsTyping()
            {
                var txt = cbFilter.Text;   // remember text
                await Task.Delay(500);        // wait some
                return txt != cbFilter.Text;  // return that text changed or not
            }
            if (await UserKeepsTyping()) return;

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
            treeView1.EndUpdate();
        }


        private void LoadFilterNode(TreeNode node)
        {
            if (node.Text.IndexOf(cbFilter.Text, StringComparison.InvariantCultureIgnoreCase) >= 0)
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
                tds.ToolTipText = di.FullName;
                //LoadFiles(subdirectory, tds);
                LoadSubDirectories(s, tds); td.EnsureVisible();
            }
        }
        #endregion



    }
}
