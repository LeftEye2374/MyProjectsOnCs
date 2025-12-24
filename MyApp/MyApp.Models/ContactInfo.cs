namespace EmployeeApp.Models
{
    public class ContactInfo
    {
        public required string FirstName { get; set;  }
        public required string LastName {  get; set; }
        public string email { get; set; }
        public string Phone { get; set; }
        public required uint Age {  get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string? NumberOfBuild {  get; set; }
        public string PostalCode { get; set; }
    }
}
