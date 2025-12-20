using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CrabCounter.Mobile.ViewModels
{
    public partial class SecondPageViewModel : ObservableObject
    {
        private 

        private const string CrabCountKey = "CrabCount";

        [ObservableProperty]
        private int crabCount;  

        public SecondPageViewModel()
        {
            CrabCount = Preferences.Get(CrabCountKey, 0);
        }

        [RelayCommand]
        private void Increment()
        {
            if (CrabCount < 100)
            {
                CrabCount++;  
            }
        }

        [RelayCommand]
        private void Decrement()
        {
            if (CrabCount > 0)
            {
                CrabCount--;  
            }
        }

        [RelayCommand]
        private async Task Save()
        {
            Preferences.Set(CrabCountKey, CrabCount);
            await Application.Current.MainPage.DisplayAlert("Успешно", $"Сохранено: {CrabCount} крабиков", "OK");
        }
    }
}
