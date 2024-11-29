using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Classes
{
    public class SavingsAccount : IBankAccount
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        public int AccountType { get; set; }
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
        public string AccountCurrency { get; set; }
        public decimal IntrestRate { get; set; } = 1.60m;
        public decimal LoanIntrestRate { get; } = 0.32m;

        public SavingsAccount() { }

        public SavingsAccount(string accountName, decimal balance, string accountCurrency, decimal intrestRate)
        {
            AccountName = accountName;
            Balance = balance;
            AccountCurrency = accountCurrency;
            IntrestRate = intrestRate;
        }

        public decimal CalculateLoanInterest(decimal loanAmount)
        {
            decimal sum;
            sum = LoanIntrestRate * loanAmount;
            return sum;
        }
    }
}
