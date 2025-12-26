namespace StudApp.Models
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public int Dormitory { get; set; }
        public int Room {  get; set; }
    }
}
