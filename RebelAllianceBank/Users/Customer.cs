using System.Linq;
using RebelAllianceBank.Interfaces;
using RebelAllianceBank.utils;
using RebelAllianceBank.Accounts;
using RebelAllianceBank.Enums;
using RebelAllianceBank.Classes;
using RebelAllianceBank.Menu;
namespace RebelAllianceBank.Users
{
    public class Customer : IUser
    {
        public int ID { get; set; }
        public string PersonalNum { get; set; }
        public string Password { get; set; }
        public string Surname { get; set; }
        public string Forename { get; set; }
        public bool LoginLock { get; set; } = false;
        
        private List<IBankAccount> _bankAccounts = new List<IBankAccount>();
        private List<Loan> _customerLoan = new List<Loan>();
        public Customer() { }
        public Customer(string pNum, string password, string surname, string forename)
        {
            PersonalNum = pNum;
            Password = password;
            Surname = surname;
            Forename = forename;
        }

        public List<IBankAccount> GetListBankAccount()
        {
            return _bankAccounts;
        }
        public List<Loan> GetListLoan()
        {
            return _customerLoan;
        }

        public void ShowBankAccounts()
        {
            Console.WriteLine("Konton");

            if (_bankAccounts.Count == 0)
            {
                Console.WriteLine("Du har inga konton att visa");
                return;
            }

            List<string> bodyKeys = [];
            foreach (var BankAccount in _bankAccounts)
            {
                bodyKeys.Add(BankAccount.AccountName);
                bodyKeys.Add(BankAccount.Balance.ToString("N2"));
                bodyKeys.Add(BankAccount.AccountCurrency);
            }
            Markdown.Table(["Konto Namn", "Saldo", "Valuta"], bodyKeys);
        }
        public void CreateAccount()
        {
            bool createAccount = false;

            do
            {
                Console.WriteLine("Vilket konto vill du skapa?\n");
                Console.WriteLine("1. Kreditkort");
                Console.WriteLine("2. ISK (investeringssparkonto");
                Console.WriteLine("3. Sparkonto");
                Console.WriteLine("4. Avsluta");
                string input = Console.ReadLine();
                int userChoice;
                bool isInt = int.TryParse(input, out userChoice);

                string accountName = "";
                string accountCurrency = "";

                if (isInt && userChoice == 4)
                {
                    break;
                }
                else if (isInt && userChoice > 0 && userChoice < 4)
                {
                    Console.Write("Vad vill du kalla kontot: ");
                    accountName = Console.ReadLine();
                    accountCurrency = Bank.exchangeRate.SetAccountCurrency();
                }

                switch (userChoice)
                {
                    case 1:
                        _bankAccounts.Add(new CardAccount(accountName, PersonalNum, accountCurrency));
                        createAccount = true;
                        break;
                    case 2:
                        _bankAccounts.Add(new ISK(accountName, PersonalNum, accountCurrency));
                        createAccount = true;
                        break;
                    case 3:
                        _bankAccounts.Add(new SavingsAccount(accountName, PersonalNum, accountCurrency));
                        createAccount = true;
                        break;
                    case 4:
                        createAccount = true;
                        break;
                    default:
                        Console.WriteLine("Fel inmatning, inget konto har skapats.\n" +
                                          "\n" +
                                          "Tryck enter för att fortsätta!");
                        while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                        Console.Clear();
                        createAccount = false;
                        break;
                }
            } while (createAccount == false);
        }
        /// <summary>
        /// A method to transfer from one of current users account to another users account.
        /// </summary>
        /// <param name="currentUser">Input personal number of logged in user</param>
        /// <param name="users">A list of all users</param>
        public void TransferUserToUser(List<IUser> users)
        {
            Customer otherUser = null;
            IBankAccount otherAccount = null;
            IBankAccount currentUserAccount = null;

            while (currentUserAccount == null)
            {
                Console.WriteLine("Skriv namnet på det konto du vill föra över från:");
                foreach (var account in _bankAccounts)
                {
                    Console.WriteLine($"{account.AccountName} (Saldo: {account.Balance:N2})");
                }
                string currentUserAccountName = Console.ReadLine();
                currentUserAccount = _bankAccounts.FirstOrDefault(account => account.AccountName.Equals(currentUserAccountName, StringComparison.OrdinalIgnoreCase));
                if (currentUserAccount == null)
                {
                    Console.WriteLine("Ogiltligt kontonamn.");
                }
            }

            while (otherUser == null)
            {
                Console.WriteLine("Vilken användare vill du föra över till? (ange personnummer)");
                string otherUserPersonalNum = Console.ReadLine();
                otherUser = users.OfType<Customer>().FirstOrDefault(user => user.PersonalNum == otherUserPersonalNum);

                if (otherUser == null)
                {
                    Console.WriteLine("Användaren hittades inte. Tryck enter för att försöka igen.");
                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                    Console.Clear();
                }
            }

            while (otherAccount == null)
            {
                Console.WriteLine("Skriv in namnet på kontot du vill föra över till:");

                foreach (var account in otherUser._bankAccounts)
                {
                    Console.WriteLine($"{account.AccountName} (Saldo: {account.Balance:N2} {account.AccountCurrency})");
                }

                string otherAccountName = Console.ReadLine();
                otherAccount = otherUser._bankAccounts.FirstOrDefault(account => account.AccountName.Equals(otherAccountName, StringComparison.OrdinalIgnoreCase));
                if (otherAccount == null)
                {
                    Console.WriteLine("Ogiltligt kontonamn. Försök igen.");
                }
            }

            decimal amount;
            while (true)
            {
                Console.WriteLine($"Hur mycket vill du föra över i {currentUserAccount.AccountCurrency}?");

                if (decimal.TryParse(Console.ReadLine(), out amount) && amount > 0 && amount <= currentUserAccount.Balance)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Felaktigt belopp. Det måste vara positivt och inte större än saldot på avsändarkontot.");
                }
            }

