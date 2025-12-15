using CommunityToolkit.Maui;
using CrabCounterApp.Core.Services;
using CrabCounterApp.DAL;
using CrabCounterApp.Mobile.Service;
using CrabCounterApp.Mobile.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace CrabCounterApp.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=crabs.db"));
        builder.Services.AddSingleton<ICrabRepository, CrabRepository>();
        builder.Services.AddSingleton<ICrabService, CrabService>();
        builder.Services.AddTransient<INavigationService, MauiNavigationService>();
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<MainViewModel>();

        builder.Services.AddSingleton<IHostedService>(provider =>
        {
            var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
            return new DbInitializer(scopeFactory);
        });

        return builder.Build();
    }
}

public class DbInitializer : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public DbInitializer(IServiceScopeFactory scopeFactory) => _scopeFactory = scopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
    }
}
