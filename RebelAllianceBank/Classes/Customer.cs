using RebelAllianceBank.Interfaces;
using RebelAllianceBank.utils;
namespace RebelAllianceBank.Classes
{
    public class Customer : IUser
    {
        public int ID { get; set; }
        public string PersonalNum { get; set; }
        public string Password { get; set; }
        public string Surname { get; set; }
        public string Forename { get; set; }
        public bool LoginLock { get; set; } = false;
        private List<IBankAccount> BankAccounts = [];
        
        public Customer() { }
        public Customer(string pNum, string password, string surname, string forename)
        private List<IBankAccount> BankAccounts = []; 
        
        public Customer(string username, string password)
        {
            PersonalNum = pNum;
            Password = password;
            Surname = surname;
            Forename = forename;
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
                Console.WriteLine("         Lån\n");

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
                    Console.WriteLine("\nVilket konto skulle du vilja sätta in pengarna på.");
                    int counter = 1;
                    foreach (var item in BankAccounts)
                    {
                        Console.WriteLine($"{counter}. {item.AccountName}");
                        counter++;
                    }
                    Console.Write("\nKontoval: ");

                    int choosenAccount = 0;
                    while (!int.TryParse(Console.ReadLine(), out choosenAccount) || choosenAccount < 0 || choosenAccount > BankAccounts.Count)
                    {
                        Console.WriteLine("Fel inmatning, försök igen");
                    }
                    
                    Console.WriteLine($"Din lånansökan på {askedLoan} {BankAccounts[choosenAccount - 1].AccountCurrency} har accepterats.\n" +
                        $"Räntan för detta lån ligger på {BankAccounts[choosenAccount-1].IntrestRate:F2}%\n" +
                        $"Månadsbeloppet blir {RentOnLoan(askedLoan, choosenAccount):F2} {BankAccounts[choosenAccount - 1].AccountCurrency}/månaden.");
                    
                    // Checks if user want to accept the loan with the terms (YES/NO).
                    Console.Write("\nAccepterar du villkoren för detta lån? (J)a / (N)ej");
                    string terms;
                    bool okTerms = false;
                    
                    while (!okTerms)
                    {
                        Console.Write("\nSvar: ");
                        terms = Console.ReadLine().ToLower(); ;
                        if (terms == "j")
                        {
                            BankAccounts[choosenAccount -1].Balance += askedLoan;
                            Console.WriteLine($"{askedLoan} {BankAccounts[choosenAccount-1].AccountCurrency} har satts in på följande konto: {BankAccounts[choosenAccount -1].AccountName}");
                            Console.ReadKey();
                            okTerms = true;
                        }
                        else if (terms == "n")
                        {
                            Console.WriteLine("Lånet har avbrutits...");
                            Console.ReadKey();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Fel inmatning, försök igen");
                            okTerms = false;
                        }
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

            if (askedLoan > (currentBalance * 5))
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
        public decimal RentOnLoan(decimal loanAmmount, int index)
        {
            foreach (var item in BankAccounts)
            {
                if (item == BankAccounts[index-1])
                {
                    loanAmmount = loanAmmount / 100 * item.IntrestRate;
                }
            }
            return loanAmmount;
        }
    }
}