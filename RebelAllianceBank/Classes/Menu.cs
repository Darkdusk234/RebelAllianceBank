namespace RebelAllianceBank.Classes;

public class Menu
{
    
     public static void AdminMenu()
        {
            bool runAdminMenu = true;

            while (runAdminMenu)
            {
                Console.Clear();
                
                Console.Write("ADMIN\n" +
                              "[1] Skapa användare\n" +
                              "[2] Ändra växelkurs\n" +
                              "[3] Lås upp användarkonto???\n" +
                              "[4] Logga ut\n" +
                              "\n" +
                              "Menyval: ");
                
                string choice = Console.ReadLine(); 
                
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Skapa användare");
                        Console.ReadKey(); //Ta ev bort sen när det finns en metod
                        break;
                    case "2": 
                        Console.WriteLine("Ändra växelkurs");
                        Console.ReadKey(); //Ta ev bort sen när det finns en metod
                        break;
                    case "3": 
                        Console.WriteLine("Lås upp användarkonto???");
                        Console.ReadKey(); //Ta ev bort sen när det finns en metod
                        break;
                    case "4":
                        runAdminMenu = false;
                        break; 
                    default: 
                        Console.Clear();
                        Console.WriteLine("Felaktig input! Tryck enter och försök igen!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        public static void CustomerMenu()
        {
            bool runCustomerMenu = true;

            while (runCustomerMenu)
            {
                Console.Clear();
                Console.Write($"Välkommen {"John Doe"}!\n" +
                              $"\n" +
                              $"HUVUDMENY:\n" +
                              $"[1] Konton\n" +
                              $"[2] Betala/Överföra\n" +
                              $"[3] Lån\n" +
                              $"[4] Logga ut\n" +
                              $"\n" +
                              $"Menyval: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CustomerMenuAccounts();
                        break;
                    case "2":
                        CustomerMenuTransaction();
                        break;
                    case "3":
                        CustomerMenuLoan();
                        break;
                    case "4":
                        runCustomerMenu = false; 
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Felaktig input! Tryck enter och försök igen!");
                        Console.ReadKey();
                        break;
                }
            }

        }

        private static void CustomerMenuAccounts()
        {
            bool runCustomerMenuAccounts = true;

            while (runCustomerMenuAccounts)
            {
                Console.Clear();
                
                Console.Write("KONTON:\n" +
                              "[1] Se över mina konton\n" +
                              "[2] Öppna nytt konto\n" +
                              "[3] Återgå till huvudmenyn\n" +
                              "\n" +
                              "Menyval: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Se över mina konton");
                        Console.ReadKey(); //Ta ev bort sen när det finns en metod
                        break;
                    case "2":
                        Console.WriteLine("Öppna nytt konto");
                        Console.ReadKey(); //Ta ev bort sen när det finns en metod
                        break;
                    case "3": 
                        runCustomerMenuAccounts = false; 
                        break;
                    default: 
                        Console.Clear();
                        Console.WriteLine("Felaktig input! Tryck enter och försök igen!");
                        Console.ReadKey();
                        break; 
                }
            }
        }
        private static void CustomerMenuTransaction()
        {
            bool runCustomerMenuTransaction = true;

            while (runCustomerMenuTransaction)
            {
                Console.Clear();

                Console.Write("BETALA/ÖVERFÖRA:\n" +
                              "[1] Ny överföring\n" +
                              "[2] Ny betalning\n" +
                              "[3] Återgå till huvudmenyn\n" +
                              "\n" +
                              "Menyval: ");
                
                string choice = Console.ReadLine(); 
                
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Ny överföring");
                        Console.ReadKey(); //Ta ev bort sen när det finns en metod
                        break; 
                    case "2":
                        Console.WriteLine("Ny betalning");
                        Console.ReadKey(); //Ta ev bort sen när det finns en metod
                        break; 
                    case "3":
                        runCustomerMenuTransaction = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Felaktig input! Tryck enter och försök igen!");
                        Console.ReadKey(); 
                        break;
                }
            }
        }

        private static void CustomerMenuLoan()
        {
            bool runCustomerMenuLoan = true;

            while (runCustomerMenuLoan)
            {
                Console.Clear();
                
                Console.Write("LÅN:\n" +
                              "[1] Mina lån\n" +
                              "[2] Ansök om nytt lån\n" +
                              "[3] Återgå till huvudmenyn\n" +
                              "\n" +
                              "Menyval: ");

                string choice = Console.ReadLine(); 

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Mina lån");
                        Console.ReadKey(); //Ta ev bort sen när det finns en metod
                        break;
                    case "2":
                        Console.WriteLine("Ansök om nytt lån");
                        Console.ReadKey(); //Ta ev bort sen när det finns en metod
                        break;
                    case "3":
                        runCustomerMenuLoan = false;
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Felaktig input! Tryck enter och försök igen!");
                        Console.ReadKey();
                        break;
                }
            }
        }
}