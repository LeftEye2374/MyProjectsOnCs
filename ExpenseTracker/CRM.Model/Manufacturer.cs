namespace CRM.Model
{
    public class Manufacturer : BaseEntity
    {
        public required string Name { get; set; }
        public required ContactInfo ContactInfo { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
