using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Threading.Channels;
using RebelAllianceBank.Classes;
using RebelAllianceBank.utils;

namespace RebelAllianceBank;

public class ExchangeRate
{
    //A dictionary to gather all currency options. Key is the abbreviation of the currency and value are instances of
    //the currency class.
    private Dictionary<string, Currency> _exchangeRates = new Dictionary<string, Currency>();

    //These lists will be used when updating the exchange rates of the currencis (and when setting defautl values)
    private string[] _currenciesToUpdate; 
    private string[] _exchangeRatesToUpdate;
        
    public ExchangeRate()
    {
        var usd = new Currency("dollar", "USA");
        _exchangeRates.Add("USD", usd);
        var jpy = new Currency("yen", "Japan");
        _exchangeRates.Add("JPY", jpy);
        var bgn = new Currency("lev", "Bulgarien");
        _exchangeRates.Add("BGN", bgn);
        var czk = new Currency("kronor", "Tjeckien");
        _exchangeRates.Add("CZK", czk);
        var dkk = new Currency("kronor", "Danmark");
        _exchangeRates.Add("DKK", dkk);
        var gbp = new Currency("pund", "Storbritannien");
        _exchangeRates.Add("GBP", gbp);
        var huf = new Currency("forint", "Ungern");
        _exchangeRates.Add("HUF", huf);
        var pln = new Currency("zloty", "Polen");
        _exchangeRates.Add("PLN", pln);
        var ron = new Currency("leu", "Rumänien");
        _exchangeRates.Add("RON", ron);
        var sek = new Currency("kronor", "Sverige");
        _exchangeRates.Add("SEK", sek);
        var chf = new Currency("franc", "Schweiz");
        _exchangeRates.Add("CHF", chf);
        var isk = new Currency("kronor","Island");
        _exchangeRates.Add("ISK", isk);
        var nok = new Currency("kronor", "Norge");
        _exchangeRates.Add("NOK", nok);
        var tryT = new Currency("ny lira", "Turkiet");
        _exchangeRates.Add("TRY", tryT);
        var aud = new Currency("dollar", "Autralien");
        _exchangeRates.Add("AUD", aud);
        var brl = new Currency("real", "Brasilien");
        _exchangeRates.Add("BRL", brl);
        var cad = new Currency("dollar", "Canada");
        _exchangeRates.Add("CAD", cad);
        var cny = new Currency("yuan renminbi", "Kina");
        _exchangeRates.Add("CNY", cny);
        var hkd = new Currency("dollar", "Honkong");
        _exchangeRates.Add("HKD", hkd);
        var idr = new Currency("rupee", "Indonesien");
        _exchangeRates.Add("IDR", idr);
        var ils = new Currency("shekel", "Israel");
        _exchangeRates.Add("ILS", ils);
        var inr = new Currency("rupee", "Indien");
        _exchangeRates.Add("INR", inr);
        var krw = new Currency("won", "Sydkorea");
        _exchangeRates.Add("KRW", krw);
        var mxn = new Currency("nuevo peso", "Mexiko");
        _exchangeRates.Add("MXN", mxn);
        var myr = new Currency("ringgit", "Malaysia");
        _exchangeRates.Add("MYR", myr);
        var nzd = new Currency("dollar", "Nya Zeeland");
        _exchangeRates.Add("NZD", nzd);
        var php = new Currency("peso", "Fillipinerna");
        _exchangeRates.Add("PHP", php);
        var sgd = new Currency("dollar", "Singapore");
        _exchangeRates.Add("SGD", sgd);
        var thb = new Currency("baht", "Thailand");
        _exchangeRates.Add("THB", thb);
        var zar = new Currency("rand", "Sydafrika");
        _exchangeRates.Add("ZAR", zar);
        
        AddDefaultExchangeRates();
    }
    /// <summary>
    /// A method for populate the currency objects with default exchange rates when the program is started. 
    /// </summary>
    private void AddDefaultExchangeRates()
    {
        //default data from ECB on nov 25th 2024. 
        string currenciesStringDefault = "Date, USD, JPY, BGN, CZK, DKK, GBP, HUF, PLN, RON, SEK, CHF, ISK, NOK, " +
                                         "TRY, AUD, BRL, CAD, CNY, HKD, IDR, ILS, INR, KRW, MXN, MYR, NZD, PHP, SGD, " +
                                         "THB, ZAR";
        string exchangeRateStringDefault = "25 November 2024, 1.0495, 161.64, 1.9558, 25.295, 7.4586, 0.83465, 409.78, " +
                                           "4.3185, 4.9767, 11.5030, 0.9324, 145.30, 11.5865, 36.3020, 1.6111, 6.0941, " +
                                           "1.4648, 7.6018, 8.1659, 16651.89, 3.8493, 88.4595, 1469.87, 21.3301, 4.6724, " +
                                           "1.7926, 61.936, 1.4128, 36.271, 18.9592, ";
        
        SplitStrings(currenciesStringDefault, exchangeRateStringDefault);

        AddExchangeRates();
    }
    /// <summary>
    /// This method splits the strings of data and makes it to arrays of currencies and exchange rates. Used when adding
    /// default data as well as when updating the axchangeRates (admin).
    /// </summary>
    /// <param name="currenciesString"></param>
    /// <param name="exchangeRatesString"></param>
    public void SplitStrings(string currenciesString, string exchangeRatesString)
    {
        _currenciesToUpdate = currenciesString.Split(new[] { ',', ' ' }, 
            StringSplitOptions.RemoveEmptyEntries);
        
        _exchangeRatesToUpdate = exchangeRatesString.Split(new[] { ',', ' ' }, 
            StringSplitOptions.RemoveEmptyEntries);
        
        for(int i = 3; i < _exchangeRatesToUpdate.Length; i++)
        {
            _exchangeRatesToUpdate[i] = _exchangeRatesToUpdate[i].Replace(".", ",");
        }
    }
    /// <summary>
    /// In this method, the usesr will paste currencies and exchangerates from ECB data according to instructions.
    /// Strings will be made into arrays with SplitStrings(). If the correlated lengths of the arrays are correct,
    /// the method will return "correct".
    /// </summary>
    /// <returns>
    /// "quit" - the user wish to get back to the update rates menu
    /// "correct" - the pasted string arrays of currencies and exchange rates have the correct lenght-correlations.
    /// If not, "incorrect" is returned
    /// </returns>
    public EnumsExchangeRate PasteAndMatchExchangeRates()
    {
        Console.Clear();
        Console.WriteLine("Öppna länk: https://www.ecb.europa.eu/stats/policy_and_exchange_rates/euro_reference_exchange" +
                          "_rates/html/index.en.html\n" +
                          "\n" +
                          "1. Här lägger du in rad ett med valutor (inklusive \"Date\") från din CVS-fil:\n" +
                          "(för att läsa instruktionerna igen, ange AVBRYT) ");
        string currenciesString = Console.ReadLine();

        if (currenciesString.ToLower() == "quit")
        {
            return EnumsExchangeRate.quit;
        }

        Console.WriteLine("\n2. Här lägger du in rad två med växelkurser (inklusive datum-info)\n" +
                          "(för att se instruktioner igen, ange AVBRYT): ");
        string exchangeRatesString = Console.ReadLine();

        if (exchangeRatesString.ToLower() == "quit")
        {
            return EnumsExchangeRate.quit;
        }
        SplitStrings(currenciesString, exchangeRatesString);
        
        //Since the currency data contains "Date", while exchange rate containse e.g."25 november 2024",the legnth of 
        //the exchangerates array needs to be decreased with 2 to match currencies. 
        if (_currenciesToUpdate.Length != _exchangeRatesToUpdate.Length - 2)
        {
            return EnumsExchangeRate.incorrect;
        }
        return EnumsExchangeRate.correct;
    }
    
