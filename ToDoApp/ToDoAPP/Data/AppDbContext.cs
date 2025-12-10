using Microsoft.EntityFrameworkCore;
using ToDoAPP.Models;

namespace ToDoAPP.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<TodoItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(255);
                entity.Property(e => e.CreatedAt).HasDefaultValue(DateTime.Now);
            });
        }
    }
}
