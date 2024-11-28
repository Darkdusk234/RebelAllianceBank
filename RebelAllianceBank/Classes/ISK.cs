using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Classes
{
    public class ISK : IBankAccount
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public int AccountType { get; set; }
        public string AccountName { get; set;  }
        public decimal Balance { get;  set;  }
        public string AccountCurrency { get;  set;  }
        public decimal IntrestRate { get; set; } = 2.94m;

        public ISK () { }

        public ISK(string accountName, decimal balance, string accountCurrency, decimal intrestRate )
        {
            AccountName = accountName;
            Balance = balance;
            AccountCurrency = accountCurrency;
            IntrestRate = intrestRate;
        }
    }
}
