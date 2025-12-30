namespace StudApp.Models
{
    public class Shift : BaseEntity
    {
        public int NumOfShift { get; set; }
        public Guid MasterId { get; set; }
        public Employee Master { get; set; }
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();    
    }
}
