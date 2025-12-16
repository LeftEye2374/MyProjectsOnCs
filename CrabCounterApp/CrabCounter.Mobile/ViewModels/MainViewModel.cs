using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CrabCounter.Mobile.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        public string username;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string userPas;


        [RelayCommand(CanExecute = nameof(CanLogin))]
        private async Task Login()
        {
            await Shell.Current.GoToAsync("SecondPage");
        }

        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(userPas);
        }
    }
}
