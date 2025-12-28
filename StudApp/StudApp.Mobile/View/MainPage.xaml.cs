using Microsoft.Extensions.Options;
using StudApp.Mobile.ViewModel;

namespace StudApp.Mobile
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
