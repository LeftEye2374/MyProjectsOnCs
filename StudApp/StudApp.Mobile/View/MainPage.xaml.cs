using StudApp.Mobile.ViewModel;

namespace StudApp.Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}
