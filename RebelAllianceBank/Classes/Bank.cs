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

        TaskManager manager = new TaskManager();
        public void Run()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Task.Run(() => manager.Start());

            FileHandler fh = new FileHandler();
            users = new List<IUser>(fh.LoadUsersWithAccountAndLoans());
            bool run = true;
            while (run)
            {
                int choice = 0;
                List<string> options = [ "Login", "Avsluta" ];
                choice = MarkdownUtils.HighLightChoiceWithMarkdown(false, new[] {"Meny"}, options, inData: option => new[] { option });
                switch (choice)
                {
                    case 0:
                        Login();
                        if (currentUser is Admin)
                        {
                            var adminMenu = new AdminMenu(currentUser, users);
                            adminMenu.Show();
                        }
                        else
                        {
                            var customerMenu = new CustomerMenu(currentUser, users);
                            customerMenu.Show();
                        }
                        break;
                    case 1:
                        run = false;
                        break;
                }
            }
            manager.Stop();
            fh.WriteUsersAndAssociatedData(users);
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
                Logo();
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
                        Logo();
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

        /// <summary>
        /// Method that runs function to unlock a locked user.
        /// </summary>
        public void UnlockUser()
        {
            while (true)
            {
                Console.WriteLine("Skriv användarnamnet av den användare du vill låsa upp. Skriv exit om du vill gå" +
                    " tillbaka till menyn");
                string usernameInput = Console.ReadLine();
                bool correctInput = false;
                bool notLockedUser = false;

                //Checks if user wants to exit from function and breaks loop if exit is inputted.
                if (usernameInput.ToUpper().Equals("EXIT"))
                {
                    break;
                }

                foreach (var user in users)
                {
                    //Checks if users username is the inputted username and checks if that user is locked. If not tells
                    // current user that that useraccount isn't locked and waits for a key press to go back to input.
                    if (user.PersonalNum.Equals(usernameInput) && user.LoginLock == false)
                    {
                        Console.WriteLine("Användaren är inte låst. Kolla om du skrivit rätt användarnamn. Tryck på valfri rangent" +
                            " för att gå vidare.");
                        correctInput = true;
                        notLockedUser = true;
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                    //Checks if users username is the inputted username and unlocks that useraccount if it is and it is
                    //locked.
                    else if (user.PersonalNum.Equals(usernameInput))
                    {
                        user.LoginLock = false;
                        Console.WriteLine("Användaren har låsts upp. Tryck på valfri tangent för att gå vidare.");
                        correctInput = true;
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    }
                }

                //Continues the loop if a correct username was inputted but that useraccount wasn't locked.
                if (correctInput && notLockedUser)
                {
                    continue;
                }
                else if (correctInput)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Det finns ingen användare med det användarnamnet.tryck på " +
                        "valfri tangent för att gå tillbaka och försök igen.");
                }
            }

        }
        public void Logo()
        {
            string[] ascii1 = {
            " 888888ba   .d888888            888888ba                    dP       ",
            " 88    `8b d8'    88            88    `8b                   88       ",
            "a88aaaa8P' 88aaaaa88a          a88aaaa8P' .d8888b. 88d888b. 88  .dP  ",
            " 88   `8b. 88     88  88888888  88   `8b. 88'  `88 88'  `88 88888\"   ",
            " 88     88 88     88            88    .88 88.  .88 88    88 88  `8b. ",
            " dP     dP 88     88            88888888P `88888P8 dP    dP dP   `YP ",
            "ooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo"
        };

            string[] ascii2 = {
            "                                                                       ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢀⣴⣦⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀",
            "                                                                       ⠀⠀⠀⠀⡠⠀⠀⠀⠀⠀⢰⣿⣿⣿⣿⠆⠀⠀⠀⠀⠀⢄⡀⠀⠀⠀⠀⠀",
            "                                                                       ⠀⠀⣴⡟⠀⠀⠀⠀⣰⣦⣀⢻⣿⣿⡏⣀⣴⣄⠀⠀⠀⠀⢻⣦⡀⠀⠀⠀",
            "                                                                       ⢠⣾⡿⠀⠀⠀⠀⠈⠛⢿⣿⣿⣿⣿⣿⣿⡿⠛⠁⠀⠀⠀⠀⢻⣿⣄⠀⠀",
            "                                                                      ⢠⣿⣿⠇⠀⠀⠀⠀⠀⠀⠈⢿⣿⣿⣿⣿⡟⠀⠀⠀⠀⠀⠀⠀⠘⣿⣿⡆⠀",
            "                                                                      ⣿⣿⣿⡀⠀⠀⠀⠀⠀⠀⠀⠘⣿⣿⣿⣿⠃⠀⠀⠀⠀⠀⠀⠀⢀⣿⣿⣿⠀",
            "                                                                     ⢸⣿⣿⣿⣇⠀⠀⠀⠀⠀⠀⠀⠀⣿⣿⣿⣿⠀⠀⠀⠀⠀⠀⠀⠀⣼⣿⣿⣿⡇",
            "                                                                     ⢸⣿⣿⣿⣿⣦⠀⠀⠀⠀⠀⠀⢰⣿⣿⣿⣿⡄⠀⠀⠀⠀⠀⢀⣼⣿⣿⣿⣿⡇",
            "⢸⣿⣿⣿⣿⣿⣷⣦⣀⣀⣀⣴⣿⣿⣿⣿⣿⣿⣤⣀⣀⣀⣴⣿⣿⣿⣿⣿⣿⡇",
            "⠀⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠁",
            "⠀⠸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠏⠀",
            "⠀⠀⠙⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠏⠀⠀",
            "⠀⠀⠀⠈⠻⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠋⠀⠀⠀",
            "⠀⠀⠀⠀⠀⠈⠻⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⠿⠋⠀⠀⠀⠀⠀",
            "⠀⠀⠀⠀⠀⠀⠀⠀⠈⠙⠛⠿⠿⣿⣿⣿⣿⡿⠿⠟⠛⠉⠀⠀⠀⠀⠀⠀⠀⠀"
        };

            int maxLines = Math.Max(ascii1.Length, ascii2.Length);
            for (int i = 0; i < maxLines; i++)
            {
                if (i > 7)
                {
                    int j = i - 8;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(ascii1[j]);
                }

                // Print the second ASCII art in red
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(ascii2[i]);
            }

            Console.ResetColor();
        }
    }
}