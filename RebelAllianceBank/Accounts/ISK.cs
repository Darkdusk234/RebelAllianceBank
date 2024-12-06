using RebelAllianceBank.Classes;
using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Accounts
{
    public class ISK : IBankAccount
    {
        private List<Transaction> _transactionsLog = new List<Transaction>(); 
        public int ID { get; set; }
        public string UserId { get; set; }
        public long AccountNr { get; set; } 
        public int AccountType { get; set; } = 2;
        public string AccountName { get; set;  }
        public decimal Balance { get; set; } = 0; 
        public string AccountCurrency { get;  set;  }
        public decimal IntrestRate { get; set; } = 3.47m;
        public List<Transaction> transactionsLog { get; set; }
       
        public ISK(string accountName, string userId)
        {
            UserId = userId;
            AccountName = accountName;
            AccountCurrency = Bank.exchangeRate.SetAccountCurrency();
            AccountNr = Bank.accountNumberCounter;
            Bank.accountNumberCounter ++; 
        }
        public void AddToTransactionLog(Transaction newTransaction)
        {
            _transactionsLog.Add(newTransaction);
        }

        public void ShowTransactionLog()
        {
            throw new NotImplementedException();
        }
    }
}
