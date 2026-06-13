using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Vos;
using blog_server.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/tool")]
[Tags("工具相关接口")]
public class ToolController : ControllerBase
{
    private readonly ToolService _toolService;
    private readonly ILogger<ToolController> _log;

    public ToolController(ToolService toolService, ILogger<ToolController> log)
    {
        _toolService = toolService;
        _log = log;
    }

    /// <summary>
    /// 工具获取
    /// </summary>
    [HttpGet("all")]
    [ProducesResponseType(typeof(Result<List<ToolVO>>), 200)]
    public Result<List<ToolVO>> GetTools()
    {
        try
        {
            return _toolService.GetTools();
        }
        catch (Exception e)
        {
            return Result<List<ToolVO>>.Error(e.Message);
        }
    }

    /// <summary>
    /// 工具添加
    /// </summary>
    [HttpPost("add")]
    [ProducesResponseType(typeof(Result<ToolVO>), 200)]
    public Result<ToolVO> AddTool([FromBody] ToolDTO toolDTO)
    {
        try
        {
            return _toolService.AddTool(toolDTO);
        }
        catch (Exception e)
        {
            return Result<ToolVO>.Error(e.Message);
        }
    }

    /// <summary>
    /// 工具修改
    /// </summary>
    [HttpPost("update")]
    [ProducesResponseType(typeof(Result<ToolVO>), 200)]
    public Result<ToolVO> UpdateTool([FromBody] ToolDTO toolDTO)
    {
        try
        {
            return _toolService.UpdateTool(toolDTO);
        }
        catch (Exception e)
        {
            return Result<ToolVO>.Error(e.Message);
        }
    }

    /// <summary>
    /// 工具删除
    /// </summary>
    [HttpPost("delete")]
    [ProducesResponseType(typeof(Result<string>), 200)]
    public Result<string> DeleteTool(long id)
    {
        try
        {
            return _toolService.DeleteTool(id);
        }
        catch (Exception e)
        {
            return Result<string>.Error(e.Message);
        }
    }
}