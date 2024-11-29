﻿using RebelAllianceBank.Interfaces;

namespace RebelAllianceBank.Classes
{
    public class Bank
    {
        List<IUser> users = new List<IUser>() { new Admin("FullAccessLogin", "02492512", "Admin", "Adminson") };
        IUser? currentUser;

        public void Run()
        {
            Login();
        }

        /// <summary>
        /// Method that runs the login system and loops until successful login was done.
        /// </summary>
        public void Login()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Välkommen till Rebel Alliance Bank. Vänligen skriv ditt personnummer.");
                string? usernameInput = Console.ReadLine();
                bool correctUser = false;
                bool correctPass = false;
                bool userLocked = false;
                int tries = 0;

                //Checks if inputted username is a valid username. Also checks if that user is locked from logging in.
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
                else if(!userLocked)
                {
                    Console.WriteLine("Det finns ingen användare med det användarnamnet. Tryck på valfri " +
                        "tangent för att gå tillbaka och försöka igen.");
                    Console.ReadKey();
                }

                if(correctPass)
                {
                    break;
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

        /// <summary>
        /// Method to create new user
        /// </summary>
        public void CreateUser()
        {
            string userType = "";
            string password = "";

            string forename = "";
            string surname = "";

            string personalNum = "";
            string birthYear = "";
            string birthMonth = "";
            string birthDay = "";
            string lastDigits = "";

            int daysInBirthMonth = 0;

            bool methodRun = true;
            //Takes in user input about what type of user that is to be created.
            while (methodRun)
            {
                Console.Clear();
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
                    continue;
                }
            }

            //Takes in all required data from user input that is needed to put together the user that is to be created's personal number
            while (methodRun)
            {
                //Takes in birth year
                while (methodRun)
                {
                    Console.Clear();
                    Console.WriteLine("Skriv in det år som användaren föddes. Skriv i formatet XXXX." +
                        " Skriv avbryt om du vill gå tillbaka till menyn.");
                    string yearInput = Console.ReadLine();

                    if (yearInput.ToUpper().Equals("AVBRYT"))
                    {
                        methodRun = false;
                        break;
                    }
                    else if (!int.TryParse(yearInput, out int inputInt))
                    {
                        Console.WriteLine("Använd enbart siffror!" +
                            " Tryck på valfri tangent för att gå tillbaka och försök igen.");
                        Console.ReadKey();
                        continue;
                    }
                    else if (yearInput.Length < 4 || yearInput.Length > 4)
                    {
                        Console.WriteLine("Fel format! Skriv födelseåret i formatet XXXX. Tryck på valfri tangent för" +
                            " att gå tillbaka och försök igen.");
                        Console.ReadKey();
                        continue;
                    }
                    //Checks if the year inputted is bigger than or equal to current year. Also checks if birthyear is more than a 100 years ago
                    else if (inputInt >= DateTime.Now.Year || inputInt < (DateTime.Now.Year - 100))
                    {
                        Console.WriteLine($"Orimligt födelseår! Skriv ett rimligt födelseår som är {DateTime.Now.Year - 100}" +
                            " eller senare. Tryck på valfri tangent för att gå tillbaka och försök igen.");
                        Console.ReadKey();
                        continue;
                    }
                    else
                    {
                        birthYear = yearInput;
                        break;
                    }
                }

                //Takes in birth month
                while (methodRun)
                {
                    Console.Clear();
                    Console.WriteLine("Skriv vilken månad användaren föddes. Skriv i formatet XX." +
                       " Skriv avbryt om du vill gå tillbaka till menyn.");
                    string monthInput = Console.ReadLine();

                    if (monthInput.ToUpper().Equals("AVBRYT"))
                    {
                        methodRun = false;
                        break;
                    }
                    else if (!int.TryParse(monthInput, out int inputInt))
                    {
                        Console.WriteLine("Använd enbart siffror!" +
                           " Tryck på valfri tangent för att gå tillbaka och försök igen.");
                        Console.ReadKey();
                        continue;
                    }
                    else if (monthInput.Length < 2 || monthInput.Length > 2)
                    {
                        Console.WriteLine("Fel format! Skriv månaden i formatet XX. Tryck på valfri tangent för" +
                            " att gå tillbaka och försök igen.");
                        Console.ReadKey();
                        continue;
                    }
                    else if (inputInt <= 0 || inputInt < 12)
                    {
                        Console.WriteLine("Det finns ingen månad motsvarande den siffran!" +
                            " Tryck på valfri tangent för att gå tillbaka och försök igen.");
                        Console.ReadKey();
                        continue;
                    }
                    else
                    {
                        birthMonth = monthInput;
                        //Sets how many days are in birth month to daysInBirthMonth variable which is used in next while loop
                        switch (inputInt)
                        {
                            case 1:
                                daysInBirthMonth = 31;
                                break;
                            case 2:
                                daysInBirthMonth = 29;
                                break;
                            case 3:
                                daysInBirthMonth = 31;
                                break;
                            case 4:
                                daysInBirthMonth = 30;
                                break;
                            case 5:
                                daysInBirthMonth = 31;
                                break;
                            case 6:
                                daysInBirthMonth = 30;
                                break;
                            case 7:
                                daysInBirthMonth = 31;
                                break;
                            case 8:
                                daysInBirthMonth = 31;
                                break;
                            case 9:
                                daysInBirthMonth = 30;
                                break;
                            case 10:
                                daysInBirthMonth = 31;
                                break;
                            case 11:
                                daysInBirthMonth = 30;
                                break;
                            case 12:
                                daysInBirthMonth = 31;
                                break;
                        }
                        break;
                    }
                }

                //Takes in birth day
                while (methodRun)
                {
                    Console.Clear();
                    Console.WriteLine("Skriv vilken dag användaren föddes. Skriv i formatet XX." +
                       " Skriv avbryt om du vill gå tillbaka till menyn.");
                    string dayInput = Console.ReadLine();

                    if (dayInput.ToUpper().Equals("AVBRYT"))
                    {
                        methodRun = false;
                        break;
                    }
                    else if (!int.TryParse(dayInput, out int inputInt))
                    {
                        Console.WriteLine("Använd enbart siffror!" +
                           " Tryck på valfri tangent för att gå tillbaka och försök igen.");
                        Console.ReadKey();
                        continue;
                    }
                    else if (dayInput.Length < 2 || dayInput.Length > 2)
                    {
                        Console.WriteLine("Fel format! Skriv dagen i formatet XX. Tryck på valfri tangent för" +
                            " att gå tillbaka och försök igen.");
                        Console.ReadKey();
                        continue;
                    }
                    //Checks if inputted day is valid for birth month
                    else if (inputInt <= 0 || inputInt > daysInBirthMonth)
                    {
                        Console.WriteLine("Den dagen finns inte i födelse månaden!" +
                           " Tryck på valfri tangent för att gå tillbaka och försök igen.");
                        Console.ReadKey();
                    }
                    else
                    {
                        birthDay = dayInput;
                        break;
                    }
                }

                //Takes in the last 4 digits of the personal number
                while (methodRun)
                {
                    Console.Clear();
                    Console.WriteLine("Skriv de 4 sista siffrorna på användarens personnummer. Skriv i formatet XXXX." +
                       " Skriv avbryt om du vill gå tillbaka till menyn.");
                    string lastDigitsInput = Console.ReadLine();

                    if (lastDigitsInput.ToUpper().Equals("AVBRYT"))
                    {
                        methodRun = false;
                        break;
                    }
                    else if (!int.TryParse(lastDigitsInput, out int inputInt))
                    {
                        Console.WriteLine("Använd enbart siffror!" +
                           " Tryck på valfri tangent för att gå tillbaka och försök igen.");
                        Console.ReadKey();
                        continue;
                    }
                    else if (lastDigitsInput.Length < 4 || lastDigitsInput.Length > 4)
                    {
                        Console.WriteLine("Fel format! Skriv dagen i formatet XX. Tryck på valfri tangent för" +
                            " att gå tillbaka och försök igen.");
                        Console.ReadKey();
                        continue;
                    }
                    else
                    {
                        lastDigits = lastDigitsInput;
                        break;
                    }
                }

                //Uses the info inputted and puts it together in the correct format for personal numbers
                personalNum = personalNum.Insert(0, birthYear.Substring(2));
                personalNum = personalNum.Insert(2, birthMonth);
                personalNum = personalNum.Insert(4, birthDay);
                personalNum = personalNum.Insert(6, lastDigits);

                bool correctPersonalNum = false;

                //Allows user to check if the personal number is correct
                while (methodRun)
                {
                    Console.Clear();
                    Console.WriteLine("Personnummret är skrivit i formatet, ÅÅMMDDXXXX. Är detta personnummer korrect" +
                        " för den användaren du vill skapa? ja/nej skriv avbryt om du vill gå tillbaka till menyn." +
                        $"\n{personalNum}");
                    string userInput = Console.ReadLine();

                    if (userInput.ToUpper().Equals("AVBRYT"))
                    {
                        methodRun = false;
                        break;
                    }
                    else if (userInput.ToUpper().Equals("JA") || userInput.ToUpper().Equals("J"))
                    {
                        correctPersonalNum = true;
                        break;
                    }
                    else if (userInput.ToUpper().Equals("NEJ") || userInput.ToUpper().Equals("N"))
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Skriv endast Ja, Nej eller Avbryt. Tryck på valfri tangent för att gå tillbaka.");
                        Console.ReadKey();
                        continue;
                    }
                }

                if(correctPersonalNum)
                {
                    break;
                }
            }

