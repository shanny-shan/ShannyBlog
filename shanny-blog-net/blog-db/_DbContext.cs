using blog_db.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace blog_db
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var listLongComparer = new ValueComparer<List<long>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, b) => HashCode.Combine(a, b)),
                c => c.ToList()
            );

            modelBuilder.Entity<Tag>().HasKey(t => t.Id);
            modelBuilder.Entity<Tag>().Property(t => t.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Category>().HasKey(c => c.Id);
            modelBuilder.Entity<Category>().Property(c => c.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Article>().HasKey(a => a.Id);
            modelBuilder.Entity<Article>().Property(a => a.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Tool>().HasKey(t => t.Id);
            modelBuilder.Entity<Tool>().Property(t => t.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Article>()
                .Property(a => a.Tags)
                .HasConversion(
                    v => JsonSerializer.Serialize(v),
                    v => JsonSerializer.Deserialize<List<long>>(v) ?? new List<long>()
                    )
                .Metadata.SetValueComparer(listLongComparer);

            modelBuilder.Entity<Article>()
                .Property(a => a.Timelines)
                .HasConversion(
                    v => JsonSerializer.Serialize(v),
                    v => JsonSerializer.Deserialize<List<long>>(v) ?? new List<long>()
                )
                .Metadata.SetValueComparer(listLongComparer);

            modelBuilder.Entity<Tool>()
                .Property(t => t.Tags)
                .HasConversion(
                    v => JsonSerializer.Serialize(v),
                    v => JsonSerializer.Deserialize<List<long>>(v) ?? new List<long>()
                )
                .Metadata.SetValueComparer(listLongComparer);

            modelBuilder.Entity<Media>()
                .Property(m => m.Tags)
                .HasConversion(
                    v => JsonSerializer.Serialize(v),
                    v => JsonSerializer.Deserialize<List<long>>(v) ?? new List<long>()
                )
                .Metadata.SetValueComparer(listLongComparer);

            modelBuilder.Entity<User>().HasKey(u => u.Uuid);
            modelBuilder.Entity<UserDetails>().HasKey(d => d.Uuid);
        }
    }
}
