using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyPageViewer.Model;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            var di = new DirectoryInfo(dir);
            var tds = treeView1.Nodes.Add(di.Name);
            tds.Tag = di.FullName;
            tds.ImageIndex = 0;
            tds.StateImageIndex = 0;
            //LoadFiles(dir, tds);
            LoadSubDirectories(dir, tds);

        }

        private void LoadSubDirectories(string dir, TreeNode td)
        {
            // Get all subdirectories
            var subdirectories = Directory.GetDirectories(dir);
            foreach (var s in subdirectories)
            {
                var di = new DirectoryInfo(s);
                var tds = td.Nodes.Add(di.Name);
                tds.StateImageIndex = 0;
                tds.Tag = di.FullName;
                //LoadFiles(subdirectory, tds);
                LoadSubDirectories(s, tds);
            }
        }

        private void LoadFiles(string dir, TreeNode td)
        {
            var files = Directory.GetFiles(dir, "*.*");
            // Loop through them to see files
            foreach (var file in files)
            {
                var fi = new FileInfo(file);
                var tds = td.Nodes.Add(fi.Name);
                tds.Tag = fi.FullName;
                tds.StateImageIndex = 1;
            }
        }


    }
}