            //Takes in the the password for the user to be created.
            while (methodRun)
            {
                Console.Clear();
                Console.WriteLine("Skriv lösenordet för användaren som ska skapas. Lösenordet måste vara minst 5 symboler." +
                    " Skriv avbryt om du vill gå tillbaka till menyn.");
                string input = Console.ReadLine();

                if (input.ToUpper().Equals("AVBRYT"))
                {
                    methodRun = false;
                    break;
                }
                else if (input.Length < 5)
                {
                    Console.WriteLine("Lösenordet måste vara 5 symboler eller längre. Tryck på valfri" +
                        " tangent för att gå tillbaka och försök igen.");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    password = input;
                    break;
                }
            }

            //Takes in forename of the user to be created.
            while (methodRun)
            {
                Console.Clear();
                Console.WriteLine("Skriv Förnamnet för användaren som ska skapas. Skriv avbryt om du vill gå tillbaka till menyn.");
                string input = Console.ReadLine();

                if (input.ToUpper().Equals("AVBRYT"))
                {
                    methodRun = false;
                    break;
                }
                else if(input.Length <= 1)
                {
                    Console.WriteLine("Förnamnet måste vara minst 2 bokstäver långt. Tryck på valfri tangent för att" +
                        " gå tillbaka och försök igen.");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    forename = input;
                    break;
                }
            }

