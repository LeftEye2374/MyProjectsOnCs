using StudApplication.Mobile.Wrapper;

namespace StudApplication.Mobile.Service
{
    public interface IEmployeeService
    {
        Task<EmployeeWrapper?> AddEmployeeAsync();
    }
}
