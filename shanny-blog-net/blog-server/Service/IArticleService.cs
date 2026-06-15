using blog_common.Enums;
using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Vos;

namespace blog_server.Service
{
    public interface IArticleService
    {
        Task<Result<ArticleVO>> AddArticle(ArticleDTO articleDTO);

        Task<Result<List<ArticleVO>>> GetArticlesByRecent();

        Task<Result<List<ArticleVO>>> GetArticlesByType(string type);

        Task<Result<ArticleVO>> GetArticleById(long id);

        Task<Result<ArticleVO>> UpdateArticle(ArticleDTO articleDTO);

        Task<Result<string>> DeleteArticle(long id);

        Task<Result<List<ArticleVO>>> GetArticlesByView();

        Task<Result<List<ArticleVO>>> GetArticleByTag(long tagId);
    }
}