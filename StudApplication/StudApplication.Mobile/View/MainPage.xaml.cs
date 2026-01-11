using StudApplication.Mobile.ViewModel;

namespace StudApplication.Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageViewModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}
