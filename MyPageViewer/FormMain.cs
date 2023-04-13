using MyPageViewer.Dlg;
using MyPageViewer.Model;

namespace MyPageViewer
{
    public partial class FormMain : Form
    {
        public static FormMain Instance { get; } = new FormMain();

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

            #region 主菜单

            tsmiStartIndex.Click += (_, _) => { PageIndexer.Instance.Start(); };
            tsmiExit.Click += (_, _) => { Close(); };
            tsmiOptions.Click += (_, _) => { (new DlgOptions()).ShowDialog(this); };

            //Menu items view
            panelTree.Visible = tsmiViewTree.Checked = MyPageSettings.Instance.ViewTree;

            tsmiViewTree.Click += (_, _) =>
            {
                MyPageSettings.Instance.ViewTree = !MyPageSettings.Instance.ViewTree;
                panelTree.Visible = tsmiViewTree.Checked = MyPageSettings.Instance.ViewTree;
            };
            panelPreview.Visible = tsmiViewPreviewPane.Checked = MyPageSettings.Instance.ViewPreview;
            tsmiViewPreviewPane.Click += (_, _) =>
            {
                MyPageSettings.Instance.ViewPreview = !MyPageSettings.Instance.ViewPreview;

                panelPreview.Visible = tsmiViewPreviewPane.Checked = MyPageSettings.Instance.ViewPreview;
            };
            tsmiViewStatus.Click += (_, _) =>
            {
                statusStrip1.Visible = !statusStrip1.Visible;
                tsmiViewStatus.Checked = statusStrip1.Visible;
            };


            #endregion


            //查询
            tbSearch.TextChanged += TbSearch_TextChanged;


            //处理命令行
            var myPageDoc = MyPageDocument.NewFromArgs(Environment.GetCommandLineArgs());

            if (myPageDoc != null)
            {
                (new FormPageViewer(myPageDoc)).Show(this);
                Hide();
            }

        }

        private CancellationTokenSource _searchCancellationTokenSource;
        private async void TbSearch_TextChanged(object sender, EventArgs e)
        {
            _searchCancellationTokenSource?.Cancel();

            _searchCancellationTokenSource = new CancellationTokenSource();
            var items = await MyPageDb.Instance.Search(tbSearch.Text,_searchCancellationTokenSource.Token);
            if (items == null) {return;}
            listView.Items.Clear();
            foreach (var poCo in items)
            {
                var listViewItem = new ListViewItem(poCo.Title, 0);
                listViewItem.SubItems.Add(poCo.FilePath);
                listView.Items.Add(listViewItem);
            }
        }


    }
}