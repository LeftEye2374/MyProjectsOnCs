namespace StudApp.Models
{
    public class Imposter : BaseEntity
    {
        public Person Person { get; set; }
        public ContactInfo ContactInfo { get; set; }

        // One-to-many: у одного Imposter может быть несколько Reports
        public ICollection<Report> CreatedReports { get; set; } = new List<Report>();
    }
}
