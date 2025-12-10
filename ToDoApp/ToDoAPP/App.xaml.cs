using Microsoft.Extensions.DependencyInjection;
using TodoApp.ViewModels;
using TodoApp.Views;
using ToDoAPP;

namespace TodoApp
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            // Получаем MainPage из DI контейнера
            MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            return new Window(new AppShell());
        }
    }
}
