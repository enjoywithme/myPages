using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPageViewer
{
    public class MyPageDocument:IDisposable
    {
        public string FilePath { get; set; }
        public string GuiId { get; set; }
        public string Title { get; set; }
        
        public bool IsModified { get; private set; }

        /// <summary>
        /// 解压后的临时根目录
        /// </summary>
        public string DocTempPath { get; private set; }
        /// <summary>
        /// 解压后的临时index.html
        /// </summary>
        public string TempIndexPath { get; private set; }

        public string TempAttachmentsPath => Path.Combine(DocTempPath, "Attachments");

        private static string _tempPath;
        private static string _homePath;
        public static string TempPath
        {
            get
            {
                if (!string.IsNullOrEmpty(_tempPath)&&Directory.Exists(_tempPath)) return _tempPath;
                var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                _homePath = Path.Combine(path, "My pages");
                if(!Directory.Exists(_homePath))
                    Directory.CreateDirectory(_homePath);
                _tempPath = Path.Combine(_homePath, "temp");
                if(!Directory.Exists(_tempPath))
                    Directory.CreateDirectory(_tempPath);

                return _tempPath;
            }
        }


        public static MyPageDocument NewFromArgs(string[] args)
        {
            if (args == null || args.Length == 0) return null;

            foreach (var arg in args)
            {
                if (File.Exists(arg) && Path.GetExtension(arg).ToLower() == ".piz")
                {

                    return new MyPageDocument()
                    {
                        FilePath = arg
                    };

                }
            }

            

            return null;
        }

        public bool ExtractToTemp(out string message)
        {
            try
            {
                if (string.IsNullOrEmpty(FilePath) || !File.Exists(FilePath))
                    throw new Exception("页文件不存在！");

                if (string.IsNullOrEmpty(GuiId))
                    GuiId = Guid.NewGuid().ToString();

                DocTempPath = Path.Combine(TempPath, GuiId);
                if (Directory.Exists(DocTempPath))
                    Directory.Delete(DocTempPath,true);
                Directory.CreateDirectory(DocTempPath);

                System.IO.Compression.ZipFile.ExtractToDirectory(FilePath, DocTempPath);

                TempIndexPath = Path.Combine(DocTempPath, "index.html");
            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }

            message = string.Empty;
            return true;
        }

        public bool RepackFromTemp(out string message)
        {

            try
            {
                var tempZip = Path.Combine(TempPath, $"{GuiId}.zip");
                if(File.Exists(tempZip))
                    File.Delete(tempZip); 
                System.IO.Compression.ZipFile.CreateFromDirectory(DocTempPath, tempZip);
                File.Move(tempZip,FilePath,true);
            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }
            message = string.Empty;

            return true;
        }

        public void SetModified()
        {
            IsModified = true;
        }



        public void Dispose()
        {
            if(Directory.Exists(DocTempPath))
                Directory.Delete(DocTempPath, true);
        }
        ~MyPageDocument(){
            Dispose();
        }
    }
}
