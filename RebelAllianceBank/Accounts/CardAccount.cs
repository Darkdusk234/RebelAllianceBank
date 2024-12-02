using RebelAllianceBank.Classes;
using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Accounts
{
    public class CardAccount : IBankAccount
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public int AccountType { get; set; } = 0;
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
        public string AccountCurrency { get; set; }
        public decimal IntrestRate { get; set; }
        public CardAccount() { }
        public CardAccount(string userId, string accountName, decimal balance, string accountCurrency, decimal intrestRate = 0)
        {
            UserId = userId;
            AccountName = accountName;
            Balance = balance;
            AccountCurrency = Bank.exchangeRate.SetAccountCurrency();
            IntrestRate = intrestRate;
        }
    }
}
