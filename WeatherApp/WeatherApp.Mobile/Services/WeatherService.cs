using System.Net.Http.Json;
using WeatherApp.Models.ApiModels;

namespace WeatherApp.Mobile.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "4e4bd609f8c87462328403e3d7a516b0";
        private const string BaseUrl = "https://api.openweathermap.org/data/2.5";

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherResponse> GetWeatherAsync(string city)
        {
            var url = $"{BaseUrl}/weather?q={city}&appid={ApiKey}&units=metric&lang=ru";
            return await _httpClient.GetFromJsonAsync<WeatherResponse>(url);
        }
    }
}
