namespace StudApplication.Models
{
    public class Intruder : BaseModel
    {
        public Person PersonInformation { get; set; }
        public ContactInfo ContactInformation { get; set; }
        public int NumberOfReports { get; set; }
    }
}
