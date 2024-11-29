using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Channels;
using RebelAllianceBank.Classes;

namespace RebelAllianceBank;

public class ExchangeRate
{
    private Dictionary<string, Currency> _exchangeRates = new Dictionary<string, Currency>();

    private string[] _currenciesToUpdate; 
    private string[] _exchangeRatesToUpdate;
        
    public ExchangeRate()
    {
        var usd = new Currency();
        _exchangeRates.Add("USD", usd);
        var jpy = new Currency();
        _exchangeRates.Add("JPY", jpy);
        var bgn = new Currency();
        _exchangeRates.Add("BGN", bgn);
        var czk = new Currency();
        _exchangeRates.Add("CZK", czk);
        var dkk = new Currency();
        _exchangeRates.Add("DKK", dkk);
        var gbp = new Currency();
        _exchangeRates.Add("GBP", gbp);
        var huf = new Currency();
        _exchangeRates.Add("HUF", huf);
        var pln = new Currency();
        _exchangeRates.Add("PLN", pln);
        var ron = new Currency();
        _exchangeRates.Add("RON", ron);
        var sek = new Currency();
        _exchangeRates.Add("SEK", sek);
        var chf = new Currency();
        _exchangeRates.Add("CHF", chf);
        var isk = new Currency();
        _exchangeRates.Add("ISK", isk);
        var nok = new Currency();
        _exchangeRates.Add("NOK", nok);
        var tryT = new Currency();
        _exchangeRates.Add("TRY", tryT);
        var aud = new Currency();
        _exchangeRates.Add("AUD", aud);
        var brl = new Currency();
        _exchangeRates.Add("BRL", brl);
        var cad = new Currency();
        _exchangeRates.Add("CAD", cad);
        var cny = new Currency();
        _exchangeRates.Add("CNY", cny);
        var hkd = new Currency();
        _exchangeRates.Add("HKD", hkd);
        var idr = new Currency();
        _exchangeRates.Add("IDR", idr);
        var ils = new Currency();
        _exchangeRates.Add("ILS", ils);
        var inr = new Currency();
        _exchangeRates.Add("INR", inr);
        var krw = new Currency();
        _exchangeRates.Add("KRW", krw);
        var mxn = new Currency();
        _exchangeRates.Add("MXN", mxn);
        var myr = new Currency();
        _exchangeRates.Add("MYR", myr);
        var nzd = new Currency();
        _exchangeRates.Add("NZD", nzd);
        var php = new Currency();
        _exchangeRates.Add("PHP", php);
        var sgd = new Currency();
        _exchangeRates.Add("SGD", sgd);
        var thb = new Currency();
        _exchangeRates.Add("THB", thb);
        var zar = new Currency();
        _exchangeRates.Add("ZAR", zar);
        
        AddDefaultExchangeRates();
    }
    
    private void AddDefaultExchangeRates()
    {
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

    public string PasteAndMatchExchangeRates()
    {
        Console.Clear();
        Console.WriteLine("Öppna länk: https://www.ecb.europa.eu/stats/policy_and_exchange_rates/euro_reference_exchange" +
                      "_rates/html/index.en.html\n" +
                      "\n" +
                      "1. Här lägger du in rad ett med valutor (inklusive \"Date\") från din CVS-fil:\n" +
                      "(för att läsa instruktionerna igen, ange QUIT) ");
        string currenciesString = Console.ReadLine();

        if (currenciesString.ToLower() == "quit")
        {
            return "quit";
        }

        Console.WriteLine("\n2. Här lägger du in rad två med växelkurser (inklusive datum-info)\n" +
                      "(för att se instruktioner igen, ange QUIT): ");
        string exchangeRatesString = Console.ReadLine();

        if (exchangeRatesString.ToLower() == "quit")
        {
            return "quit";
        }
        SplitStrings(currenciesString, exchangeRatesString);

        if (_currenciesToUpdate.Length != _exchangeRatesToUpdate.Length - 2)
        {
            return "incorrect";
        }
        else
        {
            return "correct";
        }
    }
    
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

    public bool CheckAddedExchangeRates()
    {
        
        while (true)
        {
            Console.WriteLine("Du har lagt in följande växelkurser: ");
            PrintAllRates();
            Console.WriteLine("Ser detta korrekt ut? ja/nej");
            string answer = Console.ReadLine();

            switch (answer)
            {
                case "ja":
                    return true; 
                    break;
                case "nej":
                    return false; 
                    break;
                default:
                    Console.WriteLine("Ogiltigt val! Tryck enter och försök igen");
                    Console.ReadKey();
                    break;
            }
        }
    }
    
    public void PrintAllRates()
    {
        Console.WriteLine("VÄXELKURS:");
        foreach (var currency in _exchangeRates)
        {
            Console.WriteLine(currency.Key + "   " + currency.Value.ExchangeRateToEUR);
        }
        Console.WriteLine($"Senast uppdaterad den {_exchangeRatesToUpdate[0]} {_exchangeRatesToUpdate[1]} " +
                          $"{_exchangeRatesToUpdate[2]}");
    }

    public decimal CaclulateExchangeRate(string CurrencyFrom, string CurrencyTo)
    {
        decimal calcExchangeRate = _exchangeRates[CurrencyFrom].ExchangeRateToEUR * 
                               _exchangeRates[CurrencyFrom].ExchangeRateToEUR;
        return calcExchangeRate; 
    }

}