            // Some method to check currency, implement when currency method is viable
            //CheckMethodForCurrency(currentUserAccount, otherAccount);

            currentUserAccount.Balance -= amount;
            otherAccount.Balance += amount * Bank.exchangeRate.CalculateExchangeRate(currentUserAccount.AccountCurrency, otherAccount.AccountCurrency);
            Console.WriteLine($"Överföring lyckades! {amount:N2} överfördes från {currentUserAccount.AccountName} till {otherAccount.AccountName}.");
            Console.WriteLine($"Nytt saldo för {currentUserAccount.AccountName}: {currentUserAccount.Balance:N2} {currentUserAccount.AccountCurrency}");
            Console.WriteLine($"Nytt saldo för {otherAccount.AccountName}: {otherAccount.Balance:N2} {otherAccount.AccountCurrency}");

            Console.ReadKey();
        }

        public void Transfer()
        {
            if (_bankAccounts.Count < 2)
            {
                Console.WriteLine($"{TextColor.Red}Du har inga tillräkligt många konton att överföra mellan{TextColor.NORMAL}. " +
                                  $"\n\nTryck enter för att fortsätta");
                while (Console.ReadKey(true).Key != ConsoleKey.Enter) { };
                return;
            }

            var menu = new SelectOneOrMore(["id", "Konto Namn", "Saldo", "Valuta"], PopulateAccountDetails(_bankAccounts));

            Console.Clear();
            Markdown.Paragraph($"Vilket konto vill du överföra {TextColor.Yellow}ifrån{TextColor.NORMAL}");
            int[] accountFromIndex;

            while ((accountFromIndex = menu.Show()).Length == 0)
            {
                Console.Clear();
                Markdown.Paragraph($"{TextColor.Red}Välj ett alternativ{TextColor.NORMAL}");
            }

            Console.Clear();
            Markdown.Paragraph($"Vilket konto vill du överföra {TextColor.Yellow}till{TextColor.NORMAL}");

            int[] accountToIndex = [];
            while (true)
            {
                accountToIndex = menu.Show();
                if (accountToIndex.Length != 0 && !accountToIndex[0].Equals(accountFromIndex[0]))
                {
                    break;
                }
                Console.Clear();
                Markdown.Paragraph($"{TextColor.Red}Välj ett alternativ och inte samma konto som du ville överföra ifrån{TextColor.NORMAL}");
            }

            var accountFrom = _bankAccounts[accountFromIndex[0]];
            var accountTo = _bankAccounts[accountToIndex[0]];
            
            List<IBankAccount> updatedAccounts = [
                accountFrom,
                accountTo
            ];

            Console.Clear();

            // Header
            Markdown.Header(HeaderLevel.Header2, $"Hur mycket i {accountFrom.AccountCurrency} vill du överföra från " +
                                                 $"{accountFrom.AccountName} till {accountTo.AccountName}?\n");
            Markdown.Table(["id", "Konto Namn", "Saldo", "Valuta"], PopulateAccountDetails(updatedAccounts));
            Console.Write("\nBelopp: ");
            
            decimal moneyToWithdraw;
            while (!decimal.TryParse(Console.ReadLine(), out moneyToWithdraw) || moneyToWithdraw > accountFrom.Balance || moneyToWithdraw < 0)
            {
                Markdown.Paragraph($"Välj ett mindre belopp än {accountFrom.Balance}{accountFrom.AccountCurrency}");
            }

            var newTransaction = new Transaction(moneyToWithdraw, accountFrom, accountTo); 
            Bank.transactionQueue.Enqueue(newTransaction);
            

            // accountFrom.Balance -= moneyToWithdraw;
            // accountTo.Balance += moneyToWithdraw*Bank.exchangeRate.CalculateExchangeRate(accountFrom.AccountCurrency, 
            //     accountTo.AccountCurrency);
            // Console.Clear();
            // Markdown.Header(HeaderLevel.Header2, "Summering");
            // Markdown.Table(["id", "Konto Namn", "Saldo", "Valuta"], PopulateAccountDetails(updatedAccounts));
            // Console.WriteLine("\nTryck enter för att fortsätta");
            // while (Console.ReadKey(true).Key != ConsoleKey.Enter) { };
        }
        
