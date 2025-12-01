using ExpenseTracker.Model.Base;

namespace ExpenseTracker.Model
{
    public class Account : BaseEntity
    {
        public PersonInformation PersonInformation { get; set; }
        public string Description { get; set; }
        public Decimal Balance { get; set; }
        public AccountType AccountType { get; set; }
    }
}