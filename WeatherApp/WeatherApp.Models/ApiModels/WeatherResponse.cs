using System.Drawing;

namespace WeatherApp.Models.ApiModels
{
    public class WeatherResponse
    {
        public Coord Coord { get; set; }
        public List<WeatherInfo> Weather { get; set; }
        public MainData Main { get; set; }
        public Wind Wind { get; set; }
        public Clouds Clouds { get; set; }
        public long Dt { get; set; }
        public Sys Sys { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
