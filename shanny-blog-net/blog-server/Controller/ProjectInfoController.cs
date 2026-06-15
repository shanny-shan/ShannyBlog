using blog_common.Config;
using blog_common.Result;
using blog_pojo.Vos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace blog_server.Controller
{
    [ApiController]
    [Route("project")]
    [Tags("项目信息")]
    public class ProjectInfoController : ControllerBase
    {
        private readonly AppConfig _appConfig;
        private readonly ILogger<ProjectInfoController> _log;

        public ProjectInfoController(AppConfig appConfig, ILogger<ProjectInfoController> log)
        {
            _appConfig = appConfig;
            _log = log;
        }

        /// <summary>
        /// 获取项目信息
        /// </summary>
        [HttpGet("info")]
        //[AllowAnonymous]
        [ProducesResponseType(typeof(Result<ProjectInfoVO>), 200)]
        public Result<ProjectInfoVO> Info()
        {
            try
            {
                var asmVer = Assembly.GetExecutingAssembly().GetName().Version?.ToString(3);

                var vo = new ProjectInfoVO
                {
                    Name = _appConfig.Name,
                    Description = _appConfig.Description,
                    Owner = _appConfig.Owner,
                    Version = asmVer ?? "1.0.0",
                    BuildTime = _appConfig.BuildTime,
                };
                return Result<ProjectInfoVO>.Success(vo);
            }
            catch (Exception e)
            {
                _log.LogError(e, "读取项目配置信息异常");
                return Result<ProjectInfoVO>.Error(e.Message);
            }
        }
    }
}