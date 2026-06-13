using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Vos;

namespace blog_server.Services
{
    public interface IToolService
    {
        Result<List<ToolVO>> GetTools();

        Result<ToolVO> AddTool(ToolDTO toolDTO);

        Result<ToolVO> UpdateTool(ToolDTO toolDTO);

        Result<string> DeleteTool(long id);
    }
}
