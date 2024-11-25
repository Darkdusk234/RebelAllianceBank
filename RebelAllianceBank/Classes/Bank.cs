using RebelAllianceBank.Interfaces;

namespace RebelAllianceBank.Classes
{
    public class Bank
    {
        public static void Run()
        {
            FileHandler run = new FileHandler();
            List<IUser> users = new List<IUser>(run.ReadUser());
            List<IBankAccount> accounts = new List<IBankAccount>(run.ReadAccount());
            users.Add(run.WriteUserToFile(10, "Lars", "54321", false));
            accounts.Add(run.WriteAccountToFile(0, 2, "ISK2", 1000, "SEK"));
            Console.ReadKey();
        }
    }
}
