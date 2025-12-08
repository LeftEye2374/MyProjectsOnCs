namespace HabitTracker.Models
{
    public class Habit
    {
        public int Id { get; set; }
        public strign Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Color { get; set; }
        public bool isActive { get; set; }
        public bool isActive { get; set; }

        public virtual ICollection<HabitLog> HabitLogs { get; set; } = new List<HabitLog>();
    }
}
