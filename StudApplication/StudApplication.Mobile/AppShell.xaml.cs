using StudApplication.Mobile.View;

namespace StudApplication.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage),typeof(MainPage));
            Routing.RegisterRoute(nameof(ViewPage), typeof(ViewPage));
        }
    }
}
