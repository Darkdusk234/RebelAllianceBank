﻿using RebelAllianceBank.Interfaces;
using RebelAllianceBank.Users;
using RebelAllianceBank.Menu;
using RebelAllianceBank.utils;

namespace RebelAllianceBank.Other
{
    public class Bank
    {

        IUser? currentUser;
        List<IUser> users;
        //An instance of the exchangerate class for gathering all exchangerates and methods related to them 
        public static ExchangeRate exchangeRate = new ExchangeRate();
        public static int accountNumberCounter = 1; 
        public static Queue<Transaction> transactionQueue = new Queue<Transaction>(); 

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
        /// <summary>
        /// A method for running transactions in the transactionQueue()
        /// </summary>
        public static void RunTransactionsInQueue()
        {
            while (transactionQueue.Count > 0)
            {
                //The next transaction on the que is dequeued and saved to the variable nextInQueue
                var nextInQueue = transactionQueue.Dequeue();
                //set the timestamp-field in the transaction-instans to now (for the log)
                nextInQueue.Timestamp = DateTime.Now;
                
                //If it is deposit och a "loan-deposit", the balance will only be adjusted at the "To-account" and the 
                if (nextInQueue.AccountFrom == null)
                {
                    nextInQueue.AccountTo.Balance += nextInQueue.Amount;
                    //once the transaction is made, it is added to the transactionlog of the specific account. 
                    nextInQueue.AccountTo.AddToTransactionLog(nextInQueue);
                }
                //For transfer-methods, the balance of the "to-account" is increased while the "from-account" balance
                //is decreased. The amount of the Tp-account is also adjusted in regards to its currency. 
                else
                {
                    nextInQueue.AccountFrom.Balance -= nextInQueue.Amount;
                    nextInQueue.AccountTo.Balance += nextInQueue.Amount * exchangeRate.CalculateExchangeRate(
                        nextInQueue.AccountFrom.AccountCurrency,
                        nextInQueue.AccountTo.AccountCurrency
                    );
                    //once the transaction is made, it is added to the transactionlog of the specific accounts. 
                    nextInQueue.AccountFrom.AddToTransactionLog(nextInQueue);
                    nextInQueue.AccountTo.AddToTransactionLog(nextInQueue);
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