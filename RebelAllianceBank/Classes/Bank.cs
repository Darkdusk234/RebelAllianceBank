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
                string usernameInput = Console.ReadLine();
                bool correctUser = false;
                bool correctPass = false;

                foreach (var user in users)
                {
                    if (user.Username.Equals(usernameInput))
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
                        string passwordInput = Console.ReadLine();

                        if (currentUser.Password.Equals(passwordInput))
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

        public void CreateUser()
        {
            string userType = "";
            string username = "";
            string password = "";
            bool methodRun = true;
            while (methodRun)
            {
                Console.WriteLine("Vilken typ av användare vill du skapa." +
                    "\n1. Kund" +
                    "\n2. Admin" +
                    "\n3. Gå tillbak till menyn.");
                string input = Console.ReadLine();
                bool validInput = false;

                switch (input)
                {
                    case "1":
                        userType = "Kund";
                        validInput = true;
                        break;
                    case "2":
                        userType = "Admin";
                        validInput = true;
                        break;
                    case "3":
                        methodRun = false;
                        break;
                    default:
                        break;
                }

                if(!methodRun)
                {
                    break;
                }
                else if(validInput)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Det är inte et giltligt val. Skriv siffran av det val du vill välja." +
                        " Tryck på valfri tangent tangent för att gå tillbaka till valen.");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            while(methodRun)
            {
                Console.WriteLine("Skriv användarnamnet på användaren som ska skapas." +
                    " Användarnamnet måste vara minst 5 symboler. Skriv exit om du vill gå tillbaka till menyn.");
                string input = Console.ReadLine();

                if(input.ToUpper().Equals("EXIT"))
                {
                    methodRun = false;
                    break;
                }
                else if(input.Length < 5)
                {
                    Console.WriteLine("Användarnamnet måste vara 5 symboler eller längre. Tryck på valfri" +
                        " tangent för att gå tillbaka och försök igen.");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
                else
                {
                    username = input;
                    break;
                }
            }

            while(methodRun)
            {
                Console.WriteLine("Skriv lösenordet för användaren som ska skapas. Lösenordet måste vara minst 5 symboler.");
                string input = Console.ReadLine();

                if (input.ToUpper().Equals("EXIT"))
                {
                    methodRun = false;
                    break;
                }
                else if (input.Length < 5)
                {
                    Console.WriteLine("Lösenordet måste vara 5 symboler eller längre. Tryck på valfri" +
                        " tangent för att gå tillbaka och försök igen.");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }
                else
                {
                    password = input;
                    break;
                }
            }

            if(methodRun)
            {
                if (userType.Equals("Kund"))
                {
                    users.Add(new Customer(username, password));
                }
                else if (userType.Equals("Admin"))
                {
                    users.Add(new Admin(username, password));
                }

                Console.WriteLine("Användare skapad. Tryck på valfri tangent för att gå tillbaka till menyn.");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
