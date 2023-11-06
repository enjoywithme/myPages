using MyPageLib;

namespace MyPageViewer.Controls
{
    public partial class AttachmentsControl : UserControl
    {
        private MyPageDocument _document;

        public AttachmentsControl()
        {
            InitializeComponent();
        }

        public MyPageDocument Document
        {
            get => _document;
            set
            {
                _document = value;
                LoadAttachments();
            }
        }


        public void LoadAttachments()
        {
            listAttachments.Items.Clear();
            var attachments = PageAttachment.CheckAttachmentsFromTempPath(_document);
            if (attachments == null)
            {
                return;
            }

            foreach (var attachment in attachments)
            {
                listAttachments.Items.Add(attachment);
            }
        }


        private void AttachmentsControl_Load(object sender, EventArgs e)
        {
            btAdd.Click += BtAdd_Click;
            btDelete.Click += BtDelete_Click;
            btRefresh.Click += (_, _) => LoadAttachments();
            btSaveAll.Click += BtSaveAll_Click;
        }

        private FolderBrowserDialog _folderBrowserDialog;
        private void BtSaveAll_Click(object sender, EventArgs e)
        {
            _folderBrowserDialog ??= new FolderBrowserDialog();
            if (_folderBrowserDialog.ShowDialog() == DialogResult.Cancel) return;

            var n = 0;
            try
            {
                foreach (var item in listAttachments.Items)
                {
                    var attachment = (PageAttachment)item;

                    var dest = Path.Combine(_folderBrowserDialog.SelectedPath, attachment.Name);
                    File.Copy(attachment.FilePath, dest, true);
                    n++;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Properties.Resources.Text_Error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"保存了{n}个附件！", Properties.Resources.Text_Hint, MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtDelete_Click(object sender, EventArgs e)
        {
            if (listAttachments.CheckedItems.Count == 0) return;

            if (MessageBox.Show(Properties.Resources.Text_ConfirmAttachmentsDelete, Properties.Resources.Text_Hint, MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.Cancel) return;

            try
            {
                for (var i = listAttachments.Items.Count - 1; i >= 0; i--)
                {
                    if (!listAttachments.GetItemChecked(i)) continue;
                    var attachment = (PageAttachment)listAttachments.Items[i];
                    File.Delete(attachment.FilePath);
                    listAttachments.Items.RemoveAt(i);
                    Document.SetModified();
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Properties.Resources.Text_Error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }

        private OpenFileDialog _openFileDialog;
        private void BtAdd_Click(object sender, EventArgs e)
        {
            _openFileDialog ??= new OpenFileDialog();
            _openFileDialog.Multiselect = true;
            if (_openFileDialog.ShowDialog() == DialogResult.Cancel) return;

            try
            {
                if (!Directory.Exists(Document.TempAttachmentsPath))
                    Directory.CreateDirectory(Document.TempAttachmentsPath);

                foreach (var fileName in _openFileDialog.FileNames)
                {
                    var destFile = Path.Combine(Document.TempAttachmentsPath, Path.GetFileName(fileName));

                    File.Copy(fileName, destFile, true);

                    var attachment = new PageAttachment(Document) { FilePath = destFile };
                    listAttachments.Items.Add(attachment);
                    Document.SetModified();
                }
                
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Properties.Resources.Text_Error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }

        }



    }
}
