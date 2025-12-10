using ToDoAPP.Models;

namespace ToDoAPP.Services
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
}
