using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Classes
{
    public class ISK : IBankAccount
    {
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
        public string AccountCurrency { get; set; }
        public decimal IntrestRate { get; set; } = 2.94m;

        public ISK(string accountName, decimal balance, string accountCurrency, decimal intrestRate)
        {
            AccountName = accountName;
            Balance = balance;
            AccountCurrency = accountCurrency;
            IntrestRate = intrestRate;
        }
    }
}
