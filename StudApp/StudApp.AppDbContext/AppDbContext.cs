using Microsoft.EntityFrameworkCore;
using StudApp.Models;


namespace StudApp.AppDbContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<Imposter> Imposters  { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<Report> Reports { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.CreatedReports)
                .WithOne(r => r.CreatedByEmployee)
                .HasForeignKey(r => r.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Imposter>()
                .HasMany(i => i.CreatedReports)
                .WithOne(r => r.CreatedByImposter)
                .HasForeignKey(r => r.ImposterId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Report>()
                .HasMany(r => r.Employees)
                .WithMany(e => e.AssignedReports)
                .UsingEntity(j => j.ToTable("ReportEmployees"));

            modelBuilder.Entity<Shift>()
                .HasOne(s => s.Master)
                .WithMany(e => e.ManagedShifts)
                .HasForeignKey(s => s.MasterId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Shift>()
                .HasMany(s => s.Employees)
                .WithOne(e => e.Shift)
                .HasForeignKey(e => e.ShiftId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
