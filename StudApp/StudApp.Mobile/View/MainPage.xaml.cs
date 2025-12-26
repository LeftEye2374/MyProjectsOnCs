using StudApp.Mobile.ViewModel;

namespace StudApp.Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel mainPage)
        {
            InitializeComponent();
            BindingContext = mainPage;
        }
    }
}
