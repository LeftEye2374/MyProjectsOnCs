using Microsoft.EntityFrameworkCore;
using SmartWallet.Models;

namespace SmartWallet.AppDbContext
{
    public class SqliteContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Budget> Budgets { get; set; }

        public SqliteContext(DbContextOptions<SqliteContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("lower(hex(randomblob(16)))")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Description)
                    .HasMaxLength(500);

                entity.Property(e => e.Color)
                    .HasMaxLength(7)
                    .HasDefaultValue("#3498DB");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasConversion<int>();

                entity.Property(e => e.IsDefault)
                    .HasDefaultValue(false);

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAdd();

                entity.HasIndex(e => e.Type)
                    .HasDatabaseName("IX_Category_Type");

                entity.HasIndex(e => new { e.Name, e.Type })
                    .HasDatabaseName("IX_Category_Name_Type")
                    .IsUnique();

                entity.HasMany(e => e.Transactions)
                    .WithOne(t => t.Category)
                    .HasForeignKey(t => t.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.Budgets)
                    .WithOne(b => b.Category)
                    .HasForeignKey(b => b.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("lower(hex(randomblob(16)))")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Description)
                    .HasMaxLength(1000);

                entity.Property(e => e.Amount)
                    .HasPrecision(15, 2)
                    .IsRequired();

                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasDefaultValue("RUB");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasConversion<int>();

                entity.Property(e => e.TransactionDate)
                    .IsRequired();

                entity.Property(e => e.CategoryId)
                    .IsRequired();

                entity.Property(e => e.IsRecurring)
                    .HasDefaultValue(false);

                entity.Property(e => e.RecurrencePattern)
                    .HasMaxLength(50);

                entity.Property(e => e.Tags)
                    .HasMaxLength(500);

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.HasIndex(e => e.TransactionDate)
                    .HasDatabaseName("IX_Transaction_TransactionDate");

                entity.HasIndex(e => e.CategoryId)
                    .HasDatabaseName("IX_Transaction_CategoryId");

                entity.HasIndex(e => e.Type)
                    .HasDatabaseName("IX_Transaction_Type");

                entity.HasIndex(e => new { e.TransactionDate, e.CategoryId })
                    .HasDatabaseName("IX_Transaction_Date_Category");

                entity.HasOne(e => e.Category)
                    .WithMany(c => c.Transactions)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            modelBuilder.Entity<Budget>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasDefaultValueSql("lower(hex(randomblob(16)))")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Description)
                    .HasMaxLength(1000);

                entity.Property(e => e.Limit)
                    .HasPrecision(15, 2)
                    .IsRequired();

                entity.Property(e => e.CurrentSpent)
                    .HasPrecision(15, 2)
                    .HasDefaultValue(0);

                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasDefaultValue("RUB");

                entity.Property(e => e.StartDate)
                    .IsRequired();

                entity.Property(e => e.EndDate)
                    .IsRequired();

                entity.Property(e => e.PeriodType)
                    .IsRequired()
                    .HasConversion<int>();

                entity.Property(e => e.CategoryId)
                    .IsRequired();

                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true);

                entity.Property(e => e.NotifyOnExceed)
                    .HasDefaultValue(true);

                entity.Property(e => e.AlertThresholdPercentage)
                    .HasDefaultValue(80);

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

                entity.HasIndex(e => e.CategoryId)
                    .HasDatabaseName("IX_Budget_CategoryId");

                entity.HasIndex(e => e.IsActive)
                    .HasDatabaseName("IX_Budget_IsActive");

                entity.HasIndex(e => new { e.StartDate, e.EndDate })
                    .HasDatabaseName("IX_Budget_DateRange");

                entity.HasOne(e => e.Category)
                    .WithMany(c => c.Budgets)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);  
            });

            SeedDefaultCategories.Seed(modelBuilder);
        }
    }
}
