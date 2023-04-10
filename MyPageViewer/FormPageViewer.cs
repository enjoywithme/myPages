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
            InitializeAsync();


        }

        private void FormPageViewer_FormClosed(object sender, FormClosedEventArgs e)
        {
            PagedDocument?.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (webView != null && webView.CoreWebView2 != null)
            {
                webView.CoreWebView2.Navigate(textBox1.Text);
            }
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
                textBox1.Text = PagedDocument.TempIndexPath;
                webView.CoreWebView2.Navigate(PagedDocument.TempIndexPath);
            }
        }

        void UpdateAddressBar(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            var uri = args.TryGetWebMessageAsString();
            textBox1.Text = uri;
            webView.CoreWebView2.PostWebMessageAsString(uri);
        }
    }
}
