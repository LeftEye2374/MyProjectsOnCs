using StudApp.Mobile.Wrappers;

namespace StudApp.Mobile.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeWrapper?> AddEmployeeAsync(Page page);
    }
}
