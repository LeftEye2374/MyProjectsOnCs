namespace StudApp.Models
{
    internal class Shift 
    {
        public int NumOfShift { get; set; }
        public Employee Master { get; set; }
        public ICollection<Employee> Employess { get; set; }
    }
}
