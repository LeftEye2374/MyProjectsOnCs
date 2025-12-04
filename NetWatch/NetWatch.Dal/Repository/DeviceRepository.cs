using Microsoft.EntityFrameworkCore;
using NetWatch.Model.Entities;
using NetWatch.Model.Enums;
using NetWatch.Model.Interfaces;

namespace NetWatch.DAL.Repository
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly SqlDbContext _context;

        public DeviceRepository(SqlDbContext context)
        {
            _context = context;
        }

        public IQueryable<NetworkDevice> GetAll()
        {
            return _context.NetworkDevices
                .Include(d => d.ScanHistory)
                .Include(d => d.Alerts)
                .AsQueryable();
        }

        public IQueryable<NetworkDevice> GetAllAsNoTracking()
        {
            return _context.NetworkDevices
                .Include(d => d.ScanHistory)
                .Include(d => d.Alerts)
                .AsNoTracking()
                .AsQueryable();
        }

        public async Task<NetworkDevice?> GetByIdAsync(Guid id)
        {
            return await _context.NetworkDevices
                .Include(d => d.ScanHistory)
                .Include(d => d.Alerts)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<NetworkDevice?> GetByIpAddressAsync(string ipAddress)
        {
            return await _context.NetworkDevices
                .FirstOrDefaultAsync(d => d.IpAddress == ipAddress);
        }

        public async Task<NetworkDevice?> GetByMacAddressAsync(string macAddress)
        {
            if (string.IsNullOrEmpty(macAddress))
                return null;

            return await _context.NetworkDevices
                .FirstOrDefaultAsync(d => d.MACAddress == macAddress);
        }

        public async Task AddAsync(NetworkDevice device)
        {
            await _context.NetworkDevices.AddAsync(device);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<NetworkDevice> devices)
        {
            await _context.NetworkDevices.AddRangeAsync(devices);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(NetworkDevice device)
        {
            _context.NetworkDevices.Update(device);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<NetworkDevice> devices)
        {
            _context.NetworkDevices.UpdateRange(devices);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var device = await GetByIdAsync(id);
            if (device != null)
            {
                _context.NetworkDevices.Remove(device);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<Guid> ids)
        {
            var devices = await _context.NetworkDevices
                .Where(d => ids.Contains(d.Id))
                .ToListAsync();

            _context.NetworkDevices.RemoveRange(devices);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIpAsync(string ipAddress)
        {
            return await _context.NetworkDevices
                .AnyAsync(d => d.IpAddress == ipAddress);
        }

        public async Task<bool> ExistsByMacAsync(string macAddress)
        {
            if (string.IsNullOrEmpty(macAddress))
                return false;

            return await _context.NetworkDevices
                .AnyAsync(d => d.MACAddress == macAddress);
        }

        public async Task<List<NetworkDevice>> GetByStatusAsync(NetStatus status)
        {
            return await _context.NetworkDevices
                .Where(d => d.Status == status)
                .ToListAsync();
        }

        public async Task<List<NetworkDevice>> GetByDeviceTypeAsync(DeviceType deviceType)
        {
            return await _context.NetworkDevices
                .Where(d => d.DeviceType == deviceType)
                .ToListAsync();
        }

        public async Task<List<NetworkDevice>> GetByTrustStatusAsync(bool isTrusted)
        {
            return await _context.NetworkDevices
                .Where(d => d.IsTrusted == isTrusted)
                .ToListAsync();
        }

        public async Task<List<NetworkDevice>> GetSeenAfterAsync(DateTime date)
        {
            return await _context.NetworkDevices
                .Where(d => d.LastSeen >= date)
                .ToListAsync();
        }

        public async Task<List<NetworkDevice>> GetInactiveDevicesAsync(TimeSpan threshold)
        {
            var cutoffDate = DateTime.Now - threshold;
            return await _context.NetworkDevices
                .Where(d => d.LastSeen < cutoffDate)
                .ToListAsync();
        }

        public async Task<DeviceStatistics> GetStatisticsAsync()
        {
            var devices = await _context.NetworkDevices.ToListAsync();

            return new DeviceStatistics
            {
                TotalDevices = devices.Count,
                OnlineDevices = devices.Count(d => d.Status == NetStatus.Online),
                OfflineDevices = devices.Count(d => d.Status == NetStatus.Offline),
                TrustedDevices = devices.Count(d => d.IsTrusted),
                UnknownDevices = devices.Count(d => d.Status == NetStatus.Unknown)
            };
        }

        public async Task<int> CountAsync()
        {
            return await _context.NetworkDevices.CountAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}