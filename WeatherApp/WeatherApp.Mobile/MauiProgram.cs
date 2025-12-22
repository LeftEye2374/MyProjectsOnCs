using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
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

            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<IWeatherService,WeatherService>();

            builder.Services.AddTransient<MainPageViewModel>();

            builder.Services.AddSingleton<MainPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}