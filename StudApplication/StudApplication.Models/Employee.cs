namespace StudApplication.Models
{
    public class Employee : BaseModel
    {
        public Person PersonInformation { get; set; }
        public ContactInfo ContactInformation { get; set; }
        public Autorization Autorization { get; set; }
        public int Shift { get; set; }        
        public int NumberOfReports { get; set; }
        public ICollection<Report> Reports { get; set; } = new List<Report>();

    }
}
