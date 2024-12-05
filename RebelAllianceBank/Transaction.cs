using RebelAllianceBank.Accounts;
using RebelAllianceBank.Classes;
using RebelAllianceBank.Interfaces;

namespace RebelAllianceBank
{
    public class Transaction
    {
        public decimal Amount { get; set; }
        public string OtherAccount { get; set; }
        public DateTime Timestamp{ get; set; }

        //public string AccountCurrency { get; set; }
        //public List<IBankAccount> AccountTransaction { get; set; }
        
        private List<IBankAccount> _transactions = new List<IBankAccount>();

        public Transaction(decimal amount) // check availability
        {
            OtherAccount = "External Account";
            Timestamp = DateTime.Now;
        }
        public Transaction(IBankAccount otherAccount, decimal amount )
        {
            OtherAccount = otherAccount.AccountName;
            Amount = amount;
            Timestamp = DateTime.Now;

            //AccountCurrency = Bank.exchangeRate.SetAccountCurrency();
            //AccountTransaction = IBankAccount.CardTransaction(otherAccount); // kan man göra CardTransaction() i IBankAccount och spara värdet?
        }

        public void CardTransaction(IBankAccount transaction)
        {
            _transactions.Add(transaction);
        }

        public void CardTransactionHistory()
        {
            foreach (Transaction item in _transactions)
            {
                Console.WriteLine($"Transaction from {item.OtherAccount} {item.Amount} time:{item.Timestamp}"); // Currency?
            }
        }
    }
}
