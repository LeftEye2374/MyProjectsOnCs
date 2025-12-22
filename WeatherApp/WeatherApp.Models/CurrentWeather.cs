using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Models
{
    public class CurrentWeather : BaseModel
    {
        [Required]
        public int CityId { get; set; }
        public City City { get; set; }
        public DateTime ObservationTime { get; set; }  
        public double Temperature { get; set; }        
        public double FeelsLike { get; set; }          
        public int Humidity { get; set; }              
        public double WindSpeed { get; set; }          
        public int Cloudiness { get; set; }            

        [MaxLength(20)]
        public string Condition { get; set; }          

        [MaxLength(20)]
        public string PrecipitationType { get; set; } 
    }
}
