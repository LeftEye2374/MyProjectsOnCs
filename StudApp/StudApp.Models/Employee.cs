namespace StudApp.Models
{
    public class Employee : BaseEntity
    {
        public Person PersonInfo { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public string Role { get; set; }
        public ICollection<Report> Reports { get; set; } = new List<Report>();
        public Guid ShiftId { get; set; }
        public Shift Shift { get; set; }
    }
}
