using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Classes
{
    public class CardAccount : IBankAccount
    {
        public int ID { get; set; }
        public int AccountType { get; set; }
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
        public string AccountCurrency { get; set; }

        public CardAccount(string accountName, decimal balance, string accountCurrency)
        {
            AccountName = accountName;
            Balance = balance;
            AccountCurrency = accountCurrency;
        }
    }
}
