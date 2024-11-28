using System.Collections.Specialized;
using System.Globalization;

namespace RebelAllianceBank.Classes
{
       public class Currency
       {
              private decimal _exchangeRateToEUR;
              
              public string Name { get; set; }
              public string Country { get; set; }

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

              public Currency(string name, string country)
              {
                     Name = name;
                     Country = country;
              }

       }
       
}
