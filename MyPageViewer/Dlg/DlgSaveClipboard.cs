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
    public partial class DlgSaveClipboard : Form
    {

        public DlgSaveClipboard()
        {
            InitializeComponent();
        }

        private void DlgSaveClipboard_Load(object sender, EventArgs e)
        {
            textBox1.Text = Clipboard.GetText(TextDataFormat.Html);
        }
    }
}
