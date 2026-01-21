using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudApplication.Mobile.Service;
using StudApplication.Mobile.View;
using StudApplication.Mobile.ViewModel;
using StudApplications.AppDbContext;

namespace StudApplication.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "StudApp.db");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Filename={dbPath}"));

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<ViewPage>();
            builder.Services.AddSingleton<EmployeePage>();

            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<ViewPageViewModel>();
            builder.Services.AddSingleton<EmployeePageViewModel>();

            builder.Services.AddSingleton<ICurrentShiftService, CurrentShiftService>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                //db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
            return app;
        }
    }
}