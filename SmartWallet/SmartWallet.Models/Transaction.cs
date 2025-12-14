// Models/Transaction.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartWallet.Models
{
    // Финансовая транзакция (расход или доход)
    public class Transaction : BaseEntity 
    {

        // Название/описание транзакции
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        // Подробное описание транзакции
        [StringLength(1000)]
        public string Description { get; set; }

        // Сумма денежных средств
        [Required]
        [Range(0.01, 999999999.99)]
        [Column(TypeName = "DECIMAL(15, 2)")]
        public decimal Amount { get; set; }

        // Валюта (RUB, USD, EUR и т.д.)
        [Required]
        [StringLength(3)]
        public string Currency { get; set; } = "RUB";

        // Тип транзакции: Расход или Доход
        [Required]
        public TransactionType Type { get; set; }

        // Дата проведения транзакции
        [Required]
        public DateTime TransactionDate { get; set; }

        /// Внешний ключ на категорию
        [Required]
        public Guid CategoryId { get; set; }

        /// Отметка о том, что транзакция повторяющаяся
        public bool IsRecurring { get; set; }

        /// Интервал повторения (дни, недели, месяцы)
        [StringLength(50)]
        public string RecurrencePattern { get; set; }

        /// Тэги для доп. фильтрации (через запятую)
        [StringLength(500)]
        public string Tags { get; set; }

        /// Дата последнего обновления
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Навигационные свойства
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
