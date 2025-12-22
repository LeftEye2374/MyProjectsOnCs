using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Models
{
    public class DailyForecast : BaseModel
    {
        public int CityId { get; set; }
        public City City { get; set; }
        public DateTime Date { get; set; }             
        public double MinTemperature { get; set; }     
        public double MaxTemperature { get; set; }    
        public double DayTemperature { get; set; }     
        public double NightTemperature { get; set; }   
        public int Humidity { get; set; }
        public double WindSpeed { get; set; }
        public double PrecipitationProbability { get; set; }  
        public double? PrecipitationAmount { get; set; }      
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }
        public DateTime Moonrise { get; set; }
        public DateTime Moonset { get; set; }
        [MaxLength(20)]
        public string MoonPhase { get; set; }
    }
}
