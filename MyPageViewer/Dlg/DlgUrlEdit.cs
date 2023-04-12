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
    public partial class DlgUrlEdit : Form
    {
        public string Url { get; private set; }
        public DlgUrlEdit(string url)
        {
            Url = url;
            InitializeComponent();
        }

        private void DlgUrlEdit_Load(object sender, EventArgs e)
        {
            tbUrl.Text = !string.IsNullOrEmpty(Url) ? Url : Clipboard.GetText();
            btOk.Click += BtOk_Click;
        }

        private void BtOk_Click(object sender, EventArgs e)
        {
            Url = tbUrl.Text;
            DialogResult = DialogResult.OK;
        }
    }
}
