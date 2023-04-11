using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MyPageViewer.Model
{
    public class MyPageDocument:IDisposable
    {
        public string FilePath { get; set; }
        public string GuiId { get; set; }
        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                if(_title==value) return;
                _title = value;
                SetModified(true);
            }
        }
        public string OriginalUrl { get; set; }

        private bool _manifestChanged;
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

                var fileName = Path.Combine(DocTempPath, "index.html");
                if(File.Exists(fileName))
                    TempIndexPath = fileName;

                fileName = Path.Combine(DocTempPath, "manifest.json");
                if (File.Exists(fileName))
                {
                    dynamic jo = JObject.Parse(File.ReadAllText(fileName));
                    _title = jo.title;
                    OriginalUrl =jo.originalUrl;
                }
            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }

            message = string.Empty;
            return true;
        }

        private static readonly JsonSerializerSettings JsonSerializerSettings
            = new()
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented
            };
        public bool RepackFromTemp(out string message)
        {
            try
            {
                //Save manifest
                if (_manifestChanged)
                {
                    var fileName = Path.Combine(DocTempPath, "manifest.json");
                    var jo = File.Exists(fileName) ? JObject.Parse(File.ReadAllText(fileName)) : new JObject("{}");

                    dynamic jd = jo;
                    jd.title = _title;
                    jd.originalUrl = OriginalUrl;

                    File.WriteAllText(fileName, JsonConvert.SerializeObject(jd, JsonSerializerSettings));
                }


                var tempZip = Path.Combine(TempPath, $"{GuiId}.zip");
                if(File.Exists(tempZip))
                    File.Delete(tempZip); 
                System.IO.Compression.ZipFile.CreateFromDirectory(DocTempPath, tempZip);
                File.Move(tempZip,FilePath,true);

                _manifestChanged = false;
                IsModified = false;
            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }
            message = string.Empty;

            return true;
        }

        /// <summary>
        /// 清洗HTML
        /// </summary>
        /// <returns></returns>
        public bool CleanHtml(out string message)
        {
            message = string.Empty;
            if(string.IsNullOrEmpty(TempIndexPath)) return false;
            try
            {
                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.Load(TempIndexPath);

                var imageNodes = doc.DocumentNode.SelectNodes("//img").Where(t => t.Attributes["data-src"] != null)
                    .ToList();
                foreach (var node in imageNodes)
                {
                    var imgSrc = node.Attributes["src"];
                    if (imgSrc == null) continue;
                    Debug.WriteLine(imgSrc.Value);
                    node.Attributes.RemoveAll();
                    node.Attributes.Add("src", imgSrc.Value);
                }

                doc.Save(TempIndexPath);
            }
            catch (Exception e)
            {
                message = e.Message;
                return false;
            }
            
            

            return true;
        }


        public void SetModified(bool manifest=false)
        {
            if (manifest) _manifestChanged = true;
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
