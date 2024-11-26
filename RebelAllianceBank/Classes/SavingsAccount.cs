using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Classes
{
    public class SavingsAccount : IBankAccount
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public int AccountType { get; set; }
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
        public string AccountCurrency { get; set; }
        public decimal IntrestRate { get; set; }
        public SavingsAccount() { }
        public SavingsAccount(string accountName, decimal balance, string accountCurrency, decimal intrestRate = 0)
        {
            AccountName = accountName;
            Balance = balance;
            AccountCurrency = accountCurrency;
            IntrestRate = intrestRate;
        }
    }
}
