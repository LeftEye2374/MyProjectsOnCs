using WeatherApp.Models.ApiModels;

namespace WeatherApp.Mobile.Services
{
    public interface IWeatherService
    {
        public Task<WeatherResponse> GetWeatherAsync(string city);

    }
}
