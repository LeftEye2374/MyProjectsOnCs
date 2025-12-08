using System;
using System.Collections.Generic;
using System.Text;

namespace HabitTracker.Models
{
    public class HabitLog
    {
        public int Id { get; set; }
        public int HabitId { get; set; }
        public virtual Habit Habit { get; set; } = null!;
        public DateTime CompletedDate { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
