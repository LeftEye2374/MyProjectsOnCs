using System.Windows;
using System.Windows.Controls;
using NetWatch.Desktop.Service;
using NetWatch.Desktop.ViewModels;
using NetWatch.Model.Entities;

namespace NetWatch.Desktop
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            // Создаем заглушки
            var alertService = new AlertServiceStub();
            var deviceService = new DeviceServiceStub(alertService);
            var networkScanner = new NetworkScannerStub();

            _viewModel = new MainViewModel(networkScanner, deviceService, alertService);

            // Устанавливаем DataContext
            DataContext = _viewModel;

            // Инициализация UI
            UpdateUI();

            // Подписка на события ViewModel
            _viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_viewModel.Devices))
                {
                    UpdateDevicesGrid();
                }
                UpdateUI();
            };

            // Загружаем устройства сразу
            UpdateDevicesGrid();
        }

        private void UpdateUI()
        {
            if (_viewModel == null) return;

            try
            {
                // Обновляем текстовые поля
                StatusText.Text = _viewModel.StatusMessage;
                DevicesCountText.Text = $"Devices: {_viewModel.DevicesCount}";

                // Обновляем состояние кнопок
                StartScanButton.IsEnabled = _viewModel.IsNotScanning;
                RefreshButton.IsEnabled = _viewModel.IsNotScanning;

                bool hasSelectedDevice = _viewModel.SelectedDevice != null;
                ShowDetailsButton.IsEnabled = hasSelectedDevice;
                MarkTrustedButton.IsEnabled = hasSelectedDevice;
                DeleteDeviceButton.IsEnabled = hasSelectedDevice;
                PingDeviceButton.IsEnabled = hasSelectedDevice;

                // Обновляем прогресс-бар
                if (_viewModel.IsScanning)
                {
                    ScanProgressBar.Visibility = Visibility.Visible;
                    ScanningText.Visibility = Visibility.Visible;
                    ScanProgressBar.IsIndeterminate = true;
                }
                else
                {
                    ScanProgressBar.Visibility = Visibility.Collapsed;
                    ScanningText.Visibility = Visibility.Collapsed;
                    ScanProgressBar.IsIndeterminate = false;
                }
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Error: {ex.Message}";
            }
        }

        private void UpdateDevicesGrid()
        {
            if (_viewModel?.Devices == null) return;

            try
            {
                // Устанавливаем источник данных для DataGrid
                DevicesDataGrid.ItemsSource = _viewModel.Devices;
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Error updating grid: {ex.Message}";
            }
        }

        private void DevicesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DevicesDataGrid.SelectedItem is NetworkDevice selectedDevice)
            {
                _viewModel.SelectedDevice = selectedDevice;
                UpdateUI();
            }
        }

        private async void StartScanButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel?.StartScanCommand?.CanExecute(null) == true)
            {
                await _viewModel.StartScanCommand.ExecuteAsync(null);
            }
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel?.RefreshDeviceListCommand?.CanExecute(null) == true)
            {
                await _viewModel.RefreshDeviceListCommand.ExecuteAsync(null);
            }
        }

        private void ShowDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel?.ShowDeviceDetailsCommand?.Execute(null);
        }

        private async void MarkTrustedButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel?.MarkDeviceAsTrustedCommand?.CanExecute(null) == true)
            {
                await _viewModel.MarkDeviceAsTrustedCommand.ExecuteAsync(null);
            }
        }

        private async void DeleteDeviceButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel?.DeleteSelectedDeviceCommand?.CanExecute(null) == true)
            {
                await _viewModel.DeleteSelectedDeviceCommand.ExecuteAsync(null);
            }
        }

        private async void PingDeviceButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel?.PingSelectedDeviceCommand?.CanExecute(null) == true)
            {
                await _viewModel.PingSelectedDeviceCommand.ExecuteAsync(null);
            }
        }
    }
}