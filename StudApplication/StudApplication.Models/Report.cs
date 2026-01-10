namespace StudApplication.Models
{
    public class Report : BaseModel
    {
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public Guid IntruderId { get; set; }
        public Intruder Intruder { get; set; }
        public string Description { get; set; }
        public Image Images { get; set; }

    }
}
