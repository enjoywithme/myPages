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
    public partial class DlgTagAdd : Form
    {
        public string TagText { get; private set; }
        public DlgTagAdd()
        {
            InitializeComponent();
        }

        private void DlgUrlEdit_Load(object sender, EventArgs e)
        {
            cbTag.Text = Clipboard.GetText();
            btOk.Click += BtOk_Click;
            cbTag.KeyDown += CbTag_KeyDown;
        }

        private void CbTag_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter) { btOk.PerformClick();}
        }

        private void BtOk_Click(object sender, EventArgs e)
        {
            TagText = cbTag.Text.Trim();

            if (string.IsNullOrEmpty(TagText)) return;
            DialogResult = DialogResult.OK;
        }
    }
}
