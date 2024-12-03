using RebelAllianceBank.Interfaces;

namespace RebelAllianceBank.Classes
{
    public class Loan
    {
        public string UserId { get; set; }
        public decimal LoanRent { get; set; } = 5.4m;
        public decimal loanedAmount { get; set; }
   }
}