            //Takes in the surname of the user to be created.
            while (methodRun)
            {
                Console.Clear();
                Console.WriteLine("Skriv efternamnet för användaren som ska skapas. Skriv avbryt om du vill gå tillbaka till menyn.");
                string input = Console.ReadLine();

                if (input.ToUpper().Equals("AVBRYT"))
                {
                    methodRun = false;
                    break;
                }
                else if (input.Length <= 1)
                {
                    Console.WriteLine("Efternamnet måste vara minst 2 bokstäver långt. Tryck på valfri tangent för att" +
                        " gå tillbaka och försök igen.");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    surname = input;
                    break;
                }
            }

            //Creates the new user using the inputted data.
            if (methodRun)
            {
                if (userType.Equals("Kund"))
                {
                    users.Add(new Customer(personalNum, password, surname, forename));
                }
                else if (userType.Equals("Admin"))
                {
                    users.Add(new Admin(personalNum, password, surname, forename));
                }

                Console.Clear();
                Console.WriteLine("Användare skapad. Tryck på valfri tangent för att gå tillbaka till menyn.");
                Console.ReadKey();
                Console.Clear();
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
                if(usernameInput.ToUpper().Equals("EXIT"))
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
                if(correctInput && notLockedUser)
                {
                    continue;
                }
                else if(correctInput)
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
    }
}