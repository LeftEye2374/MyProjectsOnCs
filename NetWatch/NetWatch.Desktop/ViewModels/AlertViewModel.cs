using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using NetWatch.Model;
using NetWatch.Model.Entities;
using NetWatch.Model.Interfaces;
using System.Collections.ObjectModel;

namespace NetWatch.Desktop.ViewModels
{
    public partial class AlertViewModel : ObservableObject
    {
        private readonly IAlertService _alertService;

        [ObservableProperty]
        private ObservableCollection<Alert> _alerts = [];

        [ObservableProperty]
        private Alert? _selectedAlert;

        [ObservableProperty]
        private string _searchText = "";

        [ObservableProperty]
        private bool _showOnlyUnread = true;

        [ObservableProperty]
        private int _totalAlerts;

        [ObservableProperty]
        private int _unreadAlerts;

        [ObservableProperty]
        private bool _isLoading;

        public AlertViewModel(IAlertService alertService)
        {
            _alertService = alertService;

            // Подписка на новые уведомления
            _alertService.AlertCreated += OnAlertCreated;

            // Загрузка уведомлений
            LoadAlertsCommand.ExecuteAsync(null);
        }

        [RelayCommand]
        private async Task LoadAlertsAsync()
        {
            if (IsLoading) return;

            IsLoading = true;

            try
            {
                var allAlerts = await _alertService.GetAllAlertsAsync(0, 100);

                // Применяем фильтры
                var filteredAlerts = allAlerts
                    .Where(a => !ShowOnlyUnread || !a.IsAcknowledged)
                    .Where(a => string.IsNullOrEmpty(SearchText) ||
                           a.Message.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(a => a.CreatedAt)
                    .ToList();

                Alerts.Clear();
                foreach (var alert in filteredAlerts)
                {
                    Alerts.Add(alert);
                }

                TotalAlerts = allAlerts.Count;
                UnreadAlerts = allAlerts.Count(a => !a.IsAcknowledged);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Load alerts error: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task MarkAsReadAsync()
        {
            if (SelectedAlert == null) return;

            try
            {
                await _alertService.MarkAlertAsReadAsync(SelectedAlert.Id);
                SelectedAlert.IsAcknowledged = true;

                // Обновляем статистику
                UnreadAlerts = Math.Max(0, UnreadAlerts - 1);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Mark as read error: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task MarkAllAsReadAsync()
        {
            try
            {
                await _alertService.MarkAllAlertsAsReadAsync();
                await LoadAlertsAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Mark all as read error: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task DeleteAlertAsync()
        {
            if (SelectedAlert == null) return;

            try
            {
                await _alertService.DeleteAlertAsync(SelectedAlert.Id);
                Alerts.Remove(SelectedAlert);
                SelectedAlert = null;

                TotalAlerts = Math.Max(0, TotalAlerts - 1);
                if (!SelectedAlert?.IsAcknowledged ?? false)
                {
                    UnreadAlerts = Math.Max(0, UnreadAlerts - 1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Delete alert error: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task ClearAllAlertsAsync()
        {
            try
            {
                await _alertService.ClearAllAlertsAsync();
                Alerts.Clear();
                TotalAlerts = 0;
                UnreadAlerts = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Clear all alerts error: {ex.Message}");
            }
        }

        [RelayCommand]
        private async Task FilterAlertsAsync()
        {
            await LoadAlertsAsync();
        }

        private void OnAlertCreated(object? sender, Alert alert)
        {
            // Добавляем новое уведомление в коллекцию
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                Alerts.Insert(0, alert);
                TotalAlerts++;

                if (!alert.IsAcknowledged)
                {
                    UnreadAlerts++;
                }
            });
        }
    }
}