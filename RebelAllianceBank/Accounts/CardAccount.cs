using RebelAllianceBank.Classes;
using RebelAllianceBank.Interfaces;
using System.Transactions;
namespace RebelAllianceBank.Accounts
{
    public class CardAccount : IBankAccount
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public int AccountType { get; set; } = 0;
        public string AccountName { get; set; }
        public decimal Balance { get; set; } = 0;
        public string AccountCurrency { get; set; }
        public decimal IntrestRate { get; set; } = 0.2m;
        
        private List<Transaction> _transactionLog = new List<Transaction>();
        public CardAccount() { }
        public CardAccount(string userId, string accountName)
        {
            UserId = userId;
            AccountName = accountName;
            AccountCurrency = Bank.exchangeRate.SetAccountCurrency();
        }
        
        public void CardTransaction(Transaction transaction)
        {
            _transactionLog.Add(transaction);
        }

        //public void CardTransaction(IBankAccount transaction)
        //{
        //    _transactionLog.Add(transaction);

        //}

        //public void CardTransactionHistory()
        //{
        //    foreach (Transaction item in _transactionLog)
        //    {
        //        Console.WriteLine($"{0}{1}{2}");
        //    }
        //}


    }
}
