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
        public decimal Balance { get; set; } = 0;
        public string AccountCurrency { get; set; }
        public decimal IntrestRate { get; set; } = 0.2m; 
        public CardAccount() { }
        public CardAccount(string accountName, string userId)
        {
            UserId = userId;
            AccountName = accountName;
            AccountCurrency = Bank.exchangeRate.SetAccountCurrency();
        }
    }
}
