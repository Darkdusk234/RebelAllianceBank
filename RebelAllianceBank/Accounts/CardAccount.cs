using RebelAllianceBank.Interfaces;
using RebelAllianceBank.Other;

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
            Console.WriteLine("-------------------------------------------------------------------------------------");
            Console.WriteLine($"Nuvarande saldo på konto: {this.Balance:N2} {AccountCurrency}");
            Console.WriteLine("-------------------------------------------------------------------------------------");
            const string format = "{0,-30} {1,-40} {2, -30}";
            
            foreach (var transaction in _transactionsLog)
            {
               
                //Valid for transaktions between the users own accounts
                if (transaction.AccountFrom != null && transaction.AccountFrom.AccountName == this.AccountName && 
                    transaction.AccountTo.UserId == this.UserId)
                {
                    Console.WriteLine(format, $"{transaction.Timestamp}", $"Till konto {transaction.AccountTo.AccountName.ToUpper()}",
                        $"-{transaction.Amount:N2} {this.AccountCurrency}");
                }
                //for transaktions from this account to another customer
                else if (transaction.AccountFrom != null && transaction.AccountFrom.AccountName == this.AccountName &&
                         transaction.AccountTo.UserId != this.UserId)
                {
                    Console.WriteLine(format, $"{transaction.Timestamp}",
                        $"Till kund {transaction.AccountTo.UserId}", $"-{transaction.Amount:N2} {this.AccountCurrency}");
                }
                //A deposit from loan or deposit-method
                else if (transaction.AccountTo.AccountName == this.AccountName && transaction.AccountFrom == null)
                {
                    Console.WriteLine(format, $"{transaction.Timestamp}", $"Insättning",
                        $"{transaction.Amount:N2} {this.AccountCurrency}");
                }
                //A transfer from another account of the same user
                else if (transaction.AccountTo.AccountName == this.AccountName && transaction.AccountFrom != null && 
                         transaction.AccountFrom.UserId == UserId)
                {
                    //change the amount to the correct currenct for receiving account
                    decimal amountToInCorrectCurrency = transaction.Amount *
                                                        Bank.exchangeRate.CalculateExchangeRate(
                                                            transaction.AccountFrom.AccountCurrency,
                                                            transaction.AccountTo.AccountCurrency);
                    
                    Console.WriteLine(format, $"{transaction.Timestamp}", $"Från konto {transaction.AccountFrom.AccountName.ToUpper()}",
                        $"{amountToInCorrectCurrency:N2} {this.AccountCurrency}");
                }
                //A transfer from another user
                else if (transaction.AccountTo.AccountName == this.AccountName && transaction.AccountFrom != null && 
                         transaction.AccountFrom.UserId != UserId)
                {
                    //change the amount to the correct currenct for receiving account
                    decimal amountToInCorrectCurrency = transaction.Amount *
                                                        Bank.exchangeRate.CalculateExchangeRate(
                                                            transaction.AccountFrom.AccountCurrency,
                                                            transaction.AccountTo.AccountCurrency);
                    Console.WriteLine(format, $"{transaction.Timestamp}", $"Från kund {transaction.AccountFrom.UserId}",
                        $"{amountToInCorrectCurrency:N2} {this.AccountCurrency}");
                }
                Console.WriteLine("-------------------------------------------------------------------------------------");
            }
            _transactionsLog.Reverse();
        }

    }
}
