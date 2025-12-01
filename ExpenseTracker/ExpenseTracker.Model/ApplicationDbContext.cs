using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Model
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Accounts", "dbo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.AccountType).HasConversion<string>();
                entity.Property(e => e.Balance).HasColumnType("decimal(18,2)").HasDefaultValue(0);
                entity.Property(e => e.PersonInformation);
                entity.Property(e => e.TimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken();
            });

            modelBuilder.Entity<Category>(entity => 
            {
                entity.ToTable("Categories","dbo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name);
                entity.Property(e => e.CategoryType).HasConversion<string>();
                entity.Property(e => e.TimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken();
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transactions", "dbo");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Amount).HasColumnType("decimal(18,2)");
                entity.Property(e => e.Description);

                entity.HasOne(d => d.Account)
                  .WithMany()
                  .HasForeignKey(d => d.AccountId)
                  .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Category)
                    .WithMany()
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.TransferAccount)
                   .WithMany()
                   .HasForeignKey(d => d.TransferAccountId)
                   .OnDelete(DeleteBehavior.Restrict);

            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}