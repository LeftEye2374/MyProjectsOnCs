using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NetWatch.Model;
using NetWatch.Model.Interfaces;
using System.Collections.ObjectModel;
using System.Net;

namespace NetWatch.Desktop.ViewModels
{
    public partial class ScanViewModel : ObservableObject
    {
        private readonly INetworkScanner _networkScanner;
        private readonly IDeviceService _deviceService;

        [ObservableProperty]
        private string _title = "Network Scan";

        [ObservableProperty]
        private string _ipRange = "192.168.1.0/24";

        [ObservableProperty]
        private bool _isScanning;

        [ObservableProperty]
        private double _scanProgress;

        [ObservableProperty]
        private string _statusMessage = "Ready";

        [ObservableProperty]
        private bool _scanPorts = false;

        [ObservableProperty]
        private bool _resolveHostNames = true;

        [ObservableProperty]
        private int _pingTimeout = 1000;

        [ObservableProperty]
        private ObservableCollection<ScanHistoryItem> _scanHistory = [];

        [ObservableProperty]
        private ObservableCollection<string> _suggestedRanges = [];

        public ScanViewModel(
            INetworkScanner networkScanner,
            IDeviceService deviceService)
        {
            _networkScanner = networkScanner;
            _deviceService = deviceService;

            // Подписка на события сканирования
            _networkScanner.ScanStatusChanged += OnScanStatusChanged;
            _networkScanner.ScanProgressChanged += OnScanProgressChanged;

            // Загрузка истории сканирований
            LoadSuggestedRanges();
        }

        [RelayCommand]
        private async Task StartScanAsync()
        {
            if (IsScanning || string.IsNullOrWhiteSpace(IpRange))
                return;

            IsScanning = true;
            StatusMessage = "Starting scan...";
            ScanProgress = 0;

            try
            {
                // Добавляем диапазон в историю, если его там нет
                if (!SuggestedRanges.Contains(IpRange))
                {
                    SuggestedRanges.Insert(0, IpRange);
                    if (SuggestedRanges.Count > 10)
                        SuggestedRanges.RemoveAt(SuggestedRanges.Count - 1);
                }

                // Запускаем сканирование
                var devices = await _networkScanner.ScanNetworkAsync(IpRange);

                // Сохраняем устройства
                await _deviceService.AddOrUpdateDevicesAsync(devices);

                // Добавляем запись в историю
                ScanHistory.Insert(0, new ScanHistoryItem
                {
                    Timestamp = DateTime.Now,
                    IpRange = IpRange,
                    DevicesFound = devices.Count,
                    Duration = TimeSpan.FromSeconds(2) // Примерное время
                });

                StatusMessage = $"Scan complete. Found {devices.Count} devices.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Scan failed: {ex.Message}";
            }
            finally
            {
                IsScanning = false;
                ScanProgress = 100;
            }
        }

        [RelayCommand]
        private async Task QuickScanAsync()
        {
            IpRange = "192.168.1.0/24";
            await StartScanAsync();
        }

        [RelayCommand]
        private async Task ScanSingleDeviceAsync(string ip)
        {
            IpRange = ip;
            await StartScanAsync();
        }

        [RelayCommand]
        private void StopScan()
        {
            if (IsScanning)
            {
                IsScanning = false;
                StatusMessage = "Scan stopped by user";
            }
        }

        [RelayCommand]
        private void ValidateIpRange()
        {
            if (IsValidIpRange(IpRange))
            {
                StatusMessage = "Valid IP range";
            }
            else
            {
                StatusMessage = "Invalid IP range format";
            }
        }

        [RelayCommand]
        private void UseLocalNetwork()
        {
            try
            {
                // Простая попытка определить локальную сеть
                var hostName = Dns.GetHostName();
                var addresses = Dns.GetHostAddresses(hostName);

                foreach (var address in addresses)
                {
                    if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        var ipParts = address.ToString().Split('.');
                        if (ipParts.Length == 4)
                        {
                            IpRange = $"{ipParts[0]}.{ipParts[1]}.{ipParts[2]}.0/24";
                            StatusMessage = $"Using local network: {IpRange}";
                            return;
                        }
                    }
                }

                StatusMessage = "Could not detect local network";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error detecting network: {ex.Message}";
            }
        }

        [RelayCommand]
        private void ClearHistory()
        {
            ScanHistory.Clear();
            StatusMessage = "Scan history cleared";
        }

        private void LoadSuggestedRanges()
        {
            SuggestedRanges.Clear();

            // Стандартные диапазоны
            SuggestedRanges.Add("192.168.1.0/24");
            SuggestedRanges.Add("192.168.0.0/24");
            SuggestedRanges.Add("10.0.0.0/24");
            SuggestedRanges.Add("172.16.0.0/24");
        }

        private bool IsValidIpRange(string range)
        {
            // Простая проверка CIDR
            if (range.Contains("/"))
            {
                var parts = range.Split('/');
                if (parts.Length == 2 && IPAddress.TryParse(parts[0], out _))
                {
                    if (int.TryParse(parts[1], out int mask))
                    {
                        return mask >= 0 && mask <= 32;
                    }
                }
            }

            // Проверка одиночного IP
            return IPAddress.TryParse(range, out _);
        }

        private void OnScanStatusChanged(object? sender, string status)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                StatusMessage = status;
            });
        }

        private void OnScanProgressChanged(object? sender, double progress)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                ScanProgress = progress;
            });
        }

        public class ScanHistoryItem
        {
            public DateTime Timestamp { get; set; }
            public string IpRange { get; set; } = string.Empty;
            public int DevicesFound { get; set; }
            public TimeSpan Duration { get; set; }

            public string FormattedTime => Timestamp.ToString("HH:mm:ss");
            public string FormattedDate => Timestamp.ToString("dd.MM.yyyy");
        }
    }
}