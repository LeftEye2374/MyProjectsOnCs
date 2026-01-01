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
        private EmployeeService _employeeService = new();

        [ObservableProperty]
        private ObservableCollection<EmployeeWrapper> employees = new();

        public EmployeesViewModel(SqliteDbContext context)
        {
            _context = context;
        }

        [RelayCommand]
        private async Task Appeared()
        {
            await LoadEmployeesAsync();
        }

        private async Task LoadEmployeesAsync()
        {
            Employees.Clear();
            var data = await _context.Employees
                .Include(e => e.PersonInfo)
                .Include(e => e.ContactInfo)
                .Include(e => e.Shift)
                .ToListAsync();

            foreach (var emp in data)
            {
                emp.PersonInfo ??= new();
                emp.ContactInfo ??= new();
                Employees.Add(new EmployeeWrapper(emp));
            }
        }

        [RelayCommand]
        private async Task AddEmployee()
        {
            var newWrapper = await _employeeService.AddEmployeeAsync();
            if (newWrapper != null)
            {
                Employees.Add(newWrapper);
            }
        }
    }
}
