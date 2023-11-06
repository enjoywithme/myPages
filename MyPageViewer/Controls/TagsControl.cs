using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyPageLib;
using MyPageViewer.Dlg;

namespace MyPageViewer.Controls
{
    public partial class TagsControl : UserControl
    {
        public MyPageDocument Document { get; set; }
        public TagsControl()
        {
            InitializeComponent();
        }

        public void LoadTags()
        {
            listTags.Items.Clear();

            var tags = Document?.Tags;
            if (tags == null) return;
            foreach (var tag in tags)
            {
                listTags.Items.Add(tag);
            }
        }

        private void TagsControl_Load(object sender, EventArgs e)
        {
            btAdd.Click += BtAdd_Click;
            btDelete.Click += BtDelete_Click;
        }

        private void BtDelete_Click(object sender, EventArgs e)
        {
            if (listTags.CheckedItems.Count == 0) return;

            for (var i = listTags.Items.Count - 1; i >= 0; i--)
            {
                if (!listTags.GetItemChecked(i)) continue;
                Document.RemoveTag((string)listTags.Items[i]);
                listTags.Items.RemoveAt(i);
            }
        }

        private void BtAdd_Click(object sender, EventArgs e)
        {
            var dlgTag = new DlgTagAdd();
            if (dlgTag.ShowDialog() == DialogResult.Cancel) return;

            var splits = dlgTag.TagText.Split(new[]{ "、","/","\\" },StringSplitOptions.RemoveEmptyEntries);
            if(splits.Length == 0) return;
            foreach (var split in splits)
            {
                if(string.IsNullOrWhiteSpace(split)) continue;
                if (!Document.AddTag(split)) continue;

                listTags.Items.Add(split);
            }

            

        }
    }
}
