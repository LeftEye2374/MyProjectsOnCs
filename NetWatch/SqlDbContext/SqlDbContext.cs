using Microsoft.EntityFrameworkCore;

namespace NetWatch.SqlDbContext
{
    public partial class SqlDbContext : DbContext
    {
        public SqlDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
