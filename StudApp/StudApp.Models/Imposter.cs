namespace StudApp.Models
{
    public class Imposter : BaseEntity
    {
        public Person Person { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}
