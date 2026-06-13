using blog_common.Result;
using blog_pojo.Dtos;
using blog_pojo.Vos;

namespace blog_server.Services
{
    public interface IUserService
    {
        Result<string> Save(RegisterDTO registerDTO);

        Result<LoginVO> Login(LoginDTO loginDTO);

        Result<UserInfoVO> GetUserInfo();

        Result<List<UserInfoVO>> GetUsers();

        Result<UserInfoVO> UpdateUserInfo(UserInfoDTO userInfoDTO);

        Result<string> DeleteUserByUuid(string uuid);
    }
}
