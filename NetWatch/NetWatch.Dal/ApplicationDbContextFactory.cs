using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace NetWatch.DAL
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<SqlDbContext>
    {
        public SqlDbContext CreateDbContext(string[] args)
        {
            var optionalBuilder = new DbContextOptionsBuilder<SqlDbContext>();
            var connectionString = @"server=.,5433;Database=AutoLotSamples;User Id=sa;Password=P@ssw0rd; TrustServerCertificate=true";
            optionalBuilder.UseSqlServer(connectionString);
            return new SqlDbContext(optionalBuilder.Options);
        }
    }
}
