using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudApp.Mobile.Wrappers;
using StudApp.Models;
using System.ComponentModel;

namespace StudApp.Mobile.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private EmployeeWrapper? _employeeWrapper;

        public MainViewModel()
        {
            _employeeWrapper = new EmployeeWrapper(new Employee { PersonInfo = new Person() });

            if (_employeeWrapper != null)
            {
                _employeeWrapper.PropertyChanged += OnEmployeeWrapperPropertyChanged;
            }
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
            if (e.PropertyName == nameof(EmployeeWrapper.FirstName) ||
                e.PropertyName == nameof(EmployeeWrapper.Password))
            {
                LoginCommand.NotifyCanExecuteChanged();
            }
        }

        private bool CanLogin()
        {
            return EmployeeWrapper is not null &&
                !string.IsNullOrWhiteSpace(EmployeeWrapper.FirstName) &&
                !string.IsNullOrWhiteSpace(EmployeeWrapper.Password);
        }
    }
}
