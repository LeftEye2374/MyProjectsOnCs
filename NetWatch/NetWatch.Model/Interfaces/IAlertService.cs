// NetWatch.Model/Interfaces/IAlertService.cs
using NetWatch.Model;
using NetWatch.Model.Entities;
using NetWatch.Model.Enums;

namespace NetWatch.Model.Interfaces
{
    public interface IAlertService
    {
        Task CreateAlertAsync(Alert alert);
        Task CreateNewDeviceAlertAsync(NetworkDevice device);
        Task CreateDeviceStatusChangeAlertAsync(NetworkDevice device, NetStatus previousStatus);
        Task CreateOpenPortAlertAsync(NetworkDevice device, int port);
        Task CreateUnauthorizedDeviceAlertAsync(NetworkDevice device);
        Task<List<Alert>> GetUnreadAlertsAsync();
        Task<List<Alert>> GetAllAlertsAsync(int skip = 0, int take = 50);
        Task<List<Alert>> GetDeviceAlertsAsync(Guid deviceId);
        Task MarkAlertAsReadAsync(Guid alertId);
        Task MarkAllAlertsAsReadAsync();
        Task DeleteAlertAsync(Guid alertId);
        Task DeleteReadAlertsAsync();
        Task<int> GetUnreadAlertCountAsync();
        Task ClearAllAlertsAsync();
        event EventHandler<Alert> AlertCreated;
    }
}