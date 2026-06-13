using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Vos;
using blog_server.Services;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("/tag")]
[Tags("菜单相关接口")]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;
    private readonly ILogger<TagController> _log;

    public TagController(ITagService tagService, ILogger<TagController> log)
    {
        _tagService = tagService;
        _log = log;
    }

    /// <summary>
    /// 标签获取
    /// </summary>
    [HttpGet("all")]
    [ProducesResponseType(typeof(Result<List<TagVO>>), 200)]
    public Result<List<TagVO>> GetTags()
    {
        try
        {
            return _tagService.GetTags();
        }
        catch (Exception e)
        {
            return Result<List<TagVO>>.Error(e.Message);
        }
    }

    /// <summary>
    /// 通过id获取标签
    /// </summary>
    [HttpGet("id")]
    [ProducesResponseType(typeof(Result<TagVO>), 200)]
    public Result<TagVO> GetTagsById(long id)
    {
        try
        {
            return _tagService.GetTagsById(id);
        }
        catch (Exception e)
        {
            return Result<TagVO>.Error(e.Message);
        }
    }

    /// <summary>
    /// 标签添加
    /// </summary>
    [HttpPost("add")]
    [ProducesResponseType(typeof(Result<TagVO>), 200)]
    public Result<TagVO> AddTag([FromBody] TagDTO tagDTO)
    {
        try
        {
            return _tagService.AddTag(tagDTO);
        }
        catch (Exception e)
        {
            return Result<TagVO>.Error(e.Message);
        }
    }

    /// <summary>
    /// 标签修改
    /// </summary>
    [HttpPost("update")]
    [ProducesResponseType(typeof(Result<TagVO>), 200)]
    public Result<TagVO> UpdateTag([FromBody] TagDTO tagDTO)
    {
        try
        {
            return _tagService.UpdateTag(tagDTO);
        }
        catch (Exception e)
        {
            return Result<TagVO>.Error(e.Message);
        }
    }

    /// <summary>
    /// 标签删除
    /// </summary>
    [HttpPost("delete")]
    [ProducesResponseType(typeof(Result<string>), 200)]
    public Result<string> DeleteTag(long id)
    {
        try
        {
            return _tagService.DeleteTagById(id);
        }
        catch (Exception e)
        {
            return Result<string>.Error(e.Message);
        }
    }
}