    /// <summary>
    /// Adds the echange rates that correlates to the currencies in the dictionary. 
    /// </summary>
    public void AddExchangeRates()
    {
        for (int i = 1; i < _currenciesToUpdate.Length; i++)
        {
            if (_exchangeRates.ContainsKey(_currenciesToUpdate[i]))
            {
                _exchangeRates[_currenciesToUpdate[i]].ExchangeRateToEUR =
                    Convert.ToDecimal(_exchangeRatesToUpdate[i + 2]);
            }
        }
    }
    /// <summary>
    /// A method that print the updated rates and ask the admin to see that the correct data has been added. 
    /// </summary>
    /// <returns></returns>
    public bool CheckAddedExchangeRates()
    {
        Console.Clear();
        
        while (true)
        {
            Console.WriteLine("Du har lagt in följande växelkurser: ");
            PrintAllRates();
            Console.WriteLine("\nSer detta korrekt ut? ja/nej");
            string answer = Console.ReadLine();

            switch (answer)
            {
                case "ja":
                case "j":
                    return true; 
                    break;
                case "nej":
                case "n":
                    return false; 
                    break;
                default:
                    Console.WriteLine("Ogiltigt val! Tryck enter och försök igen");
                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                    break;
            }
        }
    }
    /// <summary>
    /// A method for printing all rates int the exchange dictionary. 
    /// </summary>
    public void PrintAllRates()
    {
        Console.WriteLine("VÄXELKURS:");
        foreach (var currency in _exchangeRates)
        {
            Console.WriteLine(currency.Key + "   " + currency.Value.ExchangeRateToEUR);
        }
        Console.WriteLine($"\nSenast uppdaterad den {_exchangeRatesToUpdate[0]} {_exchangeRatesToUpdate[1]} " +
                          $"{_exchangeRatesToUpdate[2]}");
    }
    /// <summary>
    /// A method for printing all currencies to the console, inkluding name and country. 
    /// </summary>
    public void PrintAllCurrencies()
    {
        Console.WriteLine("VALUTOR:");
        foreach (var currency in _exchangeRates)
        {
            Console.WriteLine($"{currency.Key}   {currency.Value.Name}, {currency.Value.Country}");
        }
    }
    /// <summary>
    /// A method for setting account currency when creating account.
    /// </summary>
    /// <returns></returns>
    public string SetAccountCurrency()
    {
        while (true)
        {
            Console.WriteLine("Önskar du annan valuta än SEK på ditt konto? ja/nej\n");
            string answer = Console.ReadLine();

            switch (answer.ToLower())
            {
                case "ja": 
                case "j":
                    string currency = ChooseAccountCurrency();
                    if (currency != "AVBRYT")
                    {
                        Console.WriteLine($"Du har angett att du vill ha valuta {currency} på ditt konto.Stämmer det? " +
                                          $"ja/nej");
                        string answer2 = Console.ReadLine().ToLower();
                        if (answer2 == "ja" || answer2 == "j")
                        {
                            return currency;
                        }
                    }
                    break; 
                case "nej":
                case "n":
                    return "SEK";
                    break;
                default:
                    Console.WriteLine("Ogiltig input! Tryck enter för att fortsätta");
                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) { };
                    break; 
            }
        }
    }
    /// <summary>
    /// A method for manually choose account currency, Is used in AccountCurrency if the user to want to have SEK. 
    /// </summary>
    /// <returns></returns>
    public string ChooseAccountCurrency()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("VÄLJ KONTOVALUTA:\n" +
                              "1. Lista alla valutor\n" +
                              "2. Välja valuta\n" +
                              "3. Avbryt");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    PrintAllCurrencies();
                    Console.WriteLine("\nTryck enter för att återgå och välja valuta!");
                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) { };
                    break; 
                case "2":
                    string currency = "";
                    
                    //a loop that runs until the currency is in the coorrect format, or the user wish to abort. 
                    while ((currency.Length != 3 || _exchangeRates.ContainsKey(currency) == false) && currency != "AVBRYT")
                    {
                        Console.WriteLine("Ange den valuta du önskar (tre bokstäver). Skriv AVBRYT för att återgå till " +
                                          "föregående meny");
                        if (currency == "AVBRYT")
                        {
                            break;
                        }
                        currency = Console.ReadLine().ToUpper();
                    }
                    if (currency != "AVBRYT")
                    {
                        return currency;
                    }
                    break; 
                case "3":
                    return "quit";
                default:
                    Console.WriteLine("Ogiltig input! Tryck enter för att fortsätta");
                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) { };
                    break; 
            }
        }
    }
    /// <summary>
    /// A metod that calculates the ExchangeRates that will be used when transfering mony betyween accounts. 
    /// </summary>
    /// <param name="CurrencyFrom"></param>
    /// <param name="CurrencyTo"></param>
    /// <returns></returns>
    public decimal CaclulateExchangeRate(string CurrencyFrom, string CurrencyTo)
    {
        decimal calcExchangeRate = _exchangeRates[CurrencyFrom].ExchangeRateToEUR * 
                                   _exchangeRates[CurrencyFrom].ExchangeRateToEUR;
        return calcExchangeRate; 
    }

}