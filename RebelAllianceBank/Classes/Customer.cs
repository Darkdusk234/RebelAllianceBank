using RebelAllianceBank.Enums;
using RebelAllianceBank.Interfaces;
using RebelAllianceBank.utils;
namespace RebelAllianceBank.Classes
{
    public class Customer : IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        private List<IBankAccount> BankAccounts = [
            new CardAccount("Card", 500, "SEK"),
            new CardAccount("Card", 500, "SEK"),
            new CardAccount("Card", 500, "SEK"),
        ];

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

        public void TransferManyBetweenAccount()
        {
            List<string> bodyKeys = [];
            for (int i = 0; i < BankAccounts.Count; i++)
            {
                var BankAccount = BankAccounts[i];
                bodyKeys.Add((i + 1).ToString());
                bodyKeys.Add(BankAccount.AccountName);
                bodyKeys.Add(BankAccount.Balance.ToString("C"));
            }

            var menu = new SelectOneOrMore(["id", "Konto Namn", "Saldo"], bodyKeys);

           var selectedAccounts = menu.Show(2);


            // Markdown.Heder(message: "Wowo", headerLevel: HeaderLevel.Header2);
            // Markdown.Table(["id", "Konto Namn", "Saldo"], bodyKeys);

            // Markdown.Paragrath("Vilket konto vill du överföra ifrån");
            // int accountFromIndex;
            // while (
            //     !int.TryParse(Console.ReadLine(), out accountFromIndex) ||
            //     accountFromIndex <= 0 ||
            //     accountFromIndex > BankAccounts.Count
            //     )
            // {
            //     Console.Clear();
            //     Markdown.Table(["id", "Konto Namn", "Saldo"], bodyKeys);
            //     Markdown.Paragrath("Det måste vara ett nummer so mantchar konto nummer");
            // };
            // Console.Clear();
            // Markdown.Table(["id", "Konto Namn", "Saldo"], bodyKeys);
            // Markdown.Paragrath("Vilket konto vill du överföra till");
            // int accountToIndex;
            // while (
            //     !int.TryParse(Console.ReadLine(), out accountToIndex) ||
            //     accountToIndex == accountFromIndex ||
            //     accountToIndex <= 0 ||
            //     accountToIndex > BankAccounts.Count
            //     )
            // {
            //     Console.Clear();
            //     Markdown.Table(["id", "Konto Namn", "Saldo"], bodyKeys);
            //     Markdown.Paragrath("Det måste vara ett nummer so mantchar konto nummer");
            // };

            // var acountFrom = BankAccounts[accountToIndex - 1];
            // var acountTo = BankAccounts[accountFromIndex - 1];

            // Console.Clear();
            // Markdown.Table(["id", "Konto Namn", "Saldo"], bodyKeys);
            // Markdown.Paragrath($"Hur mycket vill du dra från {acountFrom.AccountName}");
            // int manyAmount;
            // while (
            //     !int.TryParse(Console.ReadLine(), out manyAmount) ||
            //     manyAmount <= 0 ||
            //     manyAmount > acountFrom.Balance
            //     )
            // {
            //     Console.Clear();
            //     Markdown.Table(["id", "Konto Namn", "Saldo"], bodyKeys);
            //     Markdown.Paragrath($"Man kan inte dra mer eller mindre än vad som finns på {acountFrom.AccountName}");
            // };

            // acountFrom.Balance -= manyAmount;
            // acountTo.Balance += manyAmount;

            // IBankAccount[] updatedAccounts = [
            //     acountFrom,
            //     acountTo,
            // ];

            // List<string> body = [];

            // for (int i = 0; i < updatedAccounts.Length; i++)
            // {
            //     var updatedAccount = updatedAccounts[i];
            //     body.Add((i + 1).ToString());
            //     body.Add(updatedAccount.AccountName);
            //     body.Add(updatedAccount.Balance.ToString("N2"));
            // }
            // Console.Clear();
            // Markdown.Heder(HeaderLevel.Header2, "De nya ballansen på de konton är");
            // Markdown.Table(["id", "Konto Namn", "Saldo"], body);
        }
    }
}