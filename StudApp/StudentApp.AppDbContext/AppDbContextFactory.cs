using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace StudentApp.AppDbContext
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionalBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = "";
            optionalBuilder.UseSqlServer(connectionString);
            return new AppDbContext(optionalBuilder.Options);
        }
    }
}
