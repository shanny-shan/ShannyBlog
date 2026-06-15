using blog_common.Enums;
using blog_pojo.Entities;
using Microsoft.Data.SqlClient;
using System.Text;

namespace blog_server.Mapper
{
    public class UserMapper
    {
        private readonly string _connStr;

        public UserMapper(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("Default");
        }

        public void InsertUser(User user)
        {
            user.CreateTime = DateTime.Now;
            user.UpdateTime = DateTime.Now;
            string sql = @"INSERT INTO shanny_blog.users(uuid,user_id,mobile,password,status,type,create_time,update_time,last_login_time)
VALUES(@uuid,@userId,@mobile,@password,@status,@type,@createTime,@updateTime,@lastLoginTime)";
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@uuid", user.Uuid);
            cmd.Parameters.AddWithValue("@userId", user.UserId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@mobile", user.Mobile ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@password", user.Password ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@status", user.Status);
            cmd.Parameters.AddWithValue("@type", user.Type);
            cmd.Parameters.AddWithValue("@createTime", user.CreateTime);
            cmd.Parameters.AddWithValue("@updateTime", user.UpdateTime);
            cmd.Parameters.AddWithValue("@lastLoginTime", user.LastLoginTime ?? (object)DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void InsertUserDetail(UserDetails detail)
        {
            detail.CreateTime = DateTime.Now;
            detail.UpdateTime = DateTime.Now;
            string sql = @"INSERT INTO shanny_blog.user_details(uuid,username,nickname,birthday,avatar,sex,create_time,update_time)
VALUES(@uuid,@username,@nickname,@birthday,@avatar,@sex,@createTime,@updateTime)";
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@uuid", detail.Uuid);
            cmd.Parameters.AddWithValue("@username", detail.Username ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@nickname", detail.Nickname ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@birthday", detail.Birthday ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@avatar", detail.Avatar ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@sex", detail.Sex);
            cmd.Parameters.AddWithValue("@createTime", detail.CreateTime);
            cmd.Parameters.AddWithValue("@updateTime", detail.UpdateTime);
            cmd.ExecuteNonQuery();
        }

        public User GetByUserId(string userId)
        {
            User res = null;
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            string sql = "select * from shanny_blog.users where user_id = @userId";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@userId", userId);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) res = MapUser(reader);
            reader.Close();
            return res;
        }

        public User GetByMobile(string mobile)
        {
            User res = null;
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            string sql = "select * from shanny_blog.users where mobile = @mobile";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@mobile", mobile);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) res = MapUser(reader);
            reader.Close();
            return res;
        }

        public UserDetails GetDetailByUuid(string uuid)
        {
            UserDetails res = null;
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            string sql = "select * from shanny_blog.user_details where uuid = @uuid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@uuid", uuid);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read()) res = MapDetail(reader);
            reader.Close();
            return res;
        }

        public void UpdateUser(User user)
        {
            user.UpdateTime = DateTime.Now;
            StringBuilder sb = new StringBuilder();
            List<SqlParameter> paras = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(user.Mobile))
            {
                sb.Append("mobile=@mobile,");
                paras.Add(new SqlParameter("@mobile", user.Mobile));
            }
            if (!string.IsNullOrEmpty(user.Password))
            {
                sb.Append("password=@password,");
                paras.Add(new SqlParameter("@password", user.Password));
            }
            sb.Append("status=@status,");
            paras.Add(new SqlParameter("@status", user.Status));
            sb.Append("type=@type,");
            paras.Add(new SqlParameter("@type", user.Type));
            sb.Append("update_time=@updateTime,");
            paras.Add(new SqlParameter("@updateTime", user.UpdateTime));
            if (user.LastLoginTime.HasValue)
            {
                sb.Append("last_login_time=@lastLoginTime,");
                paras.Add(new SqlParameter("@lastLoginTime", user.LastLoginTime.Value));
            }

            if (sb.Length > 0) sb.Length--;
            string sql = $"UPDATE shanny_blog.users SET {sb} WHERE uuid=@uuid";
            paras.Add(new SqlParameter("@uuid", user.Uuid));

            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddRange(paras.ToArray());
            cmd.ExecuteNonQuery();
        }

