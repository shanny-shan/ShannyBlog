using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Vos;

namespace blog_server.Service
{
    public interface ITagService
    {
        Task<Result<List<TagVO>>> GetTags();
        Task<Result<TagVO>> GetTagsById(long id);
        Task<Result<TagVO>> AddTag(TagDTO tagDTO);
        Task<Result<TagVO>> UpdateTag(TagDTO tagDTO);
        Task<Result<string>> DeleteTagById(long id);
    }
}