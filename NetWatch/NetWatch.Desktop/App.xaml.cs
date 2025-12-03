using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetWatch.DAL;
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

            services.AddSingleton<ViewModels.MainViewModel>();
            services.AddSingleton<ViewModels.DeviceListViewModel>();
            services.AddSingleton<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            mainWindow.DataContext = _serviceProvider.GetService<ViewModels.MainViewModel>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}