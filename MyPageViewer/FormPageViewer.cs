using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MyPageViewer.Model;
using MessageBoxButtons = System.Windows.Forms.MessageBoxButtons;
using System.Diagnostics;
using MyPageViewer.Dlg;
using ToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem;

namespace MyPageViewer
{
    public partial class FormPageViewer : Form
    {
        public MyPageDocument PageDocument { get; }
        public FormPageViewer(MyPageDocument pageDocument)
        {
            PageDocument = pageDocument;

            if (PageDocument != null)
            {
                if (!PageDocument.ExtractToTemp(out var message))
                {
                    MessageBox.Show(message, Properties.Resources.Text_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            else
            {
                return;
            }


            InitializeComponent();
        }

        private void FormPageViewer_Load(object sender, EventArgs e)
        {
            FormClosed += FormPageViewer_FormClosed;
            FormClosing += FormPageViewer_FormClosing;
            btAttachment.Click += BtAttachment_Click;
            btTags.Click += BtTags_Click;
            btZip.Click += BtZip_Click;
            btReloadFromTemp.Click += BtReloadFromTemp_Click;
            btCleanHmtl.Click += BtCleanHtml_Click;
            tbTitle.TextChanged += (o, _) =>
            {
                PageDocument.Title = tbTitle.Text;
            };
            tsAddresss.DoubleClick += (o, _) =>
            {
                try
                {
                    var url = ((ToolStripStatusLabel)o)?.Text;
                    if (string.IsNullOrEmpty(url)) return;
                    Process.Start(new ProcessStartInfo(url)
                    {
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, Properties.Resources.Text_Error, MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            };
            tssIconLink.DoubleClick += TssIconLink_DoubleClick;
            tagsControl.Document = panelAttachments.Document = PageDocument;

            tsbRate0.Click += TsbRate_Click;
            tsbRate1.Click += TsbRate_Click;
            tsbRate2.Click += TsbRate_Click;
            tsbRate3.Click += TsbRate_Click;
            tsbRate4.Click += TsbRate_Click;
            tsbRate5.Click += TsbRate_Click;


            InitializeAsync();


        }
        
        /// <summary>
        /// 五星评分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsbRate_Click(object sender, EventArgs e)
        {
            var rate = Convert.ToInt32(((ToolStripMenuItem)sender).Tag);

            tsbRate.Image = RateBitmap(rate);
            PageDocument.Rate = rate;
        }

        private Bitmap RateBitmap(int rate)
        {
            var img = Properties.Resources.star0;
            switch (rate)
            {
                case 1:
                    img = Properties.Resources.star1;
                    break;
                case 2:
                    img = Properties.Resources.star2;
                    break;
                case 3:
                    img = Properties.Resources.star3;
                    break;
                case 4:
                    img = Properties.Resources.star4;
                    break;
                case 5:
                    img = Properties.Resources.star5;
                    break;
            }
            return img;
        }

        /// <summary>
        /// 修改文章源链接地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void TssIconLink_DoubleClick(object sender, EventArgs e)
        {
            var dlgUrl = new DlgUrlEdit(PageDocument.OriginalUrl);
            if (dlgUrl.ShowDialog(this) == DialogResult.Cancel) return;
            PageDocument.OriginalUrl = dlgUrl.Url;
            tsAddresss.Text = dlgUrl.Url;
        }

        #region 工具按钮

        /// <summary>
        /// 净化HTML页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void BtCleanHtml_Click(object sender, EventArgs e)
        {
            if (!PageDocument.CleanHtml(out var message))
            {
                if (!string.IsNullOrEmpty(message))
                    MessageBox.Show(message, Properties.Resources.Text_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            PageDocument?.SetModified();
            webView.CoreWebView2.Reload();
        }

        private void BtReloadFromTemp_Click(object sender, EventArgs e)
        {
            webView.Reload();
        }


        /// <summary>
        /// 重新从临时文件夹压制文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void BtZip_Click(object sender, EventArgs e)
        {
            var ret = PageDocument.RepackFromTemp(out var message);
            MessageBox.Show(ret ? "成功重新压制源文件。" : message, Properties.Resources.Text_Error, MessageBoxButtons.OK, ret ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        private void BtTags_Click(object sender, EventArgs e)
        {
            if (panelRight.Visible && tagsControl.Visible)
            {
                panelRight.Visible = false;
                tagsControl.Dock = DockStyle.None;
                return;
            }

            panelRight.Visible = true;
            tagsControl.Visible = true;
            panelAttachments.Visible = false;

            tagsControl.Dock = DockStyle.Fill;
            tagsControl.LoadTags();
        }

        private void BtAttachment_Click(object sender, EventArgs e)
        {
            if (panelRight.Visible && panelAttachments.Visible)
            {
                panelRight.Visible = false;
                panelAttachments.Dock = DockStyle.None;
                return;
            }

            panelRight.Visible = true;
            tagsControl.Visible = false;
            panelAttachments.Visible = true;

            panelAttachments.Dock = DockStyle.Fill;
            panelAttachments.LoadAttachments();


        }

        #endregion


        #region form事件

        /// <summary>
        /// 关闭时提示保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormPageViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PageDocument is not { IsModified: true }) return;

            var ret = MessageBox.Show("文档已经修改，要保存吗？", Properties.Resources.Text_Hint, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            switch (ret)
            {
                case DialogResult.No:
                    return;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    return;
            }

            if (PageDocument.RepackFromTemp(out var message)) return;
            MessageBox.Show(message, Properties.Resources.Text_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            e.Cancel = true;

        }
        private void FormPageViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            PageDocument?.Dispose();
            FormMain.Instance.Show();
        }

        #endregion


        
        async void InitializeAsync()
        {
            await webView.EnsureCoreWebView2Async(null);
            webView.CoreWebView2.WebMessageReceived += UpdateAddressBar;

            //await webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync("window.chrome.webview.postMessage(window.document.URL);");
            //await webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync("window.chrome.webview.addEventListener(\'message\', event => alert(event.data));");

            if (PageDocument != null && webView.CoreWebView2 != null)
            {
                Text = PageDocument.FilePath;
                tsAddresss.Text = PageDocument.OriginalUrl;
                tbTitle.Text = PageDocument.Title;
                webView.CoreWebView2.Navigate(PageDocument.TempIndexPath);
                tsbRate.Image = RateBitmap(PageDocument.Rate);
            }
        }

        void UpdateAddressBar(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            var uri = args.TryGetWebMessageAsString();
            //tsAddresss.Text = uri;
            webView.CoreWebView2.PostWebMessageAsString(uri);
        }
    }
}
