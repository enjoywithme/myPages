using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using MyPageLib.PoCo;

namespace MyPageLib
{
    public class PageIndexer
    {
        public static PageIndexer Instance { get; } =new();

        public event EventHandler IndexStopped;
        public event EventHandler<string> IndexFileChanged; 

        public bool IsRunning { get; private set; }
        private bool _stopPending;

        public void Start()
        {
            if(IsRunning) return;
            _stopPending=false;
            Task.Run(Processing);

        }

        public void Stop()
        {
            _stopPending=true;

        }


        private void Processing()
        {
            IsRunning = true;
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
            

            IsRunning = false;
            IndexStopped?.Invoke(this,EventArgs.Empty);
        }

        

    }
}
