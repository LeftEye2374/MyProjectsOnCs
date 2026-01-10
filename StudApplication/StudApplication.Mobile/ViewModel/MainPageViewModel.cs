using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudApplication.Mobile.Wrappers;
using StudApplication.Models;
using System.ComponentModel;

namespace StudApplication.Mobile.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        public EmployeeWrapper? _employeeWrapper;

        public MainPageViewModel()
        {
            EmployeeWrapper = new EmployeeWrapper(new Employee());
        }

        [RelayCommand(CanExecute = nameof(CanLogin))]
        private async Task LoginAsync() 
        {
            await Shell.Current.GoToAsync("ViewPage");
        }

        partial void OnEmployeeWrapperChanged(EmployeeWrapper? oldValue, EmployeeWrapper? newValue)
        {
            if (oldValue != null)
            {
                oldValue.PropertyChanged -= OnEmployeeWrapperPropertyChanged;
            }

            if (newValue != null)
            {
                newValue.PropertyChanged += OnEmployeeWrapperPropertyChanged;
            }

            LoginCommand.NotifyCanExecuteChanged();
        }

        private void OnEmployeeWrapperPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(EmployeeWrapper.Login) ||
                e.PropertyName == nameof(EmployeeWrapper.Password))
            {
                LoginCommand.NotifyCanExecuteChanged();
            }
        }

        private bool CanLogin()
        {
            return EmployeeWrapper is not null &&
                !string.IsNullOrWhiteSpace(EmployeeWrapper.Login) &&
                !string.IsNullOrWhiteSpace(EmployeeWrapper.Password);
        }
    }
}
