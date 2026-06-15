using blog_pojo.Entities;
using Microsoft.Data.SqlClient;
using System.Text;

namespace blog_server.Mapper
{
    public class TagMapper
    {
        private readonly string _connStr;

        public TagMapper(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("Default");
        }

        public List<Tag> GetAll()
        {
            List<Tag> list = new();
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            string sql = "select * from shanny_blog.tags";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(MapReader(reader));
            }
            reader.Close();
            return list;
        }

        public Tag GetById(long id)
        {
            Tag model = null;
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            string sql = "select * from shanny_blog.tags where id = @Id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                model = MapReader(reader);
            }
            reader.Close();
            return model;
        }

        public void InsertTag(Tag tag)
        {
            string sql = @"INSERT INTO shanny_blog.tags(name,name_en) VALUES(@name,@nameEn);SELECT SCOPE_IDENTITY();";
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", tag.Name ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@nameEn", tag.NameEn ?? (object)DBNull.Value);

            var idVal = cmd.ExecuteScalar();
            if (idVal != null)
                tag.Id = Convert.ToInt64(idVal);
        }

        public void UpdateTag(Tag tag)
        {
            StringBuilder sbSet = new StringBuilder();
            List<SqlParameter> paras = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(tag.Name))
            {
                sbSet.Append("name=@name,");
                paras.Add(new SqlParameter("@name", tag.Name));
            }
            if (!string.IsNullOrEmpty(tag.NameEn))
            {
                sbSet.Append("name_en=@nameEn,");
                paras.Add(new SqlParameter("@nameEn", tag.NameEn));
            }

            if (sbSet.Length > 0)
                sbSet.Length--;

            string sql = $"UPDATE shanny_blog.tags SET {sbSet} WHERE id=@id";
            paras.Add(new SqlParameter("@id", tag.Id));

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
            string sql = "delete from shanny_blog.tags where id = @Id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        private Tag MapReader(SqlDataReader reader)
        {
            return new Tag
            {
                Id = Convert.ToInt64(reader["id"]),
                Name = reader["name"]?.ToString(),
                NameEn = reader["name_en"]?.ToString()
            };
        }
    }
}