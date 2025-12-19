using Microsoft.EntityFrameworkCore;
using CrabCounter.Models;

namespace CrabCounter.SqliteDbContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<Counter> Crabs { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<Counter>();
            modelBuilder.Entity<User>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
