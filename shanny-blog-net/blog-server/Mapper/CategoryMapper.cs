using blog_common.Enums;
using blog_pojo.Entities;
using Microsoft.Data.SqlClient;
using System.Text;

namespace blog_server.Mapper
{
    public class CategoryMapper
    {
        private readonly string _connStr;

        public CategoryMapper(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("Default");
        }

        public List<Category> GetAll()
        {
            List<Category> list = new();
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            string sql = "select * from shanny_blog.categories";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(MapReader(reader));
            }
            reader.Close();
            return list;
        }

        public Category GetById(long id)
        {
            Category model = null;
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            string sql = "select * from shanny_blog.categories where id = @Id";
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

        public void InsertCategory(Category category)
        {
            string sql = @"INSERT INTO shanny_blog.categories(name, type, sort, name_en)
                           VALUES(@name,@type,@sort,@nameEn);SELECT SCOPE_IDENTITY();";
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@name", category.Name ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@type", category.Type);
            cmd.Parameters.AddWithValue("@sort", category.Sort);
            cmd.Parameters.AddWithValue("@nameEn", category.NameEn ?? (object)DBNull.Value);

            var idVal = cmd.ExecuteScalar();
            if (idVal != null)
                category.Id = Convert.ToInt64(idVal);
        }

        public void UpdateCategory(Category category)
        {
            StringBuilder sb = new StringBuilder();
            List<SqlParameter> paras = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(category.Name))
            {
                sb.Append("name=@name,");
                paras.Add(new SqlParameter("@name", category.Name));
            }
            if (category.Sort != null)
            {
                sb.Append("sort=@sort,");
                paras.Add(new SqlParameter("@sort", category.Sort));
            }
            if (!string.IsNullOrEmpty(category.NameEn))
            {
                sb.Append("name_en=@nameEn,");
                paras.Add(new SqlParameter("@nameEn", category.NameEn));
            }
            paras.Add(new SqlParameter("@type", category.Type));
            sb.Append("type=@type,");

            if (sb.Length > 0)
                sb.Length--;

            string sql = $"UPDATE shanny_blog.categories SET {sb} WHERE id=@id";
            paras.Add(new SqlParameter("@id", category.Id));

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
            string sql = "delete from shanny_blog.categories where id = @Id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        private Category MapReader(SqlDataReader reader)
        {
            return new Category
            {
                Id = Convert.ToInt64(reader["id"]),
                Name = reader["name"]?.ToString(),
                Type = (CategoryType)Convert.ToInt32(reader["type"]),
                Sort = reader["sort"] == DBNull.Value ? 0 : Convert.ToInt32(reader["sort"]),
                NameEn = reader["name_en"]?.ToString()
            };
        }
    }
}