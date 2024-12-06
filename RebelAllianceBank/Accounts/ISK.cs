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
            _transactionsLog.Reverse();
            Console.WriteLine($"Nuvarande saldo på konto: {this.Balance}");
            Console.WriteLine("---------------------------------------------------");
            foreach (var transaction in _transactionsLog)
            {
                if (transaction.AccountFrom.AccountNr == this.AccountNr)
                {
                    Console.WriteLine(
                        $"{transaction.AccountTo}          {-transaction.Amount} {this.AccountCurrency}\n" +
                        $"{transaction.Timestamp}");
                }
                else if (transaction.AccountTo.AccountNr == this.AccountNr && transaction.AccountFrom.AccountNr != null)
                {
                    Console.WriteLine($"Insättning          {transaction.Amount} {this.AccountCurrency}\n" +
                                      $"{transaction.Timestamp}");
                }
                else
                {
                    {
                        Console.WriteLine(
                            $"{transaction.AccountFrom}          {transaction.Amount} {this.AccountCurrency}\n" +
                            $"{transaction.Timestamp}");
                    }
                    Console.WriteLine("---------------------------------------------------");
                }

                _transactionsLog.Reverse();
            }
        }
    }
}
