using Microsoft.EntityFrameworkCore;
using SmartWallet.Models;
using SmartWallet.Models.Enums;

namespace SmartWallet.AppDbContext
{
    public class SeedDefaultCategories
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var defaultCategories = new List<Category>
            {
                new Category
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Name = "Питание",
                    Description = "Продукты, рестораны, кафе",
                    Type = CategoryType.Expense,
                    Color = "#E74C3C",
                    IsDefault = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Name = "Транспорт",
                    Description = "Топливо, общественный транспорт, такси",
                    Type = CategoryType.Expense,
                    Color = "#F39C12",
                    IsDefault = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    Name = "Развлечения",
                    Description = "Кино, игры, хобби",
                    Type = CategoryType.Expense,
                    Color = "#9B59B6",
                    IsDefault = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    Name = "Здоровье",
                    Description = "Лекарства, спортзал, медицина",
                    Type = CategoryType.Expense,
                    Color = "#1ABC9C",
                    IsDefault = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    Name = "Жилище",
                    Description = "Аренда, коммунальные услуги, ремонт",
                    Type = CategoryType.Expense,
                    Color = "#34495E",
                    IsDefault = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                    Name = "Образование",
                    Description = "Курсы, книги, обучение",
                    Type = CategoryType.Expense,
                    Color = "#3498DB",
                    IsDefault = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                    Name = "Другое",
                    Description = "Разные расходы",
                    Type = CategoryType.Expense,
                    Color = "#95A5A6",
                    IsDefault = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                    Name = "Зарплата",
                    Description = "Основной доход с работы",
                    Type = CategoryType.Income,
                    Color = "#27AE60",
                    IsDefault = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000011"),
                    Name = "Бонус/Премия",
                    Description = "Премии, бонусы, прибыль",
                    Type = CategoryType.Income,
                    Color = "#2ECC71",
                    IsDefault = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000012"),
                    Name = "Инвестиции",
                    Description = "Доход от инвестиций, дивиденды",
                    Type = CategoryType.Income,
                    Color = "#16A085",
                    IsDefault = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000013"),
                    Name = "Другой доход",
                    Description = "Разные источники дохода",
                    Type = CategoryType.Income,
                    Color = "#1ABC9C",
                    IsDefault = true,
                    CreatedAt = DateTime.UtcNow
                }
            };

            modelBuilder.Entity<Category>().HasData(defaultCategories);
        }
    }
}
