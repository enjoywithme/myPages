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


        private ConditionalCollections BuildConditionalCollections(string field, string[] values)
        {
            var list = new List<KeyValuePair<WhereType, ConditionalModel>>();

            var first =true;
            foreach (var split in values)
            {
                list.Add(new KeyValuePair<WhereType, ConditionalModel>(first? WhereType.Or : WhereType.And, 
                    new ConditionalModel()
                {

                    FieldName =field,
                    ConditionalType = ConditionalType.InLike,
                    FieldValue = split
                }));
                first = false;
            }

            var c = new ConditionalCollections
            {
                ConditionalList = list
            };
            return c;

        }

        /// <summary>
        /// https://www.donet5.com/home/Doc?typeId=2314
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IList<PageDocumentPoCo>> Search(string searchString, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchString)||searchString.Length<2)
                    return null;

                var splits = searchString.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);
                
                var conModels = new List<IConditionalModel>();
                
                conModels.Add(BuildConditionalCollections("Title",splits));
                conModels.Add(BuildConditionalCollections("FilePath", splits));

                return await _db.Queryable<PageDocumentPoCo>().Where(conModels).ToListAsync(cancellationToken);
                

                //return await _db.Queryable<PageDocumentPoCo>().Where(it => 
                //        splits.All(s=> it.Title.Contains(s)) 
                //        || splits.All(s=>it.FilePath.Contains(s))
                //        )
                //    .ToListAsync(cancellationToken);


            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                return null;
            }
        }
    }
}
