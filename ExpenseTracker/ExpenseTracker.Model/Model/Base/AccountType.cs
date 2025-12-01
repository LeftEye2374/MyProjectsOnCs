namespace ExpenseTracker.Model.Base
{
    public enum AccountType
    {
        Cash,       // Наличные
        DebitCard,  // Дебетовая карта
        CreditCard, // Кредитная карта
        Savings,    // Накопительный счет
        Investment, // Инвестиционный
        Digital     // Электронный кошелек
    }
}