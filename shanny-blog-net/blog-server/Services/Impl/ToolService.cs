using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Entities;
using blog_pojo.Vos;

namespace blog_server.Services.Impl
{
    public class ToolService : IToolService
    {
        private readonly ToolMapper _toolMapper;
        private readonly TagMapper _tagMapper;

        public ToolService(ToolMapper toolMapper, TagMapper tagMapper)
        {
            _toolMapper = toolMapper;
            _tagMapper = tagMapper;
        }

        public Result<List<ToolVO>> GetTools()
        {
            List<Tool> toolList = _toolMapper.GetAll();
            List<ToolVO> voList = new List<ToolVO>();

            foreach (var tool in toolList)
            {
                ToolVO vo = MapEntityToVo(tool);
                vo.TagList = new List<Tag>();

                if (tool.Tags != null && tool.Tags.Count > 0)
                {
                    foreach (long tagId in tool.Tags)
                    {
                        Tag tag = _tagMapper.GetById(tagId);
                        if (tag != null)
                        {
                            vo.TagList.Add(tag);
                        }
                    }
                }
                voList.Add(vo);
            }

            return Result<List<ToolVO>>.Success(voList);
        }

        public Result<ToolVO> AddTool(ToolDTO toolDTO)
        {
            Tool entity = MapDtoToEntity(toolDTO);

            string src = "https://beijing-files.oss-cn-beijing.aliyuncs.com/shanny-blog/images/";
            Random rand = new Random();
            int randomNum = rand.Next(1, 7);
            entity.Image = $"{src}{randomNum}.jpg";

            _toolMapper.InsertTool(entity);
            ToolVO vo = MapEntityToVo(entity);

            return Result<ToolVO>.Success(vo);
        }

        public Result<ToolVO> UpdateTool(ToolDTO toolDTO)
        {
            if (toolDTO.Id == null)
            {
                return Result<ToolVO>.Error("UPDATE_FAIL");
            }

            Tool entity = MapDtoToEntity(toolDTO);
            _toolMapper.UpdateTool(entity);
            ToolVO vo = MapEntityToVo(entity);

            return Result<ToolVO>.Success(vo);
        }

        public Result<string> DeleteTool(long id)
        {
            _toolMapper.DeleteById(id);
            return Result<string>.Success("DELETE_SUCCESS");
        }

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
                Id = source.Id,
                Title = source.Title,
                Content = source.Content,
                Href = source.Href,
                Tags = source.Tags,
            };
        }
    }
}