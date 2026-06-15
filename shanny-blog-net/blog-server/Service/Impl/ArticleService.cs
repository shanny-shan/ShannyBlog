using blog_common.Enums;
using blog_common.Result;
using blog_db;
using blog_db.Data;
using blog_pojo.Dtos;
using blog_pojo.Vos;
using Microsoft.EntityFrameworkCore;

namespace blog_server.Service.Impl
{
    public class ArticleService : IArticleService
    {
        private readonly _DbContext _dbContext;

        public ArticleService(_DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<List<ArticleVO>>> GetArticlesByRecent()
        {
            var articles = await _dbContext.Set<Article>()
                .OrderByDescending(a => a.UpdateTime)
                .Take(5)
                .ToListAsync();

            var voList = articles.Select(x => MapArticleToVO(x)).ToList();

            return Result<List<ArticleVO>>.Success(voList);
        }

        public async Task<Result<List<ArticleVO>>> GetArticlesByType(CategoryType type)
        {
            var articles = await _dbContext.Set<Article>()
                .Where(a => a.Type == type)
                .ToListAsync();

            var voList = articles.Select(x => MapArticleToVO(x)).ToList();

            return Result<List<ArticleVO>>.Success(voList);
        }

        public async Task<Result<List<ArticleVO>>> GetArticleByTag(long tagId)
        {
            // EF自带集合包含判断，不用JSON模糊匹配
            var articles = await _dbContext.Set<Article>()
                .Where(a => a.Tags.Contains(tagId))
                .ToListAsync();

            var voList = articles.Select(x => MapArticleToVO(x)).ToList();

            return Result<List<ArticleVO>>.Success(voList);
        }

        public async Task<Result<ArticleVO>> GetArticleById(long id)
        {
            var article = await _dbContext.Set<Article>().FindAsync(id);
            if (article == null)
                return Result<ArticleVO>.Error("NOT_FOUND");

            // 阅读量+1
            article.Views++;
            await _dbContext.SaveChangesAsync();

            var vo = MapArticleToVO(article, false);
            return Result<ArticleVO>.Success(vo);
        }

        public async Task<Result<ArticleVO>> AddArticle(ArticleDTO articleDTO)
        {
            var article = MapDtoToEntity(articleDTO);

            // 随机封面图
            string src = "https://beijing-files.oss-cn-beijing.aliyuncs.com/shanny-blog/images/";
            Random rand = new Random();
            int num = rand.Next(1, 7);
            article.Image = $"{src}{num}.jpg";
            article.Href = $"{src}5.jpg";

            article.CreateTime = DateTime.Now;
            article.UpdateTime = DateTime.Now;
            article.Views = 0;

            _dbContext.Set<Article>().Add(article);
            await _dbContext.SaveChangesAsync();

            var vo = MapArticleToVO(article, false);
            return Result<ArticleVO>.Success(vo);
        }

        public async Task<Result<ArticleVO>> UpdateArticle(ArticleDTO articleDTO)
        {
            if (articleDTO.Id <= 0)
                return Result<ArticleVO>.Error("UPDATE_FAIL");

            var dbArticle = await _dbContext.Set<Article>().FindAsync(articleDTO.Id);
            if (dbArticle == null)
                return Result<ArticleVO>.Error("UPDATE_FAIL");

            MapDtoToEntity(articleDTO, dbArticle);
            dbArticle.UpdateTime = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            var vo = MapArticleToVO(dbArticle, false);
            return Result<ArticleVO>.Success(vo);
        }

        public async Task<Result<string>> DeleteArticle(long id)
        {
            var article = await _dbContext.Set<Article>().FindAsync(id);
            if (article != null)
            {
                _dbContext.Set<Article>().Remove(article);
                await _dbContext.SaveChangesAsync();
            }
            return Result<string>.Success("DELETE_SUCCESS");
        }

        public async Task<Result<List<ArticleVO>>> GetArticlesByView()
        {
            var articles = await _dbContext.Set<Article>()
                .OrderByDescending(a => a.Views)
                .Take(5)
                .ToListAsync();

            var voList = articles.Select(x => MapArticleToVO(x)).ToList();

            return Result<List<ArticleVO>>.Success(voList);
        }

        #region 映射方法（改造：查询Tag、Category走DbContext，不再调用Mapper）
        private ArticleVO MapArticleToVO(Article article, bool fillTagCategory = true)
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

            if (!fillTagCategory)
                return vo;

            // 填充Tag列表
            vo.TagList = new List<Tag>();
            if (article.Tags != null && article.Tags.Any())
            {
                var tagIds = article.Tags;
                var tagList = _dbContext.Set<Tag>().Where(t => tagIds.Contains(t.Id)).ToList();
                vo.TagList = tagList;
            }

            // 填充分类
            if (article.CategoryId > 0)
            {
                var category = _dbContext.Set<Category>().Find(article.CategoryId);
                if (category != null)
                {
                    CategoryVO cateVo = new CategoryVO
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Type = category.Type
                    };
                    vo.Category = cateVo;
                }
            }
            return vo;
        }

        private void MapDtoToEntity(ArticleDTO dto, Article target)
        {
            if (dto.Title != null) target.Title = dto.Title;
            if (dto.Content != null) target.Content = dto.Content;
            if (dto.CategoryId > 0) target.CategoryId = dto.CategoryId;
            if (dto.Tags != null) target.Tags = dto.Tags;
            if (dto.Timelines != null) target.Timelines = dto.Timelines;
            if (dto.Memo != null) target.Memo = dto.Memo;
            if (dto.Published) target.Published = dto.Published;
            if (dto.Type > 0) target.Type = dto.Type;
        }

        private Article MapDtoToEntity(ArticleDTO dto)
        {
            Article entity = new Article();
            MapDtoToEntity(dto, entity);
            entity.Id = dto.Id;
            return entity;
        }
        #endregion
    }
}