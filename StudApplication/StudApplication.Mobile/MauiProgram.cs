using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

            builder.Services.AddSingleton<MainPageViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}