using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudApplication.Mobile.Service;
using StudApplication.Mobile.Wrapper;
using StudApplication.Models;

namespace StudApplication.Mobile.ViewModel
{
    public partial class ViewPageViewModel : ObservableObject
    {
        private readonly ICurrentShiftService _shiftService;

        [ObservableProperty]
        private string _currentShift;

        [ObservableProperty]
        private DateTime _currentDate;

        [ObservableProperty]
        private EmployeeWrapper? _employeeWrapper;

        public ViewPageViewModel(ICurrentShiftService shiftService)
        {
            _shiftService = shiftService;
            CurrentShift = _shiftService.CurrentShift();
            CurrentDate = DateTime.Now;
            _employeeWrapper = new EmployeeWrapper(new Employee
            {
                PersonInformation = new Person(),
                ContactInformation = new ContactInfo(),
                Autorization = new Autorization()
            });
        }



        [RelayCommand]
        private async Task Violators()
        {
            await Shell.Current.GoToAsync("//IntruderPage");
        }

        [RelayCommand]
        private async Task Documents()
        {
            await Shell.Current.GoToAsync("//DocumentsPage");
        }

        [RelayCommand]
        private async Task Dormitory()
        {
            await Shell.Current.GoToAsync("//DormitoryPage");
        }

        [RelayCommand]
        private async Task EmployeesTable()
        {
            await Shell.Current.GoToAsync("EmployeesPage");
        }
    }
}
