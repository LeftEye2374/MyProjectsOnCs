using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using TodoApp.Services;
using ToDoAPP.Models;
using ToDoAPP.ViewModels;

namespace TodoApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ITodoService _todoService;
        private string _newTodoTitle;
        private string _newTodoDescription;
        private bool _isLoading;
        private bool _isRefreshing;
        private int _completedCount;
        private int _totalCount;

        public ObservableCollection<TodoItem> TodoItems { get; }

        public string NewTodoTitle
        {
            get => _newTodoTitle;
            set => SetProperty(ref _newTodoTitle, value);
        }

        public string NewTodoDescription
        {
            get => _newTodoDescription;
            set => SetProperty(ref _newTodoDescription, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public int CompletedCount
        {
            get => _completedCount;
            set => SetProperty(ref _completedCount, value);
        }

        public int TotalCount
        {
            get => _totalCount;
            set => SetProperty(ref _totalCount, value);
        }

        public ICommand LoadTodosCommand { get; }
        public ICommand AddTodoCommand { get; }
        public ICommand DeleteTodoCommand { get; }
        public ICommand ToggleCompletedCommand { get; }
        public ICommand RefreshCommand { get; }

        public MainViewModel(ITodoService todoService)
        {
            _todoService = todoService ?? throw new ArgumentNullException(nameof(todoService));
            TodoItems = new ObservableCollection<TodoItem>();

            LoadTodosCommand = new Command(async () => await LoadTodosAsync());
            AddTodoCommand = new Command(async () => await AddTodoAsync(), CanAddTodo);
            DeleteTodoCommand = new Command<TodoItem>(async (item) => await DeleteTodoAsync(item));
            ToggleCompletedCommand = new Command<TodoItem>(async (item) => await ToggleCompletedAsync(item));
            RefreshCommand = new Command(async () => await LoadTodosAsync());

            LoadTodosAsync().ConfigureAwait(false);
        }

        private async Task LoadTodosAsync()
        {
            IsLoading = true;
            try
            {
                var todos = await _todoService.GetAllTodosAsync();

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    TodoItems.Clear();
                    foreach (var todo in todos)
                    {
                        TodoItems.Add(todo);
                    }
                });

                await UpdateStatisticsAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка загрузки задач: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
                IsRefreshing = false;
            }
        }
        private async Task AddTodoAsync()
        {
            if (string.IsNullOrWhiteSpace(NewTodoTitle))
                return;

            var newTodo = new TodoItem
            {
                Title = NewTodoTitle.Trim(),
                Description = NewTodoDescription?.Trim() ?? string.Empty,
                IsCompleted = false,
                CreatedAt = DateTime.Now
            };

            try
            {
                await _todoService.AddTodoAsync(newTodo);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    TodoItems.Insert(0, newTodo);
                });

                NewTodoTitle = string.Empty;
                NewTodoDescription = string.Empty;

                await UpdateStatisticsAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка добавления задачи: {ex.Message}");
            }
        }
        private async Task DeleteTodoAsync(TodoItem todo)
        {
            if (todo == null) return;

            try
            {
                await _todoService.DeleteTodoAsync(todo.Id);

                MainThread.BeginInvokeOnMainThread(() =>
                {
                    TodoItems.Remove(todo);
                });

                await UpdateStatisticsAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка удаления задачи: {ex.Message}");
            }
        }

        private async Task ToggleCompletedAsync(TodoItem todo)
        {
            if (todo == null) return;

            try
            {
                todo.IsCompleted = !todo.IsCompleted;
                todo.CompletedAt = todo.IsCompleted ? DateTime.Now : null;

                await _todoService.UpdateTodoAsync(todo);

                await UpdateStatisticsAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка обновления задачи: {ex.Message}");
            }
        }

        private async Task UpdateStatisticsAsync()
        {
            CompletedCount = await _todoService.GetCompletedCountAsync();
            TotalCount = await _todoService.GetTotalCountAsync();
        }

        private bool CanAddTodo()
        {
            return !string.IsNullOrWhiteSpace(NewTodoTitle);
        }
    }
}
