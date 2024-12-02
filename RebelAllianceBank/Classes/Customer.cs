using RebelAllianceBank.Enums;
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
                Console.WriteLine($"{TextColor.Red}Du har inga konton att visa{TextColor.NORMAL}");
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
                        // Check if it is corect
                        BankAccounts.Add(new CardAccount("001", 1, accountName, 0, accountCurrency, 0.0m));
                        createAccount = true;
                        Console.ReadKey();
                        break;
                    case 2:
                        // Check if it is corect
                        BankAccounts.Add(new ISK("001", 1, accountName, 0, accountCurrency, 0.0m));
                        createAccount = true;
                        Console.ReadKey();
                        break;
                    case 3:
                        // Check if it is corect
                        BankAccounts.Add(new SavingsAccount("001", 1, accountName, 0, accountCurrency, 0.0m));
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

        public void Transfer()
        {
            if (BankAccounts.Count < 2)
            {
                Console.WriteLine($"{TextColor.Red}Du har inga tillräkligt många konton att överföra mellan{TextColor.NORMAL}");
                return;
            }

            var menu = new SelectOneOrMore(["id", "Konto Namn", "Saldo"], PopulateAccountDetails(BankAccounts));

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

            var acountFrom = BankAccounts[accountFromIndex[0]];
            var acountTo = BankAccounts[accountToIndex[0]];
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
    }
}