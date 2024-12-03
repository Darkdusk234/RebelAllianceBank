using RebelAllianceBank.Interfaces;

namespace RebelAllianceBank.Classes
{
    public class Loan
    {
        public string UserId { get; set; }
        public decimal LoanRent { get; set; }


        public Loan()
        {
            LoanRent = 5.4m;
            
        }

    }
}
