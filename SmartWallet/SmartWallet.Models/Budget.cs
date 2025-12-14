using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartWallet.Models
{
    public class Budget : BaseEntity
    {
        /// Название бюджета
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        // Описание бюджета
        [StringLength(1000)]
        public string Description { get; set; }

        /// Максимальный лимит расходов
        [Required]
        [Range(0.01, 999999999.99)]
        [Column(TypeName = "DECIMAL(15, 2)")]
        public decimal Limit { get; set; }

        /// Текущие расходы в этом бюджетном периоде
        [Column(TypeName = "DECIMAL(15, 2)")]
        public decimal CurrentSpent { get; set; } = 0;

        /// Оставшаяся сумма (Limit - CurrentSpent)
        [NotMapped]
        public decimal Remaining => Limit - CurrentSpent;

        /// Процент использования бюджета (0-100)
        [NotMapped]
        public decimal PercentageUsed => Limit > 0 ? (CurrentSpent / Limit) * 100 : 0;

        [Required]
        [StringLength(3)]
        public string Currency { get; set; } = "RUB";

        /// Начало периода бюджета
        [Required]
        public DateTime StartDate { get; set; }

        /// Окончание периода бюджета
        [Required]
        public DateTime EndDate { get; set; }

        /// Тип периода: месячный, годовой, кастомный
        [Required]
        public BudgetPeriodType PeriodType { get; set; }

        /// Внешний ключ на категорию
        [Required]
        public Guid CategoryId { get; set; }

        /// Активен ли бюджет
        public bool IsActive { get; set; } = true;

        /// Отправлять ли уведомления при превышении
        public bool NotifyOnExceed { get; set; } = true;

        /// Процент, при котором отправляется предупреждение (например, 80%)
        [Range(0, 100)]
        public int AlertThresholdPercentage { get; set; } = 80;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("CategoryId")]
        public required Category Category { get; set; }
    }
}
