using Microsoft.EntityFrameworkCore;
using CRM.Model;

namespace CRM.ApplicationSqlDbContext
{
    public partial class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ContactInfo> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manufacturer>()
                .HasMany<Product>();
            modelBuilder.Entity<Product>()
                 .HasOne(p => p.Manufacturer)           
                 .WithMany(m => m.Products)            
                 .HasForeignKey(p => p.ManufacturerId) 
                 .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ContactInfo>();

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
