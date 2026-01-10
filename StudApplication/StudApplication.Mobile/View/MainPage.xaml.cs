using StudApplication.Mobile.ViewModel;

namespace StudApplication.Mobile.View
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
