using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Classes
{
    public class SavingsAccount : IBankAccount
    {
        public string accountName { get; set; }
        public decimal balance { get; set; }
        public string accountCurrency { get; set; }
    }
}
