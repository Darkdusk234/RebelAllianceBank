using RebelAllianceBank.Interfaces;

namespace RebelAllianceBank.Classes
{
    public class Loan
    {
        public string UserId { get; set; }
        public decimal LoanRent { get; set; } = 5.4m;
        public decimal LoanedAmount { get; set; }
        public int MounthToPayBack { get; set; }
        public DateTime LoanDate { get; set; }
        
        public Loan(decimal loandAmount, IBankAccount konto)
        {
            LoanedAmount = loandAmount;
            MounthToPayBack = 12;
            LoanDate = DateTime.Now;
        }
        public void DisplayAllLoans(List<Loan> loanList)
        {
            foreach (var loan in loanList)
            {
                Console.WriteLine($"Konto: {loan.LoanedAmount}");
            }
        }
    }
}
