using MyPageLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPageViewer.Dlg
{
    public partial class DlgFolderRename : Form
    {
        private TreeNode _node;
        private string _nodeFullPath;

        public string NewNodeName { get; private set; }
        public string NewFullPath { get; private set; }


        public DlgFolderRename(TreeNode node)
        {
            InitializeComponent();
            _node = node;
        }

        private void DlgFolderRename_Load(object sender, EventArgs e)
        {
            _nodeFullPath = (string) _node.Tag;
            lbNodePath.Text = $"节点路径：{_node.FullPath}";
            tbNewName.Text = _node.Text;
            
            var splits = _nodeFullPath.Split("\\");

            btOk.Click += BtOk_Click;
        }

        private void BtOk_Click(object sender, EventArgs e)
        {
            var newName = tbNewName.Text.Trim();


            if (string.IsNullOrEmpty(newName) || newName.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
            {
                MessageBox.Show("不合法的路径名称!", Resource.TextError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MyPageSettings.Instance == null)
            {
                MessageBox.Show(Resource.TextErrorSettings, Resource.TextError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var (topFolder, topFolderPath, folderPath) = MyPageSettings.Instance.ParsePath(_nodeFullPath);


            if (topFolder == null)
            {
                MessageBox.Show("不正确的顶级目录。", Resource.TextError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (folderPath == null)
            {
                MessageBox.Show("不正确的文件夹路径。", Resource.TextError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var newFolderPath = newName;
            var index = folderPath.LastIndexOf("\\", folderPath.Length, StringComparison.InvariantCultureIgnoreCase);
            if (index >= 0)
            {
                if (folderPath.Length == 1)
                {
                    MessageBox.Show("不正确的文件夹路径。", Resource.TextError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var oldName = folderPath.Substring(index+1, folderPath.Length-index-1);
                if (string.Compare(oldName, newName, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    MessageBox.Show("新名称和旧名称相同。", Resource.TextError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                newFolderPath = folderPath.Substring(0, index) + "\\" + newName;
            }
            else
            {
                if(string.Compare(folderPath, newName, StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    MessageBox.Show("新名称和旧名称相同。", Resource.TextError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            try
            {
                var newFullPath = Path.Combine(topFolderPath, newFolderPath);
                Directory.Move(_nodeFullPath,newFullPath);
                MyPageDb.Instance.ReplaceFolderPath(topFolder, folderPath, newFolderPath);
                NewNodeName = newName;
                NewFullPath = newFullPath;

                DialogResult = DialogResult.OK;

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resource.TextError, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }




        }
    }
}
