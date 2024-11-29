﻿using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Classes
{
    public class Admin : IUser
    {
        private ExchangeRate exchangeRate = new ExchangeRate();
        public int ID { get; set; }
        public string PersonalNum { get; set; }
        public string Password { get; set; }
        public string Surname { get; set; }
        public string Forename { get; set; }
        public bool LoginLock { get; set; } = false;
        
        public Admin() { }
        public Admin(string pNum, string password, string surname, string forename)
        {
            PersonalNum = pNum;
            Password = password;
            Surname = surname;
            Forename = forename;
        }
        public void UpDateCurrency()
        {
            bool runLoop = true;

            while (runLoop)
            {
                Console.Clear();

                Console.WriteLine("UPPDATERA VÄXELKURS: \n" +
                                  "[1] Instruktioner\n" +
                                  "[2] Skriv ut växelkurser\n" +
                                  "[2] Ladda upp nya växelkurser\n" +
                                  "[3] Avbryt och återgå till föregående meny");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("INSTRUKTIONER UPPDATERA VÄXELKURS\n\n" +
                                          "För att ladda upp växelkurs gör följande:\n" +
                                          "1. Gå till länk: https://www.ecb.europa.eu/stats" +
                                          "/policy_and_exchange_rates/euro_reference_exchange_rates/html/index.en.html\n" +
                                          "2. Ladda ner en csv-fil med de senaste växelkurserna\n" +
                                          "3. Kopiera och klistra in hela första raden med valuta-namn (inkl \"Date\")\n" +
                                          "4. Kopiera och klistra in hela andra raden med växelkurser (inklusive datum)\n" +
                                          "\n" +
                                          "Tryck enter när du är redo att fortsätta");
                        Console.ReadKey();
                        break;
                    case "2":
                        exchangeRate.PrintAllRates();
                        Console.WriteLine("\nTryck enter när du är redo att fortsätta");
                        Console.ReadKey();
                        break;
                    case "3":
                        string input = exchangeRate.PasteAndMatchExchangeRates();
                        if (input == "correct")
                        {
                            exchangeRate.AddExchangeRates();
                            bool correctUpdate = exchangeRate.CheckAddedExchangeRates();
                            if (correctUpdate)
                            {
                                runLoop = false;
                            }
                            else
                            {
                                Console.WriteLine("Tryck enter för att återgå till menyn och göra ett nytt försök");
                                Console.ReadLine();
                            } 
                        }
                        else if (input == "incorrect")
                        {
                            Console.Clear();
                            Console.WriteLine("Något blev inte helt rätt när du klistrade in dina rader. Läs instruktionerna" +
                                              " och försök sen igen! Tryck enter för att fortsätta!");
                            Console.ReadKey(); 
                        }
                        break;
                    case "4":
                        runLoop = false;
                        break;
                    default:
                        Console.WriteLine("Felaktig inmatning! Försök igen!");
                        break;
                }
            }
        }
    }
}