namespace StudApp.Models
{
    public class Report : BaseEntity
    {
        public Person PersonInfo { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public string Description { get; set; }
        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public Guid ImposterId { get; set; }
        public Imposter Imposter { get; set; }
    }
}
