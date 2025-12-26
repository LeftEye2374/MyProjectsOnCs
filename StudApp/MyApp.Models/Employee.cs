namespace StudApp.Models
{
    public class Employee : BaseModel
    {
        public string Position { get; set; }
        public DateTime StartWork { get; set; }
        public DateTime EndWork { get; set; }
        public string Shift { get; set; }
    }
}
