using Microsoft.EntityFrameworkCore;
using StudApp.Models;

namespace StudApp.AppDbContext
{
    internal class SqliteDbContext : DbContext
    {

        public DbSet<Imposter> Imposters { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Shift> Shifts { get; set; }

        public SqliteDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().OwnsOne(e => e.PersonInfo);
            modelBuilder.Entity<Employee>().OwnsOne(e => e.ContactInfo);

            modelBuilder.Entity<Imposter>().OwnsOne(i => i.PersonInfo);
            modelBuilder.Entity<Imposter>().OwnsOne(i => i.ContactInfo);

            modelBuilder.Entity<Report>().OwnsOne(r => r.PersonInfo);
            modelBuilder.Entity<Report>().OwnsOne(r => r.ContactInfo);

            modelBuilder.Entity<Shift>()
                .HasOne(s => s.Master)
                .WithMany()
                .HasForeignKey(s => s.MasterId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Shift)
                .WithMany(s => s.Employees)
                .HasForeignKey(e => e.ShiftId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
