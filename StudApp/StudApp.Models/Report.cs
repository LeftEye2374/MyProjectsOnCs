namespace StudApp.Models
{
    internal class Report : BaseEntity
    {
        public Person Person { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public string Description { get; set; }
        // img
    }
}
