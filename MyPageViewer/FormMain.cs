﻿#nullable enable
using MyPageLib;
using MyPageViewer.Dlg;
using mySharedLib;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using MyPageLib.PoCo;
using MyPageViewer.Properties;

namespace MyPageViewer
{

    public partial class FormMain : Form
    {

        public static FormMain? Instance { get; set; }
        private readonly MyPageDocument? _startDocument;
        private static System.Threading.Timer? _autoIndexTimer;
        public static int InstanceCounter { get; protected set; }
        private readonly int _instanceIndex;
        public bool IsMain => _instanceIndex == 0;

        private PageDocumentPoCo? _gotoDocumentPoCo;
        private string? _lastMessage;

        public static FormMain CreateForm(MyPageDocument? startDocument = null)
        {
            var form = new FormMain(InstanceCounter, startDocument);
            InstanceCounter += 1;
            return form;
        }

        protected FormMain(int instanceIndex, MyPageDocument? startDocument = null)
        {
            InitializeComponent();
            _instanceIndex = instanceIndex;
            _startDocument = startDocument;
        }

        private void FormMain_Load(object? sender, EventArgs? e)
        {
            if (_instanceIndex > 0)
                Text = string.Format(Resource.TextSubWindowTitle, _instanceIndex);

            if (IsMain)
            {
                MyPageIndexer.Instance.IndexStopped += Instance_IndexStopped;
                MyPageIndexer.Instance.IndexFileChanged += Instance_IndexFileChanged;

                tsbStartIndex.Click += (_, _) => { StartIndex(); };
                tsbCleanDb.Click += (_, _) => { StartClean(); };
                tsbStop.Click += (_, _) => { StopAction(); };
            }
            else
            {
                tsbStartIndex.Visible = false;
                tsbStop.Visible = false;
                tsbCleanDb.Visible =false;

                tsmiStartIndex.Visible = false;
                tsmiStop.Visible =false;
                tsmiTools.Visible =false;
            }

            #region 主菜单

            tsmiStartIndex.Click += (_, _) => { StartIndex(); };
            tsmiStop.Click += (_, _) => { StopAction(); };

            tsmiExit.Click += (_, _) => { Close(); };
            tsmiOptions.Click += (_, _) =>
            {
                var ret = (new DlgOptions()).ShowDialog(this);
                if (ret != DialogResult.OK) return;
                StartAutoIndexTimer();
            };

            //Menu items view
            if (MyPageSettings.Instance != null)
            {
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
            }

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

            //Tree view menu
            treeViewMenu.Opening += TreeViewMenu_Opening;
            tsmiRenameFolder.Click += TsmiRenameFolder_Click;
            tsmiOpenFolderPath.Click += TsmiOpenFolderPath_Click;
            tsmiAddFolder.Click += TsmiAddFolder_Click;
            tsmiDeleteFolder.Click += TsmiDeleteFolder_Click;
            tsmiOpenInNewWindow.Click += TsmiOpenInNewWindow_Click;
            tsmiRefreshFolder.Click += TsmiRefreshFolder_Click;
            tsmiIndexFolder.Click += TsmiIndexFolder_Click;

            //工具栏


            tsbGotoDocFolder.Click += TsbGotoDocFolder_Click;
            tsmiPasteFromClipboard.Click += (_, _) => { PasteFromClipboard(); };
            tsbLast100Items.Click += (_, _) =>
            {
                FillListView(MyPageDb.Instance.FindLastN(100));
                RefreshStatusInfo();
            };
            tsbLastDay1.Click += (_, _) =>
            {
                FillListView(MyPageDb.Instance.FindLastDays());
                RefreshStatusInfo();
            };
            tsbLast2Days.Click += (_, _) =>
            {
                FillListView(MyPageDb.Instance.FindLastDays(1));
                RefreshStatusInfo();
            };
            tsbDelete.Click += (_, _) =>
            {
                if (listView.SelectedItems.Count == 0) return;
                if (MessageBox.Show(Resources.TextConfirmDeleteItem, Resources.Text_Hint, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No) return;
                var list = (from ListViewItem item in listView.SelectedItems select (PageDocumentPoCo)item.Tag).ToList();
                var ret = MyPageDb.Instance.BatchDelete(list, out var message);
                if (!ret)
                    MessageBox.Show(message, Resources.Text_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (!ret) return;
                foreach (ListViewItem eachItem in listView.SelectedItems)
                {
                    listView.Items.Remove(eachItem);
                }
            };

            //状态栏
            tslbIndexing.DoubleClick += TslbIndexing_DoubleClick;


            //list view
            listView.SelectedIndexChanged += ListView_SelectedIndexChanged;
            listView.ItemDrag += ListView_ItemDrag;

            Resize += FormMain_Resize;
            notifyIcon1.MouseClick += (_, _) => ShowWindow();
            notifyIcon1.MouseDoubleClick += (_, _) => ShowWindow();



            //form
            Closing += FormMain_Closing;

            //处理命令行
            if (IsMain)
            {
                if (_startDocument == null) return;
                (new FormPageViewer(_startDocument)).Show(this);
                Hide();
            }
            


        }

 


        #region 文件夹操作

        /// <summary>
        /// 刷新选定的文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiRefreshFolder_Click(object? sender, EventArgs? e)
        {

            if (naviTreeControl1.SelectedNode != null)
                ReloadListView((string)naviTreeControl1.SelectedNode.Tag);

        }

        private void TsbGotoDocFolder_Click(object? sender, EventArgs? e)
        {
            if (listView.SelectedIndices.Count == 0) return;

            var poCo = (PageDocumentPoCo)listView.SelectedItems[0].Tag;
            if (string.IsNullOrEmpty(poCo.FullFolderPath)) return;
            _gotoDocumentPoCo = poCo;

            GotoFolder(poCo.FullFolderPath);
        }

        public void GotoFolder(string folderFullPath)
        {
            naviTreeControl1.GotoPath(folderFullPath);

        }


        private void TsmiIndexFolder_Click(object? sender, EventArgs? e)
        {
            if(naviTreeControl1.SelectedNode == null)
                return;
            StartIndex((string)naviTreeControl1.SelectedNode.Tag);
        }


        private void TreeViewMenu_Opening(object? sender, System.ComponentModel.CancelEventArgs? e)
        {
            if (naviTreeControl1.SelectedNode == null)
            {
                tsmiRenameFolder.Enabled = false;
                tsmiOpenFolderPath.Enabled =false;
                tsmiDeleteFolder.Enabled = false;
                tsmiOpenFolderPath.Enabled = false;
                tsmiIndexFolder.Enabled = false;
            }
            else
            {
                tsmiRenameFolder.Enabled = true;
                tsmiOpenFolderPath.Enabled = true;
                tsmiDeleteFolder.Enabled = true;
                tsmiOpenFolderPath.Enabled = true;

                tsmiIndexFolder.Enabled = IsMain;

            }
        }

        /// <summary>
        /// 树节点改变，重新加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NaviTreeControl1_NodeChanged(object? sender, string? e)
        {
            ReloadListView(e);
        }


        /// <summary>
        /// 添加文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiAddFolder_Click(object? sender, EventArgs? e)
        {
            var dlg = new DlgFolderAdd(naviTreeControl1.SelectedNode);
            if (dlg.ShowDialog() != DialogResult.OK) return;

            naviTreeControl1.ReloadFolderTree();
            naviTreeControl1.GotoPath(dlg.NewNodeFullPath);
        }

        /// <summary>
        /// 打开节点的文件夹路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiOpenFolderPath_Click(object? sender, EventArgs? e)
        {
            var path = (string)naviTreeControl1.SelectedNode.Tag;
            try
            {
                Process.Start(new ProcessStartInfo()
                    {
                        FileName = path,
                        UseShellExecute = true,
                        Verb = "open"
                    }
                );
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resource.TextError, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        /// <summary>
        /// 重命名文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiRenameFolder_Click(object? sender, EventArgs? e)
        {
            var node = naviTreeControl1.SelectedNode;
            if (node.Parent == null)
            {
                MessageBox.Show(Resource.TextErrorRenameRoot, Resource.TextError, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var dlg = new DlgFolderRename(naviTreeControl1.SelectedNode);
            if(dlg.ShowDialog() != DialogResult.OK) return;
            
            Cursor.Current = Cursors.WaitCursor;
            naviTreeControl1.ReloadFolderTree();
            naviTreeControl1.GotoPath(dlg.NewFullPath);
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void TsmiDeleteFolder_Click(object? sender, EventArgs? e)
        {
            
            try
            {
                var path = (string)naviTreeControl1.SelectedNode.Tag;
                var parentNode = naviTreeControl1.SelectedNode.Parent;
                if (MyPageDb.Instance.IsDocumentInFolder(path))
                {
                    MessageBox.Show(Resource.TextDocumentInFolder, Resource.TextHint, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MyPageDb.Instance.IsFilesInFolder(path))
                {
                    MessageBox.Show(Resource.TextFileInFolder, Resource.TextHint, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                Directory.Delete(path);

                naviTreeControl1.ReloadFolderTree();

                if (parentNode != null)
                    naviTreeControl1.GotoPath((string)parentNode.Tag);


            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Resource.TextError, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        /// <summary>
        /// 在新窗口中打开文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiOpenInNewWindow_Click(object? sender, EventArgs? e)
        {
            var node = naviTreeControl1.SelectedNode;
            Cursor.Current = Cursors.WaitCursor;
            var form = CreateForm();
            form.Show(Instance);
            form.GotoFolder((string)node.Tag);
            Cursor.Current = Cursors.Default;
        }



        #endregion




        #region 索引


        /// <summary>
        /// 启动索引更新时钟
        /// </summary>
        private void StartAutoIndexTimer()
        {
            if (MyPageSettings.Instance != null && !MyPageSettings.Instance.AutoIndex)
            {
                _autoIndexTimer?.Change(Timeout.Infinite, Timeout.Infinite);
                return;
            }
            _autoIndexTimer ??= new System.Threading.Timer(_autoIndexTimer_Elapsed, null, Timeout.Infinite, Timeout.Infinite);
            if (MyPageSettings.Instance != null && MyPageSettings.Instance.AutoIndexIntervalSeconds > 0)
                _autoIndexTimer.Change(MyPageSettings.Instance.AutoIndexIntervalSeconds, Timeout.Infinite);
        }

        private void _autoIndexTimer_Elapsed(object? sender)
        {

            if (MyPageIndexer.Instance.IsRunning) return;
            _autoIndexTimer?.Change(Timeout.Infinite, Timeout.Infinite);

            MyPageIndexer.Instance.StartIndex();

            Invoke(() =>
            {
                tslbIndexing.Visible = true;
                tsbStartIndex.Checked = true;
            });

        }

        private void Instance_IndexFileChanged(object? sender, string? e)
        {
            Invoke(() =>
            {
                tsslInfo.Text = e;
            });
        }

        /// <summary>
        /// 开始索引
        /// </summary>
        private void StartIndex(string? startFolder=null)
        {
            if (MyPageIndexer.Instance.IsRunning) return;
            MyPageIndexer.Instance.StartIndex(startFolder);
            tslbIndexing.Image = Resources.Clock_history_frame24;
            tslbIndexing.Text = "索引中";
            tslbIndexing.Visible = true;
            tslbIndexing.DoubleClickEnabled = false;
            tsbStartIndex.Enabled = false;
            tsbCleanDb.Enabled = false;
            tsbStop.Enabled = true;

        }

        private void StartClean()
        {
            if (MyPageIndexer.Instance.IsRunning) return;
            MyPageIndexer.Instance.StartClean();
            tslbIndexing.Image = Resources.Clock_history_frame24;
            tslbIndexing.Text = "清理中";
            tslbIndexing.Visible = true;
            tslbIndexing.DoubleClickEnabled = false;
            tsbStartIndex.Enabled = false;
            tsbCleanDb.Enabled = false;
            tsbStop.Enabled = true;
        }

        private void StopAction()
        {
            MyPageIndexer.Instance.Stop();

            tsbStop.Enabled = false;

        }

        private void Instance_IndexStopped(object? sender, FuncResult? e)
        {
            Invoke(() =>
            {
                tsbStartIndex.Enabled = true;
                tsbCleanDb.Enabled = true;
                tsbStop.Enabled = false;
                if (e == null) return;

                if (!e.Success)
                {
                    tslbIndexing.Text = Resources.TextIndexErrorHappend;
                    tslbIndexing.Image = Resources.Exclamation24;
                    tslbIndexing.DoubleClickEnabled = true;
                    _lastMessage = e.Message;
                }
                else
                {
                    tslbIndexing.Visible = false;
                    tsslInfo.Text = null;
                }

            });

            StartAutoIndexTimer();
        }

        private void TslbIndexing_DoubleClick(object? sender, EventArgs? e)
        {
            if(string.IsNullOrEmpty(_lastMessage))
                return;
            Program.ShowError(_lastMessage);
            tslbIndexing.Visible = false;

        }
        #endregion


        private void ListView_SelectedIndexChanged(object? sender, EventArgs? e)
        {
            RefreshStatusInfo();
        }

        private void RefreshStatusInfo()
        {
            if (listView.SelectedIndices.Count == 0)
            {
                tsbDelete.Enabled = false;
                tsslInfo.Text = string.Format(Resources.TextTotalItemsCount, listView.Items.Count);
                return;
            }

            tsslInfo.Text = ((PageDocumentPoCo)listView.SelectedItems[0].Tag).Description;
            tsbDelete.Enabled = true;
        }



        private void OpenDocumentFromFilePath(string? filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath)) return;
            var doc = new MyPageDocument(filePath);
            var form = new FormPageViewer(doc);
            form.Show();
            WinApi.ShowToFront(form.Handle);

            form.WindowState = FormWindowState.Maximized;

        }

        #region List view 操作

        /// <summary>
        /// 从路径重新装载基类
        /// </summary>
        /// <param name="folderFullPath"></param>
        private void ReloadListView(string? folderFullPath)
        {
            listView.Items.Clear();

            if (!Directory.Exists(folderFullPath)) return;

            var pocos = MyPageDb.Instance.FindFolderPath(folderFullPath);
            FillListView(pocos);

            RefreshStatusInfo();
        }

        private void ListView_MouseDoubleClick(object? sender, MouseEventArgs? e)
        {
            if(e==null || sender == null) return;
            var info = ((ListView)sender).HitTest(e.X, e.Y);
            if (info.Item == null) return;

            var poCo = (PageDocumentPoCo)info.Item.Tag;
            OpenDocumentFromFilePath(poCo.FilePath);
        }

        private void ListView_ItemDrag(object? sender, ItemDragEventArgs? e)
        {
            if (listView.SelectedIndices.Count == 0) return;

            listView.DoDragDrop(new DataObject(DataFormats.FileDrop,
                (from ListViewItem item in listView.SelectedItems where item.Tag != null select ((PageDocumentPoCo)item.Tag).FilePath).ToArray()),
                DragDropEffects.Move);

            if(naviTreeControl1.SelectedNode!=null)
                ReloadListView((string)naviTreeControl1.SelectedNode.Tag);
        }


        #endregion

        #region 搜索
        private void TbSearch_KeyDown(object? sender, KeyEventArgs? e)
        {
            if (e is { KeyCode: Keys.Return })
                DoSearch();
        }

        private async void DoSearch()
        {
            _searchCancellationTokenSource?.Cancel();

            _searchCancellationTokenSource = new CancellationTokenSource();
            var items = await MyPageDb.Instance.Search(tbSearch.Text, _searchCancellationTokenSource.Token);
            FillListView(items);
            RefreshStatusInfo();
        }

        private void FillListView(IList<PageDocumentPoCo>? poCos)
        {
            if (poCos == null || poCos.Count == 0)
            {
                listView.Items.Clear();
                return;
            }

            listView.BeginUpdate();
            listView.Items.Clear();
            foreach (var poCo in poCos)
            {
                var listViewItem = new ListViewItem(poCo.Title, 0)
                {
                    Tag = poCo
                };
                listViewItem.SubItems.Add(poCo.Name);
                listViewItem.SubItems.Add(poCo.FileSize.FormatFileSize());
                listViewItem.SubItems.Add(poCo.DtModified.FormatModifiedDateTime());
                listViewItem.SubItems.Add(poCo.FilePath);

                listView.Items.Add(listViewItem);
                if (_gotoDocumentPoCo != null && _gotoDocumentPoCo.Guid == poCo.Guid)
                {
                    listViewItem.Selected = true;
                    listView.EnsureVisible(listViewItem.Index);
                }
            }


            listView.EndUpdate();
            _gotoDocumentPoCo = null;
        }


        private CancellationTokenSource? _searchCancellationTokenSource;
        private async void TbSearch_TextChanged(object? sender, EventArgs? e)
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

                        OpenDocumentFromFilePath(s);

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
        private void FormMain_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!MyPageIndexer.Instance.IsRunning) return;
            Program.ShowWarning("索引服务正在运行，请停止后退出。");
            e.Cancel = true;
        }

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

        private void FormMain_Resize(object? sender, EventArgs? e)
        {
            if(!IsMain) return;

            if (WindowState == FormWindowState.Minimized)
            {
                MinimizeToTray();
            }
        }

        #endregion


    }
}