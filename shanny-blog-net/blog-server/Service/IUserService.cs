using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Vos;

namespace blog_server.Service
{
    public interface IUserService
    {
        Task<Result<string>> Save(RegisterDTO registerDTO);
        Task<Result<LoginVO>> Login(LoginDTO loginDTO);
        Task<Result<UserInfoVO>> GetUserInfo();
        Task<Result<List<UserInfoVO>>> GetUsers();
        Task<Result<UserInfoVO>> UpdateUserInfo(UserInfoDTO userInfoDTO);
        Task<Result<string>> DeleteUserByUuid(string uuid);
    }
}