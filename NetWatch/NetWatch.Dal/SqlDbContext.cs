using Microsoft.EntityFrameworkCore;
using NetWatch.Model.Entities;

namespace NetWatch.DAL
{
    public partial class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {
        }

        public DbSet<Alert> Alerts { get; set; }
        public DbSet<DeviceScanHistory> DeviceScanHistories { get; set; }
        public DbSet<NetworkScanSession> NetworkScanSessions { get; set; }
        public DbSet<NetworkDevice> NetworkDevices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<NetworkDevice>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<NetworkDevice>()
                .HasIndex(d => d.IpAddress)
                .IsUnique();

            modelBuilder.Entity<NetworkDevice>()
                .HasIndex(d => d.MACAddress)
                .IsUnique();

            modelBuilder.Entity<Alert>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Alert>()
                .HasOne(a => a.Device)
                .WithMany(d => d.Alerts)
                .HasForeignKey(a => a.DeviceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NetworkScanSession>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<DeviceScanHistory>()
                .HasKey(h => h.Id);

            modelBuilder.Entity<DeviceScanHistory>()
                .HasOne(h => h.Device)
                .WithMany(d => d.ScanHistory)
                .HasForeignKey(h => h.DeviceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DeviceScanHistory>()
                .HasOne(h => h.ScanSession)
                .WithMany(s => s.DeviceScanHistories)
                .HasForeignKey(h => h.ScanSessionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}