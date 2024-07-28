using MyPageLib;

namespace MyPageViewer.Dlg
{
    public partial class DlgFolderRename : Form
    {
        private readonly TreeNode _node;
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
            lbNodePath.Text = string.Format(Resource.TextFormatNodePath, _node.FullPath);
            tbNewName.Text = _node.Text;
            

            btOk.Click += BtOk_Click;
        }

        private void BtOk_Click(object sender, EventArgs e)
        {
            var newName = tbNewName.Text.Trim();


            try
            {
                if (string.IsNullOrEmpty(newName) || newName.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                    throw new Exception("不合法的路径名称!");

                if (MyPageSettings.Instance == null)
                    throw new Exception(Resource.TextErrorSettings);

                var (topFolder, topFolderPath, folderPath) = MyPageSettings.Instance.ParsePath(_nodeFullPath);
                if (topFolder == null || topFolderPath == null || folderPath == null)
                    throw new Exception($"不能正确解析目录:{_nodeFullPath}");

                var newFolderPath = newName;
                var index = folderPath.LastIndexOf("\\", folderPath.Length, StringComparison.InvariantCultureIgnoreCase);
                if (index >= 0)
                {
                    if (folderPath.Length == 1)
                    {
                        throw new Exception("不正确的文件夹路径。");
                    }

                    var oldName = folderPath.Substring(index + 1, folderPath.Length - index - 1);
                    if (string.Compare(oldName, newName, StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        throw new Exception("新名称和旧名称相同。");
                    }

                    newFolderPath = folderPath.Substring(0, index) + "\\" + newName;
                }
                else
                {
                    if (string.Compare(folderPath, newName, StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        throw new Exception("新名称和旧名称相同。");

                    }
                }

                Cursor.Current = Cursors.WaitCursor;

                var newFullPath = Path.Combine(topFolderPath, newFolderPath);
                Directory.Move(_nodeFullPath,newFullPath);
                MyPageDb.Instance.ReplaceFolderPath(topFolder, folderPath, newFolderPath);
                NewNodeName = newName;
                NewFullPath = newFullPath;
                Cursor.Current = Cursors.Default;


                DialogResult = DialogResult.OK;

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resource.TextError, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }




        }
    }
}
