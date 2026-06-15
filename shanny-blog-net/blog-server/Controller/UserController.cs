using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Vos;
using blog_server.Service;
using Microsoft.AspNetCore.Mvc;

namespace blog_server.Controller
{
    [ApiController]
    [Route("account")]
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
        public async Task<Result<string>> Register([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                return await _userService.Save(registerDTO);
            }
            catch (Exception e)
            {
                _log.LogError(e, "用户注册异常");
                return Result<string>.Error(e.Message);
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        [HttpPost("login")]
        [ProducesResponseType(typeof(Result<LoginVO>), 200)]
        public async Task<Result<LoginVO>> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                return await _userService.Login(loginDTO);
            }
            catch (Exception e)
            {
                _log.LogError(e, "用户登录异常");
                return Result<LoginVO>.Error(e.Message);
            }
        }

        /// <summary>
        /// 获取用户基础信息
        /// </summary>
        [HttpGet("userinfo")]
        [ProducesResponseType(typeof(Result<UserInfoVO>), 200)]
        public async Task<Result<UserInfoVO>> GetUserInfo()
        {
            try
            {
                return await _userService.GetUserInfo();
            }
            catch (Exception e)
            {
                _log.LogError(e, "获取当前用户信息异常");
                return Result<UserInfoVO>.Error(e.Message);
            }
        }

        /// <summary>
        /// 获取所有用户数据
        /// </summary>
        [HttpGet("users")]
        [ProducesResponseType(typeof(Result<List<UserInfoVO>>), 200)]
        public async Task<Result<List<UserInfoVO>>> GetUsers()
        {
            try
            {
                return await _userService.GetUsers();
            }
            catch (Exception e)
            {
                _log.LogError(e, "查询全部用户异常");
                return Result<List<UserInfoVO>>.Error(e.Message);
            }
        }

        /// <summary>
        /// 用户更新
        /// </summary>
        [HttpPost("update")]
        [ProducesResponseType(typeof(Result<UserInfoVO>), 200)]
        public async Task<Result<UserInfoVO>> UpdateUserInfo([FromBody] UserInfoDTO userInfoDTO)
        {
            try
            {
                return await _userService.UpdateUserInfo(userInfoDTO);
            }
            catch (Exception e)
            {
                _log.LogError(e, "修改用户信息异常");
                return Result<UserInfoVO>.Error(e.Message);
            }
        }

        /// <summary>
        /// 账户删除
        /// </summary>
        [HttpPost("delete")]
        [ProducesResponseType(typeof(Result<string>), 200)]
        public async Task<Result<string>> DeleteUser(string uuid)
        {
            try
            {
                return await _userService.DeleteUserByUuid(uuid);
            }
            catch (Exception e)
            {
                _log.LogError(e, "删除用户异常");
                return Result<string>.Error(e.Message);
            }
        }
    }
}