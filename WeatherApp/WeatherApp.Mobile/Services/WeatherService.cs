using WeatherApp.Models.ApiModels;

namespace WeatherApp.Mobile.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "";
        private const string BaseUrl = "";

        public Task<WeatherResponse> GetWeatherAsync(string city)
        {
            throw new NotImplementedException();
        }

        public Task<WeatherResponse> GetWeatherByCoordinatesAsync(double lat, double lon)
        {
            throw new NotImplementedException();
        }
    }
}
