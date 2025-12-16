using CrabCounter.Mobile.Views;
using Microsoft.Extensions.DependencyInjection;

namespace CrabCounter.Mobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Routing.RegisterRoute("SecondPage", typeof(SecondPage));
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}