using RebelAllianceBank.Interfaces;

namespace RebelAllianceBank.Classes
{
    public class Bank
    {
        public static void Run()
        {
            FileHandler run = new FileHandler();
            List<IUser> users = new List<IUser>(run.ReadUsersFromFile());
            List<IBankAccount> accounts = new List<IBankAccount>(run.ReadAccountsFromFile());
            run.WriteUserToFile(10, "Lars", "54321", false);
            
        }
    }
}
