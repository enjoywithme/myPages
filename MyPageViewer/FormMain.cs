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

            tsmiExit.Click += (_, _) => { Close(); };
            tsmiOptions.Click += (_, _) => { (new DlgOptions()).ShowDialog(this); };


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

            //Menu items view


            //处理命令行
            var myPageDoc = MyPageDocument.NewFromArgs(Environment.GetCommandLineArgs());

            if (myPageDoc != null)
            {
                (new FormPageViewer(myPageDoc)).Show(this);
                Hide();
            }

        }

        private void TsmiOptions_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}