using System.Collections.Generic;
using System.IO;

namespace MyPageLib
{
    public class PageAttachment
    {
        public PageAttachment(MyPageDocument document)
        {
            Document = document;
        }

        public MyPageDocument Document { get; }

        public string Name {get;private set; }
        private string _filePath;

        public override string ToString()
        {
            return Name;
        }

        public string FilePath
        {
            get => _filePath;
            set
            {
                _filePath = value;
                Name = Path.GetFileName(_filePath);
            }
        }


        public static IList<PageAttachment> CheckAttachmentsFromTempPath(MyPageDocument document)
        {
            if (document==null || !Directory.Exists(document.DocTempPath)) return null;

            var path = document.TempAttachmentsPath;
            if(!Directory.Exists(path)) return null;

            var files = Directory.GetFiles(path);
            var attachments = new List<PageAttachment>();
            foreach (var file in files)
            {
                attachments.Add(new PageAttachment(document)
                {
                    FilePath = file
                });
            }

            return attachments;
        }
    }
}
