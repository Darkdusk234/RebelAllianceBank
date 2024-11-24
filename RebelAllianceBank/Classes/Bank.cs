using RebelAllianceBank.Interfaces;

namespace RebelAllianceBank.Classes
{
    public class Bank
    {
        public static void Run()
        {
            DatabaseHandler db = new DatabaseHandler();
            db.WriteUserToDatabase(0, "Göran", "apa123", true);
            db.WriteUserToDatabase(1, "Lars", "päron", false);
            List<IUser> usersdb = new List<IUser>(db.GetUsersFromDatabase());
            

            FileHandler run = new FileHandler();
            List<IUser> users = new List<IUser>(run.ReadUsersFromFile());

            List<IBankAccount> accounts = new List<IBankAccount>(run.ReadAccountsFromFile());
            users.Add(run.WriteUserToFile(10, "Lars", "54321", false));
            accounts.Add(run.WriteAccountToFile(0, 2, "ISK2", 1000, "SEK"));
            Console.ReadKey();
        }
    }
}
