using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NetWatch.Model;
using System.Collections.ObjectModel;

namespace NetWatch.Desktop.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
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

        public bool IsNotScanning => !IsScanning;

        [RelayCommand]
        private async Task StartScanAsync()
        {
            if (IsScanning) return;

            IsScanning = true;
            StatusMessage = "Scanning network...";

            try
            {
                // TODO: Реализовать настоящее сканирование сети
                await Task.Delay(2000); // Имитация сканирования

                // TODO: Добавить найденные устройства в коллекцию Devices
                StatusMessage = "Scan completed. Found 0 devices.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Scan error: {ex.Message}";
            }
            finally
            {
                IsScanning = false;
            }
        }

        [RelayCommand]
        private void RefreshDeviceList()
        {
            StatusMessage = "Refreshing device list...";
            // TODO: Загрузить устройства из базы данных
            StatusMessage = "Device list refreshed.";
        }

        [RelayCommand]
        private void ShowDeviceDetails()
        {
            if (SelectedDevice == null)
            {
                StatusMessage = "No device selected";
                return;
            }

            StatusMessage = $"Selected device: {SelectedDevice.IpAddress}";
        }

        [RelayCommand]
        private void MarkDeviceAsTrusted()
        {
            if (SelectedDevice == null) return;

            SelectedDevice.IsTrusted = !SelectedDevice.IsTrusted;
            StatusMessage = SelectedDevice.IsTrusted
                ? $"Device {SelectedDevice.IpAddress} marked as trusted"
                : $"Device {SelectedDevice.IpAddress} marked as untrusted";
        }

        [RelayCommand]
        private void DeleteSelectedDevice()
        {
            if (SelectedDevice == null) return;

            Devices.Remove(SelectedDevice);
            StatusMessage = $"Device {SelectedDevice.IpAddress} removed";
            SelectedDevice = null;
        }

        // Метод для загрузки устройств из базы данных (будет вызываться извне)
        public async Task LoadDevicesFromDatabase()
        {
            // TODO: Реализовать загрузку из БД
            StatusMessage = "Loading devices from database...";
            await Task.Delay(500); // Имитация загрузки
            StatusMessage = "Devices loaded from database.";
        }
    }
}