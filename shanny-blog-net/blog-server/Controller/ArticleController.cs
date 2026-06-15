using blog_common.Enums;
using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Vos;
using blog_server.Service;
using Microsoft.AspNetCore.Mvc;

namespace blog_server.Controller
{
    [ApiController]
    [Route("/article")]
    [Tags("文章相关接口")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly ILogger<ArticleController> _log;

        public ArticleController(IArticleService articleService, ILogger<ArticleController> log)
        {
            _articleService = articleService;
            _log = log;
        }

        /// <summary>
        /// 文章获取-最新
        /// </summary>
        [HttpGet("recent")]
        [ProducesResponseType(typeof(Result<List<ArticleVO>>), 200)]
        public async Task<Result<List<ArticleVO>>> GetArticlesByRecent()
        {
            try
            {
                return await _articleService.GetArticlesByRecent();
            }
            catch (Exception e)
            {
                _log.LogError(e, "查询最新文章异常");
                return Result<List<ArticleVO>>.Error(e.Message);
            }
        }

        /// <summary>
        /// 文章获取-浏览量排行
        /// </summary>
        [HttpGet("views")]
        [ProducesResponseType(typeof(Result<List<ArticleVO>>), 200)]
        public async Task<Result<List<ArticleVO>>> GetArticlesByView()
        {
            try
            {
                return await _articleService.GetArticlesByView();
            }
            catch (Exception e)
            {
                _log.LogError(e, "查询高浏览文章异常");
                return Result<List<ArticleVO>>.Error(e.Message);
            }
        }

        /// <summary>
        /// 文章获取-按分类
        /// </summary>
        [HttpGet("type")]
        [ProducesResponseType(typeof(Result<List<ArticleVO>>), 200)]
        public async Task<Result<List<ArticleVO>>> GetArticleByType(CategoryType type)
        {
            try
            {
                return await _articleService.GetArticlesByType(type);
            }
            catch (Exception e)
            {
                _log.LogError(e, "按分类查询文章异常");
                return Result<List<ArticleVO>>.Error(e.Message);
            }
        }

        /// <summary>
        /// 文章获取-按标签
        /// </summary>
        [HttpGet("tag")]
        [ProducesResponseType(typeof(Result<List<ArticleVO>>), 200)]
        public async Task<Result<List<ArticleVO>>> GetArticleByTag(long tagId)
        {
            try
            {
                return await _articleService.GetArticleByTag(tagId);
            }
            catch (Exception e)
            {
                _log.LogError(e, "按标签查询文章异常");
                return Result<List<ArticleVO>>.Error(e.Message);
            }
        }

        /// <summary>
        /// 文章获取-单条详情
        /// </summary>
        [HttpGet("id")]
        [ProducesResponseType(typeof(Result<ArticleVO>), 200)]
        public async Task<Result<ArticleVO>> GetArticleById(long id)
        {
            try
            {
                return await _articleService.GetArticleById(id);
            }
            catch (Exception e)
            {
                _log.LogError(e, "查询文章详情异常");
                return Result<ArticleVO>.Error(e.Message);
            }
        }

        /// <summary>
        /// 文章添加
        /// </summary>
        [HttpPost("add")]
        [ProducesResponseType(typeof(Result<ArticleVO>), 200)]
        public async Task<Result<ArticleVO>> AddArticle([FromBody] ArticleDTO articleDTO)
        {
            try
            {
                return await _articleService.AddArticle(articleDTO);
            }
            catch (Exception e)
            {
                _log.LogError(e, "新增文章异常");
                return Result<ArticleVO>.Error(e.Message);
            }
        }

        /// <summary>
        /// 文章修改
        /// </summary>
        [HttpPost("update")]
        [ProducesResponseType(typeof(Result<ArticleVO>), 200)]
        public async Task<Result<ArticleVO>> UpdateArticle([FromBody] ArticleDTO articleDTO)
        {
            try
            {
                return await _articleService.UpdateArticle(articleDTO);
            }
            catch (Exception e)
            {
                _log.LogError(e, "修改文章异常");
                return Result<ArticleVO>.Error(e.Message);
            }
        }

        /// <summary>
        /// 文章删除
        /// </summary>
        [HttpPost("delete")]
        [ProducesResponseType(typeof(Result<string>), 200)]
        public async Task<Result<string>> DeleteArticle(long id)
        {
            try
            {
                return await _articleService.DeleteArticle(id);
            }
            catch (Exception e)
            {
                _log.LogError(e, "删除文章异常");
                return Result<string>.Error(e.Message);
            }
        }
    }
}