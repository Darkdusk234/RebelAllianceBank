namespace RebelAllianceBank.Classes
{
    public class Bank
    {
        public static void Run()
        {
            var exchangeRate = new ExchangeRate(); 
            exchangeRate.UpDateCurrency();
        }
    }
}
