using NetWatch.Model;
using NetWatch.Model.Entities;
using NetWatch.Model.Enums;
using NetWatch.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NetWatch.Desktop.Service
{
    public class NetworkScannerStub : INetworkScanner
    {
        public event EventHandler<NetworkDevice>? DeviceDiscovered;
        public event EventHandler<string>? ScanStatusChanged;
        public event EventHandler<double>? ScanProgressChanged;

        public async Task<List<NetworkDevice>> ScanNetworkAsync(string networkRange)
        {
            ScanStatusChanged?.Invoke(this, "Starting scan...");

            var devices = new List<NetworkDevice>
            {
                new NetworkDevice
                {
                    Id = Guid.NewGuid(),
                    IpAddress = "192.168.1.1",
                    HostName = "Router",
                    MACAddress = "00:11:22:33:44:55",
                    Status = NetStatus.Online,
                    IsTrusted = true,
                    LastSeen = DateTime.Now,
                    DeviceType = DeviceType.Router
                },
                new NetworkDevice
                {
                    Id = Guid.NewGuid(),
                    IpAddress = "192.168.1.100",
                    HostName = "My-PC",
                    MACAddress = "AA:BB:CC:DD:EE:FF",
                    Status = NetStatus.Online,
                    IsTrusted = true,
                    LastSeen = DateTime.Now,
                    DeviceType = DeviceType.Computer
                },
                new NetworkDevice
                {
                    Id = Guid.NewGuid(),
                    IpAddress = "192.168.1.101",
                    HostName = "Smartphone",
                    MACAddress = "11:22:33:44:55:66",
                    Status = NetStatus.Offline,
                    IsTrusted = false,
                    LastSeen = DateTime.Now.AddHours(-2),
                    DeviceType = DeviceType.Phone
                }
            };

            // Симулируем прогресс сканирования
            for (int i = 0; i <= 100; i += 20)
            {
                await Task.Delay(100);
                ScanProgressChanged?.Invoke(this, i / 100.0);
                ScanStatusChanged?.Invoke(this, $"Scanning... {i}%");
            }

            // "Обнаруживаем" устройства
            foreach (var device in devices)
            {
                await Task.Delay(200);
                DeviceDiscovered?.Invoke(this, device);
            }

            ScanStatusChanged?.Invoke(this, "Scan completed");
            return devices;
        }
    }

    public class DeviceRepositoryStub : IDeviceRepository
    {
        private readonly List<NetworkDevice> _devices = new();

        public DeviceRepositoryStub()
        {
            _devices.Add(new NetworkDevice
            {
                Id = Guid.NewGuid(),
                IpAddress = "192.168.1.1",
                HostName = "Router",
                MACAddress = "00:11:22:33:44:55",
                Status = NetStatus.Online,
                IsTrusted = true,
                LastSeen = DateTime.Now,
                DeviceType = DeviceType.Router
            });
        }

        public Task<NetworkDevice?> GetByIdAsync(Guid id)
        {
            var device = _devices.Find(d => d.Id == id);
            return Task.FromResult(device);
        }

        public Task<List<NetworkDevice>> GetAllAsync()
        {
            return Task.FromResult(new List<NetworkDevice>(_devices));
        }

        public Task AddAsync(NetworkDevice device)
        {
            _devices.Add(device);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(NetworkDevice device)
        {
            var index = _devices.FindIndex(d => d.Id == device.Id);
            if (index >= 0)
            {
                _devices[index] = device;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            var device = _devices.Find(d => d.Id == id);
            if (device != null)
            {
                _devices.Remove(device);
            }
            return Task.CompletedTask;
        }

        public Task<bool> ExistsAsync(Guid id)
        {
            var exists = _devices.Exists(d => d.Id == id);
            return Task.FromResult(exists);
        }

        public Task<List<NetworkDevice>> GetByIpRangeAsync(string ipRange)
        {
            return Task.FromResult(new List<NetworkDevice>(_devices));
        }

        public IQueryable<NetworkDevice> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<NetworkDevice> GetAllAsNoTracking()
        {
            throw new NotImplementedException();
        }

        public Task<NetworkDevice?> GetByIpAddressAsync(string ipAddress)
        {
            throw new NotImplementedException();
        }

        public Task<NetworkDevice?> GetByMacAddressAsync(string macAddress)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<NetworkDevice> devices)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRangeAsync(IEnumerable<NetworkDevice> devices)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRangeAsync(IEnumerable<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsByIpAsync(string ipAddress)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsByMacAsync(string macAddress)
        {
            throw new NotImplementedException();
        }

        public Task<List<NetworkDevice>> GetByStatusAsync(NetStatus status)
        {
            throw new NotImplementedException();
        }

        public Task<List<NetworkDevice>> GetByDeviceTypeAsync(DeviceType deviceType)
        {
            throw new NotImplementedException();
        }

        public Task<List<NetworkDevice>> GetByTrustStatusAsync(bool isTrusted)
        {
            throw new NotImplementedException();
        }

        public Task<List<NetworkDevice>> GetSeenAfterAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<List<NetworkDevice>> GetInactiveDevicesAsync(TimeSpan threshold)
        {
            throw new NotImplementedException();
        }

        public Task<DeviceStatistics> GetStatisticsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync()
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }

    public class AlertServiceStub : IAlertService
    {
        private readonly List<Alert> _alerts = new();
        public event EventHandler<Alert>? AlertCreated;

        public Task CreateAlertAsync(Alert alert)
        {
            alert.Id = Guid.NewGuid();
            alert.CreatedAt = DateTime.Now;
            _alerts.Add(alert);
            AlertCreated?.Invoke(this, alert);
            return Task.CompletedTask;
        }

        public Task CreateNewDeviceAlertAsync(NetworkDevice device)
        {
            var alert = new Alert
            {
                Id = Guid.NewGuid(),
                DeviceId = device.Id,
                Message = $"New device detected: {device.IpAddress} ({device.HostName})",
                CreatedAt = DateTime.Now
            };
            return CreateAlertAsync(alert);
        }

        public Task CreateDeviceStatusChangeAlertAsync(NetworkDevice device, NetStatus previousStatus)
        {
            var alert = new Alert
            {
                Id = Guid.NewGuid(),
                DeviceId = device.Id,
                Message = $"Device {device.IpAddress} changed status: {previousStatus} -> {device.Status}",
                CreatedAt = DateTime.Now
            };
            return CreateAlertAsync(alert);
        }

        public Task CreateOpenPortAlertAsync(NetworkDevice device, int port)
        {
            var alert = new Alert
            {
                Id = Guid.NewGuid(),
                DeviceId = device.Id,
                Message = $"Open port {port} detected on {device.IpAddress}",
                CreatedAt = DateTime.Now
            };
            return CreateAlertAsync(alert);
        }

        public Task CreateUnauthorizedDeviceAlertAsync(NetworkDevice device)
        {
            var alert = new Alert
            {
                Id = Guid.NewGuid(),
                DeviceId = device.Id,
                Message = $"Unauthorized device detected: {device.IpAddress} ({device.MACAddress})",
                CreatedAt = DateTime.Now
            };
            return CreateAlertAsync(alert);
        }

        public Task<List<Alert>> GetUnreadAlertsAsync()
        {
            var unreadAlerts = _alerts.FindAll(a => !a.IsAcknowledged);
            return Task.FromResult(unreadAlerts);
        }

        public Task<List<Alert>> GetAllAlertsAsync(int skip = 0, int take = 50)
        {
            var result = _alerts
                .OrderByDescending(a => a.CreatedAt)
                .Skip(skip)
                .Take(take)
                .ToList();
            return Task.FromResult(result);
        }

        public Task<List<Alert>> GetDeviceAlertsAsync(Guid deviceId)
        {
            var deviceAlerts = _alerts.FindAll(a => a.DeviceId == deviceId);
            return Task.FromResult(deviceAlerts);
        }

        public Task MarkAlertAsReadAsync(Guid alertId)
        {
            var alert = _alerts.Find(a => a.Id == alertId);
            if (alert != null)
            {
                alert.IsAcknowledged = true;
            }
            return Task.CompletedTask;
        }

        public Task MarkAllAlertsAsReadAsync()
        {
            foreach (var alert in _alerts)
            {
                alert.IsAcknowledged = true;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAlertAsync(Guid alertId)
        {
            var alert = _alerts.Find(a => a.Id == alertId);
            if (alert != null)
            {
                _alerts.Remove(alert);
            }
            return Task.CompletedTask;
        }

        public Task DeleteReadAlertsAsync()
        {
            _alerts.RemoveAll(a => a.IsAcknowledged);
            return Task.CompletedTask;
        }

        public Task<int> GetUnreadAlertCountAsync()
        {
            var count = _alerts.Count(a => !a.IsAcknowledged);
            return Task.FromResult(count);
        }

        public Task ClearAllAlertsAsync()
        {
            _alerts.Clear();
            return Task.CompletedTask;
        }
    }

    public class DeviceServiceStub : IDeviceService
    {
        private readonly List<NetworkDevice> _devices = new();
        private readonly IAlertService _alertService;

        public DeviceServiceStub(IAlertService alertService)
        {
            _alertService = alertService ?? throw new ArgumentNullException(nameof(alertService));

            // Инициализируем тестовыми устройствами
            InitializeTestDevices();
        }

        private void InitializeTestDevices()
        {
            _devices.AddRange(new[]
            {
                new NetworkDevice
                {
                    Id = Guid.NewGuid(),
                    IpAddress = "192.168.1.1",
                    HostName = "Router",
                    MACAddress = "00:11:22:33:44:55",
                    Status = NetStatus.Online,
                    IsTrusted = true,
                    LastSeen = DateTime.Now,
                    DeviceType = DeviceType.Router
                },
                new NetworkDevice
                {
                    Id = Guid.NewGuid(),
                    IpAddress = "192.168.1.100",
                    HostName = "My-PC",
                    MACAddress = "AA:BB:CC:DD:EE:FF",
                    Status = NetStatus.Online,
                    IsTrusted = true,
                    LastSeen = DateTime.Now,
                    DeviceType = DeviceType.Computer
                }
            });
        }

        public Task<List<NetworkDevice>> GetAllDevicesAsync()
        {
            return Task.FromResult(new List<NetworkDevice>(_devices));
        }

        public Task<NetworkDevice?> GetByIdAsync(Guid id)
        {
            var device = _devices.Find(d => d.Id == id);
            return Task.FromResult(device);
        }

        public async Task AddOrUpdateDevicesAsync(List<NetworkDevice> devices)
        {
            foreach (var device in devices)
            {
                var existing = _devices.Find(d => d.IpAddress == device.IpAddress);
                if (existing != null)
                {
                    existing.HostName = device.HostName;
                    existing.MACAddress = device.MACAddress;
                    existing.Status = device.Status;
                    existing.LastSeen = DateTime.Now;
                }
                else
                {
                    _devices.Add(device);
                    await _alertService.CreateNewDeviceAlertAsync(device);
                }
            }
        }

        public async Task MarkAsTrustedAsync(Guid deviceId, bool isTrusted)
        {
            var device = _devices.Find(d => d.Id == deviceId);
            if (device != null)
            {
                var oldStatus = device.IsTrusted;
                device.IsTrusted = isTrusted;

                if (oldStatus != isTrusted)
                {
                    var alert = new Alert
                    {
                        Id = Guid.NewGuid(),
                        DeviceId = deviceId,
                        Message = isTrusted ?
                            $"Device {device.IpAddress} marked as trusted" :
                            $"Device {device.IpAddress} marked as untrusted",
                        CreatedAt = DateTime.Now
                    };
                    await _alertService.CreateAlertAsync(alert);
                }
            }
        }

        public async Task DeleteDeviceAsync(Guid deviceId)
        {
            var device = _devices.Find(d => d.Id == deviceId);
            if (device != null)
            {
                _devices.Remove(device);

                var alert = new Alert
                {
                    Id = Guid.NewGuid(),
                    Message = $"Device {device.IpAddress} deleted",
                    CreatedAt = DateTime.Now
                };
                await _alertService.CreateAlertAsync(alert);
            }
        }

        public async Task<NetworkDevice?> UpdateDeviceStatusAsync(Guid deviceId, NetStatus status)
        {
            var device = _devices.Find(d => d.Id == deviceId);
            if (device != null)
            {
                var oldStatus = device.Status;
                device.Status = status;
                device.LastSeen = DateTime.Now;

                if (oldStatus != status)
                {
                    await _alertService.CreateDeviceStatusChangeAlertAsync(device, oldStatus);
                }
            }
            return device;
        }

        public Task<NetworkDevice?> GetDeviceByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<NetworkDevice?> GetDeviceByIpAsync(string ipAddress)
        {
            throw new NotImplementedException();
        }

        public Task AddOrUpdateDeviceAsync(NetworkDevice device)
        {
            throw new NotImplementedException();
        }

        Task IDeviceService.UpdateDeviceStatusAsync(Guid deviceId, NetStatus status)
        {
            return UpdateDeviceStatusAsync(deviceId, status);
        }

        public Task<List<NetworkDevice>> SearchDevicesAsync(string searchTerm)
        {
            throw new NotImplementedException();
        }

        public Task<List<NetworkDevice>> GetDevicesByTypeAsync(DeviceType deviceType)
        {
            throw new NotImplementedException();
        }

        public Task<DeviceStatistics> GetDeviceStatisticsAsync()
        {
            throw new NotImplementedException();
        }

        public Task ClearAllDevicesAsync()
        {
            throw new NotImplementedException();
        }
    }
}