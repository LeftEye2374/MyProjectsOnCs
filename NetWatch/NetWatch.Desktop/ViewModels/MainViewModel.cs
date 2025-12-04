using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NetWatch.Model;
using NetWatch.Model.Entities;
using NetWatch.Model.Enums;
using NetWatch.Model.Interfaces;
using System.Collections.ObjectModel;

namespace NetWatch.Desktop.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly INetworkScanner _networkScanner;
        private readonly IDeviceService _deviceService;
        private readonly IAlertService _alertService;

        [ObservableProperty]
        private string _title = "NetWatch Desktop";

        [ObservableProperty]
        private bool _isScanning;

        [ObservableProperty]
        private ObservableCollection<NetworkDevice> _devices = [];

        [ObservableProperty]
        private NetworkDevice? _selectedDevice;

        [ObservableProperty]
        private string _statusMessage = "Ready";

        [ObservableProperty]
        private int _devicesCount;

        public MainViewModel(
            INetworkScanner networkScanner,
            IDeviceService deviceService,
            IAlertService alertService)
        {
            _networkScanner = networkScanner;
            _deviceService = deviceService;
            _alertService = alertService;

            _networkScanner.DeviceDiscovered += OnDeviceDiscovered;
            _networkScanner.ScanStatusChanged += OnScanStatusChanged;
            _networkScanner.ScanProgressChanged += OnScanProgressChanged;

            _alertService.AlertCreated += OnAlertCreated;

            LoadDevicesFromDatabase();
        }

        [RelayCommand]
        private async Task StartScanAsync()
        {
            if (IsScanning) return;

            IsScanning = true;
            StatusMessage = "Starting network scan...";

            try
            {
                Devices.Clear();
                var foundDevices = await _networkScanner.ScanNetworkAsync("192.168.1.0/24");
                await _deviceService.AddOrUpdateDevicesAsync(foundDevices);
                await _alertService.CreateScanCompletedAlertAsync(foundDevices.Count);
                StatusMessage = $"Scan completed. Found {foundDevices.Count} devices.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Scan failed: {ex.Message}";
                await _alertService.CreateScanFailedAlertAsync(ex.Message);
            }
            finally
            {
                IsScanning = false;
                await LoadDevicesFromDatabase();
            }
        }

        [RelayCommand]
        private async Task RefreshDeviceListAsync()
        {
            StatusMessage = "Refreshing device list...";
            await LoadDevicesFromDatabase();
            StatusMessage = $"Device list refreshed. Total: {Devices.Count}";
        }

        [RelayCommand]
        private void ShowDeviceDetails()
        {
            if (SelectedDevice == null)
            {
                StatusMessage = "No device selected";
                return;
            }

            StatusMessage = $"Selected device: {SelectedDevice.IpAddress} ({SelectedDevice.HostName})";
        }

        [RelayCommand]
        private async Task MarkDeviceAsTrustedAsync()
        {
            if (SelectedDevice == null) return;

            try
            {
                await _deviceService.MarkAsTrustedAsync(SelectedDevice.Id, !SelectedDevice.IsTrusted);
                SelectedDevice.IsTrusted = !SelectedDevice.IsTrusted;

                StatusMessage = SelectedDevice.IsTrusted
                    ? $"Device {SelectedDevice.IpAddress} marked as trusted"
                    : $"Device {SelectedDevice.IpAddress} marked as untrusted";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
            }
        }

        [RelayCommand]
        private async Task DeleteSelectedDeviceAsync()
        {
            if (SelectedDevice == null) return;

            try
            {
                await _deviceService.DeleteDeviceAsync(SelectedDevice.Id);
                Devices.Remove(SelectedDevice);
                SelectedDevice = null;

                StatusMessage = "Device deleted successfully";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error deleting device: {ex.Message}";
            }
        }

        [RelayCommand]
        private async Task PingSelectedDeviceAsync()
        {
            if (SelectedDevice == null) return;

            try
            {
                StatusMessage = $"Pinging {SelectedDevice.IpAddress}...";
                await _deviceService.UpdateDeviceStatusAsync(SelectedDevice.Id, NetStatus.Unknown);
                StatusMessage = $"Ping result for {SelectedDevice.IpAddress}: {SelectedDevice.Status}";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Ping failed: {ex.Message}";
            }
        }

        private async Task LoadDevicesFromDatabase()
        {
            try
            {
                var devices = await _deviceService.GetAllDevicesAsync();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    Devices.Clear();
                    foreach (var device in devices)
                    {
                        Devices.Add(device);
                    }
                    DevicesCount = Devices.Count;
                });
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error loading devices: {ex.Message}";
            }
        }

        private void OnDeviceDiscovered(object? sender, NetworkDevice device)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Devices.Add(device);
                DevicesCount = Devices.Count;
            });
        }

        private void OnScanStatusChanged(object? sender, string status)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                StatusMessage = status;
            });
        }

        private void OnScanProgressChanged(object? sender, double progress)
        {
            // Можно обновлять ProgressBar
        }

        private void OnAlertCreated(object? sender, Alert alert)
        {
            // Обработка новых уведомлений
            // Можно показывать тосты или обновлять счетчик уведомлений
        }

        // Свойство для привязки в UI (например, для ProgressBar)
        public bool IsNotScanning => !IsScanning;
    }
}