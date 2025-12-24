namespace EmployeeApp.Models
{
    public class User : BaseModel
    {
        public Guid Id { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public DateTime CreationAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string password { get; set; }
        public string login {  get; set; }
        public string Role { get; set; }
    }
}
