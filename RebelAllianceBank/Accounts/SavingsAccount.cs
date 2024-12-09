using RebelAllianceBank.Classes;
using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Accounts
{
    public class SavingsAccount : IBankAccount
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public int AccountType { get; set; } = 1;
        public string AccountName { get; set; }
        public decimal Balance { get; set; } = 0; 
        public string AccountCurrency { get; set; }
        public decimal IntrestRate { get; set; } = 2.1m;

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
            Bank.accountNumberCounter++;
            
        }
        
        
    }
}
