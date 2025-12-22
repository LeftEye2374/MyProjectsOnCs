namespace WeatherApp.Models.EntityModels
{
    public class WeatherCache
    {
        public string CityId { get; set; }  // Используйте CityId из API
        public string JsonData { get; set; }
        public DateTime CachedAt { get; set; }
    }
}
