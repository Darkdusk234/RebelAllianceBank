using RebelAllianceBank.Interfaces;
using RebelAllianceBank.utils;
namespace RebelAllianceBank.Classes
{
    public class Customer : IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        private List<IBankAccount> BankAccounts = [new CardAccount("kreditkort", 500m, "SEK", 13.95m), new ISK("investeringskonto", 4000m, "SEK", 2.94m), new SavingsAccount("sparkonto", 10000m, "SEK", 4m)]; 
        
        public Customer(string username, string password)
        {
            Username = username;
            Password = password;
        }
        
        public void ShowBankAccounts()
        {
            Console.WriteLine("Konton");

            if (BankAccounts.Count == 0)
            {
                Console.WriteLine("Du har inga konton att visa");
                return;
            }

            List<string> bodyKeys = [];
            foreach (var BankAccount in BankAccounts)
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

                    Console.Write("Vilken valuta vill du ha på kontot: ");
                    accountCurrency = Console.ReadLine().ToUpper();
                }

                switch (userChoice)
                {
                    case 1:
                        BankAccounts.Add(new CardAccount(accountName, 0, accountCurrency, 0.0m));
                        createAccount = true;
                        Console.ReadKey();
                        break;
                    case 2:
                        BankAccounts.Add(new ISK(accountName, 0, accountCurrency, 0.0m));
                        createAccount = true;
                        Console.ReadKey();
                        break;
                    case 3:
                        BankAccounts.Add(new SavingsAccount(accountName, 0, accountCurrency, 0.0m));
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

        public void TakeLoan()
        {
            // Checks if customer has an account.
            if (BankAccounts.Count > 0)
            {
                Console.WriteLine("Lån\n");

                // Checks how much the customer want to loan.                
                Console.Write("Hur stort lån vill du ansöka om: ");

                int askedLoan;
                while (!int.TryParse(Console.ReadLine(), out askedLoan))
                {
                    Console.WriteLine("Fel inmatning, försök igen");
                }
                
                // Checks if customer is a viable loantaker or not.                
                bool acceptedLoan = MaxLoan(askedLoan);
                if (!acceptedLoan)
                {
                    Console.WriteLine($"Din låneansökan har inte accepterats, ditt lånetak ligger på {MaxLoanRoof(askedLoan)}");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Vilket konto skulle du vilja sätta in pengarna på.");
                    int counter = 1;
                    foreach (var item in BankAccounts)
                    {
                        Console.WriteLine($"{counter}. {item.AccountName}");
                        counter++;
                    }
                    Console.Write("\nKontoval: ");
                    Console.ReadKey();

                    int choosenAccount = 0;
                    while (int.TryParse(Console.ReadLine(), out choosenAccount) || choosenAccount < 1 || choosenAccount > BankAccounts.Count)
                    {
                        Console.WriteLine("Fel inmatning, försök igen");
                    }

                    Console.WriteLine($"Din lånansökan på {askedLoan} har accepterats.\n" +
                        $"Räntan för detta lån ligger på {RentOnLoan(askedLoan)}.");
                    
                    // Checks if user want to accept the loan with the terms.
                    Console.Write("\nAccepterar du villkoren för detta lån? (J)a/(N)ej");
                    Console.Write("Svar: ");
                    Console.ReadKey();
                    
                    string terms = Console.ReadLine().ToLower();

                    while (terms != "y" && terms != "n")
                    {
                        Console.WriteLine("Fel inmatning, försök igen");
                    }

                    if (terms == "n")
                    {
                        Console.WriteLine("Lånet har avbrutits...");
                        Console.ReadKey();
                        return;
                    }
                    else if (terms == "j")
                    {
                        BankAccounts[choosenAccount -1].Balance += askedLoan;
                        Console.WriteLine($"{askedLoan} har satts in på kontot {BankAccounts[choosenAccount -1].AccountName}");
                        Console.ReadKey();
                    }
                }
            }
            else
            {
                Console.WriteLine("Du verkar inte ha något konto på denna bank. \n" +
                    " Gå tillbaka och skapa ett konto om du vill ta ett lån.");
                Console.ReadLine();
            }
        }
        /// <summary>
        /// Return a bool.
        /// </summary>
        /// <param name="askedLoan"></param>
        /// <returns></returns>
        public bool MaxLoan(decimal askedLoan)
        {
            decimal currentBalance = 0;

            foreach (var item in BankAccounts)
            {
                currentBalance += item.Balance;
            }

            if (askedLoan > currentBalance * 5)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// return highest ammount that customer can loan. 
        /// </summary>
        /// <param name="askedLoan"></param>
        /// <returns></returns>
        public decimal MaxLoanRoof(decimal askedLoan)
        {
            decimal maxLoan = 0;
            foreach (var item in BankAccounts)
            {
                maxLoan += item.Balance;
            }
            return maxLoan * 5;
        }
        /// <summary>
        /// Return rent for the asked loan.
        /// </summary>
        /// <param name="loanAmmount"></param>
        /// <returns></returns>
        public decimal RentOnLoan(decimal loanAmmount)
        {
            foreach (var item in BankAccounts)
            {
               loanAmmount /= item.IntrestRate;
            }
            return loanAmmount;
        }
    }
}