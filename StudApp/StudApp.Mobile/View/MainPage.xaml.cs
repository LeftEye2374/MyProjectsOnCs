using StudApp.Mobile.ViewModel;

namespace StudApp.Mobile.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel model)
        {
            InitializationContext();
            BindingContext = model;
        }
    }
}
