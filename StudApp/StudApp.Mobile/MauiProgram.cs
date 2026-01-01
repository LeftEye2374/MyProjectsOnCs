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
                }).UseMauiCommunityToolkit();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "StudApp.db");
            builder.Services.AddDbContext<SqliteDbContext>(options =>
                options.UseSqlite($"Filename={dbPath}"));

            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<ViewViewModel>();
            builder.Services.AddSingleton<EmployeesViewModel>();
            builder.Services.AddSingleton<AddEmployeePopupPage>();
            
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<ViewPage>();
            builder.Services.AddSingleton<EmployeesPage>();

            builder.Services.AddSingleton<IShiftService, ShiftService>();
            builder.Services.AddSingleton<IEmployeeService, EmployeeService>();
            builder.UseMauiCommunityToolkit();


#if DEBUG
            builder.Logging.AddDebug();
#endif
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<SqliteDbContext>();
                //db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
            return app;
        }
    }
}
