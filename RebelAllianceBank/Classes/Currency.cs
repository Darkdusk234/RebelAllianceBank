using System.Globalization;

namespace RebelAllianceBank.Classes
{
       public class Currency
       {
              private decimal _exchangeRateToEUR;
              
              public string Country { get; set; }
              public string CultureCodeStrings { get; set; }

              public decimal ExchangeRateToEUR
              {
                     get { return _exchangeRateToEUR; }
                     set
                     {
                            if (value > 0)
                            {
                                   _exchangeRateToEUR = value; 
                            }
                            else
                            {
                                   throw new ArgumentException("Exchange Rate must be larger than 0");
                            }
                     }
              }

              public Currency()
              {
              }

              public Currency(string country)
              {
                     Country = country; 
              }

       }
       
}
