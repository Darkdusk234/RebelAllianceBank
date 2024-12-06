using RebelAllianceBank.Classes;
using RebelAllianceBank.Interfaces;

namespace RebelAllianceBank.Accounts
{
    public class CardAccount : IBankAccount
    {
        private List<Transaction> _transactionsLog = new List<Transaction>();
        public long ID { get; set; }
        public string UserId { get; set; }
        public int AccountType { get; set; } = 0;
        public string AccountName { get; set; }
        public decimal Balance { get; set; } = 0;
        public string AccountCurrency { get; set; }
        public decimal IntrestRate { get; set; } = 0.2m;

        public CardAccount()
        {
            ID = Bank.accountNumberCounter;
            Bank.accountNumberCounter ++; 
        }

        public CardAccount(string userId, string accountName, string accountCurrency)
        {
            UserId = userId;
            AccountName = accountName;
            AccountCurrency = accountCurrency;
            ID = Bank.accountNumberCounter;
            Bank.accountNumberCounter++;
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
                if (transaction.AccountFrom.AccountName == this.AccountName)
                {
                    Console.WriteLine(
                        $"{transaction.AccountTo}          {-transaction.Amount} {this.AccountCurrency}\n" +
                        $"{transaction.Timestamp}");
                }
                else if (transaction.AccountTo.AccountName == this.AccountName && transaction.AccountFrom.AccountName != null)
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