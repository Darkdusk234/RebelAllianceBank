using RebelAllianceBank.Interfaces;
using RebelAllianceBank.utils;
using System.Globalization;
using RebelAllianceBank.Classes;

namespace RebelAllianceBank.Users;

public class Admin : IUser
{
    public int ID { get; set; }
    public string PersonalNum { get; set; }
    public string Password { get; set; }
    public string Surname { get; set; }
    public string Forename { get; set; }
    public bool LoginLock { get; set; } = false;

    public Admin() { }
    public Admin(string pNum, string password, string surname, string forename)
    {
        PersonalNum = pNum;
        Password = password;
        Surname = surname;
        Forename = forename;
    }

    /// <summary>
    /// A method used by the admin to update all currencies. 
    /// </summary>
    public void UpDateCurrency()
    {
        bool runLoop = true;

        while (runLoop)
        {
            Console.Clear();

            // Console.WriteLine("UPPDATERA VÄXELKURS: \n" +
            //                   "[1] Instruktioner\n" +
            //                   "[2] Skriv ut växelkurser\n" +
            //                   "[3] Ladda upp nya växelkurser\n" +
            //                   "[4] Avbryt och återgå till föregående meny");

            // string choice = Console.ReadLine();

            List<string> options = ["Instruktioner", "Skriv ut växelkurse", "Ladda upp nya växelkurser", "Avbryt och återgå till föregående meny"];

            int choice = MarkdownUtils.HighLightChoiceWithMarkdown(
                    cancel: false,
                    columnHeaders: new[] { "Meny val" },
                    filterData: new List<string>(options),
                    inData: option => new[] { option });

            switch (choice)
            {
                case 0:
                    Console.Clear();
                    Console.WriteLine("INSTRUKTIONER UPPDATERA VÄXELKURS\n\n" +
                                      "För att ladda upp växelkurs gör följande:\n" +
                                      "1. Gå till länk: https://www.ecb.europa.eu/stats" +
                                      "/policy_and_exchange_rates/euro_reference_exchange_rates/html/index.en.html\n" +
                                      "2. Ladda ner en csv-fil med de senaste växelkurserna (Finns under \"Downloads\" " +
                                      "och \"Last reference rates\")\n" +
                                      "3. Kopiera och klistra in hela första raden med valuta-namn (inkl \"Date\")\n" +
                                      "4. Kopiera och klistra in hela andra raden med växelkurser (inklusive datum)\n");

                    Console.WriteLine("Tryck enter när du är redo att fortsätta");
                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                    break;
                case 1:
                    Console.Clear();
                    Bank.exchangeRate.PrintAllRates();
                    Console.WriteLine("\nTryck enter när du är redo att fortsätta");
                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                    break;
                case 2:
                    EnumsExchangeRate input = Bank.exchangeRate.PasteAndMatchExchangeRates();
                    if (input == EnumsExchangeRate.correct)
                    {
                        Bank.exchangeRate.AddExchangeRates();
                        bool correctUpdate = Bank.exchangeRate.CheckAddedExchangeRates();
                        if (correctUpdate)
                        {
                            runLoop = false;
                        }
                        else
                        {
                            Console.WriteLine("Tryck enter för att återgå till menyn och göra ett nytt försök");
                            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                        }
                    }
                    else if (input == EnumsExchangeRate.incorrect)
                    {
                        Console.Clear();
                        Console.WriteLine("Något blev inte helt rätt när du klistrade in dina rader. Läs instruktionerna" +
                                          " och försök sen igen! Tryck enter för att fortsätta!");
                        while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                    }
                    break;
                case 3:
                    runLoop = false;
                    break;
                default:
                    Console.WriteLine("Felaktig inmatning! Försök igen!");
                    break;
            }
        }
    }

    /// <summary>
    /// Method to create new user
    /// </summary>
    public void CreateUser(List<IUser> users)
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

        bool methodRun = true;
        //Takes in user input about what type of user that is to be created.
        while (methodRun)
        {
            Console.Clear();
            // Console.WriteLine("Vilken typ av användare vill du skapa." +
            //                   "\n1. Kund" +
            //                   "\n2. Admin" +
            //                   "\n3. Gå tillbak till menyn.");
            // string input = Console.ReadLine();
            List<string> options = ["Kund", "Admin", "Gå tillbak till menyn."];

            int choice = MarkdownUtils.HighLightChoiceWithMarkdown(
                    cancel: false,
                    columnHeaders: new[] { "Meny val" },
                    filterData: new List<string>(options),
                    inData: option => new[] { option });

            bool validInput = false;

            switch (choice)
            {
                case 0:
                    userType = "Kund";
                    validInput = true;
                    break;
                case 1:
                    userType = "Admin";
                    validInput = true;
                    break;
                case 2:
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
                else if (inputInt <= 0 || inputInt > 12)
                {
                    Console.WriteLine("Det finns ingen månad motsvarande den siffran!" +
                                      " Tryck på valfri tangent för att gå tillbaka och försök igen.");
                    Console.ReadKey();
                    continue;
                }
                else
                {
                    birthMonth = monthInput;
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
                string dateTime = birthYear;
                dateTime = dateTime.Insert(4, $"/{birthMonth}");
                dateTime = dateTime.Insert(7, $"/{dayInput}");
                Console.WriteLine(dateTime);

                if (dayInput.ToUpper().Equals("AVBRYT"))
                {
                    methodRun = false;
                    break;
                }
                else if (!int.TryParse(dayInput, out int unusedInt))
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
                else if (!DateTime.TryParseExact(dateTime, "yyyy/MM/dd", CultureInfo.InvariantCulture,
                             DateTimeStyles.None, out DateTime unused))
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

            if (correctPersonalNum)
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
            else if (input.Length <= 1)
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
    public void UnlockUser(List<IUser> users)
    {
        while (true)
        {
            Console.Clear();
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
}