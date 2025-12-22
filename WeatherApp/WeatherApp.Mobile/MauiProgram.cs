using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using AppDbContext;
using Microsoft.EntityFrameworkCore;
using WeatherApp.Mobile.Services;
using WeatherApp.Mobile.ViewModels;
using WeatherApp.Mobile.Views;

namespace WeatherApp.Mobile
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

            string DbPath = Path.Combine(FileSystem.AppDataDirectory, "WeatherApp.db");
            builder.Services.AddDbContext<SqliteDbContexrt>(options => options.UseSqlite($"Data Source = {DbPath}"));

            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<IWeatherService,WeatherService>();

            builder.Services.AddTransient<MainPageViewModel>();

            builder.Services.AddSingleton<MainPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<SqliteDbContexrt>();
                db.Database.EnsureCreated();
            }


                return app;
        }
    }
}