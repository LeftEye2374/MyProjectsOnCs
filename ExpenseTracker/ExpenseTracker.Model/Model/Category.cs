using ExpenseTracker.Model.Base;

namespace ExpenseTracker.Model
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public CategoryType CategoryType { get; set; }
    }
}