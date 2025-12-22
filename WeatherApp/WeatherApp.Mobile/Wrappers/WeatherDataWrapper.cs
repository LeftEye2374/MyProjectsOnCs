using CommunityToolkit.Mvvm.ComponentModel;
using WeatherApp.Models.ApiModels;

namespace WeatherApp.Mobile.Wrappers
{
    public class WeatherDataWrapper : ObservableObject
    {
        private WeatherResponse _weather;
        public string CityName => _weather?.Name ?? "-"; 

        public WeatherDataWrapper(WeatherResponse weather) 
        {
            _weather = weather;
        }

        public string Temperature => _weather?.Main != null
        ? $"{_weather.Main.Temp:F1}°C"
        : "—";

        public string FeelsLike => _weather?.Main != null
            ? $"Ощущается как {_weather.Main.FeelsLike:F1}°C"
            : "";

        public string Description => _weather?.Weather?.FirstOrDefault()?.Description ?? "—";

        public string IconUrl
        {
            get
            {
                var icon = _weather?.Weather?.FirstOrDefault()?.Icon;
                return icon != null
                    ? $"https://openweathermap.org/img/wn/{icon}@2x.png"
                    : "";
            }
        }


        public string Humidity => _weather?.Main != null
            ? $"{_weather.Main.Humidity}%"
            : "—";

        public string WindSpeed => _weather?.Wind != null
            ? $"{_weather.Wind.Speed} м/с"
            : "—";

        public string Pressure => _weather?.Main != null
            ? $"{_weather.Main.Pressure} гПа"
            : "—";

        public string TempMin => _weather?.Main != null
            ? $"{_weather.Main.TempMin:F1}°"
            : "—";

        public string TempMax => _weather?.Main != null
            ? $"{_weather.Main.TempMax:F1}°"
            : "—";

        public string Country => _weather?.Sys?.Country ?? "";
    }
}
