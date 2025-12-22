using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Models
{
    public class City : BaseModel
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }               // "Москва"

        [MaxLength(100)]
        public string Country { get; set; }            // "Россия"

        [MaxLength(100)]
        public string Region { get; set; }             // "Московская область"

        [Required]
        public double Latitude { get; set; }           // 55.7558

        [Required]
        public double Longitude { get; set; }          // 37.6173

        public int OrderIndex { get; set; }            // Порядок в списке (для сортировки)

        public bool IsDefault { get; set; }            // Город по умолчанию (текущая геопозиция)

        public bool IsFavorite { get; set; }           // Избранный город

        public DateTime LastUpdated { get; set; }      // Когда обновлялись данные

        public TimeSpan TimeZoneOffset { get; set; }   // Смещение часового пояса UTC

        // Навигационные свойства
        public List<WeatherDataEntity> WeatherHistory { get; set; } = new();
        public CurrentWeatherEntity CurrentWeather { get; set; }
    }
}
