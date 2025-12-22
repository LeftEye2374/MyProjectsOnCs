using Microsoft.EntityFrameworkCore;
using WeatherApp.Models.EntityModels;

namespace AppDbContext
{
    public class SqliteDbContexrt : DbContext
    {
        public DbSet<FavoriteCity> FavoriteCities { get; set; }
        public DbSet<WeatherCache> WeatherCaches { get; set; }

        public SqliteDbContexrt(DbContextOptions<SqliteDbContexrt> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FavoriteCity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<WeatherCache>(entity =>
            {
                entity.HasKey(e => e.CityId);
                entity.Property(e => e.JsonData).IsRequired();
                entity.Property(e => e.CachedAt).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
