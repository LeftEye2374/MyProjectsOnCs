using NetWatch.Model.Entities;
using NetWatch.Model.Enums;

namespace NetWatch.Model.Interfaces
{
    public interface IDeviceRepository
    {
        IQueryable<NetworkDevice> GetAll();
        IQueryable<NetworkDevice> GetAllAsNoTracking();
        Task<NetworkDevice?> GetByIdAsync(Guid id); 
        Task<NetworkDevice?> GetByIpAddressAsync(string ipAddress);
        Task<NetworkDevice?> GetByMacAddressAsync(string macAddress);
        Task AddAsync(NetworkDevice device);
        Task AddRangeAsync(IEnumerable<NetworkDevice> devices);
        Task UpdateAsync(NetworkDevice device);
        Task UpdateRangeAsync(IEnumerable<NetworkDevice> devices);
        Task DeleteAsync(Guid id);
        Task DeleteRangeAsync(IEnumerable<Guid> ids); 
        Task<bool> ExistsByIpAsync(string ipAddress);
        Task<bool> ExistsByMacAsync(string macAddress);
        Task<List<NetworkDevice>> GetByStatusAsync(NetStatus status);
        Task<List<NetworkDevice>> GetByDeviceTypeAsync(DeviceType deviceType);
        Task<List<NetworkDevice>> GetByTrustStatusAsync(bool isTrusted);
        Task<List<NetworkDevice>> GetSeenAfterAsync(DateTime date);
        Task<List<NetworkDevice>> GetInactiveDevicesAsync(TimeSpan threshold);
        Task<DeviceStatistics> GetStatisticsAsync();
        Task<int> CountAsync();
        Task SaveChangesAsync();
    }
}