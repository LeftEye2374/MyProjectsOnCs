namespace Crabs.Models
{
    public class Autorization
    {
        public Guid Id { get; set; }         
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
