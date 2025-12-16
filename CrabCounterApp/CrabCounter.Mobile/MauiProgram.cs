using Microsoft.Extensions.Logging;
using CrabCounter.SqliteDbContext;
using Microsoft.EntityFrameworkCore;
using CrabCounter.Mobile.ViewModels;
using CrabCounter.Mobile.Views;
using CommunityToolkit.Maui;

namespace CrabCounter.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            }).UseMauiCommunityToolkit();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "crabcounter.db");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite($"Filename={dbPath}"));

            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<SecondPageViewModel>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<SecondPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}
