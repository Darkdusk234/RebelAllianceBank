using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Classes
{
    public class ISK : IBankAccount
    {
        public string AccountName { get; set;  }
        public decimal Balance { get;  set;  }
        public string AccountCurrency { get;  set;  }

         public ISK(string accountName, decimal balance, string accountCurrency)
        {
            AccountName = accountName;
            Balance = balance;
            AccountCurrency = accountCurrency;
        }
    }
}
