using blog_pojo.Entities;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Text.Json;

namespace blog_server.Mapper
{
    public class ToolMapper
    {
        private readonly string _connStr;

        public ToolMapper(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("Default");
        }

        public List<Tool> GetAll()
        {
            List<Tool> list = new();
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            string sql = "select * from shanny_blog.tools";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(MapReader(reader));
            }
            reader.Close();
            return list;
        }

        public void InsertTool(Tool tool)
        {
            tool.CreateTime = DateTime.Now;
            tool.UpdateTime = DateTime.Now;

            string tagsJson = null;
            if (tool.Tags != null && tool.Tags.Count > 0)
                tagsJson = JsonSerializer.Serialize(tool.Tags);

            string sql = @"INSERT INTO shanny_blog.tools(content,create_time,href,image,published,tags,title,update_time)
VALUES(@content,@createTime,@href,@image,@published,@tags,@title,@updateTime);SELECT SCOPE_IDENTITY();";

            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@content", tool.Content ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@createTime", tool.CreateTime);
            cmd.Parameters.AddWithValue("@href", tool.Href ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@image", tool.Image ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@published", tool.Published);
            cmd.Parameters.AddWithValue("@tags", tagsJson ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@title", tool.Title ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@updateTime", tool.UpdateTime);

            var idObj = cmd.ExecuteScalar();
            if (idObj != null)
                tool.Id = Convert.ToInt64(idObj);
        }

        public void UpdateTool(Tool tool)
        {
            tool.UpdateTime = DateTime.Now;
            StringBuilder sbSet = new StringBuilder();
            List<SqlParameter> paras = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(tool.Content))
            {
                sbSet.Append("content=@content,");
                paras.Add(new SqlParameter("@content", tool.Content));
            }
            if (!string.IsNullOrEmpty(tool.Href))
            {
                sbSet.Append("href=@href,");
                paras.Add(new SqlParameter("@href", tool.Href));
            }
            if (!string.IsNullOrEmpty(tool.Image))
            {
                sbSet.Append("image=@image,");
                paras.Add(new SqlParameter("@image", tool.Image));
            }

            sbSet.Append("published=@published,");
            paras.Add(new SqlParameter("@published", tool.Published));

            if (tool.Tags != null && tool.Tags.Count > 0)
            {
                string tagsJson = JsonSerializer.Serialize(tool.Tags);
                sbSet.Append("tags=@tags,");
                paras.Add(new SqlParameter("@tags", tagsJson));
            }
            if (!string.IsNullOrEmpty(tool.Title))
            {
                sbSet.Append("title=@title,");
                paras.Add(new SqlParameter("@title", tool.Title));
            }

            sbSet.Append("update_time=@updateTime,");
            paras.Add(new SqlParameter("@updateTime", tool.UpdateTime));

            if (sbSet.Length > 0)
                sbSet.Length--;

            string sql = $"UPDATE shanny_blog.tools SET {sbSet} WHERE id=@id";
            paras.Add(new SqlParameter("@id", tool.Id));

            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddRange(paras.ToArray());
            cmd.ExecuteNonQuery();
        }

        public void DeleteById(long id)
        {
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            string sql = "delete from shanny_blog.tools where id = @Id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        private Tool MapReader(SqlDataReader reader)
        {
            Tool item = new Tool();
            item.Id = Convert.ToInt64(reader["id"]);
            item.Title = reader["title"]?.ToString();
            item.Content = reader["content"]?.ToString();
            item.Image = reader["image"]?.ToString();
            item.Href = reader["href"]?.ToString();
            item.Published = Convert.ToBoolean(reader["published"]);
            item.CreateTime = Convert.ToDateTime(reader["create_time"]);
            item.UpdateTime = Convert.ToDateTime(reader["update_time"]);

            string tagStr = reader["tags"]?.ToString();
            if (!string.IsNullOrEmpty(tagStr))
                item.Tags = JsonSerializer.Deserialize<List<long>>(tagStr);
            else
                item.Tags = new List<long>();

            return item;
        }
    }
}