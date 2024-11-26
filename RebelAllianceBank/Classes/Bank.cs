using RebelAllianceBank.Interfaces;

namespace RebelAllianceBank.Classes
{
    public class Bank
    {
        List<IUser> users = new List<IUser>() { new Admin("FullAccessLogin", "02492512") };
        IUser? currentUser;

        public void Run()
        {
            Login();
        }

        public void Login()
        {
            while (true)
            {
                Console.WriteLine("Välkommen till Rebel Alliance Bank. Vänligen skriv ditt användarnamn.");
                string? usernameInput = Console.ReadLine();
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
                    while (true)
                    {
                        Console.WriteLine($"God dag {currentUser.Username}. Vänligen skriv ditt lösenord.");
                        string? passwordInput = Console.ReadLine();

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
                       
                        if (correctPass)
                        {
                            break;
                        }
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

        public static void AdminMenu()
        {
            bool runAdminMenu = true;

            while (runAdminMenu)
            {
                Console.Clear();

                Console.Write("ADMIN\n" +
                              "[1] Skapa användare\n" +
                              "[2] Ändra växelkurs\n" +
                              "[3] Lås upp användarkonto???\n" +
                              "[4] Logga ut\n" +
                              "\n" +
                              "Menyval: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Skapa användare");
                        Console.ReadKey(); //Ta ev bort sen när det finns en metod
                        break;
                    case "2":
                        Console.WriteLine("Ändra växelkurs");
                        Console.ReadKey(); //Ta ev bort sen när det finns en metod
                        break;
                    case "3":
                        Console.WriteLine("Lås upp användarkonto???");
                        Console.ReadKey(); //Ta ev bort sen när det finns en metod
                        break;
                    case "4":
                        runAdminMenu = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Felaktig input! Tryck enter och försök igen!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static void CustomerMenu()
        {
            bool runCustomerMenu = true;

            while (runCustomerMenu)
            {
                Console.Clear();
                Console.Write($"Välkommen {"John Doe"}!\n" +
                              $"\n" +
                              $"HUVUDMENY:\n" +
                              $"[1] Konton\n" +
                              $"[2] Betala/Överföra\n" +
                              $"[3] Lån\n" +
                              $"[4] Logga ut\n" +
                              $"\n" +
                              $"Menyval: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CustomerMenuAccounts();
                        break;
                    case "2":
                        CustomerMenuTransaction();
                        break;
                    case "3":
                        CustomerMenuLoan();
                        break;
                    case "4":
                        runCustomerMenu = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Felaktig input! Tryck enter och försök igen!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void CustomerMenuAccounts()
        {
            bool runCustomerMenuAccounts = true;

            while (runCustomerMenuAccounts)
            {
                Console.Clear();

                Console.Write("KONTON:\n" +
                              "[1] Se över mina konton\n" +
                              "[2] Öppna nytt konto\n" +
                              "[3] Återgå till huvudmenyn\n" +
                              "\n" +
                              "Menyval: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Se över mina konton");
                        Console.ReadKey(); //Ta ev bort sen när det finns en metod
                        break;
                    case "2":
                        Console.WriteLine("Öppna nytt konto");
                        Console.ReadKey(); //Ta ev bort sen när det finns en metod
                        break;
                    case "3":
                        runCustomerMenuAccounts = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Felaktig input! Tryck enter och försök igen!");
                        Console.ReadKey();
                        break;
                }
            }
        }
        private static void CustomerMenuTransaction()
        {
            bool runCustomerMenuTransaction = true;

            while (runCustomerMenuTransaction)
            {
                Console.Clear();

                Console.Write("BETALA/ÖVERFÖRA:\n" +
                              "[1] Ny överföring\n" +
                              "[2] Ny betalning\n" +
                              "[3] Återgå till huvudmenyn\n" +
                              "\n" +
                              "Menyval: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Ny överföring");
                        Console.ReadKey(); //Ta ev bort sen när det finns en metod
                        break;
                    case "2":
                        Console.WriteLine("Ny betalning");
                        Console.ReadKey(); //Ta ev bort sen när det finns en metod
                        break;
                    case "3":
                        runCustomerMenuTransaction = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Felaktig input! Tryck enter och försök igen!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void CustomerMenuLoan()
        {
            bool runCustomerMenuLoan = true;

            while (runCustomerMenuLoan)
            {
                Console.Clear();

                Console.Write("LÅN:\n" +
                              "[1] Mina lån\n" +
                              "[2] Ansök om nytt lån\n" +
                              "[3] Återgå till huvudmenyn\n" +
                              "\n" +
                              "Menyval: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Mina lån");
                        Console.ReadKey(); //Ta ev bort sen när det finns en metod
                        break;
                    case "2":
                        Console.WriteLine("Ansök om nytt lån");
                        Console.ReadKey(); //Ta ev bort sen när det finns en metod
                        break;
                    case "3":
                        runCustomerMenuLoan = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Felaktig input! Tryck enter och försök igen!");
                        Console.ReadKey();
                        break;
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

                if (!methodRun)
                {
                    break;
                }
                else if (validInput)
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

            while (methodRun)
            {
                Console.WriteLine("Skriv användarnamnet på användaren som ska skapas." +
                    " Användarnamnet måste vara minst 5 symboler. Skriv exit om du vill gå tillbaka till menyn.");
                string input = Console.ReadLine();

                if (input.ToUpper().Equals("EXIT"))
                {
                    methodRun = false;
                    break;
                }
                else if (input.Length < 5)
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

            while (methodRun)
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

            if (methodRun)
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