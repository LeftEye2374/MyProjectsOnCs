namespace StudApp.Models
{
    public class Shift 
    {
        public int NumOfShift { get; set; }
        public Employee Master { get; set; }
        public ICollection<Employee> Employess { get; set; }
    }
}
