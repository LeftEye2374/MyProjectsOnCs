using WeatherApp.Mobile.ViewModel;

namespace WeatherApp.Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
