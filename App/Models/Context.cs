using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BlogApi.Core.Database;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.App.Models
{
    public class Context : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Post> Posts { get; set; }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(entity =>
            {
                entity.ToTable("users");
                entity
                    .HasKey(b => b.Id)
                    .HasName("PrimaryKey_BlogId");
                entity
                .HasAlternateKey(c => c.Username)
                .HasName("AlternateKey_Username");
            });
        }
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            AddTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is Entity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow; // current datetime

                if (entity.State == EntityState.Added)
                {
                    ((Entity)entity.Entity).CreatedAt = now;
                }
                ((Entity)entity.Entity).UpdatedAt = now;
            }
        }
    }
}