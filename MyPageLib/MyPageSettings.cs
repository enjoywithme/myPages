using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace MyPageLib
{
    [Serializable]
    public class MyPageSettings
    {
        [XmlIgnore]
        public string SettingFilePath { get; set; }
        

        public static MyPageSettings Instance
        {
            get => _instance;
            set => _instance = value;
        }

        private bool _modified;
        private string _workingDirectory;
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

        private string _tempPath;
        private List<string> _scanFolders;
        private bool _viewTree;
        private bool _viewPreview;
        private static MyPageSettings _instance;

        /// <summary>
        /// 临时文件目录
        /// </summary>
        [XmlIgnore]
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

        public List<string> ScanFolders
        {
            get => _scanFolders;
            set => _scanFolders = value;
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
        [XmlIgnore]
        public int AutoIndexIntervalSeconds => AutoIndexInterval * (AutoIndexIntervalUnit == 0 ? 3600 : 60) * 1000;
        public static string ExecutePath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private const string SettingFileName = "mypages.xml";

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
                var serializer = new XmlSerializer(typeof(MyPageSettings));
                using Stream reader = new FileStream(settingsFile, FileMode.Open);
                Instance = (MyPageSettings)serializer.Deserialize(reader);
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
                var serializer = new XmlSerializer(typeof(MyPageSettings));
                //var settingsFile = Path.Combine(ExecutePath, SettingFileName);

                Stream fs = new FileStream(SettingFilePath, FileMode.Create);
                XmlWriter writer = new XmlTextWriter(fs, Encoding.UTF8);
                serializer.Serialize(writer, this);
                writer.Close();
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
