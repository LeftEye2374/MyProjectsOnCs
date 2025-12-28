namespace StudApp.Models
{
    public class Employee : BaseEntity
    {
        public Person Person { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public string Role { get; set; }

        // One-to-many: у одного Employee может быть много Reports (созданных им)
        public ICollection<Report> CreatedReports { get; set; } = new List<Report>();

        // Many-to-many: отчеты, в которых участвует Employee
        public ICollection<Report> AssignedReports { get; set; } = new List<Report>();

        // Many-to-one: Employee принадлежит одной Shift
        public int? ShiftId { get; set; }
        public Shift Shift { get; set; }

        // One-to-many: смены, где Employee является мастером
        public ICollection<Shift> ManagedShifts { get; set; } = new List<Shift>();
    }
}
