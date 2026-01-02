using StudApp.Mobile.Wrappers;
using StudApp.Models;

namespace StudApp.Mobile.Services
{
    public interface IEmployeeService
    {
        Task<Employee?> AddEmployeeAsync();
    }
}
