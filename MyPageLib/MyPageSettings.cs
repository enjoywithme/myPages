using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Kdbndp.NameTranslation;
using mySharedLib;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace MyPageLib
{
    [Serializable]
    public class MyPageSettings
    {
        [JsonIgnore]
        public string? SettingFilePath { get; set; }
        

        public static MyPageSettings? Instance
        {
            get => _instance;
            set => _instance = value;
        }

        private bool _modified;
        private string? _workingDirectory;
        /// <summary>
        /// 工作目录，存放数据库、临时文件
        /// </summary>
        public string WorkingDirectory
        {
            get
            {
                if (!string.IsNullOrEmpty(_workingDirectory)) return _workingDirectory;
                 var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                _workingDirectory = Path.Combine(path, "My pages");
                return _workingDirectory;
            }
            set
            {
                if(_workingDirectory==value) return;
                _workingDirectory = value;
                _tempPath = null;
            }
        }

        private string? _tempPath;
        private bool _viewTree;
        private bool _viewPreview;
        private static MyPageSettings? _instance;

        /// <summary>
        /// 临时文件目录
        /// </summary>
        [JsonIgnore]
        public string TempPath
        {
            get
            {
                if (!string.IsNullOrEmpty(_tempPath) && Directory.Exists(_tempPath)) return _tempPath;
                if (!Directory.Exists(WorkingDirectory))
                    Directory.CreateDirectory(WorkingDirectory);
                _tempPath = Path.Combine(WorkingDirectory, "temp");
                if (!Directory.Exists(_tempPath))
                    Directory.CreateDirectory(_tempPath);

                return _tempPath;
            }
        }

        private Dictionary<string,string> _topFolders = new();
        public Dictionary<string, string> TopFolders => _topFolders;

        public bool InitTopFolder(IList<string> scanFolders,out string message)
        {
            var topFolders = new Dictionary<string,string>();
            foreach (var scanFolder in scanFolders)
            {
                if (!Directory.Exists(scanFolder))
                {
                    message = $"文件夹{scanFolder}不存在";
                    return false;
                }

                var dirName = new DirectoryInfo(scanFolder).Name;
                if (topFolders.ContainsKey(dirName))
                {
                    message = $"已经存在顶级目录{dirName}。";
                    return false;
                }

                topFolders.Add(dirName,scanFolder);
            }

            _topFolders = topFolders;
            message = string.Empty;
            return true;
        }

        public (string?, string?) ParsePath(string path)
        {
            foreach (var topFolder in TopFolders)
            {
                var topPath = topFolder.Value;
                if (path.StartsWith(topPath, StringComparison.InvariantCultureIgnoreCase))
                {
                    return (topFolder.Key, path.Substring(topPath.Length).ClearPathPrefix());
                }

            }

            return (null,null);
        }


        public bool ViewTree
        {
            get => _viewTree;
            set
            {
                if (_viewTree == value) return;
                _viewTree = value;
                _modified = true;
            }
        }

        public bool ViewPreview
        {
            get => _viewPreview;
            set
            {
                if (_viewPreview == value) return;
                _viewPreview = value;
                _modified = true;
            }
        }

        public bool AutoIndex { get; set; }
        public int AutoIndexInterval { get; set; }
        public int AutoIndexIntervalUnit { get; set; } //0 = 小时，1=分钟
        [JsonIgnore]
        public int AutoIndexIntervalSeconds => AutoIndexInterval * (AutoIndexIntervalUnit == 0 ? 3600 : 60) * 1000;
        public static string? ExecutePath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private const string SettingFileName = "mypages.json";

        public static void InitInstance(string settingsPath)
        {
            var settingsFile = Path.Combine(settingsPath, SettingFileName);

            if (!File.Exists(settingsFile))
            {
                Instance = new MyPageSettings() { SettingFilePath = settingsFile };

                return;
            }

            try
            {
                Instance = JsonConvert.DeserializeObject<MyPageSettings>(File.ReadAllText(settingsFile));
                Instance.SettingFilePath = settingsFile;
            }
            catch (Exception e)
            {
                Instance = new MyPageSettings() { SettingFilePath = settingsFile };
            }

        }

        public bool Save(out string message,bool force=false)
        {
            message = string.Empty;
            if (!_modified && !force) return true;
            try
            {
                var json = JsonConvert.SerializeObject(this,Formatting.Indented);
                File.WriteAllText(SettingFilePath,json);

                _modified = false;
            }
            catch (Exception exception)
            {
                message = exception.Message;
                return false;
            }
            return true;
        }
    }
}
