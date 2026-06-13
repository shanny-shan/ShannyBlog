using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Vos;
using Microsoft.AspNetCore.Mvc;

namespace blog_server.Controllers
{
    [ApiController]
    [Route("/about")]
    [Tags("菜单相关接口")]
    public class AboutController : ControllerBase
    {
        private readonly AboutService _aboutService;
        private readonly ILogger<AboutController> _log;

        public AboutController(AboutService aboutService, ILogger<AboutController> log)
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
        public Result<List<AboutVO>> GetAboutMe()
        {
            try
            {
                return _aboutService.GetAboutMe();
            }
            catch (Exception e)
            {
                return Result<List<AboutVO>>.Error(e.Message);
            }
        }

        /// <summary>
        /// 作者信息获取
        /// </summary>
        [HttpGet("show")]
        [ProducesResponseType(typeof(Result<AboutVO>), 200)]
        public Result<AboutVO> GetAboutMeByShow()
        {
            try
            {
                return _aboutService.GetAboutMeByShow();
            }
            catch (Exception e)
            {
                return Result<AboutVO>.Error(e.Message);
            }
        }

        /// <summary>
        /// 作者信息添加
        /// </summary>
        [HttpPost("add")]
        [ProducesResponseType(typeof(Result<AboutVO>), 200)]
        public Result<AboutVO> AddAboutMe([FromBody] AboutDTO aboutDTO)
        {
            try
            {
                return _aboutService.AddAbout(aboutDTO);
            }
            catch (Exception e)
            {
                return Result<AboutVO>.Error(e.Message);
            }
        }

        /// <summary>
        /// 作者信息修改
        /// </summary>
        [HttpPost("update")]
        [ProducesResponseType(typeof(Result<AboutVO>), 200)]
        public Result<AboutVO> UpdateAboutMe([FromBody] AboutDTO aboutDTO)
        {
            try
            {
                return _aboutService.UpdateAbout(aboutDTO);
            }
            catch (Exception e)
            {
                return Result<AboutVO>.Error(e.Message);
            }
        }

        /// <summary>
        /// 作者信息删除
        /// </summary>
        [HttpPost("delete")]
        [ProducesResponseType(typeof(Result<string>), 200)]
        public Result<string> DeleteAboutMe(long id)
        {
            try
            {
                return _aboutService.DeleteAboutById(id);
            }
            catch (Exception e)
            {
                return Result<string>.Error(e.Message);
            }
        }
    }
}
