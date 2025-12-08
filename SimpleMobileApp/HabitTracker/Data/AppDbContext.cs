using Microsoft.EntityFrameworkCore;
using HabitTracker.Models;

namespace HabitTracker.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Habit> Habits { get; set; } = null!;
        public DbSet<HabitLog> HabitLogs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "HabitTracker.db");
            optionsBuilder.UseSqlite($"Data Source={dbPath}");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Habit>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .IsRequired()                  
                    .HasMaxLength(100);            

                entity.Property(e => e.Description)
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("datetime('now')");  

                entity.Property(e => e.Color)
                    .HasMaxLength(7);              // HEX цвет #RRGGBB

                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true);

                entity.HasMany(e => e.HabitLogs)
                    .WithOne(l => l.Habit)
                    .HasForeignKey(l => l.HabitId)
                    .OnDelete(DeleteBehavior.Cascade);  
            });

            modelBuilder.Entity<HabitLog>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.CompletedDate)
                    .HasDefaultValueSql("datetime('now')");

                entity.Property(e => e.Notes)
                    .HasMaxLength(500);

                entity.HasOne(e => e.Habit)
                    .WithMany(h => h.HabitLogs)
                    .HasForeignKey(e => e.HabitId);
            });
        }
    }
}
