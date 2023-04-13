using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace MyPageViewer.Model
{
    [Serializable]
    public class MyPageSettings
    {
        public static MyPageSettings Instance { get; set; }

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

        public List<string> ScanFolders { get; set; }
        public bool ViewTree { get; set; }
        public bool ViewPreview { get; set; }

        public static string ExecutePath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private const string SettingFileName = "mypages.xml";

        static MyPageSettings()
        {
            var settingsFile = Path.Combine(ExecutePath, SettingFileName);
            if (!File.Exists(settingsFile))
            {
                Instance = new MyPageSettings();
                return;
            }

            try
            {
                var serializer = new XmlSerializer(typeof(MyPageSettings));
                using Stream reader = new FileStream(settingsFile, FileMode.Open);
                Instance = (MyPageSettings)serializer.Deserialize(reader);
            }
            catch (Exception e)
            {
                Instance = new MyPageSettings();
            }
        }

        public void Save()
        {
            try
            {
                var serializer = new XmlSerializer(typeof(MyPageSettings));
                var settingsFile = Path.Combine(ExecutePath, SettingFileName);

                Stream fs = new FileStream(settingsFile, FileMode.Create);
                XmlWriter writer = new XmlTextWriter(fs, Encoding.UTF8);
                serializer.Serialize(writer, this);
                writer.Close();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
