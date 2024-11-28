using System.Linq;
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
        public List<IBankAccount> BankAccounts { get; set; } = new List<IBankAccount>();
        
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

                //switch (userChoice)
                //{
                //    case 1:
                //        BankAccounts.Add(new CardAccount(accountName, 0, accountCurrency, 0.0m));
                //        createAccount = true;
                //        Console.ReadKey();
                //        break;
                //    case 2:
                //        BankAccounts.Add(new ISK(accountName, 0, accountCurrency, 0.0m));
                //        createAccount = true;
                //        Console.ReadKey();
                //        break;
                //    case 3:
                //        BankAccounts.Add(new SavingsAccount(accountName, 0, accountCurrency, 0.0m));
                //        createAccount = true;
                //        break;
                //    case 4:
                //        createAccount = true;
                //        break;
                //    default:
                //        Console.WriteLine("Fel inmatning, inget konto har skapats.");
                //        Console.ReadKey();
                //        Console.Clear();
                //        createAccount = false;
                //        break;
                //}
            } while (createAccount == false);
        }
        public void TransferUserToUser(string currentUser, List<IUser> users)
        {
            bool userFound = true;
            string secondUser = null;
            string userChoice;
            while (userFound)
            {
                Console.WriteLine("Vilken användare vill du föra över till?");
                secondUser = Console.ReadLine();
                foreach (var user in users)
                {
                    if (user.PersonalNum == secondUser)
                    {
                        userFound = false;
                    }
                }
                if (userFound == true)
                {
                    Console.WriteLine("Användaren kunde inte hittas! Försök igen.");
                }
            }
            while (userFound == false)
            {
                Console.WriteLine("Till vilket konto vill du föra över till? Skriv in namnet.");
                foreach (var account in BankAccounts)
                {
                    if (secondUser.Equals(account.AccountName))
                    {
                        Console.WriteLine($"{account.AccountName}");
                    }
                }
                userChoice = Console.ReadLine();
                foreach( var account in BankAccounts)
                {
                    if (userChoice == account.AccountName)
                    {
                        userFound = true;
                    }
                }
                if (userFound == false)
                {
                    Console.WriteLine("Du valde inte ett konto.");
                }
            }

        }
    }
}