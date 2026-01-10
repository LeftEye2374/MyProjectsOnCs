using Microsoft.EntityFrameworkCore;
using StudApplication.Models;

namespace StudApplications.AppDbContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Intruder> Intruders { get; set; } 
        public DbSet<Report> Reports { get; set; }
             
        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(entity => {
                entity.OwnsOne(e => e.PersonInformation);
                entity.OwnsOne(e => e.ContactInformation);
                entity.OwnsOne(e => e.Autorization);
            });

            modelBuilder.Entity<Intruder>(entity => {
                entity.OwnsOne(e => e.PersonInformation);
                entity.OwnsOne(e => e.ContactInformation);
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasOne(r => r.Employee)
                      .WithMany(e => e.Reports)  
                      .HasForeignKey(r => r.EmployeeId)
                      .OnDelete(DeleteBehavior.Restrict);  

                entity.HasOne(r => r.Intruder)
                      .WithOne(i => i.Report)  
                      .HasForeignKey<Report>(r => r.IntruderId)
                      .OnDelete(DeleteBehavior.Cascade);  
            });
        }
    }
}
