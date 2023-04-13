using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.Core.Raw;
using MyPageViewer.PoCo;

namespace MyPageViewer.Model
{
    public class PageIndexer
    {
        public static PageIndexer Instance { get; } =new();

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
            foreach (var folder in MyPageSettings.Instance.ScanFolders)
            {
                foreach (var file in Directory.EnumerateFiles(folder, "*.piz", SearchOption.AllDirectories))
                {
                    Debug.WriteLine(file);
                    if(_stopPending) break;

                    var poCo = new PageDocumentPoCo() { FilePath = file, 
                        Title = Path.GetFileNameWithoutExtension(file),
                        Indexed = 0
                    };
                    MyPageDb.Instance.InsertUpdateDocument(poCo);
                }
            }

            IsRunning = false;
        }

        

    }
}
