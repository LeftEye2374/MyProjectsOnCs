using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudApp.Mobile.Services;

namespace StudApp.Mobile.ViewModel
{
    public partial class ViewViewModel : ObservableObject
    {
        private readonly IShiftService _shiftService;

        [ObservableProperty]
        private string _currentShift;

        [ObservableProperty]
        private DateTime _currentDate;

        public ViewViewModel(IShiftService shiftService)
        {
            _shiftService = shiftService;
            CurrentShift = _shiftService.GetCurrentShift();
            CurrentDate = DateTime.Now;
        }

        [RelayCommand]
        private async Task Violators()
        {
            await Shell.Current.GoToAsync("//ViolatorsPage");
        }

        [RelayCommand]
        private async Task Documents()
        {
            await Shell.Current.GoToAsync("//DocumentsPage");
        }

        [RelayCommand]
        private async Task Registration()
        {
            await Shell.Current.GoToAsync("//RegistrationPage");
        }

        [RelayCommand]
        private async Task Dormitory()
        {
            await Shell.Current.GoToAsync("//DormitoryPage");
        }

        [RelayCommand]
        private async Task Employees()
        {
            await Shell.Current.GoToAsync("//EmployeesPage");
        }
    }
}
