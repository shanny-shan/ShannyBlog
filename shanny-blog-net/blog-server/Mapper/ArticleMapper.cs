using blog_common.Enums;
using blog_pojo.Entities;
using Microsoft.Data.SqlClient;
using System.Text;
using System.Text.Json;

namespace blog_server.Mapper
{
    public class ArticleMapper
    {
        private readonly string _connStr;

        public ArticleMapper(IConfiguration configuration)
        {
            _connStr = configuration.GetConnectionString("Default");
        }

        public List<Article> GetByRecent()
        {
            List<Article> list = new();
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            string sql = "select top 5 * from shanny_blog.articles ORDER BY update_time DESC";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(MapReaderToArticle(reader));
            }
            reader.Close();
            return list;
        }

        public List<Article> GetByType(CategoryType type)
        {
            List<Article> list = new();
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            string sql = "select * from shanny_blog.articles where type = @Type";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Type", type);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(MapReaderToArticle(reader));
            }
            reader.Close();
            return list;
        }

        public Article GetById(long id)
        {
            Article result = null;
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            string sql = "select * from shanny_blog.articles where id = @Id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                result = MapReaderToArticle(reader);
            }
            reader.Close();
            return result;
        }

        public void InsertArticle(Article article)
        {
            article.CreateTime = DateTime.Now;
            article.UpdateTime = DateTime.Now;
            article.Views = 0;

            string sql = @"
INSERT INTO shanny_blog.articles(category_id, content, memo, create_time, href, image, published, tags, timelines, title, type, update_time, views)
VALUES(@categoryId, @content, @memo, @createTime, @href, @image, @published, @tags, @timelines, @title, @type, @updateTime, @views);
SELECT SCOPE_IDENTITY();";

            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql, conn);
            AddInsertParams(cmd, article);

            object idObj = cmd.ExecuteScalar();
            if (idObj != null)
                article.Id = Convert.ToInt64(idObj);
        }

        public void UpdateArticle(Article article)
        {
            article.UpdateTime = DateTime.Now;
            StringBuilder sbSet = new StringBuilder();
            List<SqlParameter> paramList = new List<SqlParameter>();

            if (article.CategoryId != null)
            {
                sbSet.Append("category_id=@categoryId,");
                paramList.Add(new SqlParameter("@categoryId", article.CategoryId));
            }
            if (!string.IsNullOrEmpty(article.Content))
            {
                sbSet.Append("content=@content,");
                paramList.Add(new SqlParameter("@content", article.Content));
            }
            if (!string.IsNullOrEmpty(article.Memo))
            {
                sbSet.Append("memo=@memo,");
                paramList.Add(new SqlParameter("@memo", article.Memo));
            }
            if (!string.IsNullOrEmpty(article.Href))
            {
                sbSet.Append("href=@href,");
                paramList.Add(new SqlParameter("@href", article.Href));
            }
            if (!string.IsNullOrEmpty(article.Image))
            {
                sbSet.Append("image=@image,");
                paramList.Add(new SqlParameter("@image", article.Image));
            }
            paramList.Add(new SqlParameter("@published", article.Published));
            sbSet.Append("published=@published,");

            if (article.Tags != null && article.Tags.Count > 0)
            {
                string tagsJson = JsonSerializer.Serialize(article.Tags);
                sbSet.Append("tags=@tags,");
                paramList.Add(new SqlParameter("@tags", tagsJson));
            }
            // 修复：判断List非空，序列化Timelines
            if (article.Timelines != null && article.Timelines.Count > 0)
            {
                string timeJson = JsonSerializer.Serialize(article.Timelines);
                sbSet.Append("timelines=@timelines,");
                paramList.Add(new SqlParameter("@timelines", timeJson));
            }
            if (!string.IsNullOrEmpty(article.Title))
            {
                sbSet.Append("title=@title,");
                paramList.Add(new SqlParameter("@title", article.Title));
            }
            paramList.Add(new SqlParameter("@type", article.Type));
            sbSet.Append("type=@type,");

            sbSet.Append("update_time=@updateTime,");
            paramList.Add(new SqlParameter("@updateTime", article.UpdateTime));
            paramList.Add(new SqlParameter("@views", article.Views));
            sbSet.Append("views=@views,");

            if (sbSet.Length > 0)
                sbSet.Length -= 1;

            string sql = $"UPDATE shanny_blog.articles SET {sbSet} WHERE id=@id";
            paramList.Add(new SqlParameter("@id", article.Id));

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
            string sql = "delete from shanny_blog.articles where id = @Id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }

        public List<Article> GetByView()
        {
            List<Article> list = new();
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            string sql = "select top 5 * from shanny_blog.articles ORDER BY views DESC";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(MapReaderToArticle(reader));
            }
            reader.Close();
            return list;
        }

        public void UpdateArticleViews(Article article)
        {
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            string sql = "UPDATE shanny_blog.articles SET views = @views WHERE id = @id";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@views", article.Views);
            cmd.Parameters.AddWithValue("@id", article.Id);
            cmd.ExecuteNonQuery();
        }

        public List<Article> GetByTag(long tagId)
        {
            List<Article> list = new();
            using SqlConnection conn = new SqlConnection(_connStr);
            conn.Open();
            string sql = @"
SELECT * FROM shanny_blog.articles
WHERE JSON_VALUE(tags, '$') LIKE '%' + CAST(@TagId AS VARCHAR) + '%'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@TagId", tagId);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(MapReaderToArticle(reader));
            }
            reader.Close();
            return list;
        }

        #region 私有映射与参数填充
        private Article MapReaderToArticle(SqlDataReader reader)
        {
            Article entity = new Article();
            entity.Id = Convert.ToInt64(reader["id"]);
            entity.CategoryId = reader["category_id"] == DBNull.Value ? 0 : Convert.ToInt64(reader["category_id"]);
            entity.Content = reader["content"]?.ToString();
            entity.Memo = reader["memo"]?.ToString();
            entity.CreateTime = Convert.ToDateTime(reader["create_time"]);
            entity.Href = reader["href"]?.ToString();
            entity.Image = reader["image"]?.ToString();
            entity.Published = Convert.ToBoolean(reader["published"]);

            string tagsStr = reader["tags"]?.ToString();
            if (!string.IsNullOrEmpty(tagsStr))
            {
                entity.Tags = JsonSerializer.Deserialize<List<long>>(tagsStr);
            }
            // 读取Timelines JSON转回List<long>
            string timeStr = reader["timelines"]?.ToString();
            if (!string.IsNullOrEmpty(timeStr))
            {
                entity.Timelines = JsonSerializer.Deserialize<List<long>>(timeStr);
            }
            entity.Title = reader["title"]?.ToString();
            entity.Type = (CategoryType)Convert.ToInt32(reader["type"]);
            entity.UpdateTime = Convert.ToDateTime(reader["update_time"]);
            entity.Views = Convert.ToInt32(reader["views"]);
            return entity;
        }

        private void AddInsertParams(SqlCommand cmd, Article article)
        {
            cmd.Parameters.AddWithValue("@categoryId", article.CategoryId);
            cmd.Parameters.AddWithValue("@content", article.Content ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@memo", article.Memo ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@createTime", article.CreateTime);
            cmd.Parameters.AddWithValue("@href", article.Href ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@image", article.Image ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@published", article.Published);

            string tagsJson = null;
            if (article.Tags != null && article.Tags.Count > 0)
            {
                tagsJson = JsonSerializer.Serialize(article.Tags);
            }
            cmd.Parameters.AddWithValue("@tags", tagsJson);


            string timeJson = null;
            if (article.Timelines != null && article.Timelines.Count > 0)
            {
                timeJson = JsonSerializer.Serialize(article.Timelines);
            }
            cmd.Parameters.AddWithValue("@timelines", timeJson);

            cmd.Parameters.AddWithValue("@title", article.Title ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@type", article.Type);
            cmd.Parameters.AddWithValue("@updateTime", article.UpdateTime);
            cmd.Parameters.AddWithValue("@views", article.Views);
        }
        #endregion
    }
}