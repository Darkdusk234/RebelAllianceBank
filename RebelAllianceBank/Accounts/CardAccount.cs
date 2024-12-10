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
        public CardAccount(string accountName, string userId, string accountCurrency)
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
            Console.WriteLine($"Nuvarande saldo på konto: {this.Balance:N2} {this.AccountCurrency}");
            Console.WriteLine("-------------------------------------------------------------------------------------");
            foreach (var transaction in _transactionsLog)
            {
                const string format = "{0,-30} {1,-40} {2, -30}";
                
                //change the amount to the correct currenct for receiving account
                decimal amountToInCorrectCurrency = transaction.Amount *
                                                           Bank.exchangeRate.CalculateExchangeRate(
                                                               transaction.AccountFrom.AccountCurrency,
                                                               transaction.AccountTo.AccountCurrency);
                
                if (transaction.AccountFrom?.AccountName == this.AccountName)
                {
                    Console.WriteLine(format, $"{transaction.Timestamp}", $"Överföring till {transaction.AccountTo.AccountName.ToUpper()}",
                        $"-{transaction.Amount:N2} {this.AccountCurrency}");
                }
                else if (transaction.AccountTo.AccountName == this.AccountName && transaction.AccountFrom != null)
                {
                    Console.WriteLine(format, $"{transaction.Timestamp}", $"Insättning från {transaction.AccountFrom.AccountName.ToUpper()}",
                        $"{amountToInCorrectCurrency:N2} {this.AccountCurrency}");
                }
                else if (transaction.AccountTo.AccountName == this.AccountName && transaction.AccountFrom == null)
                {
                    Console.WriteLine(format, $"{transaction.Timestamp}", $"Direkt insättning",
                        $"{amountToInCorrectCurrency:N2} {this.AccountCurrency}");
                }
                else
                {
                    Console.WriteLine(format, $"{transaction.Timestamp}", $"{transaction.AccountFrom?.AccountName ?? "Okänt konto"}",
                        $"{amountToInCorrectCurrency:N2} {this.AccountCurrency}"); 
                }
                Console.WriteLine("-------------------------------------------------------------------------------------");
            }
            _transactionsLog.Reverse();
        }

    }
}
