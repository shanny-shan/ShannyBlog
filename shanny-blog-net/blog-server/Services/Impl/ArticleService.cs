using blog_common.Enums;
using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Entities;
using blog_pojo.Vos;

namespace blog_server.Services.Impl
{
    public class ArticleService : IArticleService
    {
        private readonly ArticleMapper _articleMapper;
        private readonly TagMapper _tagMapper;
        private readonly CategoryMapper _categoryMapper;

        public ArticleService(ArticleMapper articleMapper, TagMapper tagMapper, CategoryMapper categoryMapper)
        {
            _articleMapper = articleMapper;
            _tagMapper = tagMapper;
            _categoryMapper = categoryMapper;
        }

        public Result<List<ArticleVO>> GetArticlesByRecent()
        {
            List<Article> articles = _articleMapper.GetByRecent();
            if (articles == null || articles.Count == 0)
            {
                return Result<List<ArticleVO>>.Success(new List<ArticleVO>());
            }

            List<ArticleVO> voList = new();
            foreach (var item in articles)
            {
                voList.Add(MapArticleToVO(item));
            }
            return Result<List<ArticleVO>>.Success(voList);
        }

        public Result<List<ArticleVO>> GetArticlesByType(CategoryType type)
        {
            List<Article> articles = _articleMapper.GetByType(type);
            List<ArticleVO> voList = new();
            foreach (var item in articles)
            {
                voList.Add(MapArticleToVO(item));
            }
            return Result<List<ArticleVO>>.Success(voList);
        }

        public Result<List<ArticleVO>> GetArticleByTag(long tagId)
        {
            List<Article> articles = _articleMapper.GetByTag(tagId);
            List<ArticleVO> voList = new();
            foreach (var item in articles)
            {
                voList.Add(MapArticleToVO(item));
            }
            return Result<List<ArticleVO>>.Success(voList);
        }

        public Result<ArticleVO> GetArticleById(long id)
        {
            Article article = _articleMapper.GetById(id);
            article.Views += 1;
            _articleMapper.UpdateArticleViews(article);

            ArticleVO vo = MapArticleToVO(article, false);
            return Result<ArticleVO>.Success(vo);
        }

        public Result<ArticleVO> AddArticle(ArticleDTO articleDTO)
        {
            Article article = MapDtoToEntity(articleDTO);

            string src = "https://beijing-files.oss-cn-beijing.aliyuncs.com/shanny-blog/images/";
            Random rand = new Random();
            int num = rand.Next(1, 7);
            article.Image = $"{src}{num}.jpg";
            article.Href = $"{src}5.jpg";

            _articleMapper.InsertArticle(article);
            ArticleVO vo = MapArticleToVO(article, false);
            return Result<ArticleVO>.Success(vo);
        }

        public Result<ArticleVO> UpdateArticle(ArticleDTO articleDTO)
        {
            if (articleDTO.Id == null)
            {
                return Result<ArticleVO>.Error("UPDATE_FAIL");
            }
            Article article = MapDtoToEntity(articleDTO);
            _articleMapper.UpdateArticle(article);
            ArticleVO vo = MapArticleToVO(article, false);
            return Result<ArticleVO>.Success(vo);
        }

        public Result<string> DeleteArticle(long id)
        {
            _articleMapper.DeleteById(id);
            return Result<string>.Success("DELETE_SUCCESS");
        }

        public Result<List<ArticleVO>> GetArticlesByView()
        {
            List<Article> articles = _articleMapper.GetByView();
            if (articles == null || articles.Count == 0)
            {
                return Result<List<ArticleVO>>.Success(new List<ArticleVO>());
            }

            List<ArticleVO> voList = new();
            foreach (var item in articles)
            {
                voList.Add(MapArticleToVO(item));
            }
            return Result<List<ArticleVO>>.Success(voList);
        }

        #region 映射工具方法
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

            vo.TagList = new List<Tag>();
            if (article.Tags != null && article.Tags.Count > 0)
            {
                foreach (long tid in article.Tags)
                {
                    Tag tag = _tagMapper.GetById(tid);
                    if (tag != null)
                        vo.TagList.Add(tag);
                }
            }

            Category category = _categoryMapper.GetById(article.CategoryId);
            if (category != null)
            {
                CategoryVO cateVo = new CategoryVO();
                cateVo.Id = category.Id;
                cateVo.Name = category.Name;
                cateVo.Type = category.Type;
                vo.Category = cateVo;
            }
            return vo;
        }

        private Article MapDtoToEntity(ArticleDTO dto)
        {
            Article entity = new Article();
            entity.Id = dto.Id;
            entity.Title = dto.Title;
            entity.Content = dto.Content;
            entity.CategoryId = dto.CategoryId;
            entity.Tags = dto.Tags;
            entity.Timelines = dto.Timelines;
            return entity;
        }
    }
        #endregion
}