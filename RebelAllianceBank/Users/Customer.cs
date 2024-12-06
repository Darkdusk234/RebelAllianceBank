﻿using System.Linq;
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
            }
            Markdown.Table(["Konto Namn", "Saldo"], bodyKeys);
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
                }

                switch (userChoice)
                {
                    case 1:
                        _bankAccounts.Add(new CardAccount(accountName, PersonalNum));
                        createAccount = true;
                        Console.ReadKey();
                        break;
                    case 2:
                        _bankAccounts.Add(new ISK(accountName, PersonalNum));
                        createAccount = true;
                        Console.ReadKey();
                        break;
                    case 3:
                        _bankAccounts.Add(new SavingsAccount(accountName, PersonalNum));
                        createAccount = true;
                        break;
                    case 4:
                        createAccount = true;
                        break;
                    default:
                        Console.WriteLine("Fel inmatning, inget konto har skapats.");
                        Console.ReadKey();
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
                    Console.WriteLine($"{account.AccountName} (Saldo: {account.Balance:N2})");
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
                Console.WriteLine("Hur mycket vill du föra över?");

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
            otherAccount.Balance += amount;
            Console.WriteLine($"Överföring lyckades! {amount:N2} överfördes från {currentUserAccount.AccountName} till {otherAccount.AccountName}.");
            Console.WriteLine($"Nytt saldo för {currentUserAccount.AccountName}: {currentUserAccount.Balance:N2}");
            Console.WriteLine($"Nytt saldo för {otherAccount.AccountName}: {otherAccount.Balance:N2}");

            Console.ReadKey();
        }

        public void Transfer()
        {
            if (_bankAccounts.Count < 2)
            {
                Console.WriteLine($"{TextColor.Red}Du har inga tillräkligt många konton att överföra mellan{TextColor.NORMAL}");
                return;
            }

            var menu = new SelectOneOrMore(["id", "Konto Namn", "Saldo"], PopulateAccountDetails(_bankAccounts));

            Console.Clear();
            Markdown.Paragrath($"Vilket konto vill du överföra {TextColor.Yellow}ifrån{TextColor.NORMAL}");
            int[] accountFromIndex;

            while ((accountFromIndex = menu.Show()).Length == 0)
            {
                Console.Clear();
                Markdown.Paragrath($"{TextColor.Red}Välj ett alternativ{TextColor.NORMAL}");
            }

            Console.Clear();
            Markdown.Paragrath($"Vilket konto vill du överföra {TextColor.Yellow}till{TextColor.NORMAL}");

            int[] accountToIndex = [];
            while (true)
            {
                accountToIndex = menu.Show();
                if (accountToIndex.Length != 0 && !accountToIndex[0].Equals(accountFromIndex[0]))
                {
                    break;
                }
                Console.Clear();
                Markdown.Paragrath($"{TextColor.Red}Välj ett alternativ och inte samma konto som du ville överföra ifrån{TextColor.NORMAL}");
            }

            var acountFrom = _bankAccounts[accountFromIndex[0]];
            var acountTo = _bankAccounts[accountToIndex[0]];
            List<IBankAccount> updatedAccounts = [
                acountFrom,
                acountTo
            ];

            Console.Clear();

            // Heder
            Markdown.Header(HeaderLevel.Header2, $"Hur mycket vill du dra ifrån {acountFrom.AccountName}?");
            Markdown.Table(["id", "Konto Namn", "Saldo"], PopulateAccountDetails(updatedAccounts));
            int manyToDrow;
            while (!int.TryParse(Console.ReadLine(), out manyToDrow) || manyToDrow > acountFrom.Balance || manyToDrow < 0)
            {
                Markdown.Paragrath($"Välj ett mindre belopp än {acountFrom.Balance}{acountFrom.AccountCurrency}");
            }

            acountFrom.Balance -= manyToDrow;
            acountTo.Balance += manyToDrow;
            Console.Clear();
            Markdown.Header(HeaderLevel.Header2, "Summering");
            Markdown.Table(["id", "Konto Namn", "Saldo"], PopulateAccountDetails(updatedAccounts));
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
            }

            return bodyKeys;
        }

        public void TakeLoan()
        {
            bool loanComplete = false;
           
            decimal availableToLoan = (MaxAccountBalance() * 5);
            decimal newLoanTaken = availableToLoan;

            int choosenAccountIndex = 0;

            while (!loanComplete)
            {
                Loan newLoan = new Loan();
                if (_bankAccounts.Count <= 0)
                {
                    Console.WriteLine("Du har inga konton. Skapa ett konto först innan du tar ett lån.");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine($"Ditt lånetak är: {newLoanTaken:F0} ");
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
                        Console.WriteLine("\nTryck på valfri tangent för att gå vidare");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }
                    else if (askedLoan > newLoanTaken)
                    {
                        Console.WriteLine($"Du kan inte låna mer än {newLoanTaken}.\n");
                        continue;
                    }
                    Console.Clear();

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
                    Console.Clear();

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
                        Console.Clear();
                        newLoan.loanedAmount += askedLoan;
                        _customerLoan.Add(newLoan); // add the loan amount to the loanlist.
                        _bankAccounts[choosenAccountIndex - 1].Balance += askedLoan; // add the loanamount to the account.
                        //newLoanTaken -= askedLoan; // Removes the loanamount from the allowed loan ammount.

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
            return maxAccountBalance - MaxCurrentLoan();
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
                if (quitOrNot == "nej" || quitOrNot == "n")
                {
                    Console.WriteLine("\nDu har valt att avsluta låneansökan.");
                    Console.WriteLine("Tryck på valfri tangent för att avsluta.");
                    Console.ReadKey();
                    return false;
                }
                else if (quitOrNot == "ja" || quitOrNot == "j")
                {
                    Console.WriteLine("Tryck på valfri tangent för att gå vidare med en ny ansökan.");
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
                    Console.WriteLine("\nLånet har avbrutits..");
                    Console.WriteLine("\nTryckk på valfri tangent för att gå vidare.");
                    Console.ReadKey();
                    Console.Clear();
                    return false;
                }
                else
                {
                    Console.WriteLine("Hint: Testa med ett \"J\" eller \"N\".");
                    continue;
                }
            }
        }

        public void DisplayLoan()
        {
            decimal totalLoanAmount = 0;
            foreach (var loan in _customerLoan)
            {
                totalLoanAmount += loan.loanedAmount;
            }
            Console.WriteLine($"\nDu har för närvarande: {totalLoanAmount:F2}kr i lån.");
            //Console.WriteLine($"\nDin beräknade månadsavgift är: {totalLoanAmount /12:F2}kr");
            Console.WriteLine("\nTryck på valfri tangent för att fortsätta.");
            Console.ReadKey();
        }

    }
}