using blog_pojo.Entities;
using Microsoft.Data.SqlClient;
using System.Text;

namespace blog_server.Mapper
{
    public class AboutMapper
    {
        private readonly string _connStr;

        public AboutMapper(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("Default");
        }

        public About GetByShow(bool isActive)
        {
            About result = null;
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            string sql = "select * from shanny_blog.abouts where is_active = @IsActive";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@IsActive", isActive);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                result = MapReaderToEntity(reader);
            }
            reader.Close();
            return result;
        }

        public List<About> GetAll()
        {
            List<About> list = new();
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            string sql = "select * from shanny_blog.abouts";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(MapReaderToEntity(reader));
            }
            reader.Close();
            return list;
        }

        /// <summary>
        /// 新增，自增主键id（useGeneratedKeys=true）
        /// </summary>
        public void InsertAbout(About about)
        {
            about.CreateTime = DateTime.Now;
            about.UpdateTime = DateTime.Now;

            string sql = @"
                INSERT INTO shanny_blog.abouts(avatar, bili_bili, create_time, github, introduce, name, other, is_active, steam, tag, update_time, web)
                VALUES(@avatar, @biliBili, @createTime, @github, @introduce, @name, @other, @isActive, @steam, @tag, @updateTime, @web);
                SELECT SCOPE_IDENTITY();";

            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            AddInsertParams(cmd, about);

            // 获取自增ID赋值给实体
            object idObj = cmd.ExecuteScalar();
            if (idObj != null)
                about.Id = Convert.ToInt64(idObj);
        }

        /// <summary>
        /// 动态字段更新，对应Mybatis <if> 判断
        /// </summary>
        public void UpdateAbout(About about)
        {
            about.UpdateTime = DateTime.Now;
            StringBuilder sbSet = new StringBuilder();
            List<SqlParameter> paramList = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(about.Avatar))
            {
                sbSet.Append("avatar=@avatar,");
                paramList.Add(new SqlParameter("@avatar", about.Avatar));
            }
            if (!string.IsNullOrEmpty(about.BiliBili))
            {
                sbSet.Append("bili_bili=@biliBili,");
                paramList.Add(new SqlParameter("@biliBili", about.BiliBili));
            }
            if (!string.IsNullOrEmpty(about.Github))
            {
                sbSet.Append("github=@github,");
                paramList.Add(new SqlParameter("@github", about.Github));
            }
            if (!string.IsNullOrEmpty(about.Introduce))
            {
                sbSet.Append("introduce=@introduce,");
                paramList.Add(new SqlParameter("@introduce", about.Introduce));
            }
            if (!string.IsNullOrEmpty(about.Name))
            {
                sbSet.Append("name=@name,");
                paramList.Add(new SqlParameter("@name", about.Name));
            }
            if (!string.IsNullOrEmpty(about.Other))
            {
                sbSet.Append("other=@other,");
                paramList.Add(new SqlParameter("@other", about.Other));
            }
            paramList.Add(new SqlParameter("@isActive", about.IsActive));
            sbSet.Append("is_active=@isActive,");

            if (!string.IsNullOrEmpty(about.Steam))
            {
                sbSet.Append("steam=@steam,");
                paramList.Add(new SqlParameter("@steam", about.Steam));
            }
            if (!string.IsNullOrEmpty(about.Tag))
            {
                sbSet.Append("tag=@tag,");
                paramList.Add(new SqlParameter("@tag", about.Tag));
            }

            sbSet.Append("update_time=@updateTime,");
            paramList.Add(new SqlParameter("@updateTime", about.UpdateTime));

            // 删掉最后一个逗号
            if (sbSet.Length > 0)
                sbSet.Length -= 1;

            string sql = $"UPDATE shanny_blog.abouts SET {sbSet} WHERE id=@id";
            paramList.Add(new SqlParameter("@id", about.Id));

            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddRange(paramList.ToArray());
            cmd.ExecuteNonQuery();
        }

        public void DeleteById(long id)
        {
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            string sql = "delete from shanny_blog.abouts where id = @Id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        #region 私有映射、参数填充
        private About MapReaderToEntity(SqlDataReader reader)
        {
            return new About
            {
                Id = Convert.ToInt64(reader["id"]),
                Avatar = reader["avatar"]?.ToString(),
                BiliBili = reader["bili_bili"]?.ToString(),
                CreateTime = Convert.ToDateTime(reader["create_time"]),
                Github = reader["github"]?.ToString(),
                Introduce = reader["introduce"]?.ToString(),
                Name = reader["name"]?.ToString(),
                Other = reader["other"]?.ToString(),
                IsActive = Convert.ToBoolean(reader["is_active"]),
                Steam = reader["steam"]?.ToString(),
                Tag = reader["tag"]?.ToString(),
                UpdateTime = Convert.ToDateTime(reader["update_time"]),
                Web = reader["web"]?.ToString()
            };
        }

        private void AddInsertParams(SqlCommand cmd, About about)
        {
            cmd.Parameters.AddWithValue("@avatar", about.Avatar ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@biliBili", about.BiliBili ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@createTime", about.CreateTime);
            cmd.Parameters.AddWithValue("@github", about.Github ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@introduce", about.Introduce ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@name", about.Name ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@other", about.Other ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@isActive", about.IsActive);
            cmd.Parameters.AddWithValue("@steam", about.Steam ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@tag", about.Tag ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@updateTime", about.UpdateTime);
            cmd.Parameters.AddWithValue("@web", about.Web ?? (object)DBNull.Value);
        }
        #endregion
    }
}