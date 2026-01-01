using StudApp.Mobile.View;

namespace StudApp.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(ViewPage), typeof(ViewPage));
        }
    }
}
