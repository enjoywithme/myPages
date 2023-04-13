namespace MyPageViewer
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //Menu items view
            tsmiViewTree.Click += (_, _) =>
            {
                panelTree.Visible = !panelTree.Visible;
                tsmiViewTree.Checked = panelTree.Visible;
            };

            tsmiViewPreviewPane.Click += (_, _) =>
            {
                panelPreview.Visible = !panelPreview.Visible;
                tsmiViewPreviewPane.Checked = panelPreview.Visible;
            };
            tsmiViewStatus.Click += (_, _) =>
            {
                statusStrip1.Visible = !statusStrip1.Visible;
                tsmiViewStatus.Checked = statusStrip1.Visible;
            };
        }
    }
}