using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using NetWatch.Model;
using NetWatch.Model.Entities;
using NetWatch.Model.Enums;
using NetWatch.Model.Interfaces;
using System.Collections.ObjectModel;

namespace NetWatch.Desktop.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly IDeviceService _deviceService;
        private readonly IAlertService _alertService;
        private readonly IScanSessionRepository _scanSessionRepository;

        [ObservableProperty]
        private string _title = "Dashboard";

        [ObservableProperty]
        private int _totalDevices;

        [ObservableProperty]
        private int _onlineDevices;

        [ObservableProperty]
        private int _offlineDevices;

        [ObservableProperty]
        private int _trustedDevices;

        [ObservableProperty]
        private int _untrustedDevices;

        [ObservableProperty]
        private int _totalAlerts;

        [ObservableProperty]
        private int _unreadAlerts;

        [ObservableProperty]
        private int _criticalAlerts;

        [ObservableProperty]
        private int _scanSessionsCount;

        [ObservableProperty]
        private DateTime _lastScanTime;

        [ObservableProperty]
        private string _lastScanRange = "N/A";

        [ObservableProperty]
        private ObservableCollection<DeviceTypeStat> _deviceTypeStats = [];

        [ObservableProperty]
        private ObservableCollection<Alert> _recentAlerts = [];

        [ObservableProperty]
        private bool _isLoading;

        public DashboardViewModel(
            IDeviceService deviceService,
            IAlertService alertService,
            IScanSessionRepository scanSessionRepository)
        {
            _deviceService = deviceService;
            _alertService = alertService;
            _scanSessionRepository = scanSessionRepository;

            // Асинхронная загрузка данных при создании
            LoadDashboardDataCommand.ExecuteAsync(null);
        }

        [RelayCommand]
        private async Task LoadDashboardDataAsync()
        {
            if (IsLoading) return;

            IsLoading = true;

            try
            {
                await Task.WhenAll(
                    LoadDeviceStatisticsAsync(),
                    LoadAlertStatisticsAsync(),
                    LoadScanStatisticsAsync(),
                    LoadDeviceTypeStatsAsync(),
                    LoadRecentAlertsAsync()
                );
            }
            catch (Exception ex)
            {
                // Логирование
                Console.WriteLine($"Dashboard load error: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task LoadDeviceStatisticsAsync()
        {
            try
            {
                var devices = await _deviceService.GetAllDevicesAsync();
                TotalDevices = devices.Count;

                OnlineDevices = devices.Count(d => d.Status == NetStatus.Online);
                OfflineDevices = devices.Count(d => d.Status == NetStatus.Offline);
                TrustedDevices = devices.Count(d => d.IsTrusted);
                UntrustedDevices = devices.Count(d => !d.IsTrusted);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Device stats error: {ex.Message}");
            }
        }

        private async Task LoadAlertStatisticsAsync()
        {
            try
            {
                var alerts = await _alertService.GetAllAlertsAsync(0, 100);
                TotalAlerts = alerts.Count;

                UnreadAlerts = alerts.Count(a => !a.IsAcknowledged);
                CriticalAlerts = alerts.Count(a => a.AlertLevel == AlertLevel.Critical);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Alert stats error: {ex.Message}");
            }
        }

        private async Task LoadScanStatisticsAsync()
        {
            try
            {
                var lastSession = await _scanSessionRepository.GetLastSessionAsync();

                if (lastSession != null)
                {
                    LastScanTime = lastSession.StartTime;
                    LastScanRange = lastSession.IPRangeScanned;
                }

                var sessions = await _scanSessionRepository.GetAll().ToListAsync();
                ScanSessionsCount = sessions.Count;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Scan stats error: {ex.Message}");
            }
        }

        private async Task LoadDeviceTypeStatsAsync()
        {
            try
            {
                DeviceTypeStats.Clear();

                var devices = await _deviceService.GetAllDevicesAsync();

                var stats = devices
                    .GroupBy(d => d.DeviceType)
                    .Select(g => new DeviceTypeStat
                    {
                        DeviceType = g.Key,
                        Count = g.Count(),
                        Percentage = devices.Count > 0 ? (double)g.Count() / devices.Count * 100 : 0
                    })
                    .OrderByDescending(s => s.Count)
                    .ToList();

                foreach (var stat in stats)
                {
                    DeviceTypeStats.Add(stat);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Device type stats error: {ex.Message}");
            }
        }

        private async Task LoadRecentAlertsAsync()
        {
            try
            {
                RecentAlerts.Clear();

                var alerts = await _alertService.GetAllAlertsAsync(0, 10);

                foreach (var alert in alerts)
                {
                    RecentAlerts.Add(alert);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Recent alerts error: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task AcknowledgeAllAlertsAsync()
        {
            try
            {
                await _alertService.MarkAllAlertsAsReadAsync();
                await LoadAlertStatisticsAsync();
                await LoadRecentAlertsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Acknowledge all error: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task RefreshDashboardAsync()
        {
            await LoadDashboardDataAsync();
        }

        [RelayCommand]
        private void NavigateToAlerts()
        {
            // Навигация к уведомлениям
            // Реализовать через NavigationService или Messenger
        }

        [RelayCommand]
        private void NavigateToDevices()
        {
            // Навигация к устройствам
        }

        public class DeviceTypeStat
        {
            public DeviceType DeviceType { get; set; }
            public int Count { get; set; }
            public double Percentage { get; set; }
            public string FormattedPercentage => $"{Percentage:F1}%";
        }
    }
}