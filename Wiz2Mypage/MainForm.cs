using System.Diagnostics;
using System.IO.Compression;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text;
using MyPageLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WizKMCoreLib;
using mySharedLib;

namespace Wiz2Mypage
{

    public partial class MainForm : Form
    {
        private FolderBrowserDialog _folderBrowserDialog;
        private bool _isRunning;
        private bool _stopRequest;

        private string WizDbPath;

        public class RunningState
        {
            public int Step { get; set; }
            public int Counter { get; set; }
            public string Message { get; set; } = string.Empty;


        }


        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            progressBar1.DisplayStyle = ProgressBarDisplayText.NumberOfTotal;

            WizDbPath = mySharedLib.Utility.GetSetting("WizIndexDbPath", "");

            btChoseDir.Click += BtChoseDir_Click;
            btStart.Click += BtStart_Click;

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.ProgressChanged += BackgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;

            FormClosing += MainForm_FormClosing;
        }

        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            if (_isRunning)
            {
                MessageBox.Show($"正在运行，停止运行后退出。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }
        }

        private void BackgroundWorker1_RunWorkerCompleted(object? sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            _isRunning = false;
            btStart.Text = "开始";
            btStart.BackColor = Color.Turquoise;

            btChoseDir.Enabled = true;
            tbSrcDir.Enabled = true;
        }

        private void BackgroundWorker1_ProgressChanged(object? sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            var state = (RunningState)e.UserState;
            switch (state.Step)
            {
                case 0:
                    progressBar1.Maximum = state.Counter;
                    progressBar1.Value = 0;
                    LogMessage($"共查找到 {state.Counter} 个文件。");
                    break;

                case 1:
                    progressBar1.Value = state.Counter;
                    progressBar1.Text = $"{state.Counter} / {progressBar1.Maximum}";
                    LogMessage(state.Message);
                    break;
            }
        }

        private void BackgroundWorker1_DoWork(object? sender, System.ComponentModel.DoWorkEventArgs e)
        {
            var startDir = (string)e.Argument;
            var listFiles = new List<string>();
            foreach (var file in Directory.EnumerateFiles(startDir, "*.ziw", SearchOption.AllDirectories))
            {
                if (_stopRequest) return;
                Debug.WriteLine(file);
                listFiles.Add(file);
            }

            backgroundWorker1.ReportProgress(0, new RunningState { Step = 0, Counter = listFiles.Count });

            var i = 0;
            foreach (var file in listFiles)
            {
                if (_stopRequest) return;
                var ret = ConvertFile(file, out var message);
                i++;
                backgroundWorker1.ReportProgress(0, new RunningState() { Step = 1, Counter = i, Message = message });
                if (!ret) return;
            }
        }

        private void BtStart_Click(object? sender, EventArgs e)
        {
            if (_isRunning)
            {
                _stopRequest = true;
                return;
            }

            if (string.IsNullOrEmpty(tbSrcDir.Text) || !Directory.Exists(tbSrcDir.Text))
            {
                MessageBox.Show($"不正确的开始目录！\r\n{tbSrcDir.Text}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var startDir = tbSrcDir.Text;
            tbSrcDir.Enabled = false;
            btChoseDir.Enabled = false;
            btStart.Text = "停止";
            btStart.BackColor = Color.Crimson;

            _isRunning = true;
            _stopRequest = false;
            tbLog.Clear();
            backgroundWorker1.RunWorkerAsync(startDir);

        }

        private void BtChoseDir_Click(object? sender, EventArgs e)
        {
            _folderBrowserDialog ??= new FolderBrowserDialog();
            if (_folderBrowserDialog.ShowDialog() != DialogResult.OK) return;
            tbSrcDir.Text = _folderBrowserDialog.SelectedPath;
        }


        private void LogMessage(string message)
        {
            tbLog.AppendText(message);
            tbLog.AppendText("\r\n");
        }


        private void MakeFolder(string path)
        {
            if (Directory.Exists(path)) return;
            Directory.CreateDirectory(path);
        }

        private bool ConvertFile(string file, out string message)
        {
            try
            {
                //创建文档临时目录
                var guId = Guid.NewGuid().ToString();
                var pageDocTempPath = Path.Combine(MyPageSettings.Instance.TempPath, guId);
                if (Directory.Exists(pageDocTempPath))
                    Directory.Delete(pageDocTempPath, true);
                Directory.CreateDirectory(pageDocTempPath);

                ZipFile.ExtractToDirectory(file, pageDocTempPath);

                var wizDb = new WizDatabaseClass();
                wizDb.Open(WizDbPath);


                //创建manifest
                var fileName = Path.Combine(pageDocTempPath, "manifest.json");
                var jo = File.Exists(fileName) ? JObject.Parse(File.ReadAllText(fileName)) : new JObject();

                WizDocument wizDocument = null;
                var title = Path.GetFileNameWithoutExtension(file);

                var dateModified = DateTime.Now;
                //查找相同URL的文档
                if (!string.IsNullOrEmpty(file))
                {
                    wizDocument = (WizDocument)wizDb.FileNameToDocument(file);
                    if (wizDocument != null)
                    {
                        jo["title"] = wizDocument.Title;
                        jo["originalUrl"] = wizDocument.URL;
                        jo["archiveTime"] = wizDocument.DataDateModified.ToString("s") + "Z";
                        dateModified = wizDocument.DateModified;
                        title = wizDocument.Title;

                        var wizTags = (WizTagCollection)wizDocument.Tags;
                        if (wizTags.count > 0)
                        {
                            var tags = (from WizTag tag in wizTags select tag.DisplayName).ToList();
                            jo["tags"] = JToken.FromObject(tags);
                        }

                        var wizAttachments = (WizDocumentAttachmentCollection)wizDocument.Attachments;
                        if (wizAttachments.count > 0)
                        {
                            var attachmentFolder = Path.Combine(pageDocTempPath, MyPageDocument.AttachmentsFolderName);
                            MakeFolder(attachmentFolder);

                            foreach (WizDocumentAttachment attachment in wizAttachments)
                            {
                                File.Copy(attachment.FileName, Path.Combine(attachmentFolder, Path.GetFileName(attachment.FileName)));
                            }
                        }
                    }
                }

                //写入manifest
                File.WriteAllText(fileName, JsonConvert.SerializeObject(jo, JsonSerializerSettings), Encoding.UTF8);

                //打包
                var tempZip = Path.Combine(MyPageSettings.Instance.TempPath, $"{guId}.zip");
                if (File.Exists(tempZip))
                    File.Delete(tempZip);
                System.IO.Compression.ZipFile.CreateFromDirectory(pageDocTempPath, tempZip);

                //移动文件
                var pizFileName = $"{title} - {dateModified:yyyy_MM_dd_HH_mm_ss}.piz".MakeValidFileName();
                pizFileName = Path.Combine(Path.GetDirectoryName(file), pizFileName);
                File.Move(tempZip, pizFileName, true);

                //设置修改日期
                File.SetLastWriteTime(pizFileName, dateModified);
                File.SetCreationTime(pizFileName, dateModified);

                //索引文件
                MyPageIndexer.Instance.IndexFile(pizFileName);

                //删除临时目录
                Directory.Delete(pageDocTempPath, true);

                //删除wiz笔记文档
                wizDocument?.Delete();
                wizDb.Close();

            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }



            message = $"成功处理 {file}";
            return true;
        }

        private static readonly JsonSerializerSettings JsonSerializerSettings
            = new()
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };
    }
}