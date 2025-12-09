namespace HabitTracker.Models
{
    public class Habit
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Color { get; set; } = "#007AFF";
        public bool IsActive { get; set; } = true;

        public virtual ICollection<HabitLog> HabitLogs { get; set; } = new List<HabitLog>();
    }
}