        public void Deposit()
        {
            if (_bankAccounts.Count < 1)
            {
                Console.WriteLine($"{TextColor.Red}Du har inga konton att sätta in pengar på {TextColor.NORMAL}. " +
                                  $"\n\nTryck enter för att fortsätta");
                while (Console.ReadKey(true).Key != ConsoleKey.Enter) { };
                return;
            }
            var menu = new SelectOneOrMore(["id", "Konto Namn", "Saldo", "Valuta"], PopulateAccountDetails(_bankAccounts));

            Console.Clear();
            Markdown.Paragraph($"{TextColor.Yellow}Till{TextColor.NORMAL} vilket konto vill du sätta in dina pengar? ");
            int[] accountToIndex;

            while ((accountToIndex = menu.Show()).Length == 0)
            {
                Console.Clear();
                Markdown.Paragraph($"{TextColor.Red}Välj ett alternativ{TextColor.NORMAL}");
            }

            Console.Clear();

            var accountTo = _bankAccounts[accountToIndex[0]];
            
            List<IBankAccount> updatedAccounts = [
                accountTo,
            ];

            Console.Clear();

            // Header
            decimal moneyToDepositinAccountCurrency = 0; 
            bool runLoopSetAmount = true;
            while (runLoopSetAmount)
            {
                string defaultcurrency = "SEK";
                Markdown.Header(HeaderLevel.Header2, $"Hur mycket i {defaultcurrency} vill du sätta in på " +
                                                     $"{accountTo.AccountName}?\n");
                Markdown.Table(["id", "Konto Namn", "Saldo", "Valuta"], PopulateAccountDetails(updatedAccounts));
                Console.Write("\nBelopp: ");

                decimal moneyToDeposit;
                while (!decimal.TryParse(Console.ReadLine(), out moneyToDeposit) || moneyToDeposit < 0)
                {
                    Markdown.Paragraph($"Välj ett nytt belopp som är större än 0");
                }

                moneyToDepositinAccountCurrency = moneyToDeposit *
                                                          Bank.exchangeRate.CalculateExchangeRate(defaultcurrency,
                                                              accountTo.AccountCurrency);
                string answer = "";
                while (answer != "ja" && answer != "j" && answer != "nej" && answer != "n")
                {
                    Console.Write($"Du har angett att du vill sätta in {moneyToDeposit} {defaultcurrency} " +
                                      $"({moneyToDepositinAccountCurrency:N2} {accountTo.AccountCurrency})\n" +
                                      $"\n" +
                                      $"Stämmer detta? ja/nej: ");
                    answer = Console.ReadLine().ToLower();
                    if (answer != "ja" && answer != "j" && answer == "nej" && answer != "n")
                    {
                        Console.WriteLine("Ogiltigt val!Tryck enter för att lägga in ett nytt belopp!");
                        while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                    }
                }
                if (answer == "ja" || answer == "j")
                {
                    runLoopSetAmount = false;
                }
            }
            var newTransaction = new Transaction(moneyToDepositinAccountCurrency, accountTo); 
            Bank.transactionQueue.Enqueue(newTransaction);
            
            //accountTo.Balance += moneyToDepositinAccountCurrency;
            
            // Console.Clear();
            // Markdown.Header(HeaderLevel.Header2, "Summering");
            // Markdown.Table(["id", "Konto Namn", "Saldo", "Valuta"], PopulateAccountDetails(updatedAccounts));
            // Console.WriteLine("\nTryck enter för att fortsätta");
            // while (Console.ReadKey(true).Key != ConsoleKey.Enter) { };
        }
        
