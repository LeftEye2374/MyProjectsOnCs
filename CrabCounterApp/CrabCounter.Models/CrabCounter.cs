namespace CrabCounter.Models
{
    public class CrabCounter
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
