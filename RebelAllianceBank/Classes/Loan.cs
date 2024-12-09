using RebelAllianceBank.Interfaces;

namespace RebelAllianceBank.Classes
{
    public class Loan
    {
        public string UserId { get; set; }
        public decimal LoanRent { get; set; } = 5.4m;
        public decimal LoanedAmount { get; set; }
        public int MonthsToPayBack { get; set; }
        public decimal MounthlyPayment { get; set; }
        public decimal RemainingLoan { get; set; }
        public DateTime LoanDate { get; set; }
        
        public List<Loan> LoanList = new List<Loan>();

        public Loan()
        {

        }
        public Loan(decimal loanedAmount, int mounthsToLoan, List<Loan> account)
        {
            LoanedAmount = loanedAmount;
            MonthsToPayBack = mounthsToLoan * 12;
            LoanDate = DateTime.Now; // Sets a time and date when customer takes a loan.
            RemainingLoan = LoanedAmount;
            MounthlyPayment = CalculateMounthlyPayment();
            LoanList = account;
        }
        
        public decimal CalculateMounthlyPayment()
        {
            decimal rent = LoanRent / 100; // Convert rent to a better format to calculate %. Ex 5,4 / 100 = 0,054. 
            decimal mounthlyRent = (LoanedAmount * rent) / MonthsToPayBack; // Calculate mounthly rent. Ex (100 000 * 0,054) / 12(1Y). 450
            decimal mounthlyPayment = (LoanedAmount + mounthlyRent) / 12; // Calculate mounthly cost based on loan. Ex (100 000 + 450) / 12.

            return mounthlyPayment;
        }
        public void PayOffLoan(decimal amount)
        {
            if (RemainingLoan == 0)
            {
                Console.WriteLine("Du har inget lån att betala tillbaka.");
                return;
            }
            else
            {
                bool payBack = false;
                while (!payBack)
                {
                    if (amount <= 0)
                    {
                        Console.WriteLine("Betalningen måste vara större än 0.");
                        Console.ReadKey();
                        return;
                    }
                    else if (amount > RemainingLoan)
                    {
                        Console.WriteLine("Lånet är mindre än betalbeloppet.");
                    }

                    RemainingLoan -= amount;
                    payBack = true;

                    if (RemainingLoan <= 0)
                    {
                        Console.WriteLine("Lånet är helt återbetalt.");
                        RemainingLoan = 0;
                    }
                    else
                    {
                        Console.WriteLine($"Betalning mottagen. Återstående skuld: {RemainingLoan:C}");
                    }
                }
            }
        }
    }
}