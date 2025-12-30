namespace StudApp.Models
{
    public class Imposter : BaseEntity
    {
        public Person PersonInfo { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public ICollection<Report> Reports { get; set; } = new List<Report>();
    }
}
