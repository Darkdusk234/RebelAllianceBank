using RebelAllianceBank.Classes;
using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Accounts
{
    public class CardAccount : IBankAccount
    {
        private List<Transaction> _transactionsLog = new List<Transaction>(); 
        public int ID { get; set; }
        public string UserId { get; set; }
        public long AccountNr { get; set; }  
        public int AccountType { get; set; } = 0;
        public string AccountName { get; set; }
        public decimal Balance { get; set; } = 0;
        public string AccountCurrency { get; set; }
        public decimal IntrestRate { get; set; } = 0.2m; 
        
        public CardAccount(string userId, string accountName)
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
            foreach (var transaction in _transactionsLog)
            {
                if (transaction.AccountFrom.AccountNr == this.AccountNr)
                {
                    Console.WriteLine("");
                }
                else if (transaction.AccountTo.AccountNr == this.AccountNr && transaction.AccountFrom.AccountNr != null)
                {
                }
                else
                {
                    
                }
            }
        }
    }
}
