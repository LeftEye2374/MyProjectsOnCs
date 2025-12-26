using Microsoft.EntityFrameworkCore;
using StudApp.Models;

namespace StudentApp.AppDbContext
{
    public class SqliteDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Student> Students { get; set; }

        public SqliteDbContext(DbContextOptions<SqliteDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>();
            modelBuilder.Entity<Student>();
        }
    }
}
