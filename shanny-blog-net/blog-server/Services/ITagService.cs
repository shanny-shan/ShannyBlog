using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Vos;

namespace blog_server.Services
{
    public interface ITagService
    {
        Result<List<TagVO>> GetTags();

        Result<TagVO> AddTag(TagDTO tagDTO);

        Result<TagVO> GetTagsById(long id);

        Result<TagVO> UpdateTag(TagDTO tagDTO);

        Result<string> DeleteTagById(long id);
    }
}
