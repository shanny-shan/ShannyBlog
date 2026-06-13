using blog_common.Enums;
using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Vos;

namespace blog_server.Services
{
    public interface IArticleService
    {
        Result<ArticleVO> AddArticle(ArticleDTO articleDTO);

        Result<List<ArticleVO>> GetArticlesByRecent();

        Result<List<ArticleVO>> GetArticlesByType(CategoryType type);

        Result<ArticleVO> GetArticleById(long id);

        Result<ArticleVO> UpdateArticle(ArticleDTO articleDTO);

        Result<string> DeleteArticle(long id);

        Result<List<ArticleVO>> GetArticlesByView();

        Result<List<ArticleVO>> GetArticleByTag(long tagId);
    }
}
