using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Accounts
{
    public class SavingsAccount : IBankAccount
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public int AccountType { get; set; } = 1;
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
        public string AccountCurrency { get; set; }
        public decimal IntrestRate { get; set; } = 1.60m;

        public SavingsAccount() { }
        public SavingsAccount(string userId, int accountType, string accountName, decimal balance, string accountCurrency, decimal intrestRate = 0)
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
