using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Classes
{
    public class Customer : IUser, IBankAccount
    {
        List<IBankAccount> accounts = new List<IBankAccount>();


        public string userName { get; set; }
        public string password { get; set; }
        public string accountName { get; set; }
        public decimal balance { get; set; }
        public string accountCurrency { get; set; }

        public Customer()
        {

        }

        public void CreateCardAccount(string accountName, decimal balance, string accountCurrency)
        {
           accounts.Add(new CardAccount());
        }
        public void CreateISKAccount(string accountName, decimal balance, string accountCurrency)
        {
            accounts.Add(new ISK());
        }
        public void CreateSavingsAccount(string accountName, decimal balance, string accountCurrency)
        {
            accounts.Add(new SavingsAccount());
        }
    }
}
