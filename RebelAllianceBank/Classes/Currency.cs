using System.Collections.Specialized;
using System.Globalization;

namespace RebelAllianceBank.Classes
{
       public class Currency
       {
              //The data that will ube used to update the exchangerates come from European Central Bank. Therefore, 
              //the rates are compared to EUR in the back-ground. 
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
              public decimal ExchangeRateToSEK
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

              public Currency(string name, string country)
              {
                     Name = name;
                     Country = country;
              }

       }
       
}
