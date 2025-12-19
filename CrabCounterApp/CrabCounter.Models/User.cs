namespace CrabCounter.Models
{
    public class User : ModelBase
    {
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
