using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Extensions;
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
            return null;
        }
    }
}
