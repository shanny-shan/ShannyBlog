using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Vos;

namespace blog_server.Service
{
    public interface IToolService
    {
        Task<Result<List<ToolVO>>> GetTools();
        Task<Result<ToolVO>> AddTool(ToolDTO toolDTO);
        Task<Result<ToolVO>> UpdateTool(ToolDTO toolDTO);
        Task<Result<string>> DeleteTool(long id);
    }
}