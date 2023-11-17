using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Meilisearch;
using MyPageLib.PoCo;
using mySharedLib;

namespace MyPageLib
{
    public class MyPageIndexer
    {
        public enum ScanMode
        {
            FullScan,
            ScanWaitList
        }

        public const string MeilisearchIndexKey = "myPages";
        private const int MaxErrorLimit = 10;
        public static MyPageIndexer Instance { get; } = new();

        public event EventHandler? IndexStopped;
        public event EventHandler<string>? IndexFileChanged;

        public bool IsRunning { get; private set; }
        private bool _stopPending;

        private readonly ConcurrentQueue<string> _filesWaitIndex = new();
        private Meilisearch.Index? _meiliSearchIndex;

        private StringBuilder? _errorBuilder;
        public bool IsError => _errorCount > 0;
        private int _errorCount;
        public string? ErrorMessage => _errorBuilder?.ToString();

        public void Start(ScanMode mode = ScanMode.FullScan)
        {
            if (IsRunning) return;

            Task.Run(() => { Processing(mode);});

        }

        public void Stop()
        {
            _stopPending = true;
        }

        public void Enqueue(string fileName)
        {
            _filesWaitIndex.Enqueue(fileName);
            Processing(ScanMode.ScanWaitList);
        }

        /// <summary>
        /// 索引路径指定的文件
        /// </summary>
        /// <param name="file"></param>
        public async void IndexFile(string file)
        {
            var poCoLocal = new PageDocumentPoCo()
            {
                FilePath = file,
                LocalPresent = 1
            };
            poCoLocal.CheckInfo();

            var poCo = MyPageDb.Instance.FindFilePath(file);
            if (poCo != null)
            {
                var modified = false;
                if (poCo.DataMd5 != poCoLocal.DataMd5)
                {
                    poCo.DataMd5 = poCoLocal.DataMd5;
                    modified = true;
                }

                if (poCo.DtModified != poCoLocal.DtModified)
                {
                    poCo.DataMd5 = poCoLocal.DataMd5;
                    modified = true;
                }

                if (poCo.FileSize != poCoLocal.FileSize)
                {
                    poCo.FileSize = poCoLocal.FileSize;
                    modified = true;
                }

                if (modified)
                {
                     await FullTextIndexPoCo(poCo);
                    MyPageDb.Instance.UpdateDocument(poCo);
                }
                else if (poCo.FullTextIndexed == 0)
                {
                    poCo.ContentText = poCoLocal.ContentText;
                    var fullIndexed = await  FullTextIndexPoCo(poCo);
                    if(fullIndexed)
                        MyPageDb.Instance.UpdateDocument(poCo);
                }
            }
            else
            {
                 await FullTextIndexPoCo(poCoLocal);
                 MyPageDb.Instance.InsertDocument(poCoLocal);
            }

        }

        /// <summary>
        /// 提交全文索引文档
        /// </summary>
        /// <param name="poCo"></param>
        /// <returns></returns>
        private async Task<bool> FullTextIndexPoCo(PageDocumentPoCo poCo)
        {
            if (_meiliSearchIndex == null)
            {
                poCo.FullTextIndexed = 0;
                return false;
            }

            try
            {
                await _meiliSearchIndex.AddDocumentsAsync(new[] { poCo }, "guid"); //主key必须小写
                poCo.FullTextIndexed = 1;
            }
            catch (Exception ex)
            {
                _errorBuilder?.AppendLine($"全文索引条目错误：{ex.Message}");
                _errorCount++;
                return false;
            }
            
            return true;
        }

        private void ScanWaitList()
        {
            while (true)
            {
                if (!_filesWaitIndex.TryDequeue(out var file)) break;

                IndexFile(file);
            }
        }

        private void ScanLocalFolder()
        {
            if (MyPageSettings.Instance?.TopFolders == null) return;

            //先更新所有纪录的Local present 为 false
            MyPageDb.Instance.UpdateLocalPresentFalse();
            var counter = 0;
            foreach (var folder in MyPageSettings.Instance.TopFolders)
            {
                foreach (var file in Directory.EnumerateFiles(folder.Value, "*.piz", SearchOption.AllDirectories))
                {
                    counter++;
                    IndexFileChanged?.Invoke(this, $"[{counter}]{file}");

                    if (_stopPending) break;
                    IndexFile(file);

                    if (_errorCount < MaxErrorLimit) continue;
                    _errorBuilder?.AppendLine("发生错误太多，终止索引，请检查后重试。");
                    return;
                }
            }

            //删除本地不存在的条目
            if (!_stopPending)
                MyPageDb.Instance.CleanUpLocalNotPresent();

        }


        private void Processing(ScanMode scanMode)
        {
            _stopPending = false;
            _errorCount = 0;
            _errorBuilder = new StringBuilder();
            IsRunning = true;

            //Init full text search
            if (MyPageSettings.Instance != null && MyPageSettings.Instance.EnableFullTextIndex)
            {
                try
                {
                    var client = new MeilisearchClient(MyPageSettings.Instance.MeilisearchServer,
                        MyPageSettings.Instance.MeilisearchMasterKey);
                    _meiliSearchIndex = client.Index(MeilisearchIndexKey);
                }
                catch (Exception e)
                {
                    MyLog.Log(e.Message);
                    _errorBuilder.AppendLine(e.Message);
                    _meiliSearchIndex = null;
                    _errorCount++;
                }
            }
            else
                _meiliSearchIndex = null;

            //Index files
            try
            {
                //Index documents
                if (scanMode == ScanMode.ScanWaitList)
                    ScanWaitList();
                else
                {
                    ScanWaitList();
                    ScanLocalFolder();
                    ScanWaitList();

                }
            }
            catch (Exception e)
            {
                MyLog.Log(e.Message);
                _errorBuilder.AppendLine(e.Message);
                _errorCount++;
            }

            IsRunning = false;
            IndexStopped?.Invoke(this, EventArgs.Empty);
        }



    }
}
