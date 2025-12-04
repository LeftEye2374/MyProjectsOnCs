using Microsoft.EntityFrameworkCore;
using NetWatch.Model;
using NetWatch.Model.Entities;
using NetWatch.Model.Interfaces;

namespace NetWatch.DAL.Repository
{
    public class ScanSessionRepository : IScanSessionRepository
    {
        private readonly SqlDbContext _context;

        public ScanSessionRepository(SqlDbContext context)
        {
            _context = context;
        }

        public IQueryable<NetworkScanSession> GetAll()
        {
            return _context.NetworkScanSessions
                .Include(s => s.DeviceScanHistories)
                .ThenInclude(h => h.Device)
                .AsQueryable();
        }

        public async Task<NetworkScanSession?> GetByIdAsync(Guid id)
        {
            return await _context.NetworkScanSessions
                .Include(s => s.DeviceScanHistories)
                .ThenInclude(h => h.Device)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<NetworkScanSession?> GetLastSessionAsync()
        {
            return await _context.NetworkScanSessions
                .OrderByDescending(s => s.StartTime)
                .FirstOrDefaultAsync();
        }

        public async Task<List<NetworkScanSession>> GetSessionsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.NetworkScanSessions
                .Where(s => s.StartTime >= startDate && s.StartTime <= endDate)
                .OrderByDescending(s => s.StartTime)
                .ToListAsync();
        }

        public async Task AddAsync(NetworkScanSession session)
        {
            await _context.NetworkScanSessions.AddAsync(session);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(NetworkScanSession session)
        {
            _context.NetworkScanSessions.Update(session);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var session = await GetByIdAsync(id);
            if (session != null)
            {
                _context.NetworkScanSessions.Remove(session);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ScanSessionStatistics> GetStatisticsAsync()
        {
            var sessions = await _context.NetworkScanSessions.ToListAsync();

            var stats = new ScanSessionStatistics
            {
                TotalSessions = sessions.Count,
                TotalDevicesFound = sessions.Sum(s => s.DevicesFoundCount),
                LastScanDate = sessions.Any() ? sessions.Max(s => s.StartTime) : DateTime.MinValue
            };

            if (sessions.Any())
            {
                stats.AverageDevicesPerSession = sessions.Average(s => s.DevicesFoundCount);

                var completedSessions = sessions.Where(s => s.EndTime.HasValue).ToList();
                if (completedSessions.Any())
                {
                    stats.AverageScanDuration = TimeSpan.FromSeconds(
                        completedSessions.Average(s => (s.EndTime!.Value - s.StartTime).TotalSeconds));
                }
            }

            return stats;
        }

        public async Task<List<DeviceScanHistory>> GetDeviceScanHistoryAsync(Guid deviceId, int limit = 50)
        {
            return await _context.DeviceScanHistories
                .Include(h => h.ScanSession)
                .Where(h => h.DeviceId == deviceId)
                .OrderByDescending(h => h.ScanTimestamp)
                .Take(limit)
                .ToListAsync();
        }

        public async Task AddDeviceScanHistoryAsync(DeviceScanHistory history)
        {
            await _context.DeviceScanHistories.AddAsync(history);
            await _context.SaveChangesAsync();
        }

        public async Task AddDeviceScanHistoryRangeAsync(IEnumerable<DeviceScanHistory> histories)
        {
            await _context.DeviceScanHistories.AddRangeAsync(histories);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.NetworkScanSessions.CountAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}