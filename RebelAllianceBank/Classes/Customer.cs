using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Classes
{
    public class Customer : IUser
    {
        List<IBankAccount> accounts = new List<IBankAccount>();

        public string UserName { get; set; }
        public string Password { get; set; }

        public Customer()
        {

        }

        public void CreateCardAccount(string accountName, decimal balance, string accountCurrency)
        {
           accounts.Add(new CardAccount(accountName, 0 , accountCurrency ));
        }
        public void CreateISKAccount(string accountName, decimal balance, string accountCurrency)
        {
            accounts.Add(new ISK(accountName, 0, accountCurrency));
        }
        public void CreateSavingsAccount(string accountName, decimal balance, string accountCurrency)
        {
            accounts.Add(new SavingsAccount(accountName, 0, accountCurrency));
        }
    }
}
