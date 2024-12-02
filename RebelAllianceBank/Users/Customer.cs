using System.Linq;
using RebelAllianceBank.Interfaces;
using RebelAllianceBank.utils;
using RebelAllianceBank.Accounts;
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

                //switch (userChoice)
                //{
                //    case 1:
                //        _bankAccounts.Add(new CardAccount(accountName, 0, accountCurrency, 0.0m));
                //        createAccount = true;
                //        Console.ReadKey();
                //        break;
                //    case 2:
                //        _bankAccounts.Add(new ISK(accountName, 0, accountCurrency, 0.0m));
                //        createAccount = true;
                //        Console.ReadKey();
                //        break;
                //    case 3:
                //        _bankAccounts.Add(new SavingsAccount(accountName, 0, accountCurrency, 0.0m));
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

    }
}