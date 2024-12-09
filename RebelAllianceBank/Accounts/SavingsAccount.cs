using RebelAllianceBank.Classes;
using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Accounts
{
    public class SavingsAccount : IBankAccount
    {
        private List<Transaction> _transactionsLog = new List<Transaction>(); 
        public long ID { get; set; }
        public string UserId { get; set; }
        public int AccountType { get; set; } = 1;
        public string AccountName { get; set; }
        public decimal Balance { get; set; } = 0; 
        public string AccountCurrency { get; set; }
        public decimal IntrestRate { get; set; } = 2.1m;
        public List<Transaction> transactionsLog { get; set; }

        public SavingsAccount()
        {
            ID = Bank.accountNumberCounter; 
            Bank.accountNumberCounter ++; 
        }
        public SavingsAccount(string accountName, string userId, string accountCurrency )
        {
            UserId = userId;
            AccountName = accountName;
            AccountCurrency = accountCurrency;
            ID = Bank.accountNumberCounter;
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
                if (transaction.AccountFrom?.AccountName == this.AccountName)
                {
                    Console.WriteLine(
                        $"{transaction.AccountTo.AccountName}          -{transaction.Amount} {this.AccountCurrency}\n" +
                        $"{transaction.Timestamp}");
                }
                else if (transaction.AccountTo.AccountName == this.AccountName && transaction.AccountFrom != null)
                {
                    Console.WriteLine(
                        $"Insättning från {transaction.AccountFrom.AccountName}          {transaction.Amount} {this.AccountCurrency}\n" +
                        $"{transaction.Timestamp}");
                }
                else if (transaction.AccountTo.AccountName == this.AccountName && transaction.AccountFrom == null)
                {
                    Console.WriteLine(
                        $"Direkt insättning          {transaction.Amount} {this.AccountCurrency}\n" +
                        $"{transaction.Timestamp}");
                }
                else
                {
                    Console.WriteLine(
                        $"{transaction.AccountFrom?.AccountName ?? "Okänt konto"}          {transaction.Amount} {this.AccountCurrency}\n" +
                        $"{transaction.Timestamp}");
                }
                Console.WriteLine("---------------------------------------------------");
            }
            _transactionsLog.Reverse();
        }
        
        
    }
}
