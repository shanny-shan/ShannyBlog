using blog_common.Config;
using blog_common.Result;
using blog_pojo.Vos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/project")]
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
    [ProducesResponseType(typeof(Result<ProjectInfoVO>), 200)]
    public Result<ProjectInfoVO> Info()
    {
        var vo = new ProjectInfoVO
        {
            Name = _appConfig.Name,
            Description = _appConfig.Description,
            Owner = _appConfig.Owner,
            Version = _appConfig.Version,
            BuildTime = _appConfig.BuildTime
        };
        return Result<ProjectInfoVO>.Success(vo);
    }
}