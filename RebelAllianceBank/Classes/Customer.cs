using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Classes
{
    public class Customer : IUser
    {
        List<IBankAccount> accounts = new List<IBankAccount>();
        public string Username { get; set; }
        public string Password { get; set; }


        public Customer(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public void CreateCardAccount(string accountName, decimal balance, string accountCurrency, decimal intrestRate)
        {
            Console.WriteLine($"Räntan för detta kontot blir: {intrestRate}%");
            Console.WriteLine("Vill du fortsätta? (J)a,(N)ej?");

            bool keepRunning = true;
            while (keepRunning)
            {
                var key = Console.ReadKey().Key;

                if (key == ConsoleKey.J)
                {
                    accounts.Add(new CardAccount(accountName, 0, accountCurrency, intrestRate));
                    Console.WriteLine($"\nDet nya kontot har skapats.");
                    keepRunning = false;
                }
                else if (key == ConsoleKey.N)
                {
                    Console.WriteLine("\nInget konto har skapats.");
                    keepRunning = false;
                }
                else
                {
                    Console.WriteLine("\nFel inmatning, försök med \"Y\" eller \"N\"");
                }
            }
            Console.ReadKey();
        }

        public void CreateISKAccount(string accountName, decimal balance, string accountCurrency, decimal intrestRate)
        {
            Console.WriteLine($"Räntan för detta kontot blir: {intrestRate}%");
            Console.WriteLine("Vill du fortsätta? (J)a,(N)ej?");

            bool keepRunning = true;
            while (keepRunning)
            {
                var key = Console.ReadKey().Key;

                if (key == ConsoleKey.J)
                {
                    accounts.Add(new ISK(accountName, 0, accountCurrency, intrestRate));
                    Console.WriteLine($"\nDet nya kontot har skapats..");
                    keepRunning = false;
                }
                else if (key == ConsoleKey.N)
                {
                    Console.WriteLine("\nInget nytt konto har skapats.");
                    keepRunning = false;
                }
                else
                {
                    Console.WriteLine("\nFel inmatning, försök med \"Y\" eller \"N\"");
                }
            }
            Console.ReadKey();
        }

        public void CreateSavingsAccount(string accountName, decimal balance, string accountCurrency, decimal intrestRate)
        {
            Console.WriteLine($"Räntan för detta kontot blir: {intrestRate}%");
            Console.WriteLine("Vill du fortsätta? (J)a,(N)ej?");

            bool keepRunning = true;
            while (keepRunning)
            {
                var key = Console.ReadKey().Key;

                if (key == ConsoleKey.J)
                {
                    accounts.Add(new SavingsAccount(accountName, 0, accountCurrency, intrestRate));
                    Console.WriteLine($"\nDet nya kontot har skapats.");
                    keepRunning = false;
                }
                else if (key == ConsoleKey.N)
                {
                    Console.WriteLine("\nInget konto har skapats.");
                    keepRunning = false;
                }
                else
                {
                    Console.WriteLine("\nFel inmatning, försök med \"Y\" eller \"N\"");
                }
            }
            Console.ReadKey();
        }
    }
}