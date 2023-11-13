using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using MyPageLib.PoCo;
using System.Linq;
using SqlSugar;

namespace MyPageLib
{
    public class MyPageDb
    {
        public static MyPageDb Instance { get; } = new();

        public string DataBaseName => Path.Combine(MyPageSettings.Instance.WorkingDirectory, "myPage.db");

        public string ConnectionString => $"Data Source={DataBaseName};";
        private static readonly List<string> SimilarSkipWords = new() { "pro", "ultimate" };

        //用单例模式
        private readonly SqlSugarScope _db;

        public MyPageDb()
        {
            _db = new SqlSugarScope(new ConnectionConfig()
            {
                ConnectionString = ConnectionString,
                DbType = SqlSugar.DbType.Sqlite,
                IsAutoCloseConnection = true
            }
                );
        }

        public async void InsertUpdateDocument(PageDocumentPoCo documentPoCo)
        {

                var poCo = await _db.Queryable<PageDocumentPoCo>().FirstAsync(x 
                    => x.Name == documentPoCo.Name
                && x.TopFolder == documentPoCo.TopFolder
                && x.FolderPath == documentPoCo.FolderPath
                && x.FileExt == documentPoCo.FileExt);
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

        /// <summary>
        /// 更新所有纪录的本地文件标志为FALSE
        /// </summary>
        public void UpdateLocalPresentFalse()
        {
            _db.Updateable<object>()
                .AS("PG_DOCUMENT")
                .SetColumns("LOCAL_PRESENT", 0)
                .Where("LOCAL_PRESENT=1")
                .ExecuteCommand();
        }

        /// <summary>
        /// 删除本地文件不存在的条目
        /// </summary>
        public void CleanUpLocalNotPresent()
        {
            _db.Deleteable<PageDocumentPoCo>().Where(co => co.LocalPresent==0).ExecuteCommand();
        }


        public void DeleteDocumentByFilePath(string filePath)
        {
            var poCo = new PageDocumentPoCo { FilePath = filePath };
            _db.Deleteable(poCo).ExecuteCommand();
        }

        public PageDocumentPoCo FindFilePath(string filePath)
        {
            var poco = new PageDocumentPoCo() { FilePath = filePath };

            return _db.Queryable<PageDocumentPoCo>().First(it => it.Name == poco.Name
            && it.FileExt == poco.FileExt
            && it.FolderPath == poco.FolderPath
            && it.TopFolder == poco.TopFolder);
        }

        public PageDocumentPoCo? FindOriginUrl(string originUrl)
        {
            return _db.Queryable<PageDocumentPoCo>().First(it => it.OriginUrl == originUrl);
        }


        public bool MoveFile(string orgFileName, string dstFileName,out string message)
        {
            try
            {
                File.Move(orgFileName, dstFileName, true);
                DeleteDocumentByFilePath(orgFileName);
                MyPageIndexer.Instance.IndexFile(dstFileName);

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
        /// 按照更改日期返回最后N条纪录
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public IList<PageDocumentPoCo> FindLastN(int n)
        {
            return _db.Queryable<PageDocumentPoCo>().OrderByDescending(co => co.DtModified).Take(n).ToList();
        }

        public IList<PageDocumentPoCo> FindLastDays(int n)
        {
            var dt = DateTime.Now.AddDays(-n);
            return _db.Queryable<PageDocumentPoCo>().Where(co=>co.DtModified>=dt).OrderByDescending(co => co.DtModified).ToList();
        }

        public IList<PageDocumentPoCo> FindSimilarTitle(string similarTitle)
        {
            var splits = similarTitle.Split();
            var firstWord = true;
            var sb = new StringBuilder();
            var exp = Expressionable.Create<PageDocumentPoCo>();

            foreach (var split in splits)
            {
                if (split.Length <= 2 || SimilarSkipWords.Any(x => split.ToLower() == x)) continue;
                //if (firstWord)
                //{
                //    sb.Append($"Title like '%{split}%'");
                //    firstWord = false;
                //    continue;
                //}
                //sb.Append($"AND Title like '%{split}%'");

                exp.And(it => it.Title.Contains(split));

            }


            return _db.Queryable<PageDocumentPoCo>().Where(exp.ToExpression()).ToList();
        }

        /// <summary>
        /// 删除一组文档
        /// </summary>
        /// <param name="documents"></param>
        public bool BatchDelete(IList<PageDocumentPoCo> documents,out string message)
        {
            
            try
            {
                _db.Deleteable<PageDocumentPoCo>(documents).ExecuteCommand(); //批量删除
                foreach (var co in documents)
                {
                    if(File.Exists(co.FilePath))
                        File.Delete(co.FilePath);
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return false;
            }

            message = $"成功删除{documents.Count}条纪录。";
            return true;
        }

        private ConditionalCollections BuildConditionalCollections(string field, string[] values)
        {
            var list = new List<KeyValuePair<WhereType, ConditionalModel>>();

            var first = true;
            foreach (var split in values)
            {
                list.Add(new KeyValuePair<WhereType, ConditionalModel>(first ? WhereType.Or : WhereType.And,
                    new ConditionalModel()
                    {

                        FieldName = field,
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

        private ConditionalCollections BuildNotDeletedCondition()
        {
            var list = new List<KeyValuePair<WhereType, ConditionalModel>>();

            list.Add(new KeyValuePair<WhereType, ConditionalModel>(WhereType.And,
                new ConditionalModel
                {

                    FieldName = "LOCAL_PRESENT",
                    ConditionalType = ConditionalType.Equal,
                    FieldValue = "1"
                }));
            list.Add(new KeyValuePair<WhereType, ConditionalModel>(WhereType.And,
                new ConditionalModel
                {

                    FieldName = "DELETED",
                    ConditionalType = ConditionalType.Equal,
                    FieldValue = "0"
                }));

            var c = new ConditionalCollections
            {
                ConditionalList = list
            };
            return c;

        }


        public IList<PageDocumentPoCo> FindFolderPath(string folderFullPath)
        {
            try
            {
                var (topFolder, folderPath) = MyPageSettings.Instance.ParsePath(folderFullPath);

                return _db.Queryable<PageDocumentPoCo>().Where(it => it.FolderPath == folderPath
                && it.TopFolder == topFolder).OrderByDescending(it=>it.Title).ToList();
            }
            catch (Exception)
            {
                // ignored
            }

            return null;
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
                if (string.IsNullOrWhiteSpace(searchString) || searchString.Length < 2)
                    return null;

                var splits = searchString.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);

                var conModels = new List<IConditionalModel>
                {
                    BuildConditionalCollections("Title", splits),
                    BuildConditionalCollections("FolderPath", splits),
                    BuildConditionalCollections("FileExt", splits),

                    BuildNotDeletedCondition()
                };

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
