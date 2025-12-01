using ExpenseTracker.Model.Base;

namespace ExpenseTracker.Model
{
    public class Transaction : BaseEntity
    {
        public Decimal Amount { get; set; }
        public string Description { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}