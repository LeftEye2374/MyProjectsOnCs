using Microsoft.EntityFrameworkCore;
using NetWatch.DAL;
using NetWatch.Model.Entities;
using NetWatch.Model.Enums;
using NetWatch.Model.Interfaces;

namespace NetWatch.Desktop.Service
{
    public class AlertService : IAlertService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly SqlDbContext _context;

        public event EventHandler<Alert>? AlertCreated;

        public AlertService(IDeviceRepository deviceRepository, SqlDbContext context)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CreateAlertAsync(Alert alert)
        {
            try
            {
                if (alert.DeviceId.HasValue)
                {
                    var deviceExists = await _deviceRepository.GetByIdAsync(alert.DeviceId.Value) != null;
                    if (!deviceExists)
                        throw new ArgumentException($"Device with ID {alert.DeviceId} not found");
                }

                alert.CreatedAt = DateTime.UtcNow;
                await _context.Alerts.AddAsync(alert);
                await _context.SaveChangesAsync();

                AlertCreated?.Invoke(this, alert);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating alert: {ex.Message}");
                throw;
            }
        }

        public async Task CreateNewDeviceAlertAsync(NetworkDevice device)
        {
            if (device == null) throw new ArgumentNullException(nameof(device));

            var alert = new Alert
            {
                DeviceId = device.Id,
                AlertType = AlertType.NewDevice,
                Message = $"Обнаружено новое устройство: {device.IpAddress} ({device.HostName ?? "Неизвестное имя"})",
                AlertLevel = device.IsTrusted ? AlertLevel.Info : AlertLevel.Warning,
                IsAcknowledged = false
            };

            await CreateAlertAsync(alert);
        }

        public async Task CreateDeviceStatusChangeAlertAsync(NetworkDevice device, NetStatus previousStatus)
        {
            if (device == null) throw new ArgumentNullException(nameof(device));

            var alert = new Alert
            {
                DeviceId = device.Id,
                AlertType = previousStatus == NetStatus.Online ? AlertType.DeviceOffline : AlertType.DeviceOnline,
                Message = $"Устройство {device.IpAddress} изменило статус: {previousStatus} → {device.Status}",
                AlertLevel = AlertLevel.Info,
                IsAcknowledged = false
            };

            await CreateAlertAsync(alert);
        }

        public async Task CreateOpenPortAlertAsync(NetworkDevice device, int port)
        {
            if (device == null) throw new ArgumentNullException(nameof(device));

            var alert = new Alert
            {
                DeviceId = device.Id,
                AlertType = AlertType.PortOpen,
                Message = $"Обнаружен открытый порт {port} на устройстве {device.IpAddress}",
                AlertLevel = device.IsTrusted ? AlertLevel.Warning : AlertLevel.Critical,
                IsAcknowledged = false
            };

            await CreateAlertAsync(alert);
        }

        public async Task CreateUnauthorizedDeviceAlertAsync(NetworkDevice device)
        {
            if (device == null) throw new ArgumentNullException(nameof(device));

            var alert = new Alert
            {
                DeviceId = device.Id,
                AlertType = AlertType.UnauthorizedDevice,
                Message = $"Неавторизованное устройство обнаружено в сети: {device.IpAddress} ({device.MACAddress})",
                AlertLevel = AlertLevel.Critical,
                IsAcknowledged = false
            };

            await CreateAlertAsync(alert);
        }

        public async Task<List<Alert>> GetUnreadAlertsAsync()
        {
            return await _context.Alerts
                .Include(a => a.Device)
                .Where(a => !a.IsAcknowledged)
                .OrderByDescending(a => a.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Alert>> GetAllAlertsAsync(int skip = 0, int take = 50)
        {
            return await _context.Alerts
                .Include(a => a.Device)
                .OrderByDescending(a => a.CreatedAt)
                .Skip(skip)
                .Take(take)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Alert>> GetDeviceAlertsAsync(Guid deviceId)
        {
            return await _context.Alerts
                .Where(a => a.DeviceId == deviceId)
                .OrderByDescending(a => a.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task MarkAlertAsReadAsync(Guid alertId)
        {
            var alert = await _context.Alerts.FindAsync(alertId);
            if (alert != null)
            {
                alert.IsAcknowledged = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task MarkAllAlertsAsReadAsync()
        {
            var unreadAlerts = await _context.Alerts
                .Where(a => !a.IsAcknowledged)
                .ToListAsync();

            foreach (var alert in unreadAlerts)
            {
                alert.IsAcknowledged = true;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAlertAsync(Guid alertId)
        {
            var alert = await _context.Alerts.FindAsync(alertId);
            if (alert != null)
            {
                _context.Alerts.Remove(alert);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteReadAlertsAsync()
        {
            var readAlerts = await _context.Alerts
                .Where(a => a.IsAcknowledged)
                .ToListAsync();

            _context.Alerts.RemoveRange(readAlerts);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetUnreadAlertCountAsync()
        {
            return await _context.Alerts
                .CountAsync(a => !a.IsAcknowledged);
        }

        public async Task ClearAllAlertsAsync()
        {
            var allAlerts = await _context.Alerts.ToListAsync();
            _context.Alerts.RemoveRange(allAlerts);
            await _context.SaveChangesAsync();
        }

        public async Task CreateScanCompletedAlertAsync(int devicesFound)
        {
            var alert = new Alert
            {
                AlertType = AlertType.ScanCompleted,
                Message = $"Сканирование сети завершено. Найдено устройств: {devicesFound}",
                AlertLevel = AlertLevel.Info,
                IsAcknowledged = false
            };

            await CreateAlertAsync(alert);
        }

        public async Task CreateScanFailedAlertAsync(string errorMessage)
        {
            var alert = new Alert
            {
                AlertType = AlertType.ScanFailed,
                Message = $"Ошибка сканирования сети: {errorMessage}",
                AlertLevel = AlertLevel.Critical,
                IsAcknowledged = false
            };

            await CreateAlertAsync(alert);
        }

        public async Task CreateMacAddressChangedAlertAsync(NetworkDevice device, string oldMacAddress)
        {
            if (device == null) throw new ArgumentNullException(nameof(device));

            var alert = new Alert
            {
                DeviceId = device.Id,
                AlertType = AlertType.MacAddressChanged,
                Message = $"MAC-адрес устройства {device.IpAddress} изменился: {oldMacAddress} → {device.MACAddress}",
                AlertLevel = AlertLevel.Warning,
                IsAcknowledged = false
            };

            await CreateAlertAsync(alert);
        }

        public async Task<List<Alert>> GetAlertsByLevelAsync(AlertLevel level, int limit = 50)
        {
            return await _context.Alerts
                .Include(a => a.Device)
                .Where(a => a.AlertLevel == level)
                .OrderByDescending(a => a.CreatedAt)
                .Take(limit)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Alert>> GetRecentAlertsAsync(TimeSpan timeSpan)
        {
            var cutoffDate = DateTime.UtcNow - timeSpan;

            return await _context.Alerts
                .Include(a => a.Device)
                .Where(a => a.CreatedAt >= cutoffDate)
                .OrderByDescending(a => a.CreatedAt)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> HasCriticalAlertsAsync()
        {
            return await _context.Alerts
                .AnyAsync(a => a.AlertLevel == AlertLevel.Critical && !a.IsAcknowledged);
        }
    }
}