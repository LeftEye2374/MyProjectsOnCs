using Microsoft.EntityFrameworkCore;
using CrabCounter.Models;

namespace CrabCounter.SqliteDbContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<Counter> crabs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
