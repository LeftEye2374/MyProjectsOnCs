using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using StudApp.AppDbContext;
using StudApp.Mobile.Services;
using StudApp.Mobile.Wrappers;
using StudApp.Models;
using System.Collections.ObjectModel;

namespace StudApp.Mobile.ViewModel
{

    public partial class EmployeesViewModel : ObservableObject
    {
        private readonly SqliteDbContext _context;
        private readonly IEmployeeService _employeeService;

        [ObservableProperty]
        private ObservableCollection<EmployeeWrapper> employees = new();

        public EmployeesViewModel(SqliteDbContext context, IEmployeeService employeeService)
        {
            _context = context;
            _employeeService = employeeService;
        }

        [RelayCommand]
        private async Task LoadEmployees()
        {
            try
            {
                Employees.Clear();
                var data = await _context.Employees
                    .Include(e => e.PersonInfo)
                    .Include(e => e.ContactInfo)
                    .ToListAsync();

                foreach (var emp in data)
                {
                    emp.PersonInfo ??= new Person();
                    emp.ContactInfo ??= new ContactInfo();
                    Employees.Add(new EmployeeWrapper(emp));
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", $"Не удалось загрузить сотрудников: {ex.Message}", "OK");
            }
        }

        [RelayCommand]
        private async Task AddEmployee()
        {
            try
            {
                if (Application.Current.MainPage is Page page)
                {
                    var newWrapper = await _employeeService.AddEmployeeAsync(page);

                    // ДОБАВЛЯЕМ новый wrapper в список сразу
                    if (newWrapper != null)
                    {
                        Employees.Add(newWrapper);
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ошибка", $"Не удалось добавить сотрудника: {ex.Message}", "OK");
            }
        }
    }
}