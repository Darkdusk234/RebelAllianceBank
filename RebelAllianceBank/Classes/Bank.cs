using RebelAllianceBank.Interfaces;

namespace RebelAllianceBank.Classes
{
    public class Bank
    {
        public static void Run()
        {
            List<IUser> users = new List<IUser>();
            FileHandler run = new FileHandler();
            users = run.ReadUsersFromFile();
            run.WriteUserToFile("Lars", "54321", false);
        }
    }
}
