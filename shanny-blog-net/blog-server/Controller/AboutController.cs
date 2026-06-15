using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Vos;
using blog_server.Service;
using Microsoft.AspNetCore.Mvc;

namespace blog_server.Controller
{
    [ApiController]
    [Route("about")]
    [Tags("菜单相关接口")]
    public class AboutController : ControllerBase
    {
        // 注入接口IAboutService
        private readonly IAboutService _aboutService;
        private readonly ILogger<AboutController> _log;

        public AboutController(IAboutService aboutService, ILogger<AboutController> log)
        {
            _aboutService = aboutService;
            _log = log;
        }

        /// <summary>
        /// 作者信息获取
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [ProducesResponseType(typeof(Result<List<AboutVO>>), 200)]
        public async Task<Result<List<AboutVO>>> GetAboutMe()
        {
            try
            {
                return await _aboutService.GetAboutMe();
            }
            catch (Exception e)
            {
                _log.LogError(e, "查询全部作者信息异常");
                return Result<List<AboutVO>>.Error(e.Message);
            }
        }

        /// <summary>
        /// 作者信息获取
        /// </summary>
        [HttpGet("show")]
        [ProducesResponseType(typeof(Result<AboutVO>), 200)]
        public async Task<Result<AboutVO>> GetAboutMeByShow()
        {
            try
            {
                return await _aboutService.GetAboutMeByShow();
            }
            catch (Exception e)
            {
                _log.LogError(e, "查询展示作者信息异常");
                return Result<AboutVO>.Error(e.Message);
            }
        }

        /// <summary>
        /// 作者信息添加
        /// </summary>
        [HttpPost("add")]
        [ProducesResponseType(typeof(Result<AboutVO>), 200)]
        public async Task<Result<AboutVO>> AddAboutMe([FromBody] AboutDTO aboutDTO)
        {
            try
            {
                return await _aboutService.AddAbout(aboutDTO);
            }
            catch (Exception e)
            {
                _log.LogError(e, "新增作者信息异常");
                return Result<AboutVO>.Error(e.Message);
            }
        }

        /// <summary>
        /// 作者信息修改
        /// </summary>
        [HttpPost("update")]
        [ProducesResponseType(typeof(Result<AboutVO>), 200)]
        public async Task<Result<AboutVO>> UpdateAboutMe([FromBody] AboutDTO aboutDTO)
        {
            try
            {
                return await _aboutService.UpdateAbout(aboutDTO);
            }
            catch (Exception e)
            {
                _log.LogError(e, "修改作者信息异常");
                return Result<AboutVO>.Error(e.Message);
            }
        }

        /// <summary>
        /// 作者信息删除
        /// </summary>
        [HttpPost("delete")]
        [ProducesResponseType(typeof(Result<string>), 200)]
        public async Task<Result<string>> DeleteAboutMe(long id)
        {
            try
            {
                return await _aboutService.DeleteAboutById(id);
            }
            catch (Exception e)
            {
                _log.LogError(e, "删除作者信息异常");
                return Result<string>.Error(e.Message);
            }
        }
    }
}