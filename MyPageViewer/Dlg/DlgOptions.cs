using MyPageLib;

namespace MyPageViewer.Dlg
{
    public partial class DlgOptions : Form
    {
        public DlgOptions()
        {
            InitializeComponent();
        }

        private void DlgOptions_Load(object sender, EventArgs e)
        {
            if (MyPageSettings.Instance == null)
                throw new Exception("Invalid myPage settings.");

            tbWorkingDir.Text = MyPageSettings.Instance.WorkingDirectory;


            rbNoAutoScan.Checked = !MyPageSettings.Instance.AutoIndex;
            rbScanInterval.Checked = MyPageSettings.Instance.AutoIndex;
            tbAutoIndexInterval.Text = MyPageSettings.Instance.AutoIndexInterval.ToString();
            cbAutoIndexUnit.SelectedIndex = MyPageSettings.Instance.AutoIndexIntervalUnit;

            cbEnableFullText.Checked = MyPageSettings.Instance.EnableFullTextIndex;
            tbMeilisearchAddress.Text = MyPageSettings.Instance.MeilisearchServer;
            tbMeilisearchMasterKey.Text = MyPageSettings.Instance.MeilisearchMasterKey;

            listScanFolders.Items.Clear();
            if (MyPageSettings.Instance?.TopFolders != null)
            {
                foreach (var folder in MyPageSettings.Instance.TopFolders)
                {
                    listScanFolders.Items.Add(folder.Value);
                }
            }



            btSetWorkingDir.Click += BtSetWorkingDir_Click;
            btAddScanFolder.Click += BtAddScanFolder_Click;
            btRemoveScanFolder.Click += BtRemoveScanFolder_Click;
            btlClearIndex.Click += BtlClearIndex_Click;
            btOk.Click += BtOk_Click;
        }

        private async void BtlClearIndex_Click(object sender, EventArgs e)
        {
            var ret = await MyPageIndexer.Instance.ClearMeiliIndex(tbMeilisearchAddress.Text,
                tbMeilisearchMasterKey.Text);
            if(ret)
                Program.ShowWarning("全文索引被成功删除。");
            else
                Program.ShowError($"删除全文索引失败:\r\n{ret.Message}");
            
        }

        private void BtOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbWorkingDir.Text) || !Directory.Exists(tbWorkingDir.Text))
            {
                Program.ShowWarning("选择一个有效的工作目录！");
                tabPageIndex.Select();
                tbWorkingDir.SelectAll();
                tbWorkingDir.Focus();
                return;
            }

            if (MyPageSettings.Instance == null)
                throw new Exception("Invalid myPage settings.");

            MyPageSettings.Instance.AutoIndex = rbScanInterval.Checked;
            if (MyPageSettings.Instance.AutoIndex)
            {
                if (!int.TryParse(tbAutoIndexInterval.Text, out var interval) || interval <= 0)
                {
                    Program.ShowWarning("不正确的索引周期！");
                    tabPageIndex.Select();

                    tbAutoIndexInterval.SelectAll();
                    tbAutoIndexInterval.Focus();
                    return;

                }
                MyPageSettings.Instance.AutoIndexInterval = interval;
                MyPageSettings.Instance.AutoIndexIntervalUnit = cbAutoIndexUnit.SelectedIndex;
            }

            MyPageSettings.Instance.WorkingDirectory = tbWorkingDir.Text;
            if (!MyPageSettings.Instance.InitTopFolder(listScanFolders.Items.Cast<string>().ToList(), out var message))
            {
                Program.ShowWarning(message);
                return;

            }

            //full text search
            if (cbEnableFullText.Checked)
            {
                if (string.IsNullOrEmpty(tbMeilisearchAddress.Text) ||
                    !Uri.IsWellFormedUriString(tbMeilisearchAddress.Text, UriKind.Absolute))
                {
                    Program.ShowWarning("Meilisearch服务地址无效。");
                    return;
                }

                MyPageSettings.Instance.MeilisearchServer = tbMeilisearchAddress.Text;
                MyPageSettings.Instance.MeilisearchMasterKey = tbMeilisearchMasterKey.Text;
                MyPageSettings.Instance.EnableFullTextIndex = true;
            }
            else
                MyPageSettings.Instance.EnableFullTextIndex = false;


            MyPageSettings.Instance.Save(out _, true);

            DialogResult = DialogResult.OK;
        }

        private void BtRemoveScanFolder_Click(object sender, EventArgs e)
        {
            if (listScanFolders.SelectedIndex == -1) return;
            listScanFolders.Items.RemoveAt(listScanFolders.SelectedIndex);
        }

        private void BtAddScanFolder_Click(object sender, EventArgs e)
        {
            _folderBrowserDialog ??= new FolderBrowserDialog();
            if (_folderBrowserDialog.ShowDialog(this) == DialogResult.Cancel) return;

            var folders = listScanFolders.Items.Cast<string>();
            if (folders.Any(x => x == _folderBrowserDialog.SelectedPath)) return;
            listScanFolders.Items.Add(_folderBrowserDialog.SelectedPath);
        }

        private FolderBrowserDialog _folderBrowserDialog;
        private void BtSetWorkingDir_Click(object sender, EventArgs e)
        {
            _folderBrowserDialog ??= new FolderBrowserDialog();
            if (_folderBrowserDialog.ShowDialog(this) == DialogResult.Cancel) return;
            tbWorkingDir.Text = _folderBrowserDialog.SelectedPath;
        }


    }
}
