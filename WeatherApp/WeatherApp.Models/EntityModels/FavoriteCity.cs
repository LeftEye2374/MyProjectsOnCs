namespace WeatherApp.Models.EntityModels
{
    public class FavoriteCity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
