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
        
        public Loan()
        {

        }
        public Loan(decimal loanedAmount, int mounthsToLoan)
        {
            // implement ID
            LoanedAmount = loanedAmount;
            MonthsToPayBack = mounthsToLoan;
            LoanDate = DateTime.Now; // Sets a time and date when customer takes a loan.
            RemainingLoan = LoanedAmount;
            MounthlyPayment = CalculateMounthlyPayment();
        }
        
        public decimal CalculateMounthlyPayment()
        {
            decimal rentOnLoan = (LoanedAmount / 100) * LoanRent;
            decimal sum = LoanedAmount / MonthsToPayBack + rentOnLoan;
            return sum;
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