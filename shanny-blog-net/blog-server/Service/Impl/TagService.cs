using blog_common.Constant;
using blog_common.Result;
using blog_db;
using blog_db.Data;
using blog_pojo.Dtos;
using blog_pojo.Vos;
using Microsoft.EntityFrameworkCore;

namespace blog_server.Service.Impl
{
    public class TagService : ITagService
    {
        private readonly _DbContext _dbContext;

        public TagService(_DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<List<TagVO>>> GetTags()
        {
            var tagList = await _dbContext.Set<Tag>().ToListAsync();
            var voList = tagList.Select(x => MapTagToVo(x)).ToList();
            return Result<List<TagVO>>.Success(voList);
        }

        public async Task<Result<TagVO>> GetTagsById(long id)
        {
            var tag = await _dbContext.Set<Tag>().FindAsync(id);
            if (tag == null)
                return Result<TagVO>.Error(ResultMsg.AccountNotFound);
            TagVO vo = MapTagToVo(tag);
            return Result<TagVO>.Success(vo);
        }

        public async Task<Result<TagVO>> AddTag(TagDTO tagDTO)
        {
            Tag tag = MapDtoToEntity(tagDTO);
            _dbContext.Set<Tag>().Add(tag);
            await _dbContext.SaveChangesAsync();
            TagVO vo = MapTagToVo(tag);
            return Result<TagVO>.Success(ResultMsg.InsertSuccess, vo);
        }

        public async Task<Result<TagVO>> UpdateTag(TagDTO tagDTO)
        {
            if (tagDTO.Id <= 0)
                return Result<TagVO>.Error(ResultMsg.UpdateFail);

            var dbTag = await _dbContext.Set<Tag>().FindAsync(tagDTO.Id);
            if (dbTag == null)
                return Result<TagVO>.Error(ResultMsg.UpdateFail);

            MapDtoCoverEntity(tagDTO, dbTag);
            await _dbContext.SaveChangesAsync();

            TagVO vo = MapTagToVo(dbTag);
            return Result<TagVO>.Success(ResultMsg.UpdateSuccess, vo);
        }

        public async Task<Result<string>> DeleteTagById(long id)
        {
            var tag = await _dbContext.Set<Tag>().FindAsync(id);
            if (tag != null)
            {
                _dbContext.Set<Tag>().Remove(tag);
                await _dbContext.SaveChangesAsync();
            }
            return Result<string>.Success(ResultMsg.DeleteSuccess);
        }

        #region 映射方法
        private TagVO MapTagToVo(Tag source)
        {
            return new TagVO
            {
                Id = source.Id,
                Name = source.Name,
                NameEn = source.NameEn
            };
        }

        private Tag MapDtoToEntity(TagDTO source)
        {
            return new Tag
            {
                Id = source.Id > 0 ? source.Id : 0L,
                Name = source.Name,
                NameEn = source.NameEn
            };
        }

        private void MapDtoCoverEntity(TagDTO dto, Tag target)
        {
            if (!string.IsNullOrEmpty(dto.Name))
                target.Name = dto.Name;
            if (!string.IsNullOrEmpty(dto.NameEn))
                target.NameEn = dto.NameEn;
        }
        #endregion
    }
}