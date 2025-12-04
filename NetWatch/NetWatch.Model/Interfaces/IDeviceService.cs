using NetWatch.Model.Entities;
using NetWatch.Model.Enums;

namespace NetWatch.Model.Interfaces
{
    public interface IDeviceService
    {
        Task<List<NetworkDevice>> GetAllDevicesAsync();
        Task<NetworkDevice?> GetDeviceByIdAsync(Guid id);
        Task<NetworkDevice?> GetDeviceByIpAsync(string ipAddress);
        Task AddOrUpdateDeviceAsync(NetworkDevice device);
        Task AddOrUpdateDevicesAsync(List<NetworkDevice> devices);
        Task DeleteDeviceAsync(Guid id);
        Task UpdateDeviceStatusAsync(Guid deviceId, NetStatus status);
        Task MarkAsTrustedAsync(Guid deviceId, bool isTrusted);
        Task<List<NetworkDevice>> SearchDevicesAsync(string searchTerm);
        Task<List<NetworkDevice>> GetDevicesByTypeAsync(DeviceType deviceType);
        Task<DeviceStatistics> GetDeviceStatisticsAsync();
        Task ClearAllDevicesAsync();
    }

    public class DeviceStatistics
    {
        public int TotalDevices { get; set; }
        public int OnlineDevices { get; set; }
        public int OfflineDevices { get; set; }
        public int TrustedDevices { get; set; }
        public int UnknownDevices { get; set; }
    }
}