using blog_common.Context;
using blog_common.Enums;
using blog_common.Result;
using blog_common.Utils;
using blog_pojo.Dtos;
using blog_pojo.Entities;
using blog_pojo.Vos;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace blog_server.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly UserMapper _userMapper;
        private readonly JwtProperties _jwtProperties;

        public UserService(UserMapper userMapper, IOptions<JwtProperties> jwtOpts)
        {
            _userMapper = userMapper;
            _jwtProperties = jwtOpts.Value;
        }

        public Result<string> Save(RegisterDTO registerDTO)
        {
            var userByUserId = _userMapper.GetByUserId(registerDTO.UserId);
            var userByMobile = _userMapper.GetByMobile(registerDTO.Mobile);

            if (userByUserId != null)
                return Result<string>.Error("USER_ID_EXISTED");
            if (userByMobile != null)
                return Result<string>.Error("MOBILE_EXISTED");

            User user = new User();
            MapRegisterToUser(registerDTO, user);

            string uuid = Guid.NewGuid().ToString();
            string pwdMd5 = ComputeMd5(registerDTO.Password);
            user.Uuid = uuid;
            user.Password = pwdMd5;
            user.Status = UserStatus.Active;
            user.Type = UserType.User;
            user.LastLoginTime = DateTime.Now;

            _userMapper.InsertUser(user);

            UserDetails detail = new UserDetails();
            detail.Uuid = uuid;
            detail.Sex = UserSex.Unknown;

            string src = "https://beijing-files.oss-cn-beijing.aliyuncs.com/shanny-blog/images/";
            Random rand = new Random();
            int num = rand.Next(1, 7);
            detail.Avatar = $"{src}{num}.jpg";

            _userMapper.InsertUserDetail(detail);
            return Result<string>.Success("REGISTER_SUCCESS");
        }

        public Result<LoginVO> Login(LoginDTO loginDTO)
        {
            string userId = loginDTO.UserId;
            string inputPwd = loginDTO.Password;

            User user = _userMapper.GetByUserId(userId);
            if (user == null)
                return Result<LoginVO>.Error("ACCOUNT_NOT_FOUND");

            string md5Pwd = ComputeMd5(inputPwd);
            if (md5Pwd != user.Password)
                return Result<LoginVO>.Error("PASSWORD_ERROR");

            Dictionary<string, object> claims = new();
            claims.Add("userId", userId);
            string token = JwtUtil.CreateJwt(_jwtProperties.UserSecretKey, _jwtProperties.UserTtl, claims);

            LoginVO vo = new LoginVO
            {
                UserId = userId,
                Token = token
            };

            user.LastLoginTime = DateTime.Now;
            _userMapper.UpdateUser(user);

            return Result<LoginVO>.Success(vo);
        }

        public Result<UserInfoVO> GetUserInfo()
        {
            string userId = BaseContext.GetCurrentId();
            if (string.IsNullOrEmpty(userId))
                return Result<UserInfoVO>.Error("USERINFO_IS_NULL");

            User user = _userMapper.GetByUserId(userId);
            UserDetails detail = _userMapper.GetDetailByUuid(user.Uuid);

            UserInfoVO vo = new UserInfoVO
            {
                Uuid = user.Uuid,
                UserId = user.UserId,
                Mobile = user.Mobile,
                Status = user.Status,
                Type = user.Type,
                CreateTime = user.CreateTime,
                UpdateTime = user.UpdateTime,
                LastLoginTime = user.LastLoginTime,
                UserDetails = detail
            };
            return Result<UserInfoVO>.Success(vo);
        }

        public Result<List<UserInfoVO>> GetUsers()
        {
            string userId = BaseContext.GetCurrentId();
            if (string.IsNullOrEmpty(userId))
                return Result<List<UserInfoVO>>.Error("LOGIN_ERROR");

            List<User> userList = _userMapper.GetUsers();
            List<UserDetails> detailList = _userMapper.GetUserDetails();
            Dictionary<string, UserDetails> detailMap = detailList.ToDictionary(d => d.Uuid);

            List<UserInfoVO> voList = new();
            foreach (var u in userList)
            {
                var vo = new UserInfoVO
                {
                    Uuid = u.Uuid,
                    UserId = u.UserId,
                    Mobile = u.Mobile,
                    Status = u.Status,
                    Type = u.Type,
                    CreateTime = u.CreateTime,
                    UpdateTime = u.UpdateTime,
                    LastLoginTime = u.LastLoginTime,
                    UserDetails = detailMap.TryGetValue(u.Uuid, out var d) ? d : null
                };
                voList.Add(vo);
            }
            return Result<List<UserInfoVO>>.Success(voList);
        }

        public Result<UserInfoVO> UpdateUserInfo(UserInfoDTO userInfoDTO)
        {
            if (string.IsNullOrEmpty(userInfoDTO.UserId))
                return Result<UserInfoVO>.Error("UPDATE_FAIL");

            User user = new User();
            MapUserInfoDtoToUser(userInfoDTO, user);

            UserDetails detail = new UserDetails();
            MapDetailDtoToDetail(userInfoDTO.UserDetails, detail);

            _userMapper.UpdateUser(user);
            _userMapper.UpdateUserDetail(detail);

            return Result<UserInfoVO>.Success();
        }

        public Result<string> DeleteUserByUuid(string uuid)
        {
            if (string.IsNullOrEmpty(uuid))
                return Result<string>.Error("DELETE_FAIL");

            _userMapper.DeleteUserByUuid(uuid);
            _userMapper.DeleteInfoByUuid(uuid);
            return Result<string>.Success("DELETE_SUCCESS");
        }

        #region 工具私有方法
        private string ComputeMd5(string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            byte[] hash = MD5.HashData(bytes);
            StringBuilder sb = new();
            foreach (var b in hash)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }

        private void MapRegisterToUser(RegisterDTO dto, User user)
        {
            user.UserId = dto.UserId;
            user.Mobile = dto.Mobile;
        }

        private void MapUserInfoDtoToUser(UserInfoDTO dto, User user)
        {
            user.UserId = dto.UserId;
            user.Mobile = dto.Mobile;
            user.Status = dto.Status;
            user.Type = dto.Type;
            user.Uuid = dto.Uuid;
        }

        private void MapDetailDtoToDetail(UserDetails dto, UserDetails detail)
        {
            detail.Uuid = dto.Uuid;
            detail.Avatar = dto.Avatar;
            detail.Sex = dto.Sex;
            detail.Username = dto.Username;
            detail.Birthday = dto.Birthday;
            detail.Nickname = dto.Nickname;
        }
        #endregion
    }
}