using blog_common.Config;
using blog_common.Context;
using blog_common.Enums;
using blog_common.Result;
using blog_common.Utils;
using blog_db;
using blog_db.Data;
using blog_pojo.Dtos;
using blog_pojo.Vos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace blog_server.Service.Impl
{
    public class UserService : IUserService
    {
        private readonly _DbContext _dbContext;
        private readonly JwtConfig _jwtConfig;

        public UserService(_DbContext dbContext, IOptions<JwtConfig> jwtConfig)
        {
            _dbContext = dbContext;
            _jwtConfig = jwtConfig.Value;
        }

        public async Task<Result<string>> Save(RegisterDTO registerDTO)
        {
            bool existUserId = await _dbContext.Set<User>().AnyAsync(u => u.UserId == registerDTO.UserId);
            bool existMobile = await _dbContext.Set<User>().AnyAsync(u => u.Mobile == registerDTO.Mobile);

            if (existUserId)
                return Result<string>.Error("USER_ID_EXISTED");
            if (existMobile)
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
            user.CreateTime = DateTime.Now;
            user.UpdateTime = DateTime.Now;

            UserDetails detail = new UserDetails();
            detail.Uuid = uuid;
            detail.Sex = UserSex.Unknown;
            detail.CreateTime = DateTime.Now;
            detail.UpdateTime = DateTime.Now;

            string src = "https://beijing-files.oss-cn-beijing.aliyuncs.com/shanny-blog/images/";
            Random rand = new Random();
            int num = rand.Next(1, 7);
            detail.Avatar = $"{src}{num}.jpg";

            _dbContext.Set<User>().Add(user);
            _dbContext.Set<UserDetails>().Add(detail);
            await _dbContext.SaveChangesAsync();

            return Result<string>.Success("REGISTER_SUCCESS");
        }

        public async Task<Result<LoginVO>> Login(LoginDTO loginDTO)
        {
            string userId = loginDTO.UserId;
            string inputPwd = loginDTO.Password;

            User? user = await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
                return Result<LoginVO>.Error("ACCOUNT_NOT_FOUND");

            string md5Pwd = ComputeMd5(inputPwd);
            if (md5Pwd != user.Password)
                return Result<LoginVO>.Error("PASSWORD_ERROR");

            Dictionary<string, object> claims = new();
            claims.Add("userId", userId);
            string token = JwtUtil.CreateJwt(_jwtConfig.UserSecretKey, _jwtConfig.UserTtl, claims);

            LoginVO vo = new LoginVO
            {
                UserId = userId,
                Token = token
            };

            user.LastLoginTime = DateTime.Now;
            user.UpdateTime = DateTime.Now;
            await _dbContext.SaveChangesAsync();

            return Result<LoginVO>.Success(vo);
        }

        public async Task<Result<UserInfoVO>> GetUserInfo()
        {
            string userId = BaseContext.GetCurrentId();
            if (string.IsNullOrEmpty(userId))
                return Result<UserInfoVO>.Error("USERINFO_IS_NULL");

            User? user = await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
                return Result<UserInfoVO>.Error("USERINFO_IS_NULL");

            UserDetails? detail = await _dbContext.Set<UserDetails>().FirstOrDefaultAsync(d => d.Uuid == user.Uuid);

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

        public async Task<Result<List<UserInfoVO>>> GetUsers()
        {
            string userId = BaseContext.GetCurrentId();
            if (string.IsNullOrEmpty(userId))
                return Result<List<UserInfoVO>>.Error("LOGIN_ERROR");

            List<User> userList = await _dbContext.Set<User>().ToListAsync();
            List<UserDetails> detailList = await _dbContext.Set<UserDetails>().ToListAsync();
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

        public async Task<Result<UserInfoVO>> UpdateUserInfo(UserInfoDTO userInfoDTO)
        {
            if (string.IsNullOrEmpty(userInfoDTO.UserId) || string.IsNullOrEmpty(userInfoDTO.Uuid))
                return Result<UserInfoVO>.Error("UPDATE_FAIL");

            User? dbUser = await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.Uuid == userInfoDTO.Uuid);
            UserDetails? dbDetail = await _dbContext.Set<UserDetails>().FirstOrDefaultAsync(d => d.Uuid == userInfoDTO.Uuid);

            if (dbUser == null || dbDetail == null)
                return Result<UserInfoVO>.Error("UPDATE_FAIL");

            MapUserInfoDtoToUser(userInfoDTO, dbUser);
            MapDetailDtoToDetail(userInfoDTO.UserDetails, dbDetail);
            dbUser.UpdateTime = DateTime.Now;
            dbDetail.UpdateTime = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            UserInfoVO vo = new UserInfoVO
            {
                Uuid = dbUser.Uuid,
                UserId = dbUser.UserId,
                Mobile = dbUser.Mobile,
                Status = dbUser.Status,
                Type = dbUser.Type,
                CreateTime = dbUser.CreateTime,
                UpdateTime = dbUser.UpdateTime,
                LastLoginTime = dbUser.LastLoginTime,
                UserDetails = dbDetail
            };
            return Result<UserInfoVO>.Success(vo);
        }

        public async Task<Result<string>> DeleteUserByUuid(string uuid)
        {
            if (string.IsNullOrEmpty(uuid))
                return Result<string>.Error("DELETE_FAIL");

            User? user = await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.Uuid == uuid);
            UserDetails? detail = await _dbContext.Set<UserDetails>().FirstOrDefaultAsync(d => d.Uuid == uuid);

            if (user != null)
                _dbContext.Set<User>().Remove(user);
            if (detail != null)
                _dbContext.Set<UserDetails>().Remove(detail);

            await _dbContext.SaveChangesAsync();
            return Result<string>.Success("DELETE_SUCCESS");
        }

        #region 私有工具映射
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
            if (!string.IsNullOrEmpty(dto.UserId))
                user.UserId = dto.UserId;
            if (!string.IsNullOrEmpty(dto.Mobile))
                user.Mobile = dto.Mobile;
            user.Status = dto.Status;
            user.Type = dto.Type;
        }

        private void MapDetailDtoToDetail(UserDetails dto, UserDetails detail)
        {
            if (!string.IsNullOrEmpty(dto.Avatar))
                detail.Avatar = dto.Avatar;
            detail.Sex = dto.Sex;
            if (!string.IsNullOrEmpty(dto.Username))
                detail.Username = dto.Username;
            if (!string.IsNullOrEmpty(dto.Nickname))
                detail.Nickname = dto.Nickname;
            if (dto.Birthday.HasValue)
                detail.Birthday = dto.Birthday.Value;
        }
        #endregion
    }
}