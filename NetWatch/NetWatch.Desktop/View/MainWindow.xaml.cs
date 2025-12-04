using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NetWatch.Desktop.ViewModels;

namespace NetWatch.Desktop
{
    public partial class MainWindow : Window
    {
        public MainViewModel MainViewModel { get; }
        public DashboardViewModel DashboardViewModel { get; }
        public ScanViewModel ScanViewModel { get; }
        public AlertViewModel AlertViewModel { get; }

        public MainWindow(
            MainViewModel mainViewModel,
            DashboardViewModel dashboardViewModel,
            ScanViewModel scanViewModel,
            AlertViewModel alertViewModel)
        {
            InitializeComponent();

            MainViewModel = mainViewModel;
            DashboardViewModel = dashboardViewModel;
            ScanViewModel = scanViewModel;
            AlertViewModel = alertViewModel;

            DataContext = this;
        }

        private void OnRangeItemClicked(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock textBlock && textBlock.DataContext is string ipRange)
            {
                ScanViewModel.IpRange = ipRange;
                ScanViewModel.ValidateIpRangeCommand.Execute(null);
            }
        }
    }
}