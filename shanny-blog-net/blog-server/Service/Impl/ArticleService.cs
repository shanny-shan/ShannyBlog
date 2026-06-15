using blog_common.Constant;
using blog_common.Enums;
using blog_common.Result;
using blog_db;
using blog_db.Data;
using blog_pojo.Dtos;
using blog_pojo.Vos;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection;

namespace blog_server.Service.Impl
{
    public class ArticleService : IArticleService
    {
        private readonly _DbContext _dbContext;
        private readonly Random _rand = new Random();

        public ArticleService(_DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region 
        private static T GetEnumByDescription<T>(string desc) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                var descAttr = field.GetCustomAttribute<DescriptionAttribute>();
                if (descAttr != null && descAttr.Description == desc)
                {
                    return (T)field.GetValue(null)!;
                }
            }
            throw new ArgumentException("无匹配枚举值");
        }
        #endregion

        public async Task<Result<List<ArticleVO>>> GetArticlesByRecent()
        {
            var articles = await _dbContext.Set<Article>()
                .OrderByDescending(a => a.UpdateTime)
                .Take(5)
                .ToListAsync();

            var allTagIds = articles
                .Where(a => a.Tags != null && a.Tags.Any())
                .SelectMany(a => a.Tags)
                .Distinct()
                .ToList();
            var allCateIds = articles
                .Where(a => a.CategoryId > 0)
                .Select(a => a.CategoryId)
                .Distinct()
                .ToList();

            var tagDict = (await _dbContext.Set<Tag>()
                .Where(t => allTagIds.Contains(t.Id))
                .ToListAsync()).ToDictionary(t => t.Id);

            var cateDict = (await _dbContext.Set<Category>()
                .Where(c => allCateIds.Contains(c.Id))
                .ToListAsync()).ToDictionary(c => c.Id);

            var voList = new List<ArticleVO>();
            foreach (var item in articles)
            {
                voList.Add(MapArticleToVO(item, tagDict, cateDict));
            }

            return Result<List<ArticleVO>>.Success(voList);
        }

        public async Task<Result<List<ArticleVO>>> GetArticlesByType(string type)
        {
            if (!Enum.TryParse<CategoryType>(type, out var realType))
            {
                return Result<List<ArticleVO>>.Error(ResultMsg.ParamInvalid);
            }

            var articles = await _dbContext.Set<Article>()
                .Where(a => a.Type == realType)
                .ToListAsync();

            var allTagIds = articles
                .Where(a => a.Tags != null && a.Tags.Any())
                .SelectMany(a => a.Tags)
                .Distinct()
                .ToList();
            var allCateIds = articles
                .Where(a => a.CategoryId > 0)
                .Select(a => a.CategoryId)
                .Distinct()
                .ToList();

            var tagDict = (await _dbContext.Set<Tag>()
                .Where(t => allTagIds.Contains(t.Id))
                .ToListAsync()).ToDictionary(t => t.Id);

            var cateDict = (await _dbContext.Set<Category>()
                .Where(c => allCateIds.Contains(c.Id))
                .ToListAsync()).ToDictionary(c => c.Id);

            var voList = new List<ArticleVO>();
            foreach (var item in articles)
            {
                voList.Add(MapArticleToVO(item, tagDict, cateDict));
            }

            return Result<List<ArticleVO>>.Success(voList);
        }

        public async Task<Result<List<ArticleVO>>> GetArticleByTag(long tagId)
        {
            var allArticles = await _dbContext.Set<Article>().ToListAsync();
            var articles = allArticles.Where(a => a.Tags != null && a.Tags.Contains(tagId)).ToList();

            var allTagIds = articles
                .Where(a => a.Tags != null && a.Tags.Any())
                .SelectMany(a => a.Tags)
                .Distinct()
                .ToList();

            var allCateIds = articles
                .Where(a => a.CategoryId > 0)
                .Select(a => a.CategoryId)
                .Distinct()
                .ToList();

            var tagDict = (await _dbContext.Set<Tag>()
                .Where(t => allTagIds.Contains(t.Id))
                .ToListAsync()).ToDictionary(t => t.Id);

            var cateDict = (await _dbContext.Set<Category>()
                .Where(c => allCateIds.Contains(c.Id))
                .ToListAsync()).ToDictionary(c => c.Id);

            var voList = new List<ArticleVO>();
            foreach (var item in articles)
            {
                voList.Add(MapArticleToVO(item, tagDict, cateDict));
            }

            return Result<List<ArticleVO>>.Success(voList);
        }

        public async Task<Result<ArticleVO>> GetArticleById(long id)
        {
            var article = await _dbContext.Set<Article>().FindAsync(id);
            if (article == null)
                return Result<ArticleVO>.Error(ResultMsg.SelectFail);

            article.Views++;
            await _dbContext.SaveChangesAsync();

            Dictionary<long, Tag> tagDict = new();
            Dictionary<long, Category> cateDict = new();

            if (article.Tags != null && article.Tags.Any())
            {
                tagDict = (await _dbContext.Set<Tag>()
                    .Where(t => article.Tags.Contains(t.Id))
                    .ToListAsync()).ToDictionary(x => x.Id);
            }
            if (article.CategoryId > 0)
            {
                var cate = await _dbContext.Set<Category>().FindAsync(article.CategoryId);
                if (cate != null)
                    cateDict.Add(cate.Id, cate);
            }

            var vo = MapArticleToVO(article, tagDict, cateDict, false);
            return Result<ArticleVO>.Success(vo);
        }

