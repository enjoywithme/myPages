using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MyPageLib.PoCo;
using System.Linq;
using Microsoft.VisualBasic.FileIO;
using SqlSugar;
using Meilisearch;
using mySharedLib;

namespace MyPageLib
{
    public class MyPageDb
    {
        public static MyPageDb Instance { get; } = new();

        public string? DataBaseName => MyPageSettings.Instance==null?null:Path.Combine(MyPageSettings.Instance.WorkingDirectory, "myPage.db");

        public string ConnectionString => $"Data Source={DataBaseName};";
        private static readonly List<string> SimilarSkipWords = new() { "pro", "ultimate" };

        //用单例模式
        private readonly SqlSugarScope _db;
        public SqlSugarScope Db => _db;

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



        public async void UpdateDocument(PageDocumentPoCo documentPoCo)
        {
            await _db.Updateable(documentPoCo).ExecuteCommandAsync();
        }

        public async void InsertDocument(PageDocumentPoCo documentPoCo)
        {
            if (string.IsNullOrEmpty(documentPoCo.Guid))
                documentPoCo.Guid = Guid.NewGuid().ToString();
            await _db.Insertable(documentPoCo).ExecuteCommandAsync();
        }


        /// <summary>
        /// 更新所有纪录的全文索引标志
        /// </summary>
        public void UpdateFullTextIndexed(int indexed=0)
        {
            _db.Updateable<object>()
                .AS("PG_DOCUMENT")
                .SetColumns("FullTextIndexed", indexed)
                .Where($"FullTextIndexed<>{indexed}")
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
            _db.Deleteable<PageDocumentPoCo>().Where(it => it.Name == poCo.Name
                                                           && it.FileExt == poCo.FileExt
                                                           && it.FolderPath == poCo.FolderPath
                                                           && it.TopFolder == poCo.TopFolder).ExecuteCommand();
        }

        public void DeleteDocument(PageDocumentPoCo poCo)
        {
            _db.Deleteable<PageDocumentPoCo>().Where(it => it.Name == poCo.Name
                                                           && it.FileExt == poCo.FileExt
                                                           && it.FolderPath == poCo.FolderPath
                                                           && it.TopFolder == poCo.TopFolder).ExecuteCommand();
        }

        /// <summary>
        /// 根据文件完整路径查找记录
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public PageDocumentPoCo? FindFilePath(string filePath)
        {
            var poCo = new PageDocumentPoCo() { FilePath = filePath };

            return _db.Queryable<PageDocumentPoCo>().First(it => it.Name == poCo.Name
            && it.FileExt == poCo.FileExt
            && it.FolderPath == poCo.FolderPath
            && it.TopFolder == poCo.TopFolder);
        }


        /// <summary>
        /// 根据记录的原始链接查找
        /// </summary>
        /// <param name="originUrl"></param>
        /// <returns></returns>
        public PageDocumentPoCo? FindOriginUrl(string originUrl)
        {
            return _db.Queryable<PageDocumentPoCo>().Where(it => it.OriginUrl == originUrl).OrderByDescending(x=>x.DtCreated).First();
        }


        /// <summary>
        /// 移动/重命名文件
        /// </summary>
        /// <param name="orgFileName"></param>
        /// <param name="dstFileName"></param>
        /// <returns></returns>
        public FuncResult MoveFile(string orgFileName, string dstFileName)
        {
            var ret = new FuncResult();
            try
            {
                File.Move(orgFileName, dstFileName, true);
                var orgPoCo = FindFilePath(orgFileName);
                if (orgPoCo != null)
                {
                    DeleteDocument(orgPoCo);
                    if (orgPoCo.FullTextIndexed == 1)
                    {
                        MyPageIndexer.Instance.DeleteDocumentFromMeiliIndex(orgPoCo);
                        
                    }
                }

                MyPageIndexer.Instance.IndexFile(dstFileName);

            }
            catch (Exception e)
            {
                ret.False(e.Message);
                return ret;
            }

            return ret;

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

        public IList<PageDocumentPoCo> FindLastDays(int n=0)
        {
            var dt = DateTime.Today.AddDays(-n);
            return _db.Queryable<PageDocumentPoCo>().Where(co=>co.DtModified>=dt).OrderByDescending(co => co.DtModified).ToList();
        }


        /// <summary>
        /// 根据标题查找类似的文章
        /// </summary>
        /// <param name="similarTitle"></param>
        /// <returns></returns>
        public IList<PageDocumentPoCo> FindSimilarTitle(string similarTitle)
        {
            var splits = similarTitle.Split();
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

                exp.And(it => it.Title!=null && it.Title.Contains(split));

            }


            return _db.Queryable<PageDocumentPoCo>().Where(exp.ToExpression()).ToList();
        }

