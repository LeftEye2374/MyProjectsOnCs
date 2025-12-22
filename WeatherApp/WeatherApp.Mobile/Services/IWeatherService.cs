using WeatherApp.Models.ApiModels;

namespace WeatherApp.Mobile.Services
{
    public interface IWeatherService
    {
        public Task<WeatherResponse> GetWeatherAsync(string city);
        public Task<WeatherResponse> GetWeatherByCoordinatesAsync(double lat, double lon);

    }
}
