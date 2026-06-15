using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Vos;
using blog_server.Service;
using Microsoft.AspNetCore.Mvc;

namespace blog_server.Controller
{
    [ApiController]
    [Route("/tool")]
    [Tags("工具相关接口")]
    public class ToolController : ControllerBase
    {
        private readonly IToolService _toolService;
        private readonly ILogger<ToolController> _log;

        public ToolController(IToolService toolService, ILogger<ToolController> log)
        {
            _toolService = toolService;
            _log = log;
        }

        /// <summary>
        /// 工具获取
        /// </summary>
        [HttpGet("all")]
        [ProducesResponseType(typeof(Result<List<ToolVO>>), 200)]
        public async Task<Result<List<ToolVO>>> GetTools()
        {
            try
            {
                return await _toolService.GetTools();
            }
            catch (Exception e)
            {
                _log.LogError(e, "查询全部工具异常");
                return Result<List<ToolVO>>.Error(e.Message);
            }
        }

        /// <summary>
        /// 工具添加
        /// </summary>
        [HttpPost("add")]
        [ProducesResponseType(typeof(Result<ToolVO>), 200)]
        public async Task<Result<ToolVO>> AddTool([FromBody] ToolDTO toolDTO)
        {
            try
            {
                return await _toolService.AddTool(toolDTO);
            }
            catch (Exception e)
            {
                _log.LogError(e, "新增工具异常");
                return Result<ToolVO>.Error(e.Message);
            }
        }

        /// <summary>
        /// 工具修改
        /// </summary>
        [HttpPost("update")]
        [ProducesResponseType(typeof(Result<ToolVO>), 200)]
        public async Task<Result<ToolVO>> UpdateTool([FromBody] ToolDTO toolDTO)
        {
            try
            {
                return await _toolService.UpdateTool(toolDTO);
            }
            catch (Exception e)
            {
                _log.LogError(e, "修改工具异常");
                return Result<ToolVO>.Error(e.Message);
            }
        }

        /// <summary>
        /// 工具删除
        /// </summary>
        [HttpPost("delete")]
        [ProducesResponseType(typeof(Result<string>), 200)]
        public async Task<Result<string>> DeleteTool(long id)
        {
            try
            {
                return await _toolService.DeleteTool(id);
            }
            catch (Exception e)
            {
                _log.LogError(e, "删除工具异常");
                return Result<string>.Error(e.Message);
            }
        }
    }
}