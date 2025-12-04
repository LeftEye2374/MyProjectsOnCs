using Microsoft.EntityFrameworkCore;
using NetWatch.Model.Entities;

namespace NetWatch.DAL
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
        }

        public DbSet<Alert> Alerts { get; set; } = null!;
        public DbSet<DeviceScanHistory> DeviceScanHistories { get; set; } = null!;
        public DbSet<NetworkScanSession> NetworkScanSessions { get; set; } = null!;
        public DbSet<NetworkDevice> NetworkDevices { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NetworkDevice>()
                .HasMany(d => d.ScanHistory)
                .WithOne(h => h.Device)
                .HasForeignKey(h => h.DeviceId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<NetworkDevice>()
                .HasMany(d => d.Alerts)
                .WithOne(a => a.Device)
                .HasForeignKey(a => a.DeviceId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<NetworkScanSession>()
                .HasMany(s => s.DeviceScanHistories)
                .WithOne(h => h.ScanSession)
                .HasForeignKey(h => h.ScanSessionId)
                .OnDelete(DeleteBehavior.Cascade); 

            modelBuilder.Entity<NetworkDevice>()
                .HasIndex(d => d.IpAddress)
                .IsUnique();

            modelBuilder.Entity<NetworkDevice>()
                .HasIndex(d => d.MACAddress)
                .IsUnique();

            modelBuilder.Entity<Alert>()
                .HasIndex(a => a.CreatedAt);

            modelBuilder.Entity<DeviceScanHistory>()
                .HasIndex(h => h.ScanTimestamp);
        }
    }
}