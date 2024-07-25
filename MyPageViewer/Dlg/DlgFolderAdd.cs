using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPageViewer.Dlg
{
    public partial class DlgFolderAdd : Form
    {
        private readonly TreeNode _node;
        private readonly string _nodeFullPath;

        public string NewNodeFullPath { get; private set; }

        public DlgFolderAdd(TreeNode node)
        {
            InitializeComponent();
            _node = node;

            _nodeFullPath = (string)node.Tag;
        }

        private void DlgFolderAdd_Load(object sender, EventArgs e)
        {
            lbParentNode.Text = string.Format(Resource.TextParentNodeName, _node.FullPath);

            btOk.Click += BtOk_Click;
        }

        private void BtOk_Click(object sender, EventArgs e)
        {
            var newName = tbNewName.Text.Trim();


            if (string.IsNullOrEmpty(newName) || newName.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
            {
                MessageBox.Show(Resource.TextIllegalPathName, Resource.TextError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var newPath = Path.Combine(_nodeFullPath, newName);
            try
            {
                Directory.CreateDirectory(newPath);
                
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resource.TextError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            NewNodeFullPath = newPath;
            DialogResult = DialogResult.OK;

        }
    }
}
