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

        private void AddAttachmentItemControl(PageAttachment attachment)
        {

            var attachmentControl = new AttachmentItemControl(attachment);
            attachmentControl.AttachmentDeleted += AttachmentControl_AttachmentDeleted;
            flowLayoutPanel1.Controls.Add(attachmentControl);

        }

        private void AttachmentControl_AttachmentDeleted(object sender, EventArgs e)
        {
            if (sender is AttachmentItemControl control)
            {
                flowLayoutPanel1.Controls.Remove(control);
            }
            Document.SetModified();

        }

        public void LoadAttachments()
        {
            flowLayoutPanel1.Controls.Clear();

            var attachments = PageAttachment.CheckAttachmentsFromTempPath(_document);
            if (attachments == null)
            {
                return;

            }

            foreach (var attachment in attachments)
            {
                AddAttachmentItemControl(attachment);
            }
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            LoadAttachments();
        }

        private void AttachmentsControl_Load(object sender, EventArgs e)
        {
            btAdd.Click += BtAdd_Click;
        }

        private OpenFileDialog _openFileDialog;
        private void BtAdd_Click(object sender, EventArgs e)
        {
            _openFileDialog ??= new OpenFileDialog();
            if(_openFileDialog.ShowDialog() == DialogResult.Cancel || string.IsNullOrEmpty(_openFileDialog.FileName)) return;

            try
            {
                if (!Directory.Exists(Document.TempAttachmentsPath))
                    Directory.CreateDirectory(Document.TempAttachmentsPath);

                var destFile = Path.Combine(Document.TempAttachmentsPath, Path.GetFileName(_openFileDialog.FileName));

                File.Copy(_openFileDialog.FileName, destFile,true);

                var attachment = new PageAttachment(Document) { FilePath = destFile };
                AddAttachmentItemControl(attachment);
                Document.SetModified();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Properties.Resources.Text_Error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            
        }
    }
}
