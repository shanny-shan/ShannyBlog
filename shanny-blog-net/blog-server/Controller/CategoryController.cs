using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Vos;
using blog_server.Service;
using Microsoft.AspNetCore.Mvc;

namespace blog_server.Controller
{
    [ApiController]
    [Route("category")]
    [Tags("菜单相关接口")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _log;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> log)
        {
            _categoryService = categoryService;
            _log = log;
        }

        /// <summary>
        /// 菜单获取
        /// </summary>
        [HttpGet("all")]
        [ProducesResponseType(typeof(Result<List<CategoryVO>>), 200)]
        public async Task<Result<List<CategoryVO>>> GetCategories()
        {
            try
            {
                return await _categoryService.GetCategories();
            }
            catch (Exception e)
            {
                _log.LogError(e, "查询全部分类异常");
                return Result<List<CategoryVO>>.Error(e.Message);
            }
        }

        /// <summary>
        /// 菜单添加
        /// </summary>
        [HttpPost("add")]
        [ProducesResponseType(typeof(Result<CategoryVO>), 200)]
        public async Task<Result<CategoryVO>> AddCategory([FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                return await _categoryService.AddCategory(categoryDTO);
            }
            catch (Exception e)
            {
                _log.LogError(e, "新增分类异常");
                return Result<CategoryVO>.Error(e.Message);
            }
        }

        /// <summary>
        /// 菜单修改
        /// </summary>
        [HttpPost("update")]
        [ProducesResponseType(typeof(Result<CategoryVO>), 200)]
        public async Task<Result<CategoryVO>> UpdateCategory([FromBody] CategoryDTO categoryDTO)
        {
            try
            {
                return await _categoryService.UpdateCategory(categoryDTO);
            }
            catch (Exception e)
            {
                _log.LogError(e, "修改分类异常");
                return Result<CategoryVO>.Error(e.Message);
            }
        }

        /// <summary>
        /// 菜单删除
        /// </summary>
        [HttpPost("delete")]
        [ProducesResponseType(typeof(Result<string>), 200)]
        public async Task<Result<string>> DeleteCategory(long id)
        {
            try
            {
                return await _categoryService.DeleteCategoryById(id);
            }
            catch (Exception e)
            {
                _log.LogError(e, "删除分类异常");
                return Result<string>.Error(e.Message);
            }
        }
    }
}