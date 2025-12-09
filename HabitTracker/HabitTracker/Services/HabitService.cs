using HabitTracker.Data;
using HabitTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace HabitTracker.Services
{
    public class HabitService
    {
        private readonly AppDbContext _dbContext;

        public HabitService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Habit>> GetHabitsAsync()
        {
            return await _dbContext.Habits
                .Where(H => H.IsActive)
                .OrderByDescending(h => h.CreatedDate)
                .ToListAsync();
        }

        public async Task<Habit> AddHabitAsync(string name, string description, string color)
        {
            var habit = new Habit{
                Name = name,
                Description = description,
                Color = color,
                CreatedDate = DateTime.Now,
                IsActive = true
            };
            _dbContext.Habits.Add(habit);
            await _dbContext.SaveChangesAsync();
            return habit;
        }
        public async Task DeleteHabitAsync(int habitId)
        {
            var habit = await _dbContext.Habits.FindAsync(habitId);
            if (habit != null)
            {
                habit.IsActive = false;
                _dbContext.Habits.Update(habit);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<HabitLog> LogHabitCompletionAsync(int habitId, string notes = "")
        {
            var log = new HabitLog
            {
                HabitId = habitId,
                CompletedDate = DateTime.Now,
                Notes = notes
            };

            _dbContext.HabitLogs.Add(log);
            await _dbContext.SaveChangesAsync();
            return log;
        }

        public async Task<int> GetHabitCompletionCountAsync(int habitId, int days = 7)
        {
            var startDate = DateTime.Now.AddDays(-days);
            return await _dbContext.HabitLogs
                .Where(l => l.HabitId == habitId && l.CompletedDate >= startDate)
                .CountAsync();
        }
    }
}
