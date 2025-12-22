using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WeatherApp.Mobile.Services;
using WeatherApp.Mobile.Wrappers;

namespace WeatherApp.Mobile.ViewModels
{

    public partial class MainPageViewModel : ObservableObject
    {
        private readonly IWeatherService _weatherService;

        public MainPageViewModel(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [ObservableProperty]
        private string cityName = "Краснодар";

        [ObservableProperty]
        private WeatherDataWrapper currentWeather;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SearchWeatherCommand))]
        [NotifyCanExecuteChangedFor(nameof(RefreshWeatherCommand))]
        private bool isBusy;

        [ObservableProperty]
        private bool isWeatherVisible;

        [RelayCommand(CanExecute = nameof(CanSearch))]
        private async Task SearchWeatherAsync()
        {
            if (string.IsNullOrWhiteSpace(CityName))
                return;

            try
            {
                IsBusy = true;
                IsWeatherVisible = false;

                var weatherResponse = await _weatherService.GetWeatherAsync(CityName);
                CurrentWeather = new WeatherDataWrapper(weatherResponse);

                IsWeatherVisible = true;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка",
                    $"Не удалось загрузить погоду: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand(CanExecute = nameof(CanRefresh))]
        private async Task RefreshWeatherAsync()
        {
            if (CurrentWeather != null)
            {
                await SearchWeatherAsync();
            }
        }

        private bool CanSearch() => !IsBusy;
        private bool CanRefresh() => !IsBusy && CurrentWeather != null;
    }
}