using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyPageViewer.Controls
{
    public partial class AttachmentItemControl : UserControl
    {
        public PageAttachment PageAttachment { get; }

        public event EventHandler AttachmentDeleted;

        public AttachmentItemControl(PageAttachment pageAttachment)
        {
            PageAttachment = pageAttachment;
            InitializeComponent();

            label1.Text = PageAttachment?.Name;


        }


        private void AttachmentItemControl_Load(object sender, EventArgs e)
        {
            btDelete.Click += BtDelete_Click;
            

        }

        private void BtDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"确定删除附件{PageAttachment.Name}?", "提示", MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Question) == DialogResult.Cancel) return;

            try
            {
                File.Delete(PageAttachment.FilePath);
                AttachmentDeleted?.Invoke(this, EventArgs.Empty);

            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Properties.Resources.Text_Error, MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
