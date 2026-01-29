using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudApplication.Mobile.Service;
using StudApplication.Mobile.Wrapper;
using System.Collections.ObjectModel;

namespace StudApplication.Mobile.ViewModel
{
    public partial class EmployeePageViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<EmployeeWrapper> employees = new();

        private readonly IEmployeeService _employeeService;

        public EmployeePageViewModel(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("ViewPage");
        }

        [RelayCommand]
        private async Task AddEmployee()
        {
            await Shell.Current.GoToAsync("AddEmployeePage");
        }
    }
}