        /// <summary>
        /// A method for running queued transactions using the Asynch method
        /// </summary>
        public static void RunTransactionsInQueue()
        {
            foreach (var transaction in Bank.transactionQueue)
            {
                var nextInQueue = Bank.transactionQueue.Dequeue();
                //Set the time of the transaction
                nextInQueue.Timestamp = DateTime.Now; 
                //for deposits, there are no accountFrom
                if (nextInQueue.AccountFrom == null)
                {
                    nextInQueue.AccountTo.Balance += nextInQueue.Amount;
                    nextInQueue.AccountTo.AddToTransactionLog(nextInQueue);
                }
                else
                {
                    //perform the actual transactions
                    nextInQueue.AccountFrom.Balance -= nextInQueue.Amount;
                    //change the amount for the receiving account to the correct currency. 
                    nextInQueue.AccountTo.Balance += nextInQueue.Amount * Bank.exchangeRate.CalculateExchangeRate
                        (nextInQueue.AccountFrom.AccountCurrency, nextInQueue.AccountTo.AccountCurrency);
                    nextInQueue.AccountFrom.AddToTransactionLog(nextInQueue);
                    nextInQueue.AccountTo.AddToTransactionLog(nextInQueue);
                }
            }
        }
        private static List<string> PopulateAccountDetails(List<IBankAccount> updatedAccounts)
        {
            List<string> bodyKeys = [];
            for (int i = 0; i < updatedAccounts.Count; i++)
            {
                var BankAccount = updatedAccounts[i];
                bodyKeys.Add((i + 1).ToString());
                bodyKeys.Add(BankAccount.AccountName);
                bodyKeys.Add(BankAccount.Balance.ToString("N2"));
                bodyKeys.Add(BankAccount.AccountCurrency);
            }

            return bodyKeys;
        }

        public void ShowAccountLogs()
        {
            Console.WriteLine("KONTOLOGGAR");
            foreach (var account in _bankAccounts)
            {
                Console.WriteLine(account.AccountName);
                Console.WriteLine("---------------------------------------------------");
                account.ShowTransactionLog();
            }
        }

