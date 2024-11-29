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
        private List<IBankAccount> _bankAccounts = [new CardAccount("0", 0, "kreditkort", 500m, "SEK", 13.95m),
                                           new ISK("investeringskonto", 4000m, "SEK", 2.94m),
                               new SavingsAccount("sparkonto", 10000m, "SEK", 4m)];
        
        public Customer() { }
        public Customer(string pNum, string password, string surname, string forename)
        {
            PersonalNum = pNum;
            Password = password;
            Surname = surname;
            Forename = forename;
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

                    Console.Write("Vilken valuta vill du ha på kontot: ");
                    accountCurrency = Console.ReadLine().ToUpper();
                }

                switch (userChoice)
                {
                    case 1:
                        _bankAccounts.Add(new CardAccount("0", 0, accountName, 0, accountCurrency, 0.0m));
                        createAccount = true;
                        Console.ReadKey();
                        break;
                    case 2:
                        _bankAccounts.Add(new ISK(accountName, 0, accountCurrency, 0.0m));
                        createAccount = true;
                        Console.ReadKey();
                        break;
                    case 3:
                        _bankAccounts.Add(new SavingsAccount(accountName, 0, accountCurrency, 0.0m));
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
            bool loanComplete = false;
            
            while (!loanComplete)
            {   
                // Checks if customer has an account.
                if (_bankAccounts.Count > 0)
                {
                    Console.WriteLine("Lån\n");

                    // Checks how much the customer want to loan.                
                    Console.Write("Hur stort lån vill du ansöka om: ");

                    int askedLoan;
                    while (!int.TryParse(Console.ReadLine(), out askedLoan) || askedLoan <= 0)
                    {
                        Console.WriteLine("Fel inmatning, försök igen");
                    }

                    // Checks if customer is a viable loantaker or not.                
                    if(askedLoan > MaxLoan())
                    {
                        Console.WriteLine($"Din låneansökan har inte accepterats, ditt lånetak ligger på {MaxLoan()}\n");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("\nVilket konto skulle du vilja sätta in pengarna på.");
                        int counter = 1;
                        foreach (var item in _bankAccounts)
                        {
                            Console.WriteLine($"{counter}. {item.AccountName}");
                            counter++;
                        }
                        Console.Write("\nKontoval: ");

                        int choosenAccount = 0;
                        while (!int.TryParse(Console.ReadLine(), out choosenAccount) || choosenAccount < 0 || choosenAccount > _bankAccounts.Count)
                        {
                            Console.WriteLine("Fel inmatning, försök igen");
                        }
                        foreach (var item in _bankAccounts)
                        {
                            if (item == _bankAccounts[choosenAccount -1])
                            {
                                Console.WriteLine($"Din lånansökan på {askedLoan} {_bankAccounts[choosenAccount - 1].AccountCurrency} har accepterats.\n" +
                                    $"Räntan för detta lån ligger på {_bankAccounts[choosenAccount - 1].LoanIntrestRate:F2}%\n" +
                                    $"Månadsbeloppet blir {item.CalculateLoanInterest(askedLoan)} {_bankAccounts[choosenAccount - 1].AccountCurrency}/månaden.");
                            }
                        }

                        // Checks if user want to accept the loan with the terms (YES/NO).
                        Console.Write("\nAccepterar du villkoren för detta lån? Ja/Nej");
                        string terms;
                        bool okTerms = false;

                        while (!okTerms)
                        {
                            Console.Write("\nSvar: ");
                            terms = Console.ReadLine().ToLower();
                            if (terms == "ja" || terms == "j")
                            {
                                _bankAccounts[choosenAccount - 1].Balance += askedLoan;
                                Console.WriteLine($"{askedLoan} {_bankAccounts[choosenAccount - 1].AccountCurrency} har satts in på följande konto: {_bankAccounts[choosenAccount - 1].AccountName}");
                                Console.ReadKey();
                                okTerms = true;
                                loanComplete = true;
                            }
                            else if (terms == "nej" || terms == "n")
                            {
                                Console.WriteLine("Lånet har avbrutits...");
                                Thread.Sleep(1500);
                                Console.Clear();
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

                if (loanComplete)
                {
                    break;
                }
                else
                {
                    // Checks if customer want to quit the current method or ask for a new loan.
                    Console.WriteLine("\nVill du ansöka om nytt lån? Ja/Nej");
                    Console.Write("svar:");
                    string quitOrNot = Console.ReadLine().ToLower(); // Set ReadKey to true to remove the input text in the consol.
                    Console.Clear();
                    if (quitOrNot == "nej" || quitOrNot == "n")
                    {
                        Console.WriteLine("\nDu har valt att avsluta låneansökan.");
                        break;
                    }
                    else if (quitOrNot == "ja" || quitOrNot == "j")
                    {
                        Console.WriteLine("Går vidare med en ny ansökan");
                        Thread.Sleep(1500);
                        Console.Clear();
                    }
                }
            }
            Console.Clear();
        }
        // TO-DO [] se till att man inte kan låna över taket.
        /// <summary>
        /// return highest ammount that customer can loan. 
        /// </summary>
        /// <param name="askedLoan"></param>
        /// <returns></returns>
        public decimal MaxLoan()
        {
            decimal maxLoan = 0;
            foreach (var item in _bankAccounts)
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
        /// 
        //public decimal RentOnLoan(decimal loanAmmount, int index)
        //{
        //    foreach (var item in BankAccounts)
        //    {
        //        if (item == BankAccounts[index-1])
        //        {
        //            loanAmmount = loanAmmount / 100 * item.IntrestRate;
        //        }
        //    }
        //    return loanAmmount;
        //}
    }
}