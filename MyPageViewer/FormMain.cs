using MyPageViewer.Dlg;
using MyPageViewer.Model;
using mySharedLib;
using System.IO.MemoryMappedFiles;
using System.Diagnostics;

namespace MyPageViewer
{
    public partial class FormMain : Form
    {

        public static FormMain Instance { get; set; }
        private readonly MyPageDocument _startDocument;


        public FormMain(MyPageDocument startDocument)
        {
            InitializeComponent();
            _startDocument = startDocument;
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
                tsmiViewTree.Checked = MyPageSettings.Instance.ViewTree;
                panelTree.Visible = MyPageSettings.Instance.ViewTree;
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
            tbSearch.KeyDown += TbSearch_KeyDown;
            listView.MouseDoubleClick += ListView_MouseDoubleClick;

            //Tree
            naviTreeControl1.NodeChanged += NaviTreeControl1_NodeChanged;

            //Toolbar
            tsbPasteFromClipboard.Click += (_, _) => { PasteFromClipboard(); };
            tsmiPasteFromClipboard.Click += (_, _) => { PasteFromClipboard(); };

            //list view
            listView.SelectedIndexChanged += ListView_SelectedIndexChanged;
            listView.ItemDrag += ListView_ItemDrag;

            Resize += FormMain_Resize;
            notifyIcon1.MouseClick += (_, _) => ShowWindow();
            notifyIcon1.MouseDoubleClick += (_, _) => ShowWindow();

            //处理命令行
            if (_startDocument == null) return;
            (new FormPageViewer(_startDocument)).Show(this);
            Hide();
        }



        private void ListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshStatusInfo();
        }

        private void RefreshStatusInfo()
        {
            if (listView.SelectedIndices.Count == 0)
            {
                tsslInfo.Text = $"共 {listView.Items.Count} 个对象。";
                return;
            }
        }

        private void NaviTreeControl1_NodeChanged(object sender, string e)
        {
            listView.Items.Clear();

            if (Directory.Exists(e))
            {
                var files = Directory.EnumerateFiles(e, "*.piz");
                foreach (var file in files)
                {
                    var listViewItem = new ListViewItem(Path.GetFileNameWithoutExtension(file), 0)
                    {
                        Tag = file
                    };
                    listViewItem.SubItems.Add(file);
                    listView.Items.Add(listViewItem);
                }
                RefreshStatusInfo();

            }
        }

        private void OpenFilePath(string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath)) return;
            var doc = new MyPageDocument(filePath);
            var form = new FormPageViewer(doc);
            form.Show();
            WinApi.ShowToFront(form.Handle);

            form.WindowState = FormWindowState.Maximized;

        }

        #region List view动作
        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var info = ((ListView)sender).HitTest(e.X, e.Y);
            if (info.Item == null) return;

            var filePath = info.Item.SubItems[1].Text;
            OpenFilePath(filePath);
        }
        private void ListView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (listView.SelectedIndices.Count == 0) return;

            listView.DoDragDrop(new DataObject(DataFormats.FileDrop,
                (from ListViewItem item in listView.SelectedItems where item.Tag != null select (string)item.Tag).ToArray()),
                DragDropEffects.Move);
        }
        #endregion

        #region 搜索
        private void TbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
                DoSearch();
        }

        private async void DoSearch()
        {
            _searchCancellationTokenSource?.Cancel();

            _searchCancellationTokenSource = new CancellationTokenSource();
            var items = await MyPageDb.Instance.Search(tbSearch.Text, _searchCancellationTokenSource.Token);
            if (items == null) { return; }
            listView.BeginUpdate();
            listView.Items.Clear();
            foreach (var poCo in items)
            {
                var listViewItem = new ListViewItem(poCo.Title, 0)
                {
                    Tag = poCo.FilePath
                };
                listViewItem.SubItems.Add(poCo.FilePath);
                listView.Items.Add(listViewItem);
            }
            listView.EndUpdate();
            RefreshStatusInfo();
        }


        private CancellationTokenSource _searchCancellationTokenSource;
        private async void TbSearch_TextChanged(object sender, EventArgs e)
        {

            async Task<bool> UserKeepsTyping()
            {
                var txt = tbSearch.Text;   // remember text
                await Task.Delay(500);        // wait some
                return txt != tbSearch.Text;  // return that text changed or not
            }
            if (await UserKeepsTyping()) return;

            DoSearch();
        }
        #endregion

        #region 操作

        private void PasteFromClipboard()
        {
            if (!Clipboard.ContainsText(TextDataFormat.Html)) return;
            var dlg = new DlgSaveClipboard();
            dlg.ShowDialog(this);
        }

        #endregion

        protected override void WndProc(ref Message message)
        {
            if (message.Msg == SingleInstance.WM_SHOWFIRSTINSTANCE)
            {
                if (message.LParam != IntPtr.Zero)
                {
                    try
                    {
                        var mmf = MemoryMappedFile.CreateOrOpen(SingleInstance.Instance.MmfName, SingleInstance.MmfLength, MemoryMappedFileAccess.ReadWrite);
                        var n = (int)message.LParam;
                        var accessor = mmf.CreateViewAccessor(0, SingleInstance.MmfLength);
                        var bytes = new byte[n];
                        var c = accessor.ReadArray(0, bytes, 0, n);
                        var s = System.Text.Encoding.Default.GetString(bytes, 0, c);

                        OpenFilePath(s);

                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message);
                    }
                }
                else
                    ShowWindow();


            }

            base.WndProc(ref message);
        }

        #region 窗口控制

        public void ShowWindow()
        {
            // Insert code here to make your form show itself.
            WinApi.ShowToFront(this.Handle);
        }

        void MinimizeToTray()
        {
            notifyIcon1.Visible = true;
            //WindowState = FormWindowState.Minimized;
            Hide();
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                MinimizeToTray();
            }
        }

        #endregion


    }
}