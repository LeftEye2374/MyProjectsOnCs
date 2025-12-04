using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetWatch.DAL;
using NetWatch.DAL.Repository;
using NetWatch.Desktop.Service;
using NetWatch.Desktop.Services;
using NetWatch.Desktop.ViewModels;
using NetWatch.Model.Interfaces;
using System.Windows;

namespace NetWatch.Desktop
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SqlDbContext>(options =>
                 options.UseSqlServer(@"server=.,5433;Database=AutoLotSamples;User Id=sa;Password=P@ssw0rd; TrustServerCertificate=true"));


            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IScanSessionRepository, ScanSessionRepository>();

            services.AddSingleton<INetworkScanner, NetworkScannerService>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<IAlertService, AlertService>();

            services.AddSingleton<MainViewModel>();
            services.AddSingleton<DeviceListViewModel>();
            services.AddSingleton<ScanViewModel>();
            services.AddSingleton<AlertViewModel>();
            services.AddSingleton<DashboardViewModel>();

            services.AddSingleton<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<SqlDbContext>();
                context.Database.EnsureCreated();
            }

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.DataContext = _serviceProvider.GetRequiredService<MainViewModel>();
            mainWindow.Show();
        }
    }
}

