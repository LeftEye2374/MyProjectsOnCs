namespace CRM.Model
{
    public class Product : BaseEntity
    {
        public required string Name { get; set; }
        public required double Price { get; set; }
        public required string Type { get; set; }
        public string? Description { get; set; }
        public virtual required Manufacturer Manufacturer { get; set; }
        public required Guid ManufacturerId { get; set; }
    }
}
