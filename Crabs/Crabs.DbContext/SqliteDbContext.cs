using Crabs.Models;
using Microsoft.EntityFrameworkCore;


namespace Crabs.SqliteDbContext
{
    public class SqliteDbContext : DbContext
    {
        private readonly string _dbPath;
        public DbSet<Autorization> Items { get; set; }

        public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options)
        {
        }

        protected SqliteDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var dbPath = _dbPath;
                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autorization>(entity =>
            {
                entity.ToTable("Autorizations");
                entity.Property(entity => entity.Username);
                entity.Property(entity => entity.Password);
            });
        }
    }
}
