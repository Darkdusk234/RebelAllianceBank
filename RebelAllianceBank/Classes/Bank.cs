using RebelAllianceBank.Interfaces;

namespace RebelAllianceBank.Classes
{
    public class Bank
    {
        List<IUser> users;
        IUser? currentUser;

        public void Run()
        {
            FileHandler fh = new FileHandler();
            users = new List<IUser>(fh.ReadUser());
            Login();
            if (currentUser is Admin)
            {
                Menu.AdminMenu(users);
            }
            else
            {
                Menu.CustomerMenu(currentUser);
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

        //To Be Fixed -DO NOT USE
        //public void CreateUser()
        //{
        //    string userType = "";
        //    string username = "";
        //    string password = "";
        //    bool methodRun = true;
        //    while (methodRun)
        //    {
        //        Console.WriteLine("Vilken typ av användare vill du skapa." +
        //            "\n1. Kund" +
        //            "\n2. Admin" +
        //            "\n3. Gå tillbak till menyn.");
        //        string input = Console.ReadLine();
        //        bool validInput = false;

        //        switch (input)
        //        {
        //            case "1":
        //                userType = "Kund";
        //                validInput = true;
        //                break;
        //            case "2":
        //                userType = "Admin";
        //                validInput = true;
        //                break;
        //            case "3":
        //                methodRun = false;
        //                break;
        //            default:
        //                break;
        //        }

        //        if (!methodRun)
        //        {
        //            break;
        //        }
        //        else if (validInput)
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Det är inte et giltligt val. Skriv siffran av det val du vill välja." +
        //                " Tryck på valfri tangent tangent för att gå tillbaka till valen.");
        //            Console.ReadKey();
        //            Console.Clear();
        //        }
        //    }

        //    while (methodRun)
        //    {
        //        Console.WriteLine("Skriv användarnamnet på användaren som ska skapas." +
        //            " Användarnamnet måste vara minst 5 symboler. Skriv exit om du vill gå tillbaka till menyn.");
        //        string input = Console.ReadLine();

        //        if (input.ToUpper().Equals("EXIT"))
        //        {
        //            methodRun = false;
        //            break;
        //        }
        //        else if (input.Length < 5)
        //        {
        //            Console.WriteLine("Användarnamnet måste vara 5 symboler eller längre. Tryck på valfri" +
        //                " tangent för att gå tillbaka och försök igen.");
        //            Console.ReadKey();
        //            Console.Clear();
        //            continue;
        //        }
        //        else
        //        {
        //            username = input;
        //            break;
        //        }
        //    }

        //    while (methodRun)
        //    {
        //        Console.WriteLine("Skriv lösenordet för användaren som ska skapas. Lösenordet måste vara minst 5 symboler.");
        //        string input = Console.ReadLine();

        //        if (input.ToUpper().Equals("EXIT"))
        //        {
        //            methodRun = false;
        //            break;
        //        }
        //        else if (input.Length < 5)
        //        {
        //            Console.WriteLine("Lösenordet måste vara 5 symboler eller längre. Tryck på valfri" +
        //                " tangent för att gå tillbaka och försök igen.");
        //            Console.ReadKey();
        //            Console.Clear();
        //            continue;
        //        }
        //        else
        //        {
        //            password = input;
        //            break;
        //        }
        //    }

        //    if (methodRun)
        //    {
        //        if (userType.Equals("Kund"))
        //        {
        //            users.Add(new Customer(username, password));
        //        }
        //        else if (userType.Equals("Admin"))
        //        {
        //            users.Add(new Admin(username, password));
        //        }

        //        Console.WriteLine("Användare skapad. Tryck på valfri tangent för att gå tillbaka till menyn.");
        //        Console.ReadKey();
        //        Console.Clear();
        //    }
        //}

        /// <summary>
        /// Method that runs function to unlock a locked user.
        /// </summary>
        //public void UnlockUser()
        //{
        //    while (true)
        //    {
        //        Console.WriteLine("Skriv användarnamnet av den användare du vill låsa upp. Skriv exit om du vill gå" +
        //            " tillbaka till menyn");
        //        string usernameInput = Console.ReadLine();
        //        bool correctInput = false;
        //        bool notLockedUser = false;

        //        //Checks if user wants to exit from function and breaks loop if exit is inputted.
        //        if (usernameInput.ToUpper().Equals("EXIT"))
        //        {
        //            break;
        //        }

        //        foreach (var user in users)
        //        {
        //            //Checks if users username is the inputted username and checks if that user is locked. If not tells
        //            // current user that that useraccount isn't locked and waits for a key press to go back to input.
        //            if (user.PersonalNum.Equals(usernameInput) && user.LoginLock == false)
        //            {
        //                Console.WriteLine("Användaren är inte låst. Kolla om du skrivit rätt användarnamn. Tryck på valfri rangent" +
        //                    " för att gå vidare.");
        //                correctInput = true;
        //                notLockedUser = true;
        //                Console.ReadKey();
        //                Console.Clear();
        //                break;
        //            }
        //            //Checks if users username is the inputted username and unlocks that useraccount if it is and it is
        //            //locked.
        //            else if (user.PersonalNum.Equals(usernameInput))
        //            {
        //                user.LoginLock = false;
        //                Console.WriteLine("Användaren har låsts upp. Tryck på valfri tangent för att gå vidare.");
        //                correctInput = true;
        //                Console.ReadKey();
        //                Console.Clear();
        //                break;
        //            }
        //        }

        //        //Continues the loop if a correct username was inputted but that useraccount wasn't locked.
        //        if (correctInput && notLockedUser)
        //        {
        //            continue;
        //        }
        //        else if (correctInput)
        //        {
        //            break;
        //        }
        //        else
        //        {
        //            Console.WriteLine("Det finns ingen användare med det användarnamnet.tryck på " +
        //                "valfri tangent för att gå tillbaka och försök igen.");
        //        }
        //    }

        //}
    }
}