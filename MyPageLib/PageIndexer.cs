using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using MyPageLib.PoCo;

namespace MyPageLib
{
    public class PageIndexer
    {
        public enum ScanMode
        {
            FullScan,
            ScanWaitList
        }

        private ScanMode _currentScanMode;

        public static PageIndexer Instance { get; } =new();

        public event EventHandler IndexStopped;
        public event EventHandler<string> IndexFileChanged; 

        public bool IsRunning { get; private set; }
        private bool _stopPending;

        private ConcurrentQueue<string> _filesWaitIndex = new ConcurrentQueue<string>();

        public void Start(ScanMode mode = ScanMode.FullScan)
        {
            if(IsRunning) return;
            _currentScanMode = mode;
            _stopPending=false;
            Task.Run(Processing);

        }

        public void Stop()
        {
            _stopPending=true;
        }

        public void Enqueue(string fileName)
        {
            _filesWaitIndex.Enqueue(fileName);
            Start(ScanMode.ScanWaitList);
        }

        private void ScanWaitList()
        {
            while (true)
            {
                if(!_filesWaitIndex.TryDequeue(out var file)) break;

                var poCo = new PageDocumentPoCo()
                {
                    FilePath = file,
                    Name = Path.GetFileNameWithoutExtension(file),
                    LocalPresent = 1
                };
                poCo.CheckInfo();
                MyPageDb.Instance.InsertUpdateDocument(poCo);
            }
        }

        private void ScanLocalFolder()
        {
            try
            {
                //先更新所有纪录的Local present 为 false
                MyPageDb.Instance.UpdateLocalPresentFalse();

                foreach (var folder in MyPageSettings.Instance.ScanFolders)
                {
                    foreach (var file in Directory.EnumerateFiles(folder, "*.piz", SearchOption.AllDirectories))
                    {
                        Debug.WriteLine(file);
                        IndexFileChanged?.Invoke(this, file);


                        if (_stopPending) break;

                        var poCo = new PageDocumentPoCo()
                        {
                            FilePath = file,
                            Name = Path.GetFileNameWithoutExtension(file),
                            LocalPresent = 1
                        };
                        poCo.CheckInfo();
                        MyPageDb.Instance.InsertUpdateDocument(poCo);

                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
        }


        private void Processing()
        {
            IsRunning = true;

            if(_currentScanMode == ScanMode.ScanWaitList)
                ScanWaitList();
            else
            {
                ScanWaitList();
                ScanLocalFolder();
                ScanWaitList();

            }

            IsRunning = false;
            IndexStopped?.Invoke(this,EventArgs.Empty);
        }

        

    }
}