        public void UpdateUserDetail(UserDetails detail)
        {
            detail.UpdateTime = DateTime.Now;
            StringBuilder sb = new StringBuilder();
            List<SqlParameter> paras = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(detail.Username))
            {
                sb.Append("username=@username,");
                paras.Add(new SqlParameter("@username", detail.Username));
            }
            if (!string.IsNullOrEmpty(detail.Nickname))
            {
                sb.Append("nickname=@nickname,");
                paras.Add(new SqlParameter("@nickname", detail.Nickname));
            }
            if (detail.Birthday.HasValue)
            {
                sb.Append("birthday=@birthday,");
                paras.Add(new SqlParameter("@birthday", detail.Birthday.Value));
            }
            if (!string.IsNullOrEmpty(detail.Avatar))
            {
                sb.Append("avatar=@avatar,");
                paras.Add(new SqlParameter("@avatar", detail.Avatar));
            }
            sb.Append("sex=@sex,");
            paras.Add(new SqlParameter("@sex", detail.Sex));
            sb.Append("update_time=@updateTime,");
            paras.Add(new SqlParameter("@updateTime", detail.UpdateTime));

            if (sb.Length > 0) sb.Length--;
            string sql = $"UPDATE shanny_blog.user_details SET {sb} WHERE uuid=@uuid";
            paras.Add(new SqlParameter("@uuid", detail.Uuid));

            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddRange(paras.ToArray());
            cmd.ExecuteNonQuery();
        }

        public List<User> GetUsers()
        {
            List<User> list = new();
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            string sql = "select * from shanny_blog.users";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) list.Add(MapUser(reader));
            reader.Close();
            return list;
        }

        public List<UserDetails> GetUserDetails()
        {
            List<UserDetails> list = new();
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            string sql = "select * from shanny_blog.user_details";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read()) list.Add(MapDetail(reader));
            reader.Close();
            return list;
        }

        public void DeleteUserByUuid(string uuid)
        {
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            string sql = "delete from shanny_blog.users where uuid=@uuid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@uuid", uuid);
            cmd.ExecuteNonQuery();
        }

        public void DeleteInfoByUuid(string uuid)
        {
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            string sql = "delete from shanny_blog.user_details where uuid=@uuid";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@uuid", uuid);
            cmd.ExecuteNonQuery();
        }

        private User MapUser(SqlDataReader reader)
        {
            return new User
            {
                Uuid = reader["uuid"]?.ToString(),
                UserId = reader["user_id"]?.ToString(),
                Mobile = reader["mobile"]?.ToString(),
                Password = reader["password"]?.ToString(),
                Status = (UserStatus)Convert.ToInt32(reader["status"]),
                Type = (UserType)Convert.ToInt32(reader["type"]),
                CreateTime = Convert.ToDateTime(reader["create_time"]),
                UpdateTime = Convert.ToDateTime(reader["update_time"]),
                LastLoginTime = reader["last_login_time"] == DBNull.Value ? null : Convert.ToDateTime(reader["last_login_time"])
            };
        }

        private UserDetails MapDetail(SqlDataReader reader)
        {
            return new UserDetails
            {
                Uuid = reader["uuid"]?.ToString(),
                Username = reader["username"]?.ToString(),
                Nickname = reader["nickname"]?.ToString(),
                Birthday = reader["birthday"] == DBNull.Value ? null : Convert.ToDateTime(reader["birthday"]),
                Avatar = reader["avatar"]?.ToString(),
                Sex = (UserSex)Convert.ToInt32(reader["sex"]),
                CreateTime = Convert.ToDateTime(reader["create_time"]),
                UpdateTime = Convert.ToDateTime(reader["update_time"])
            };
        }
    }
}