        public async Task<Result<ArticleVO>> AddArticle(ArticleDTO articleDTO)
        {
            var article = MapDtoToEntity(articleDTO);

            string src = "https://beijing-files.oss-cn-beijing.aliyuncs.com/shanny-blog/images/";
            int num = _rand.Next(1, 7);
            article.Image = $"{src}{num}.jpg";
            article.Href = $"{src}5.jpg";

            article.CreateTime = DateTime.Now;
            article.UpdateTime = DateTime.Now;
            article.Views = 0;

            _dbContext.Set<Article>().Add(article);
            await _dbContext.SaveChangesAsync();

            var emptyTagDict = new Dictionary<long, Tag>();
            var emptyCateDict = new Dictionary<long, Category>();
            var vo = MapArticleToVO(article, emptyTagDict, emptyCateDict, false);
            return Result<ArticleVO>.Success(ResultMsg.InsertSuccess, vo);
        }

        public async Task<Result<ArticleVO>> UpdateArticle(ArticleDTO articleDTO)
        {
            if (articleDTO.Id <= 0)
                return Result<ArticleVO>.Error(ResultMsg.UpdateFail);

            var dbArticle = await _dbContext.Set<Article>().FindAsync(articleDTO.Id);
            if (dbArticle == null)
                return Result<ArticleVO>.Error(ResultMsg.UpdateFail);

            MapDtoToEntity(articleDTO, dbArticle);
            dbArticle.UpdateTime = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            var emptyTagDict = new Dictionary<long, Tag>();
            var emptyCateDict = new Dictionary<long, Category>();
            var vo = MapArticleToVO(dbArticle, emptyTagDict, emptyCateDict, false);
            return Result<ArticleVO>.Success(ResultMsg.UpdateSuccess, vo);
        }

        public async Task<Result<string>> DeleteArticle(long id)
        {
            var article = await _dbContext.Set<Article>().FindAsync(id);
            if (article == null)
            {
                return Result<string>.Error(ResultMsg.DeleteFail);
            }
            _dbContext.Set<Article>().Remove(article);
            await _dbContext.SaveChangesAsync();
            return Result<string>.Success(ResultMsg.DeleteSuccess);
        }

        public async Task<Result<List<ArticleVO>>> GetArticlesByView()
        {
            var articles = await _dbContext.Set<Article>()
                .OrderByDescending(a => a.Views)
                .Take(5)
                .ToListAsync();

            var allTagIds = articles
                .Where(a => a.Tags != null && a.Tags.Any())
                .SelectMany(a => a.Tags)
                .Distinct()
                .ToList();
            var allCateIds = articles
                .Where(a => a.CategoryId > 0)
                .Select(a => a.CategoryId)
                .Distinct()
                .ToList();

            var tagDict = (await _dbContext.Set<Tag>()
                .Where(t => allTagIds.Contains(t.Id))
                .ToListAsync()).ToDictionary(t => t.Id);

            var cateDict = (await _dbContext.Set<Category>()
                .Where(c => allCateIds.Contains(c.Id))
                .ToListAsync()).ToDictionary(c => c.Id);

            var voList = new List<ArticleVO>();
            foreach (var item in articles)
            {
                voList.Add(MapArticleToVO(item, tagDict, cateDict));
            }

            return Result<List<ArticleVO>>.Success(voList);
        }

        #region 
        private ArticleVO MapArticleToVO(Article article, Dictionary<long, Tag> tagDict, Dictionary<long, Category> cateDict, bool fillTagCategory = true)
        {
            ArticleVO vo = new ArticleVO();
            vo.Id = article.Id;
            vo.Title = article.Title;
            vo.Content = article.Content;
            vo.Image = article.Image;
            vo.Href = article.Href;
            vo.Views = article.Views;
            vo.Timelines = article.Timelines;
            vo.Tags = article.Tags;
            vo.Published = article.Published;
            vo.CreateTime = article.CreateTime;
            vo.UpdateTime = article.UpdateTime;
            vo.CategoryId = article.CategoryId;
            vo.Type = article.Type;

            if (!fillTagCategory)
                return vo;

            vo.TagList = new List<Tag>();
            if (article.Tags != null && article.Tags.Any())
            {
                vo.TagList = article.Tags
                    .Where(tagDict.ContainsKey)
                    .Select(p => tagDict[p])
                    .ToList();
            }

            if (article.CategoryId > 0 && cateDict.ContainsKey(article.CategoryId))
            {
                var category = cateDict[article.CategoryId];
                vo.Category = new CategoryVO
                {
                    Id = category.Id,
                    Name = category.Name,
                    Type = category.Type
                };
            }
            return vo;
        }

        private void MapDtoToEntity(ArticleDTO dto, Article target)
        {
            target.Title = dto.Title;
            target.Content = dto.Content;
            target.CategoryId = dto.CategoryId;
            target.Tags = dto.Tags;
            target.Timelines = dto.Timelines;
            target.Memo = dto.Memo;
            target.Published = dto.Published;
            target.Type = dto.Type;
            target.Image = dto.Image;
            target.Href = dto.Href;
            target.Views = dto.Views;
        }

        private Article MapDtoToEntity(ArticleDTO dto)
        {
            Article entity = new Article();
            MapDtoToEntity(dto, entity);
            return entity;
        }
        #endregion
    }
}