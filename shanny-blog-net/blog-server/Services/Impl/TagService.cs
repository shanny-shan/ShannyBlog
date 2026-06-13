using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Entities;
using blog_pojo.Vos;

namespace blog_server.Services.Impl
{
    public class TagService : ITagService
    {
        private readonly TagMapper _tagMapper;

        public TagService(TagMapper tagMapper)
        {
            _tagMapper = tagMapper;
        }

        public Result<List<TagVO>> GetTags()
        {
            List<Tag> tagList = _tagMapper.GetAll();
            List<TagVO> voList = new List<TagVO>();
            foreach (var tag in tagList)
            {
                voList.Add(MapTagToVo(tag));
            }
            return Result<List<TagVO>>.Success(voList);
        }

        public Result<TagVO> GetTagsById(long id)
        {
            Tag tag = _tagMapper.GetById(id);
            TagVO vo = MapTagToVo(tag);
            return Result<TagVO>.Success(vo);
        }

        public Result<TagVO> AddTag(TagDTO tagDTO)
        {
            Tag tag = MapDtoToEntity(tagDTO);
            _tagMapper.InsertTag(tag);
            TagVO vo = MapTagToVo(tag);
            return Result<TagVO>.Success(vo);
        }

        public Result<TagVO> UpdateTag(TagDTO tagDTO)
        {
            if (tagDTO.Id == null)
            {
                return Result<TagVO>.Error("UPDATE_FAIL");
            }
            Tag tag = MapDtoToEntity(tagDTO);
            _tagMapper.UpdateTag(tag);
            TagVO vo = MapTagToVo(tag);
            return Result<TagVO>.Success(vo);
        }

        public Result<string> DeleteTagById(long id)
        {
            _tagMapper.DeleteById(id);
            return Result<string>.Success("DELETE_SUCCESS");
        }

        private TagVO MapTagToVo(Tag source)
        {
            return new TagVO
            {
                Id = source.Id,
                Name = source.Name
            };
        }

        private Tag MapDtoToEntity(TagDTO source)
        {
            return new Tag
            {
                Id = source.Id,
                Name = source.Name
            };
        }
    }
}