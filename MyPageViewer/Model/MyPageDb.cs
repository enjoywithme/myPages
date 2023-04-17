using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Formats.Asn1;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using MyPageViewer.PoCo;
using SqlSugar;

namespace MyPageViewer.Model
{
    internal class MyPageDb
    {
        public static MyPageDb Instance { get; }= new();

        public string DataBaseName => Path.Combine(MyPageSettings.Instance.WorkingDirectory, "myPage.db");

        public string ConnectionString => $"Data Source={DataBaseName};";

        //用单例模式
        private readonly SqlSugarScope _db;

        public MyPageDb()
        {
            _db = new SqlSugarScope(new ConnectionConfig()
            {
                ConnectionString = ConnectionString,
                DbType = DbType.Sqlite,
                IsAutoCloseConnection = true
            }
                );
        }

        public async void InsertUpdateDocument(PageDocumentPoCo documentPoCo)
        {
            try
            {
                var poCo = await _db.Queryable<PageDocumentPoCo>().FirstAsync(x => x.FilePath == documentPoCo.FilePath);
                if (poCo != null)
                {
                    if (documentPoCo.Guid != poCo.Guid)
                        documentPoCo.Guid = poCo.Guid;
                    await _db.Updateable(documentPoCo).ExecuteCommandAsync();
                }
                else
                {
                    if (string.IsNullOrEmpty(documentPoCo.Guid))
                        documentPoCo.Guid = Guid.NewGuid().ToString();
                    await _db.Insertable(documentPoCo).ExecuteCommandAsync();

                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            
        }

        public async void DeleteDocument(PageDocumentPoCo documentPoCo)
        {

            var list = new List<PageDocumentPoCo> { documentPoCo };
            await _db.Deleteable<PageDocumentPoCo>().WhereColumns(list, it => new { it.FilePath }).ExecuteCommandAsync();
        }

        public PageDocumentPoCo FindFilePath(string filePath)
        {
            return _db.Queryable<PageDocumentPoCo>().First(it => it.FilePath == filePath);
        }

        public async Task<IList<PageDocumentPoCo>> Search(string searchString, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchString)||searchString.Length<2)
                    return null;

                return await _db.Queryable<PageDocumentPoCo>().Where(it => it.Title.Contains(searchString)
                                                                     || it.FilePath.Contains(searchString))
                    .ToListAsync(cancellationToken);

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
    }
}
