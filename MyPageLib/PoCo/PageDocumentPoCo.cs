using System;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using mySharedLib;
using Newtonsoft.Json.Linq;
using SqlSugar;

namespace MyPageLib.PoCo
{
    [SugarTable("PG_DOCUMENT")]
    public class PageDocumentPoCo
    {
        [SugarColumn(IsPrimaryKey = true, ColumnName = "Doc_Guid")]
        public string Guid { get; set; }
        public string Name { get; set; }

        public string Title { get; set; }
        public string FilePath { get; set; }
        public string FolderPath { get; set; }
        public long FileSize { get; set; }
        public string Data_Md5 { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime DtModified { get; set; }
        public DateTime archiveTime { get; set; }
        public int Rate { get; set; }
        public string OriginUrl { get; set; }
        public int Indexed { get; set; }
        [SugarColumn(ColumnName = "LOCAL_PRESENT")]
        public int LocalPresent { get; set; }

        public override string ToString()
        {
            return Title;
        }

        [SugarColumn(IsIgnore = true)]
        public string Description =>
            $"大小:{FileSize.FormatFileSize()},修改时间:{DtModified.FormatModifiedDateTime()},路径:{FolderPath}";

        public void CheckInfo()
        {
            var fi = new FileInfo(FilePath);
            FileSize = fi.Length;
            DtCreated = fi.CreationTime;
            DtModified = fi.LastWriteTime;

            FolderPath = Path.GetDirectoryName(FilePath);

            //MD5
            using var md5 = MD5.Create();
            using var stream = File.OpenRead(FilePath);
            Data_Md5 = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty).ToLower();

            //manifest
            using var zip = ZipFile.OpenRead(FilePath);

            foreach (var entry in zip.Entries)
                if (entry.Name == "manifest.json")
                {
                    var sr = new StreamReader(entry.Open(), Encoding.UTF8);
                    var json = sr.ReadToEnd();
                    var jo = JObject.Parse(json);
                    if (jo.ContainsKey("title")) Title = (string)jo["title"];
                    if(jo.ContainsKey("originalUrl")) OriginUrl = (string)jo["originalUrl"];
                    if (jo.ContainsKey("archiveTime"))  archiveTime = DateTime.Parse((string)jo["archiveTime"]);
                    break;
                }

            if (string.IsNullOrEmpty(Title))
                Title = Name;
        }

    }
}