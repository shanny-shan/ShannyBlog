using blog_common.Result;
using blog_db;
using blog_db.Data;
using blog_pojo.Dtos;
using blog_pojo.Vos;
using Microsoft.EntityFrameworkCore;

namespace blog_server.Service.Impl
{
    public class ToolService : IToolService
    {
        private readonly _DbContext _dbContext;

        public ToolService(_DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<List<ToolVO>>> GetTools()
        {
            var toolList = await _dbContext.Set<Tool>().ToListAsync();
            List<ToolVO> voList = new List<ToolVO>();

            foreach (var tool in toolList)
            {
                ToolVO vo = MapEntityToVo(tool);
                vo.TagList = new List<Tag>();

                if (tool.Tags != null && tool.Tags.Any())
                {
                    var tagIds = tool.Tags;
                    var tagDataList = await _dbContext.Set<Tag>()
                        .Where(t => tagIds.Contains(t.Id))
                        .ToListAsync();
                    vo.TagList = tagDataList;
                }
                voList.Add(vo);
            }

            return Result<List<ToolVO>>.Success(voList);
        }

        public async Task<Result<ToolVO>> AddTool(ToolDTO toolDTO)
        {
            Tool entity = MapDtoToEntity(toolDTO);

            string src = "https://beijing-files.oss-cn-beijing.aliyuncs.com/shanny-blog/images/";
            Random rand = new Random();
            int randomNum = rand.Next(1, 7);
            entity.Image = $"{src}{randomNum}.jpg";

            entity.CreateTime = DateTime.Now;
            entity.UpdateTime = DateTime.Now;
            entity.Published = false;

            _dbContext.Set<Tool>().Add(entity);
            await _dbContext.SaveChangesAsync();

            ToolVO vo = MapEntityToVo(entity);
            return Result<ToolVO>.Success(vo);
        }

        public async Task<Result<ToolVO>> UpdateTool(ToolDTO toolDTO)
        {
            if (toolDTO.Id <= 0)
            {
                return Result<ToolVO>.Error("UPDATE_FAIL");
            }

            var dbTool = await _dbContext.Set<Tool>().FindAsync(toolDTO.Id);
            if (dbTool == null)
            {
                return Result<ToolVO>.Error("UPDATE_FAIL");
            }

            MapDtoCoverEntity(toolDTO, dbTool);
            dbTool.UpdateTime = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            ToolVO vo = MapEntityToVo(dbTool);
            return Result<ToolVO>.Success(vo);
        }

        public async Task<Result<string>> DeleteTool(long id)
        {
            var tool = await _dbContext.Set<Tool>().FindAsync(id);
            if (tool != null)
            {
                _dbContext.Set<Tool>().Remove(tool);
                await _dbContext.SaveChangesAsync();
            }
            return Result<string>.Success("DELETE_SUCCESS");
        }

        #region 映射方法
        private ToolVO MapEntityToVo(Tool source)
        {
            return new ToolVO
            {
                Id = source.Id,
                Title = source.Title,
                Content = source.Content,
                Image = source.Image,
                Href = source.Href,
                Tags = source.Tags,
                Published = source.Published,
                CreateTime = source.CreateTime,
                UpdateTime = source.UpdateTime
            };
        }

        private Tool MapDtoToEntity(ToolDTO source)
        {
            return new Tool
            {
                Id = source.Id > 0 ? source.Id : 0L,
                Title = source.Title,
                Content = source.Content,
                Href = source.Href,
                Tags = source.Tags ?? new List<long>()
            };
        }

        private void MapDtoCoverEntity(ToolDTO dto, Tool target)
        {
            if (!string.IsNullOrEmpty(dto.Title))
                target.Title = dto.Title;
            if (!string.IsNullOrEmpty(dto.Content))
                target.Content = dto.Content;
            if (!string.IsNullOrEmpty(dto.Href))
                target.Href = dto.Href;
            if (dto.Tags != null)
                target.Tags = dto.Tags;
        }
        #endregion
    }
}