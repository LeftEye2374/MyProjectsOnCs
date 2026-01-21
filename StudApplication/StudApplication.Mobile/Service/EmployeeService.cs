using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Extensions;
using StudApplication.Mobile.Service;
using StudApplication.Mobile.Views;
using StudApplication.Mobile.Wrapper;
using StudApplication.Models;
using StudApplications.AppDbContext;

namespace StudApplication.Mobile.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<EmployeeWrapper?> AddEmployeeAsync()
        {
            var page = Application.Current?.MainPage;
            if (page == null) return null;

            var employee = new Employee
            {
                PersonInformation = new Person(),
                ContactInformation = new ContactInfo(),
                Autorization = new Autorization(),
                Shift = 0,
                NumberOfReports = 0
            };

            var wrapper = new EmployeeWrapper(employee);

            var popup = new AddEmployeePopup(wrapper);

            IPopupResult<bool> popupResult =
                await page.ShowPopupAsync<bool>(popup, PopupOptions.Empty, CancellationToken.None); // [page:1]

            if (popupResult.WasDismissedByTappingOutsideOfPopup)
                return null;

            if (popupResult.Result != true)
                return null;

            try
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return wrapper;
            }
            catch (Exception ex)
            {
                await page.DisplayAlert("Ошибка БД", ex.InnerException?.Message ?? ex.Message, "OK");
                return null;
            }
        }
    }
}
