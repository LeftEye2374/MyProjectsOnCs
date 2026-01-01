using CommunityToolkit.Maui.Views;
using StudApp.AppDbContext;
using StudApp.Mobile.View;
using StudApp.Mobile.Wrappers;
using StudApp.Models;

namespace StudApp.Mobile.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly SqliteDbContext _context;

        public EmployeeService()
        {
        }

        public EmployeeService(SqliteDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeWrapper?> AddEmployeeAsync()
        {
            var newEmployee = new Employee
            {
                PersonInfo = new(),
                ContactInfo = new()
            };

            var wrapper = new EmployeeWrapper(newEmployee);

            var page = new AddEmployeePopupPage(wrapper);
            var result = await Application.Current.MainPage.ShowPopupAsync(page);

            if (result is bool success && success)
            {
                if (string.IsNullOrEmpty(wrapper.FirstName) || string.IsNullOrEmpty(wrapper.LastName))
                    return null;

                _context.Employees.Add(newEmployee);
                await _context.SaveChangesAsync();
                return wrapper;
            }

            return null;
        }
    }
}