        /// <summary>
        /// 删除一组文档
        /// </summary>
        /// <param name="documents"></param>
        /// <param name="message"></param>
        public bool BatchDelete(IList<PageDocumentPoCo> documents,out string message)
        {
            
            try
            {
                _db.Deleteable<PageDocumentPoCo>(documents).ExecuteCommand(); //批量删除
                foreach (var co in documents)
                {
                    if (File.Exists(co.FilePath))
                    {
                        //File.Delete(co.FilePath);
                        FileSystem.DeleteFile(co.FilePath,UIOption.OnlyErrorDialogs,RecycleOption.SendToRecycleBin);
                    }
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

        /// <summary>
        /// 根据全路径查找路径下的记录
        /// </summary>
        /// <param name="folderFullPath"></param>
        /// <returns></returns>
        public IList<PageDocumentPoCo>? FindFolderPath(string folderFullPath)
        {
            try
            {
                if(MyPageSettings.Instance==null) return null; 
                var (topFolder,topFolderPath, folderPath) = MyPageSettings.Instance.ParsePath(folderFullPath);

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
        /// 检查给定的完整目录下是否在数据库中有记录
        /// </summary>
        /// <param name="folderFullPath"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public bool IsDocumentInFolder(string folderFullPath)
        {
            if (MyPageSettings.Instance == null) 
                throw new Exception("没有正确的配置信息。");


            var (topFolder, topFolderPath, folderPath) = MyPageSettings.Instance.ParsePath(folderFullPath);
            
            if(topFolder == null)
                throw new Exception("不正确的顶级目录。");

            if(string.IsNullOrEmpty(folderPath))
                return _db.Queryable<PageDocumentPoCo>().Any(it => it.TopFolder == topFolder);

            var subFolder = folderPath + "\\";

            return _db.Queryable<PageDocumentPoCo>().Any(it => 
                it.TopFolder == topFolder
                && it.FolderPath!=null
                && (it.FolderPath == folderPath
                || it.FolderPath.StartsWith(subFolder)));
        }

        public bool IsFilesInFolder(string folderFullPath)
        {
            return Directory.EnumerateFileSystemEntries(folderFullPath).Any();
        }


        /// <summary>
        /// https://www.donet5.com/home/Doc?typeId=2314
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<IList<PageDocumentPoCo>?> Search(string searchString, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchString) || searchString.Length < 2)
                    return null;

                var fullIndexed = await SearchFullIndex(searchString, cancellationToken);
                
                if(fullIndexed == null||fullIndexed.Count==0) 
                    return await SearchLocalIndex(searchString, cancellationToken);
                return fullIndexed;
                

            }
            catch (Exception e)
            {
                MyLog.Log(e.Message);
            }

            return null;
        }


        /// <summary>
        /// 在全文索引中搜索
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<IList<PageDocumentPoCo>?> SearchFullIndex(string searchString, CancellationToken cancellationToken)
        {
            if (MyPageSettings.Instance == null || !MyPageSettings.Instance.EnableFullTextIndex ||
                string.IsNullOrEmpty(MyPageSettings.Instance.MeilisearchServer))
                return null;

            var client = new MeilisearchClient(MyPageSettings.Instance.MeilisearchServer, MyPageSettings.Instance.MeilisearchMasterKey);
            var index = client.Index(MyPageIndexer.MeilisearchIndexKey);

            var result = await index.SearchAsync<PageDocumentPoCo>(searchString, cancellationToken: cancellationToken);
            return result?.Hits.ToList();
        }

        /// <summary>
        /// 在本地index.db中搜索
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<IList<PageDocumentPoCo>?> SearchLocalIndex(string searchString, CancellationToken cancellationToken)
        {
            var splits = searchString.Split(Array.Empty<char>(), StringSplitOptions.RemoveEmptyEntries);

            var conModels = new List<IConditionalModel>
            {
                BuildConditionalCollections("Title", splits),
                BuildConditionalCollections("FolderPath", splits),
                BuildConditionalCollections("FileExt", splits),

                BuildNotDeletedCondition()
            };

            return await _db.Queryable<PageDocumentPoCo>().Where(conModels).ToListAsync(cancellationToken);
        }

        public void ReplaceFolderPath(string topFolder, string folderPath,string newFolderPath)
        {

            //更新目录下的文档
            var docs = _db.Queryable<PageDocumentPoCo>().Where(it => it.TopFolder == topFolder
            && it.FolderPath == folderPath).ToList();
            foreach (var poCo in docs)
            {
                poCo.FolderPath = newFolderPath;

            }
            _db.Updateable(docs)
                .UpdateColumns(it => new { it.FolderPath })
                .ExecuteCommand();

            //更新子目录下的文档
            var s = folderPath + "\\";
            docs = _db.Queryable<PageDocumentPoCo>().Where(it => it.TopFolder == topFolder
                                                                 && it.FolderPath!=null
                                                                 && it.FolderPath.StartsWith(s)).ToList();
            foreach (var poCo in docs)
            {
                if(poCo.FolderPath==null)
                    continue;
                poCo.FolderPath = poCo.FolderPath.Replace(folderPath, newFolderPath,
                    StringComparison.InvariantCultureIgnoreCase);
            }

            _db.Updateable(docs)
                .UpdateColumns(it => new { it.FolderPath })
                .ExecuteCommand();
        }
    }
}
