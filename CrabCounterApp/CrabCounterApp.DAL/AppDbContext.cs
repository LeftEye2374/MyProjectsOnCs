using CrabCounterApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CrabCounterApp.SqliteDbContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<Autorization> Autorizations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autorization>().HasKey(e => e.Id);
            modelBuilder.Entity<Autorization>().HasIndex(e => e.Id).IsUnique();
        }
    }
}
