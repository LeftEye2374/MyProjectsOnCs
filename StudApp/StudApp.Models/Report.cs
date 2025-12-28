namespace StudApp.Models
{
    public class Report : BaseEntity
    {
        public Person Person { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public string Description { get; set; }

        // Many-to-many: у одного Report может быть несколько Employees
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();

        // Foreign keys для определения создателя отчета
        public int? EmployeeId { get; set; }
        public Employee CreatedByEmployee { get; set; }

        public int? ImposterId { get; set; }
        public Imposter CreatedByImposter { get; set; }

    }
}
