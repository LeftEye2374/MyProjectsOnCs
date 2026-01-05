using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudApp.AppDbContext;
using StudApp.Mobile.Services;
using StudApp.Mobile.View;
using StudApp.Mobile.ViewModel;

namespace StudApp.Mobile
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
                .UseMauiCommunityToolkit();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "StudApp.db");
            System.Diagnostics.Debug.WriteLine($"Путь к БД: {dbPath}");

            builder.Services.AddScoped<SqliteDbContext>(sp =>
            {
                var options = new DbContextOptionsBuilder<SqliteDbContext>()
                    .UseSqlite($"Data Source={dbPath}")
                    .Options;
                return new SqliteDbContext(options);
            });

            builder.Services.AddScoped<MainViewModel>();
            builder.Services.AddScoped<ViewViewModel>();
            builder.Services.AddScoped<EmployeesViewModel>();
            builder.Services.AddTransient<AddEmployeePopup>();
            builder.Services.AddScoped<DocumentsPage>();

            builder.Services.AddScoped<MainPage>();
            builder.Services.AddScoped<ViewPage>();
            builder.Services.AddScoped<EmployeesPage>();
            builder.Services.AddTransient<DocumentsViewModel>();

            builder.Services.AddScoped<IShiftService, ShiftService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IDocumentService, DocumentService>();
            builder.Services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();


#if DEBUG
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            using var scope = app.Services.CreateScope();
            var initializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
            initializer.InitializeAsync().GetAwaiter().GetResult();
            return app;
        }
    }
}
