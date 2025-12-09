using HabitTracker.ViewModels;

namespace HabitTracker.Views;

public partial class HabitListPage : ContentPage
{
    public HabitListPage()
    {
        InitializeComponent();
        BindingContext = new HabitListViewModel();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is HabitListViewModel vm)
        {
            // Выполняем асинхронную операцию без await в обычном методе
            MainThread.BeginInvokeOnMainThread(async () =>
            {
                await vm.LoadHabitsCommand.Execute(null);
            });
        }
    }
}
