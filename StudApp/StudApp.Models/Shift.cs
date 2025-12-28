namespace StudApp.Models
{
    public class Shift 
    {
        public int NumOfShift { get; set; }
        public int MasterId { get; set; }
        public Employee Master { get; set; }
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
