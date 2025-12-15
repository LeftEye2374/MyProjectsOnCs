using CrabCounterApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CrabCounterApp.DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<CrabCount> CrabCounts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CrabCount>().HasKey(e => e.Id);
            modelBuilder.Entity<CrabCount>().HasIndex(e => e.Id).IsUnique();
        }
    }
}
