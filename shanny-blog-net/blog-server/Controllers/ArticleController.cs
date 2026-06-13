using blog_common.Enums;
using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Vos;
using blog_server.Services;
using Microsoft.AspNetCore.Mvc;

namespace blog_server.Controllers
{
    [ApiController]
    [Route("/article")]
    [Tags("文章相关接口")]
    public class ArticleController
    {
        private readonly IArticleService _articleService;
        private readonly ILogger<ArticleController> _log;

        public ArticleController(IArticleService articleService, ILogger<ArticleController> log)
        {
            _articleService = articleService;
            _log = log;
        }

        /// <summary>
        /// 文章获取
        /// </summary>
        [HttpGet("recent")]
        [ProducesResponseType(typeof(Result<List<ArticleVO>>), 200)]
        public Result<List<ArticleVO>> GetArticlesByRecent()
        {
            try
            {
                return _articleService.GetArticlesByRecent();
            }
            catch (Exception e)
            {
                return Result<List<ArticleVO>>.Error(e.Message);
            }
        }

        /// <summary>
        /// 文章获取
        /// </summary>
        [HttpGet("views")]
        [ProducesResponseType(typeof(Result<List<ArticleVO>>), 200)]
        public Result<List<ArticleVO>> GetArticlesByView()
        {
            try
            {
                return _articleService.GetArticlesByView();
            }
            catch (Exception e)
            {
                return Result<List<ArticleVO>>.Error(e.Message);
            }
        }

        /// <summary>
        /// 文章获取
        /// </summary>
        [HttpGet("type")]
        [ProducesResponseType(typeof(Result<List<ArticleVO>>), 200)]
        public Result<List<ArticleVO>> GetArticleByType(CategoryType type)
        {
            try
            {
                return _articleService.GetArticlesByType(type);
            }
            catch (Exception e)
            {
                return Result<List<ArticleVO>>.Error(e.Message);
            }
        }

        /// <summary>
        /// 文章获取
        /// </summary>
        [HttpGet("tag")]
        [ProducesResponseType(typeof(Result<List<ArticleVO>>), 200)]
        public Result<List<ArticleVO>> GetArticleByTag(long tagId)
        {
            try
            {
                return _articleService.GetArticleByTag(tagId);
            }
            catch (Exception e)
            {
                return Result<List<ArticleVO>>.Error(e.Message);
            }
        }

        /// <summary>
        /// 文章获取
        /// </summary>
        [HttpGet("id")]
        [ProducesResponseType(typeof(Result<ArticleVO>), 200)]
        public Result<ArticleVO> GetArticleById(long id)
        {
            try
            {
                return _articleService.GetArticleById(id);
            }
            catch (Exception e)
            {
                return Result<ArticleVO>.Error(e.Message);
            }
        }

        /// <summary>
        /// 文章添加
        /// </summary>
        [HttpPost("add")]
        [ProducesResponseType(typeof(Result<ArticleVO>), 200)]
        public Result<ArticleVO> AddArticle([FromBody] ArticleDTO articleDTO)
        {
            try
            {
                return _articleService.AddArticle(articleDTO);
            }
            catch (Exception e)
            {
                return Result<ArticleVO>.Error(e.Message);
            }
        }

        /// <summary>
        /// 文章修改
        /// </summary>
        [HttpPost("update")]
        [ProducesResponseType(typeof(Result<ArticleVO>), 200)]
        public Result<ArticleVO> UpdateArticle([FromBody] ArticleDTO articleDTO)
        {
            try
            {
                return _articleService.UpdateArticle(articleDTO);
            }
            catch (Exception e)
            {
                return Result<ArticleVO>.Error(e.Message);
            }
        }

        /// <summary>
        /// 文章删除
        /// </summary>
        [HttpPost("delete")]
        [ProducesResponseType(typeof(Result<string>), 200)]
        public Result<string> DeleteArticle(long id)
        {
            try
            {
                return _articleService.DeleteArticle(id);
            }
            catch (Exception e)
            {
                return Result<string>.Error(e.Message);
            }
        }
    }
}
