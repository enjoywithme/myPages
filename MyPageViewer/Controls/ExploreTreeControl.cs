using MyPageLib;
using mySharedLib;
using System.Diagnostics;
using System.Windows.Forms;
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
        private const string TreeNodeTypeName = "System.Windows.Forms.TreeNode";

        public event EventHandler<string> NodeChanged;
        private readonly TreeView _cacheTree = new();
        private string _nodeFilter;

        public TreeNode SelectedNode => treeView1.SelectedNode;

        public ExploreTreeControl()
        {
            InitializeComponent();
            treeView1.ShowNodeToolTips = true;
        }
        private void ExploreTreeControl_Load(object sender, EventArgs e)
        {
            ReloadFolderTree();

            btRefresh.Click += BtRefresh_Click;

            treeView1.AfterSelect += TreeView1_AfterSelect;
            treeView1.NodeMouseClick += (_, args) => treeView1.SelectedNode = args.Node;
            treeView1.DragEnter += TreeView1_DragEnter;
            treeView1.DragOver += TreeView1_DragOver;
            treeView1.DragDrop += TreeView1_DragDrop;
            treeView1.ItemDrag += TreeView1_ItemDrag;
            treeView1.AllowDrop = true;


            cbFilter.TextUpdate += (_, _) => DisplayTree();
        }



        private void BtRefresh_Click(object sender, EventArgs e)
        {
            ReloadFolderTree();
        }


        /// <summary>
        /// 选中代表路径的节点
        /// </summary>
        /// <param name="folderPath">实际路径，包括根目录</param>
        /// <param name="topNode">开始搜索的顶级目录，默认节点的顶级目录</param>
        public void GotoPath(string folderPath,TreeNode topNode=null)
        {
            TreeNode tn = null;
            var nodes = topNode == null ? treeView1.Nodes : topNode.Nodes;
            foreach (TreeNode node in nodes)
            {
                tn = FindNodeByPath(node, folderPath);
                if (tn != null) break;

            }

            if (tn == null) return;

            treeView1.SelectedNode = tn;
        }

        /// <summary>
        /// 根据节点路径递归查找节点
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="folderPath"></param>
        /// <returns></returns>
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

        
        #region 数据拖放

        /// <summary>
        /// 发起节点拖放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Item is not TreeNode node) return;
            
            node.ImageIndex = 2;
            var ret = treeView1.DoDragDrop(node, DragDropEffects.Move);
            if (ret != DragDropEffects.Move)
            {
                node.ImageIndex = 0;
                return;
            }

            //重新装载原节点的父节点
            var origParentNode = node.Parent;
            if (origParentNode == null) return;
            ReloadNode(origParentNode);

            //选中父节点
            treeView1.SelectedNode = origParentNode;
        }

        private void TreeView1_DragDrop(object sender, DragEventArgs e)
        {
            var targetPoint = treeView1.PointToClient(new Point(e.X, e.Y));

            var targetNode = treeView1.GetNodeAt(targetPoint);
            if (targetNode == null || e.Data == null || targetNode.Tag == null) return;

            var destPath = (string)targetNode.Tag;
            if (!Directory.Exists(destPath)) return;

            if (DoDragDropFile(e, destPath))
                return;

            DoDragDropNode(e,targetNode,destPath);

        }

        /// <summary>
        /// 放下文件
        /// </summary>
        /// <param name="e"></param>
        /// <param name="destPath"></param>
        /// <returns></returns>
        private bool DoDragDropFile(DragEventArgs e,string destPath)
        {

            if (e.Data==null || !e.Data.GetDataPresent(DataFormats.FileDrop)) return false;


            var fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (fileList == null) return true;
            var pizFiles = fileList.Where(x => Path.GetExtension(x).ToLower() == ".piz").ToList();


            var sb = new FuncResult();

            foreach (var pizFile in pizFiles)
            {
                var ret = MyPageDb.Instance.MoveFile(pizFile, Path.Combine(destPath, Path.GetFileName(pizFile)));
                if (!ret)
                    sb.False($"移动文件{pizFile}错误:\r\n{ret.Message}");
            }

            if (!sb) { MessageBox.Show(sb.Message); }

            NodeChanged?.Invoke(this, destPath);

            return true;
        }


        /// <summary>
        /// 放下节点
        /// </summary>
        /// <param name="e"></param>
        /// <param name="targetNode"></param>
        /// <param name="destFullPath"></param>
        private void DoDragDropNode(DragEventArgs e, TreeNode targetNode, string destFullPath)
        {
            if (e.Data == null || !e.Data.GetDataPresent(TreeNodeTypeName, false)) return;

            var itemNode = (TreeNode)e.Data.GetData(TreeNodeTypeName);
            var origFullPath = (string)itemNode?.Tag;
            if(origFullPath == null) return;

            var ret = MessageBox.Show(string.Format(Resource.TextConfirmMoveFolderNode, origFullPath, destFullPath), Resource.TextHint,
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (ret == DialogResult.No)
            {
                e.Effect = DragDropEffects.None;
                return;
            }


            try
            {
                if (MyPageSettings.Instance == null)
                    throw new Exception(Resource.TextErrorSettings);

                var origFolderName = Path.GetFileName(origFullPath);
                var (destTopFolder, destTopFolderPath, destFolderPath) = MyPageSettings.Instance.ParsePath(destFullPath);
                if (destTopFolder == null || destFolderPath == null || destTopFolderPath == null)
                    throw new Exception($"不能正确解析目录:{destFullPath}");

                var (topFolder, topFolderPath, folderPath) = MyPageSettings.Instance.ParsePath(origFullPath);
                if (topFolder == null || topFolderPath==null||folderPath==null)
                    throw new Exception($"不能正确解析目录:{origFullPath}");

                var newFolderPath = destFolderPath + "\\" + origFolderName;
                var newFullPath = Path.Combine(destTopFolderPath, newFolderPath);

                if (Directory.Exists(newFullPath))
                    throw new Exception($"文件夹\r\n{newFullPath}\r\n已经存在！");

                Cursor.Current = Cursors.WaitCursor;

                Directory.Move(origFullPath, newFullPath);
                MyPageDb.Instance.ReplaceFolderPath(topFolder, folderPath, newFolderPath, destTopFolder);

                //重新目标装载节点
                ReloadNode(targetNode);

                //选中新节点
                GotoPath(newFullPath,targetNode);

                Cursor.Current = Cursors.Default;

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resource.TextError, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void TreeView1_DragOver(object sender, DragEventArgs e)
        {
            var targetPoint = treeView1.PointToClient(new Point(e.X, e.Y));
            var node = treeView1.GetNodeAt(targetPoint);
            if(node==null) return;
            treeView1.SelectedNode = node;

            if (CheckIfCanDropFile(e))
                return;

            CheckIfCanDropNode(e,node);

        }

        private void TreeView1_DragEnter(object sender, DragEventArgs e)
        {
            var targetPoint = treeView1.PointToClient(new Point(e.X, e.Y));
            var node = treeView1.GetNodeAt(targetPoint);
            if (node == null) return;

            if (CheckIfCanDropFile(e))
                return;

            CheckIfCanDropNode(e, node);

        }


        private bool CheckIfCanDropFile(DragEventArgs e)
        {

            if (e.Data==null || !e.Data.GetDataPresent(DataFormats.FileDrop)) return false;

            var fileList = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if (fileList != null && fileList.Any(x => Path.GetExtension(x).ToLower() == ".piz"))
            {
                e.Effect = DragDropEffects.Move;
                return true;
            }

            return true;
        }


        /// <summary>
        /// 检查是否能够放下节点对象
        /// </summary>
        /// <param name="e"></param>
        /// <param name="targetNode"></param>
        /// <returns></returns>
        private void CheckIfCanDropNode(DragEventArgs e, TreeNode targetNode)
        {
            if (e.Data==null || !e.Data.GetDataPresent(TreeNodeTypeName, false)) return;


            var itemNode = (TreeNode)e.Data.GetData(TreeNodeTypeName);

            if (targetNode == null || itemNode == null)
            {
                e.Effect = DragDropEffects.None;
                return;
            }

            var targetFullPath = (string)targetNode.Tag;
            var origFullPath = (string)itemNode.Tag;

            Debug.WriteLine($"1-{targetFullPath}\r\n2-{origFullPath}");

            var isSubFolder = IsSubDirectoryOf(targetFullPath, origFullPath);
            if (isSubFolder is null or true
                || string.Compare(targetFullPath, origFullPath, StringComparison.InvariantCultureIgnoreCase) == 0
               )
            {
                e.Effect = DragDropEffects.None;
                return;
            }


            e.Effect = DragDropEffects.Move;
        }

        /// <summary>
        /// 判断路径是否是另外一个子目录的
        /// </summary>
        /// <param name="candidate"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool? IsSubDirectoryOf(string candidate, string other)
        {
            var isChild = false;
            try
            {
                var candidateInfo = new DirectoryInfo(candidate);
                var otherInfo = new DirectoryInfo(other);

                while (candidateInfo.Parent != null)
                {
                    if (candidateInfo.Parent.FullName == otherInfo.FullName)
                    {
                        isChild = true;
                        break;
                    }

                    candidateInfo = candidateInfo.Parent;
                }
            }
            catch (Exception)
            {
                return null;
            }

            return isChild;
        }

        #endregion

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var nodePath = (string)e.Node?.Tag;
            NodeChanged?.Invoke(this, nodePath);
        }


        #region 装载文件夹

        /// <summary>
        /// 重新装载树节点
        /// </summary>
        public void ReloadFolderTree()
        {
            if (MyPageSettings.Instance?.TopFolders is not { Count: > 0 }) return;
            _cacheTree.Nodes.Clear();

            foreach (var scanFolder in MyPageSettings.Instance.TopFolders)
            {
                LoadDirectory(scanFolder.Value);
            }
        }


        /// <summary>
        /// 装载顶级目录
        /// </summary>
        /// <param name="dir"></param>
        private void LoadDirectory(string dir)
        {
            var di = new DirectoryInfo(dir);
            var tds = _cacheTree.Nodes.Add(di.Name);
            tds.Tag = di.FullName;
            tds.ImageIndex = 0;
            tds.StateImageIndex = 0;
            tds.ToolTipText = di.FullName;
            LoadSubDirectories(dir, tds);

            FilterTree();
        }

        /// <summary>
        /// 重新装载某个节点
        /// </summary>
        /// <param name="node"></param>
        private void ReloadNode(TreeNode node)
        {
            if (node?.Tag is not string nodeFullPath) return;

            TreeNode cacheNode = null;
            //在cache中搜索
            foreach (TreeNode cachedTopNode in _cacheTree.Nodes)
            {
                cacheNode = FindNodeByPath(cachedTopNode,nodeFullPath);
                if(cacheNode != null) break;
            }
            if(cacheNode == null) return;

            cacheNode.Nodes.Clear();
            LoadSubDirectories(nodeFullPath,cacheNode);

            //装载到实际节点
            var treeView = node.TreeView;
            treeView.BeginUpdate();
            node.Nodes.Clear();

            foreach (TreeNode nodeNode in cacheNode.Nodes)
            {
                node.Nodes.Add((TreeNode)nodeNode.Clone());
            }

            treeView.EndUpdate();
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
                LoadSubDirectories(s, tds); td.EnsureVisible();
            }
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
            _nodeFilter = cbFilter.Text;

            FilterTree();

        }



        private void FilterTree()
        {
            //blocks repainting tree till all objects loaded
            treeView1.BeginUpdate();

            treeView1.Nodes.Clear();
            if (!string.IsNullOrEmpty(_nodeFilter))
            {
                foreach (TreeNode parentNode in _cacheTree.Nodes)
                {
                    LoadFilterNode(parentNode);
                }
            }
            else
            {
                foreach (TreeNode node in _cacheTree.Nodes)
                {
                    treeView1.Nodes.Add((TreeNode)node.Clone());
                }
            }

            //展开第一级节点
            foreach (TreeNode treeNode in treeView1.Nodes)
            {
                treeNode.Expand();
            }


            //enables redrawing tree after all objects have been added
            treeView1.EndUpdate();
        }


        private void LoadFilterNode(TreeNode node)
        {
            if (node.Text.IndexOf(_nodeFilter, StringComparison.InvariantCultureIgnoreCase) >= 0)
                treeView1.Nodes.Add((TreeNode)node.Clone());

            foreach (TreeNode treeNode in node.Nodes)
            {
                LoadFilterNode(treeNode);
            }

        }


        #endregion



    }
}
