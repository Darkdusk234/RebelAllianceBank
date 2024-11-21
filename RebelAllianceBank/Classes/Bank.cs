using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Classes
{
    public class Bank
    {
        List<IUser> users = new List<IUser>() { new Admin("FullAccessLogin", "02492512") };
        IUser currentUser;

        public void Run()
        {
            Login();
        }

        public void Login()
        {
            while (true)
            {
                Console.WriteLine("Välkommen till Rebel Alliance Bank. Vänligen skriv ditt användarnamn.");
                string input = Console.ReadLine();
                bool correctUser = false;
                bool correctPass = false;

                foreach (var user in users)
                {
                    if (user.Username.Equals(input))
                    {
                        currentUser = user;
                        correctUser = true;
                        break;
                    }
                }

                if (correctUser)
                {
                    while(true)
                    {
                        Console.WriteLine($"God dag {currentUser.Username}. Vänligen skriv ditt lösenord.");
                        input = Console.ReadLine();

                        if (currentUser.Password.Equals(input))
                        {
                            correctPass = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Felaktigt lösenord. Tryck på valfri tangent för att försöka igen.");
                            Console.ReadKey();
                            Console.Clear();
                        }
                    }

                    if(correctPass)
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Det finns ingen användare med det användarnamnet. Tryck på valfri " +
                        "tangent för att gå tillbaka och försöka igen.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }
    }
}
