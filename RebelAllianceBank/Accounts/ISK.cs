using RebelAllianceBank.Classes;
using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Accounts
{
    public class ISK : IBankAccount
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public int AccountType { get; set; } = 2;
        public string AccountName { get; set;  }
        public decimal Balance { get;  set;  }
        public string AccountCurrency { get;  set;  }
        public decimal IntrestRate { get; set; } = 3.47m;
        public ISK() { }
        public ISK(string accountName, string userId)
        {
            UserId = userId;
            AccountName = accountName;
            AccountCurrency = Bank.exchangeRate.SetAccountCurrency();
        }
    }
}
