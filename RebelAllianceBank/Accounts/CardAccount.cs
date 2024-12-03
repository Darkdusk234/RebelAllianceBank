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
        public decimal IntrestRate { get; set; } = 2.6m;

        public CardAccount() { }

        public CardAccount(string userId, int accountType, string accountName, decimal balance, string accountCurrency, decimal intrestRate)
        {
            UserId = userId;
            AccountType = accountType;
            AccountName = accountName;
            Balance = balance;
            AccountCurrency = accountCurrency;
            IntrestRate = intrestRate;
        }
    }
}
