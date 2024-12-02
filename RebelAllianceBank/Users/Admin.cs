using RebelAllianceBank.Interfaces;
using System.Globalization;
using RebelAllianceBank.Classes;
using RebelAllianceBank.Users;

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
                dateTime = dateTime.Insert(3, $"/{birthMonth}");
                dateTime = dateTime.Insert(5, $"/{dayInput}");

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
}
