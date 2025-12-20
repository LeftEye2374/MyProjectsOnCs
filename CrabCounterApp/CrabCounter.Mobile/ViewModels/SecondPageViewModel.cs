using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrabCounter.Mobile.Wrappers;
using CrabCounter.Models;
using CrabCounter.SqliteDbContext;

namespace CrabCounter.Mobile.ViewModels
{
    public partial class SecondPageViewModel : ObservableObject
    {
        private readonly AppDbContext _context;

        [ObservableProperty]
        private Counter _counter;

        [ObservableProperty]
        private CounterWrapper _wrapper;

        public SecondPageViewModel(AppDbContext context)
        {
            _context = context;
            if (context.Crabs.Any()) Counter = context.Crabs.FirstOrDefault();
            else Counter = new Counter { Number = 50 };
            Wrapper = new CounterWrapper(Counter);
        }

        [RelayCommand]
        private void Increment()
        {
            if (Wrapper.Number < 100)
            {
                Wrapper.Number++; 
            }
        }

        [RelayCommand]
        private void Decrement()
        {
            if (Wrapper.Number > 0)
            {
                Wrapper.Number--;
            }
        }

        [RelayCommand]
        private async Task Save()
        {
            Counter.Number = Wrapper.Number;
            _context.Crabs.Update(Counter);
            await _context.SaveChangesAsync();
            await Application.Current.MainPage.DisplayAlertAsync("Успешно", $"Сохранено: {Wrapper.Number} крабиков", "OK");
        }
    }
}
