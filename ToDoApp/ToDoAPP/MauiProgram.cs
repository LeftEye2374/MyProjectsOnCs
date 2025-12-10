using Microsoft.EntityFrameworkCore;
using TodoApp.Services;
using TodoApp.ViewModels;
using TodoApp.Views;
using ToDoAPP.Data;

namespace TodoApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureServices();

            return builder.Build();
        }

        private static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
        {
            // Регистрация DbContext
            var connectionString = "Data Source=app.db;";
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(connectionString)
            );

            // Регистрация сервисов
            builder.Services.AddSingleton<ITodoService, TodoService>();

            // Регистрация ViewModels
            builder.Services.AddSingleton<MainViewModel>();

            // Регистрация Views
            builder.Services.AddSingleton<MainPage>();

            // Регистрация App
            builder.Services.AddSingleton<App>();

            return builder;
        }
    }
}
