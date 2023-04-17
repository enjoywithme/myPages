using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MyPageViewer.PoCo
{
    [SugarTable("PG_DOCUMENT")]
    public class PageDocumentPoCo
    {
        [SugarColumn(IsPrimaryKey = true, ColumnName = "Doc_Guid")]
        public string Guid { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public DateTime DtCreated { get; set; }
        public DateTime DtModified { get; set; }
        public int Rate { get; set; }
        public string OriginUrl { get; set; }
        public int Indexed { get; set; }

        
    }
}
