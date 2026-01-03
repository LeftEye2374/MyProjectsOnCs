using CommunityToolkit.Maui.Views;
using Microsoft.EntityFrameworkCore;
using StudApp.AppDbContext;
using StudApp.Mobile.Views;
using StudApp.Mobile.Wrappers;
using StudApp.Models;
using System.Diagnostics;

namespace StudApp.Mobile.Services {

    public class EmployeeService : IEmployeeService
    {
        private readonly SqliteDbContext _context;

        public EmployeeService(SqliteDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeWrapper?> AddEmployeeAsync(Page page)
        {
            var newEmployee = new Employee
            {
                PersonInfo = new Person(),
                ContactInfo = new ContactInfo(),
                Role = "",
                NumOfShift = 0,
                ShiftId = null,
                Password = "",
                Reports = new List<Report>()
            };

            var wrapper = new EmployeeWrapper(newEmployee);

            var popup = new AddEmployeePopup(wrapper);
            var result = await page.ShowPopupAsync(popup);

            if (result is true)
            {
                if (string.IsNullOrWhiteSpace(wrapper.FirstName) ||
                    string.IsNullOrWhiteSpace(wrapper.LastName))
                {
                    await page.DisplayAlert("Ошибка", "Заполните имя и фамилию!", "OK");
                    return null;
                }

                if (wrapper.NumOfShift <= 0)
                {
                    await page.DisplayAlert("Ошибка", "Укажите номер смены!", "OK");
                    return null;
                }

                try
                {
                    _context.Employees.Add(newEmployee);
                    var saved = await _context.SaveChangesAsync();
                    Debug.WriteLine($" Сохранено в БД: {saved} записей");

                    var count = await _context.Employees.CountAsync();
                    Debug.WriteLine($" Всего сотрудников в БД: {count}");

                    await page.DisplayAlert("Успех", "Сотрудник добавлен!", "OK");
                    return wrapper;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"❌ Ошибка при сохранении: {ex.Message}");
                    Debug.WriteLine($"❌ InnerException: {ex.InnerException?.Message}");
                    await page.DisplayAlert("Ошибка БД", ex.InnerException?.Message ?? ex.Message, "OK");
                    return null;
                }
            }
            return null;
        }
    }
}
