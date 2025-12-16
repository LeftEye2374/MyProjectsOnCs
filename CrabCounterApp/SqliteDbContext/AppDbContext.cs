using CrabCounter.Models;
using Microsoft.EntityFrameworkCore;

namespace SqliteDbContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<CrabCounter> crabs { get; set; }

        public AppDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=crabiki.db");
        }
    }
}