        public void TakeLoan()
        {
            Loan newLoan = new Loan();
            bool loanComplete = false;
            decimal availableToLoan = (MaxAccountBalance() * 5) - MaxCurrentLoan();
            decimal newLoanTaken = availableToLoan;
            int choosenAccountIndex = 0;

            while (!loanComplete)
            {
                if (_bankAccounts.Count <= 0)
                {
                    Console.WriteLine("Du har inga konton. Skapa ett konto först innan du tar ett lån.");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine($"Ditt lånetak är: {newLoanTaken} ");
                    if (newLoanTaken <= 0)
                    {
                        Console.WriteLine($"Du kan tyvärr inte låna mer än: {newLoanTaken}.");
                        Console.ReadKey();
                        return;
                    }

                    decimal askedLoan;
                    Console.Write("\nHur stort lån vill du ansöka om:  ");
                    if (!decimal.TryParse(Console.ReadLine(), out askedLoan) || askedLoan <= 0)
                    {
                        Console.WriteLine("Felaktig inmatning.");
                        Thread.Sleep(1000);
                        Console.Clear();
                        continue;
                    }
                    else if (askedLoan > newLoanTaken)
                    {
                        Console.WriteLine($"Du kan inte låna mer än {newLoanTaken}.\n"); // known bug, need currency
                        continue;
                    }

                    Console.WriteLine("\nVälj vilket konto du vill sätta in pengarna på:\n");
                    for (int i = 0; i < _bankAccounts.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {_bankAccounts[i].AccountName}");
                    }
                    if (!int.TryParse(Console.ReadLine(), out choosenAccountIndex) || choosenAccountIndex <= 0 || choosenAccountIndex > _bankAccounts.Count)
                    {
                        Console.WriteLine("Felaktigt val.");
                        continue;
                    }

                    foreach (var account in _bankAccounts)
                    {
                        if (account == _bankAccounts[choosenAccountIndex - 1])
                        {
                            Console.WriteLine($"Din lånansökan på {askedLoan} {_bankAccounts[choosenAccountIndex - 1].AccountCurrency} har accepterats.\n" +
                            $"Räntan för detta lån ligger på {newLoan.LoanRent}%");
                        }
                    }

                    // Checks if user want to accept the loan with the terms (YES/NO).
                    if (AcceptLoanTerms())
                    {
                        newLoan.loanedAmount += askedLoan;
                        _customerLoan.Add(newLoan); // add the loan amount to the loanlist.
                        _bankAccounts[choosenAccountIndex - 1].Balance += askedLoan; // add the loanamount to the account.
                        newLoanTaken -= askedLoan; // Removes the loanamount from the allowed loan ammount.

                        Console.WriteLine($"{askedLoan} {_bankAccounts[choosenAccountIndex - 1].AccountCurrency}" +
                                $" har satts in på följande konto: {_bankAccounts[choosenAccountIndex - 1].AccountName}");
                    }
                    // checks if customer want to take another loan.
                    if (!ContinueLoan())
                    {
                        loanComplete = true;
                    }
                }
            }
        }

        public decimal MaxAccountBalance()
        {
            decimal maxAccountBalance = 0;
            foreach (var account in _bankAccounts)
            {
                maxAccountBalance += account.Balance;
            }
            return maxAccountBalance;
        }

        public decimal MaxCurrentLoan()
        {
            decimal maxCurrentLoans = 0;
            foreach (var loan in _customerLoan)
            {
                maxCurrentLoans += loan.loanedAmount;
            }
            return maxCurrentLoans;
        }

        public bool ContinueLoan()
        {
            // Checks if customer want to quit the current method or ask for a new loan.

            while (true)
            {
                Console.WriteLine("\nVill du ansöka om nytt lån? Ja/Nej");
                Console.Write("svar:");
                string quitOrNot = Console.ReadLine().ToLower(); // Set ReadKey to true to remove the input text in the consol.
                Console.Clear();
                if (quitOrNot == "nej" || quitOrNot == "n")
                {
                    Console.WriteLine("\nDu har valt att avsluta låneansökan.");
                    Console.WriteLine("Tryck på valfri tangent för att avsluta.");
                    Console.ReadKey();
                    return false;
                }
                else if (quitOrNot == "ja" || quitOrNot == "j")
                {
                    Console.WriteLine("Går vidare med en ny ansökan.");
                    Thread.Sleep(2000);
                    Console.Clear();
                    return true;
                }
                else
                {
                    continue;
                }

            }
        }

        public bool AcceptLoanTerms()
        {

            while (true)
            {
                // Checks if user want to accept the loan with the terms (YES/NO).
                Console.Write("\nAccepterar du villkoren för detta lån? Ja/Nej");
                Console.Write("\nSvar: ");
                string terms = Console.ReadLine().ToLower();
                if (terms == "ja" || terms == "j")
                {
                    return true;
                }
                else if (terms == "nej" || terms == "n")
                {
                    Console.WriteLine("Lånet har avbrutits...");
                    Thread.Sleep(1500);
                    Console.Clear();
                    return false;
                }
                else
                {
                    Console.WriteLine("Hint: Testa med ett \"J\" eller \"N\".");
                    Thread.Sleep(1500);
                    continue;
                }
            }
        }
    }
}