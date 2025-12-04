// NetWatch.Model/Interfaces/IDeviceService.cs
using NetWatch.Model;
using NetWatch.Model.Entities;
using NetWatch.Model.Enums;

namespace NetWatch.Model.Interfaces
{
    public interface IDeviceService
    {
        Task<List<NetworkDevice>> GetAllDevicesAsync();
        Task<NetworkDevice?> GetDeviceByIdAsync(int id);
        Task<NetworkDevice?> GetDeviceByIpAsync(string ipAddress);
        Task AddOrUpdateDeviceAsync(NetworkDevice device);
        Task AddOrUpdateDevicesAsync(List<NetworkDevice> devices);
        Task DeleteDeviceAsync(int id);
        Task UpdateDeviceStatusAsync(int deviceId, NetStatus status);
        Task MarkAsTrustedAsync(int deviceId, bool isTrusted);
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