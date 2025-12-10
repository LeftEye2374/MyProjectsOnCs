using Microsoft.EntityFrameworkCore;
using ToDoAPP.Data;
using ToDoAPP.Models;

namespace TodoApp.Services
{
    public interface ITodoService
    {
        Task<List<TodoItem>> GetAllTodosAsync();
        Task<TodoItem> GetTodoByIdAsync(int id);
        Task AddTodoAsync(TodoItem todo);
        Task UpdateTodoAsync(TodoItem todo);
        Task DeleteTodoAsync(int id);
        Task<int> GetCompletedCountAsync();
        Task<int> GetTotalCountAsync();
    }

    public class TodoService : ITodoService
    {
        private readonly AppDbContext _context;

        public TodoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TodoItem>> GetAllTodosAsync()
        {
            return await _context.TodoItems
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<TodoItem> GetTodoByIdAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task AddTodoAsync(TodoItem todo)
        {
            _context.TodoItems.Add(todo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTodoAsync(TodoItem todo)
        {
            _context.TodoItems.Update(todo);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTodoAsync(int id)
        {
            var todo = await _context.TodoItems.FindAsync(id);
            if (todo != null)
            {
                _context.TodoItems.Remove(todo);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetCompletedCountAsync()
        {
            return await _context.TodoItems.CountAsync(x => x.IsCompleted);
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.TodoItems.CountAsync();
        }
    }
}
