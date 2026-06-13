using blog_pojo.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace blog_pojo.Entities
{
    public class _DbContext : DbContext
    {
        public _DbContext(DbContextOptions<_DbContext> options) : base(options) { }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TimeLine> TimeLines { get; set; }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserDetails> UserDetails { get; set; }

        private static readonly JsonSerializerOptions _jsonOpts = new JsonSerializerOptions();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var longListConvert = new ValueConverter<List<long>, string>(
                 list => list == null ? null : JsonSerializer.Serialize(list, _jsonOpts),
                 str => string.IsNullOrEmpty(str) ? new List<long>() : JsonSerializer.Deserialize<List<long>>(str, _jsonOpts)
             );

            modelBuilder.Entity<Article>()
                .Property(a => a.Tags)
                .HasConversion(longListConvert);

            modelBuilder.Entity<Article>()
                .Property(a => a.Timelines)
                .HasConversion(longListConvert);

            modelBuilder.Entity<Media>()
                .Property(m => m.Tags)
                .HasConversion(longListConvert);

            modelBuilder.Entity<Tool>()
                .Property(t => t.Tags)
                .HasConversion(longListConvert);
        }
    }
}
