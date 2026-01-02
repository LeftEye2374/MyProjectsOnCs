using StudApp.AppDbContext;
using StudApp.Mobile.Wrappers;
using StudApp.Models;

namespace StudApp.Mobile.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly SqliteDbContext _context;
        private EmployeeWrapper _wrapper;

        public EmployeeService()
        {
        }

        public Task<Employee?> AddEmployeeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
