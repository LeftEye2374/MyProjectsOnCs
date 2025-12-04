using Microsoft.EntityFrameworkCore;
using NetWatch.Model;
using NetWatch.Model.Entities;
using NetWatch.Model.Enums;
using NetWatch.Model.Interfaces;

namespace NetWatch.Desktop.Service
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IAlertService _alertService;

        public DeviceService(IDeviceRepository deviceRepository, IAlertService alertService)
        {
            _deviceRepository = deviceRepository;
            _alertService = alertService;
        }

        public async Task<List<NetworkDevice>> GetAllDevicesAsync()
        {
            return await _deviceRepository.GetAllAsNoTracking().ToListAsync();
        }

        public async Task<NetworkDevice?> GetDeviceByIdAsync(Guid id)
        {
            return await _deviceRepository.GetByIdAsync(id);
        }

        public async Task<NetworkDevice?> GetDeviceByIpAsync(string ipAddress)
        {
            return await _deviceRepository.GetByIpAddressAsync(ipAddress);
        }

        public async Task AddOrUpdateDeviceAsync(NetworkDevice device)
        {
            var existingDevice = await _deviceRepository.GetByIpAddressAsync(device.IpAddress);

            if (existingDevice == null)
            {
                device.FirstSeen = DateTime.Now;
                await _deviceRepository.AddAsync(device);
                await _alertService.CreateNewDeviceAlertAsync(device);
            }
            else
            {
                var previousStatus = existingDevice.Status;

                existingDevice.HostName = device.HostName ?? existingDevice.HostName;
                existingDevice.MACAddress = device.MACAddress ?? existingDevice.MACAddress;
                existingDevice.Vendor = device.Vendor ?? existingDevice.Vendor;
                existingDevice.DeviceType = device.DeviceType;
                existingDevice.LastSeen = DateTime.Now;
                existingDevice.Status = device.Status;

                if (previousStatus != device.Status)
                {
                    await _alertService.CreateDeviceStatusChangeAlertAsync(existingDevice, previousStatus);
                }

                await _deviceRepository.UpdateAsync(existingDevice);
            }
        }

        public async Task AddOrUpdateDevicesAsync(List<NetworkDevice> devices)
        {
            foreach (var device in devices)
            {
                await AddOrUpdateDeviceAsync(device);
            }
        }

        public async Task DeleteDeviceAsync(Guid id)
        {
            await _deviceRepository.DeleteAsync(id);
        }

        public async Task UpdateDeviceStatusAsync(Guid deviceId, NetStatus status)
        {
            var device = await _deviceRepository.GetByIdAsync(deviceId);
            if (device != null)
            {
                var previousStatus = device.Status;
                device.Status = status;
                device.LastSeen = DateTime.Now;

                await _deviceRepository.UpdateAsync(device);

                if (previousStatus != status)
                {
                    await _alertService.CreateDeviceStatusChangeAlertAsync(device, previousStatus);
                }
            }
        }

        public async Task MarkAsTrustedAsync(Guid deviceId, bool isTrusted)
        {
            var device = await _deviceRepository.GetByIdAsync(deviceId);
            if (device != null)
            {
                device.IsTrusted = isTrusted;
                await _deviceRepository.UpdateAsync(device);
            }
        }

        public async Task<List<NetworkDevice>> SearchDevicesAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return await GetAllDevicesAsync();

            var devices = _deviceRepository.GetAllAsNoTracking();

            return await devices
                .Where(d =>
                    d.IpAddress.Contains(searchTerm) ||
                    d.HostName != null && d.HostName.Contains(searchTerm) ||
                    d.MACAddress != null && d.MACAddress.Contains(searchTerm) ||
                    d.Vendor != null && d.Vendor.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<List<NetworkDevice>> GetDevicesByTypeAsync(DeviceType deviceType)
        {
            return await _deviceRepository.GetByDeviceTypeAsync(deviceType);
        }

        public async Task<DeviceStatistics> GetDeviceStatisticsAsync()
        {
            return await _deviceRepository.GetStatisticsAsync();
        }

        public async Task ClearAllDevicesAsync()
        {
            var devices = await _deviceRepository.GetAll().ToListAsync();
            var deviceIds = devices.Select(d => d.Id).ToList();

            await _deviceRepository.DeleteRangeAsync(deviceIds);
        }

        public async Task SaveDevicesAsync(List<NetworkDevice> devices)
        {
            await AddOrUpdateDevicesAsync(devices);
        }
    }
}