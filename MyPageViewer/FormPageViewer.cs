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
using MessageBoxButtons = System.Windows.Forms.MessageBoxButtons;

namespace MyPageViewer
{
    public partial class FormPageViewer : Form
    {
        public MyPageDocument PagedDocument { get; }
        public FormPageViewer(MyPageDocument pagedDocument)
        {
            PagedDocument = pagedDocument;

            if (PagedDocument != null)
            {
                if (!PagedDocument.ExtractToTemp(out var message))
                {
                    MessageBox.Show(message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            btZip.Click += BtZip_Click;
            btReloadFromTemp.Click += BtReloadFromTemp_Click;
            btCleanHmtl.Click += BtCleanHtml_Click;

            panelAttachments.Document = PagedDocument;

            InitializeAsync();


        }

        /// <summary>
        /// 净化HTML页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void BtCleanHtml_Click(object sender, EventArgs e)
        {
            if (!PagedDocument.CleanHtml(out var message))
            {
                if (!string.IsNullOrEmpty(message))
                    MessageBox.Show(message, Properties.Resources.Text_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            PagedDocument?.SetModified();
            webView.CoreWebView2.Reload();
        }

        private void BtReloadFromTemp_Click(object sender, EventArgs e)
        {
            webView.Reload();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (webView != null && webView.CoreWebView2 != null)
            {
                webView.CoreWebView2.Navigate(textBox1.Text);
            }
        }

        /// <summary>
        /// 重新从临时文件夹压制文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void BtZip_Click(object sender, EventArgs e)
        {
            var ret = PagedDocument.RepackFromTemp(out var message);
            MessageBox.Show(ret ? "成功重新压制源文件。" : message, Properties.Resources.Text_Error, MessageBoxButtons.OK, ret ? MessageBoxIcon.Information : MessageBoxIcon.Error);
        }

        private void BtAttachment_Click(object sender, EventArgs e)
        {
            panelAttachments.Visible = !panelAttachments.Visible;
            if (panelAttachments.Visible)
            {
                panelAttachments.LoadAttachments();
            }
        }

        private void FormPageViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (PagedDocument == null || !PagedDocument.IsModified) return;

            if (MessageBox.Show("文档已经修改，要保存吗？", Properties.Resources.Text_Hint, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel) return;

            if (PagedDocument.RepackFromTemp(out var message)) return;
            MessageBox.Show(message, Properties.Resources.Text_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            e.Cancel = true;

        }
        private void FormPageViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            PagedDocument?.Dispose();
        }



        async void InitializeAsync()
        {
            await webView.EnsureCoreWebView2Async(null);
            webView.CoreWebView2.WebMessageReceived += UpdateAddressBar;

            //await webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync("window.chrome.webview.postMessage(window.document.URL);");
            //await webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync("window.chrome.webview.addEventListener(\'message\', event => alert(event.data));");

            if (PagedDocument != null && webView.CoreWebView2 != null)
            {
                Text = PagedDocument.FilePath;
                tsAddresss.Text = PagedDocument.TempIndexPath;
                webView.CoreWebView2.Navigate(PagedDocument.TempIndexPath);
            }
        }

        void UpdateAddressBar(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            var uri = args.TryGetWebMessageAsString();
            tsAddresss.Text = uri;
            webView.CoreWebView2.PostWebMessageAsString(uri);
        }
    }
}
