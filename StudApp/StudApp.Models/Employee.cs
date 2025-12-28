namespace StudApp.Models
{
    public class Employee : BaseEntity
    {
        public Person Person { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public string Role { get; set; }
        public ICollection<Report> CreatedReports { get; set; } = new List<Report>();
        public ICollection<Report> AssignedReports { get; set; } = new List<Report>();
        public int? ShiftId { get; set; }
        public Shift Shift { get; set; }
        public ICollection<Shift> ManagedShifts { get; set; } = new List<Shift>();
    }
}
