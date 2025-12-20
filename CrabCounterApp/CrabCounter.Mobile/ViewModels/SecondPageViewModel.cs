using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrabCounter.Models;
using CrabCounter.SqliteDbContext;

namespace CrabCounter.Mobile.ViewModels
{
    public partial class SecondPageViewModel : ObservableObject
    {
        private readonly AppDbContext _context;

        [ObservableProperty]
        private int crabCount;
        private Counter currentNum;

        public SecondPageViewModel(AppDbContext context)
        {
            _context = context;
            currentNum = context.Crabs.FirstOrDefault();
            crabCount = currentNum?.Count ?? 50;
        }

        [RelayCommand]
        private void Increment()
        {
            if (CrabCount < 100)
            {
                CrabCount++; 
                if(currentNum != null)
                {
                    currentNum.Count = crabCount;
                }
            }
        }

        [RelayCommand]
        private void Decrement()
        {
            if (CrabCount > 0)
            {
                CrabCount--;
                if (currentNum != null)
                {
                    currentNum.Count = crabCount;
                }
            }
        }

        [RelayCommand]
        private async Task Save()
        {
            await _context.SaveChangesAsync();
            await Application.Current.MainPage.DisplayAlertAsync("Успешно", $"Сохранено: {CrabCount} крабиков", "OK");
        }
    }
}
