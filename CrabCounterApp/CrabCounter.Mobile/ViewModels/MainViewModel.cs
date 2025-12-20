using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CrabCounter.Mobile.Wrappers;
using CrabCounter.Models;
using CrabCounter.SqliteDbContext;
using System.ComponentModel;

namespace CrabCounter.Mobile.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {

        [ObservableProperty]
        private UserWrapper? _userWrapper;

        public MainViewModel(AppDbContext context)
        {
            UserWrapper = new UserWrapper(new User());
        }

        [RelayCommand(CanExecute = nameof(CanLogin))]
        private async Task LoginAsync()
        {
            await Shell.Current.GoToAsync("SecondPage");
        }

        partial void OnUserWrapperChanged(UserWrapper? oldValue, UserWrapper? newValue)
        {
            if (oldValue != null)
            {
                oldValue.PropertyChanged -= OnUserWrapperPropertyChanged;
            }

            if (newValue != null)
            {
                newValue.PropertyChanged += OnUserWrapperPropertyChanged;
            }

            LoginCommand.NotifyCanExecuteChanged();
        }

        private void OnUserWrapperPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UserWrapper.Username) ||
                e.PropertyName == nameof(UserWrapper.Password))
            {
                LoginCommand.NotifyCanExecuteChanged();
            }
        }

        private bool CanLogin()
        {
            return UserWrapper is not null &&
                !string.IsNullOrWhiteSpace(UserWrapper.Username) &&
                !string.IsNullOrWhiteSpace(UserWrapper.Password);
        }
    }
}
