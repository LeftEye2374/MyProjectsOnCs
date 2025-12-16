using Microsoft.EntityFrameworkCore;
using CrabCounter.Models;

namespace CrabCounter.SqliteDbContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<Counter> crabs { get; set; }

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
