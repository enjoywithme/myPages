using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Meilisearch;
using MyPageLib.PoCo;
using mySharedLib;

namespace MyPageLib
{
    public class MyPageIndexer
    {
        public enum ActionType
        {
            None,
            IndexDb,
            CleanDb
        }


        public const string MeilisearchIndexKey = "myPages";
        private const int MaxErrorLimit = 10;
        public static MyPageIndexer Instance { get; } = new();

        public ActionType CurrentAction { get; private set; }
        public bool IsRunning { get; private set; }
        //private bool _stopPending;
        private CancellationTokenSource? _cancellationTokenSource;

        public event EventHandler? IndexStopped;
        public event EventHandler<string>? IndexFileChanged;

        private Meilisearch.Index? _meiliSearchIndex;

        private FuncResult? _errorBuilder;
        public bool IsError => _errorBuilder is { Success: false };
        public string? Message => _errorBuilder?.Message;

        /// <summary>
        /// 开始索引的顶级目录
        /// </summary>
        private IList<string>? _indexStartTopFolder;

        public void StartIndex(string? startFolder=null)
        {
            if (IsRunning) return;

            if (startFolder == null)
            {
                _indexStartTopFolder = MyPageSettings.Instance?.TopFolders.Values.ToList();
            }
            else
            {
                _indexStartTopFolder = new List<string>();
                _indexStartTopFolder.Add(startFolder);
            }

            CurrentAction = ActionType.IndexDb;
            Task.Run(Indexing);

        }

        public void StartClean()
        {
            if (IsRunning) return;
            CurrentAction = ActionType.CleanDb;
            Task.Run(Cleaning);
        }

        public void Stop()
        {
            _cancellationTokenSource?.Cancel();
        }


        /// <summary>
        /// 索引路径指定的文件
        /// </summary>
        /// <param name="file"></param>
        public void IndexFile(string file)
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

                if (poCo.LocalPresent != poCoLocal.LocalPresent)
                {
                    poCo.LocalPresent = 1;
                    modified = true;
                }

                if (modified)
                {
                    FullTextIndexPoCo(poCo);
                    MyPageDb.Instance.UpdateDocumentAsync(poCo);
                }
                else if (poCo.FullTextIndexed == 0)
                {
                    poCo.ContentText = poCoLocal.ContentText;
                    var fullIndexed = FullTextIndexPoCo(poCo);
                    if (fullIndexed)
                        MyPageDb.Instance.UpdateDocumentAsync(poCo);
                }
            }
            else
            {
                FullTextIndexPoCo(poCoLocal);
                MyPageDb.Instance.InsertDocument(poCoLocal);
            }

        }

        /// <summary>
        /// 提交全文索引文档
        /// </summary>
        /// <param name="poCo"></param>
        /// <returns></returns>
        private bool FullTextIndexPoCo(PageDocumentPoCo poCo)
        {
            if (_meiliSearchIndex == null)
            {
                poCo.FullTextIndexed = 0;
                return false;
            }

            try
            {
                if (_meiliSearchIndex.AddDocumentsAsync(new[] { poCo }, "guid").Wait(3000)) //主key必须小写
                    poCo.FullTextIndexed = 1;
            }
            catch (Exception ex)
            {
                _errorBuilder?.Log($"全文索引条目错误：{ex.Message}");
                return false;
            }

            return true;
        }


        private void ScanLocalFolder()
        {
            if (_indexStartTopFolder == null) return;

            //先更新所有纪录的Local present 为 false
            //MyPageDb.Instance.UpdateLocalPresentFalse();
            var counter = 0;
            foreach (var folder in _indexStartTopFolder)
            {
                foreach (var file in Directory.EnumerateFiles(folder, "*.piz", SearchOption.AllDirectories))
                {
                    counter++;
                    IndexFileChanged?.Invoke(this, $"[{counter}]{file}");

                    if (_cancellationTokenSource is { IsCancellationRequested: true }) break;
                    IndexFile(file);

                    if (_errorBuilder is { ErrorCount: < MaxErrorLimit }) continue;
                    _errorBuilder?.False("发生错误太多，终止索引，请检查后重试。");
                    return;
                }
            }

            //删除本地不存在的条目
            //if (!_stopPending)
            //    MyPageDb.Instance.CleanUpLocalNotPresent();

        }


        public async Task<FuncResult> ClearMeiliIndex(string meiliAddress, string meiliMasterKey)
        {
            var ret = new FuncResult();
            try
            {
                var client = new MeilisearchClient(meiliAddress, meiliMasterKey);
                var task = await client.DeleteIndexAsync(MeilisearchIndexKey);
                if (task.Status == TaskInfoStatus.Failed || task.Status == TaskInfoStatus.Canceled)
                {
                    ret.False(string.Join("\r\n", task.Error.Values));
                    return ret;
                }
                MyPageDb.Instance.UpdateFullTextIndexed();
            }
            catch (Exception e)
            {
                ret.False(e.Message);
            }


            return ret;
        }

        public void DeleteDocumentFromMeiliIndex(PageDocumentPoCo poCo)
        {
            if (MyPageSettings.Instance == null || !MyPageSettings.Instance.EnableFullTextIndex) return;


            try
            {
                if (_meiliSearchIndex == null)
                    InitMeiliSearch();
                _meiliSearchIndex?.DeleteOneDocumentAsync(poCo.Guid).Wait(2000);

            }
            catch (Exception e)
            {

            }

        }

        private void InitMeiliSearch()
        {
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
                    _errorBuilder?.False(e.Message);
                    _meiliSearchIndex = null;
                }
            }
            else
                _meiliSearchIndex = null;
        }

        /// <summary>
        /// 从本地文件夹中索引文件-处理队列
        /// </summary>
        private void Indexing()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _errorBuilder = new FuncResult();
            IsRunning = true;

            InitMeiliSearch();

            //Index files
            try
            {

                ScanLocalFolder();

            }
            catch (Exception e)
            {
                _errorBuilder.False(e.Message);
            }

            IsRunning = false;
            IndexStopped?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Clean files does not exists
        /// </summary>
        private void Cleaning()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _errorBuilder = new FuncResult();
            IsRunning = true;

            InitMeiliSearch();

            var counter = 0;

            MyPageDb.Instance.Db.Queryable<PageDocumentPoCo>().ForEach(poCo =>
            {
                var file = poCo.FilePath;
                counter++;
                IndexFileChanged?.Invoke(this, $"[{counter}]{file}");

                if (_cancellationTokenSource.IsCancellationRequested) return;

                if (string.IsNullOrEmpty(file) && File.Exists(file)) return;

                try
                {
                    poCo.LocalPresent = 0;
                    MyPageDb.Instance.UpdateDocumentAsync(poCo);

                    _meiliSearchIndex?.DeleteOneDocumentAsync(poCo.Guid).Wait();
                }
                catch (Exception e)
                {
                    _errorBuilder.False(e.Message);
                    if (_errorBuilder.ErrorCount > MaxErrorLimit)
                    {
                        _errorBuilder.False("发生错误太多，终止清除，请检查后重试。");
                        _cancellationTokenSource?.Cancel();
                    }
                }



            }, 200, _cancellationTokenSource);//设置分页 

            IsRunning = false;
            IndexStopped?.Invoke(this, EventArgs.Empty);

        }

    }
}
