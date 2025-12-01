namespace CRM.Model
{
    public class ContactInfo : BaseEntity
    {
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public required string Adress { get; set; }
    }
}
