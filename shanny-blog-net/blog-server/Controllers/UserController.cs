using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Vos;
using blog_server.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/account")]
[Tags("账户相关接口")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _log;

    public UserController(IUserService userService, ILogger<UserController> log)
    {
        _userService = userService;
        _log = log;
    }

    /// <summary>
    /// 用户注册
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(typeof(Result<string>), 200)]
    public Result<string> Register([FromBody] RegisterDTO registerDTO)
    {
        try
        {
            return _userService.Save(registerDTO);
        }
        catch (Exception e)
        {
            return Result<string>.Error(e.Message);
        }
    }

    /// <summary>
    /// 用户登录
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(Result<LoginVO>), 200)]
    public Result<LoginVO> Login([FromBody] LoginDTO loginDTO)
    {
        try
        {
            return _userService.Login(loginDTO);
        }
        catch (Exception e)
        {
            return Result<LoginVO>.Error(e.Message);
        }
    }

    /// <summary>
    /// 获取用户基础信息
    /// </summary>
    [HttpGet("userinfo")]
    [ProducesResponseType(typeof(Result<UserInfoVO>), 200)]
    public Result<UserInfoVO> GetUserInfo()
    {
        try
        {
            return _userService.GetUserInfo();
        }
        catch (Exception e)
        {
            return Result<UserInfoVO>.Error(e.Message);
        }
    }

    /// <summary>
    /// 获取所有用户数据
    /// </summary>
    [HttpGet("users")]
    [ProducesResponseType(typeof(Result<List<UserInfoVO>>), 200)]
    public Result<List<UserInfoVO>> GetUsers()
    {
        try
        {
            return _userService.GetUsers();
        }
        catch (Exception e)
        {
            return Result<List<UserInfoVO>>.Error(e.Message);
        }
    }

    /// <summary>
    /// 用户更新
    /// </summary>
    [HttpPost("update")]
    [ProducesResponseType(typeof(Result<UserInfoVO>), 200)]
    public Result<UserInfoVO> UpdateUserInfo([FromBody] UserInfoDTO userInfoDTO)
    {
        try
        {
            return _userService.UpdateUserInfo(userInfoDTO);
        }
        catch (Exception e)
        {
            return Result<UserInfoVO>.Error(e.Message);
        }
    }

    /// <summary>
    /// 账户删除
    /// </summary>
    [HttpPost("delete")]
    [ProducesResponseType(typeof(Result<string>), 200)]
    public Result<string> DeleteUser(string uuid)
    {
        try
        {
            return _userService.DeleteUserByUuid(uuid);
        }
        catch (Exception e)
        {
            return Result<string>.Error(e.Message);
        }
    }
}