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
        private readonly AppDbContext _context;

        [ObservableProperty]
        private User? _user;

        [ObservableProperty]
        private UserWrapper? _userWrapper;

        public MainViewModel(AppDbContext context)
        {
            _context = context;

            _userWrapper = new UserWrapper(User);

            _userWrapper.PropertyChanged += OnUserWrapperPropertyChanged;
        }

        [RelayCommand(CanExecute = nameof(CanLogin))]
        private async Task LoginAsync()
        {
            await Shell.Current.GoToAsync("SecondPage");
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
