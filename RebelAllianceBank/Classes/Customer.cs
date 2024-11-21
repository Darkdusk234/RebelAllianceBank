using RebelAllianceBank.Interfaces;
using RebelAllianceBank.utils;
namespace RebelAllianceBank.Classes
{
    public class Customer : IUser
    {
        private List<IBankAccount> BankAccounts = new List<IBankAccount> {
            new CardAccount("Card Akount", 7800.56789m, "Kr"),
            new ISK("ISK", 100000, "USD"),
            new SavingsAccount("Savings Account", 100000, "$"),
        };

        public void ShowBankAccounts()
        {
            Console.WriteLine("Konton");

            if (BankAccounts.Count == 0)
            {
                Console.WriteLine("Du har inga konton att visa");
            }


            List<string> keys = [];
            foreach (var BankAccount in BankAccounts)
            {
                keys.Add(BankAccount.AccountName);
                keys.Add(BankAccount.Balance.ToString());
            }
            Markdown.Table(["Konto Namn", "Saldo"], keys);
        }
    }
}
