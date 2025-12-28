namespace StudApp.Models
{
    internal class Employee : BaseEntity
    {
        public Person Person { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public String Role { get; set; }
        public ICollection<Report> Reports { get; set; }
    }
}
