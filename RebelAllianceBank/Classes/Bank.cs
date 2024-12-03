using RebelAllianceBank.Interfaces;
using RebelAllianceBank.Users;
using RebelAllianceBank.utils;
using RebelAllianceBank.Menu;

namespace RebelAllianceBank.Classes
{
    public class Bank
    {
        IUser? currentUser;
        List<IUser> users;
        public static ExchangeRate exchangeRate = new ExchangeRate(); 
        
        public void Run()
        {
            FileHandler fh = new FileHandler();
            users = new List<IUser>(fh.ReadUserAndAccounts());
            bool run = true;
            while (run)
            {
                Login();
                if (currentUser is Admin)
                {
                    var adminMenu = new AdminMenu(currentUser, users);
                    
                    
                    adminMenu.Show();
                }
                else
                {
                    var customerMenu = new CustomerMenu(currentUser);
                    customerMenu.Show();
                }
            }
        }

        //public void Login()
        //{
        //    while (true)
        //    {
        //        Console.Clear();
        //        Console.WriteLine("Välkommen till Rebel Alliance Bank. Vänligen ange ditt personnummer.");
        //        Console.Write("Personnummer: ");
        //        string? usernameInput = Console.ReadLine();

        //        if (string.IsNullOrWhiteSpace(usernameInput))
        //        {
        //            Console.WriteLine("Personnummer får inte vara tomt. Försök igen.");
        //            Console.ReadKey();
        //            continue;
        //        }

        //        currentUser = users.FirstOrDefault(user => user.PersonalNum == usernameInput);
        //        if (currentUser != null)
        //        {
        //            if (currentUser.LoginLock)
        //            {
        //                Console.WriteLine("Användaren är låst. Kontakta administratör.\n" +
        //                                  "Tryck på enter för att fortsätta");
        //                while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
        //            }
        //            else if (Authenticate(currentUser))
        //            {
        //                break;
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("Användarnamnet hittades inte. Tryck på enter för att försöka igen.");
        //            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
        //        }
        //    }
        //}
        //public bool Authenticate(IUser user)
        //{
        //    int tries = 3;
        //    while (tries > 0)
        //    {
        //        Console.Clear();
        //        Console.WriteLine($"God dag {user.Surname}. Vänligen skriv ditt lösenord. Du har {tries} försök kvar.");
        //        string? passwordInput = Console.ReadLine();

        //        if (user.Password == passwordInput)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            tries--;
        //            Console.WriteLine($"Felaktigt lösenord. Du har {tries} försök kvar.");
        //            if (tries == 0)
        //            {
        //                Console.WriteLine("Användaren är nu låst. Kontakta administratör!\n" +
        //                    "Tryck på enter för att återgå");
        //                while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
        //                user.LoginLock = true;
        //                return false;
        //            }
        //            Console.WriteLine($"Tryck på enter för att försöka igen.");
        //            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
        //        }
        //    }
        //    return false;
        //}


        /// <summary>
        /// Method that runs the login system and loops until successful login was done.
        /// </summary>
        public void Login()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Välkommen till Rebel Alliance Bank. Vänligen ange ditt personnummer.");
                Console.Write("Personnummer: ");
                string? usernameInput = Console.ReadLine();
                bool correctUser = false;
                bool correctPass = false;
                bool userLocked = false;
                int tries = 0;

                //Checks if inputted username is a valid username.Also checks if that user is locked from logging in.
                foreach (var user in users)
                {
                    if (user.PersonalNum.Equals(usernameInput) && user.LoginLock == true)
                    {
                        Console.WriteLine("Användaren är låst. Kontakta admin för att låsa upp användaren. Tryck på" +
                            " valfri tangent för att gå tillbaka.");
                        userLocked = true;
                        Console.ReadKey();
                        break;
                    }
                    else if (user.PersonalNum.Equals(usernameInput))
                    {
                        currentUser = user;
                        correctUser = true;
                        break;
                    }
                }

                if (correctUser)
                {
                    tries = 0;

                    //Loops until correct password is inputted or if wrong password is inputted 3 times.
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine($"God dag {currentUser.Surname}. Vänligen skriv ditt lösenord.");
                        string? passwordInput = Console.ReadLine();

                        if (currentUser.Password.Equals(passwordInput))
                        {
                            correctPass = true;
                            break;
                        }
                        else
                        {
                            tries++;
                            if (tries == 3)
                            {
                                Console.WriteLine("Inlogging misslyckades 3 gånger i rad. Användaren är nu låst." +
                                    " Kontakta en admin för att låsa upp kontot.");
                                currentUser.LoginLock = true;
                                Console.ReadKey();
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Felaktigt lösenord. Tryck på valfri tangent för att försöka igen.");
                                Console.ReadKey();
                            }
                        }

                        if (correctPass)
                        {
                            break;
                        }
                    }
                }
                else if (!userLocked)
                {
                    Console.WriteLine("Det finns ingen användare med det användarnamnet. Tryck på valfri " +
                        "tangent för att gå tillbaka och försöka igen.");
                    Console.ReadKey();
                }

                if (correctPass)
                {
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
    }
}