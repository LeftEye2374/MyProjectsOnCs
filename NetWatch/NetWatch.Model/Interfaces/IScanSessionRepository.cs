using NetWatch.Model.Entities;

namespace NetWatch.Model.Interfaces
{
    public interface IScanSessionRepository
    {
        IQueryable<NetworkScanSession> GetAll();
        Task<NetworkScanSession?> GetByIdAsync(Guid id);
        Task<NetworkScanSession?> GetLastSessionAsync();
        Task<List<NetworkScanSession>> GetSessionsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task AddAsync(NetworkScanSession session);
        Task UpdateAsync(NetworkScanSession session);
        Task DeleteAsync(Guid id);
        Task<ScanSessionStatistics> GetStatisticsAsync();
        Task<List<DeviceScanHistory>> GetDeviceScanHistoryAsync(Guid deviceId, int limit = 50);
        Task AddDeviceScanHistoryAsync(DeviceScanHistory history);
        Task AddDeviceScanHistoryRangeAsync(IEnumerable<DeviceScanHistory> histories);
        Task<int> CountAsync();
        Task SaveChangesAsync();
    }

    public class ScanSessionStatistics
    {
        public int TotalSessions { get; set; }
        public int TotalDevicesFound { get; set; }
        public double AverageDevicesPerSession { get; set; }
        public TimeSpan AverageScanDuration { get; set; }
        public DateTime LastScanDate { get; set; }
    }
}