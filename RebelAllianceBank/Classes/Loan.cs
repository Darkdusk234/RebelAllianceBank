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
        public DateTime LoanDate { get; set; } // Sätter låndatum och tid.
        public Loan()
        {

        }
        //public Loan(decimal loandAmount, IBankAccount konto)
        //{
        //    LoanedAmount = loandAmount;
        //    MonthsToPayBack = 12;
        //    LoanDate = DateTime.Now;

        //}
        public Loan(decimal loanedAmount, int mounthsToLoan, IBankAccount account)
        {
            LoanedAmount = loanedAmount;
            MonthsToPayBack = mounthsToLoan * 12;
            LoanDate = DateTime.Now;
            RemainingLoan = LoanedAmount;
            MounthlyPayment = CalculateMounthlyPayment();
        }

        public void DisplayAllLoans(List<Loan> loanList, List<IBankAccount> accountList)
        {
            int count = 0;
            foreach (var account in accountList)
            {
                foreach (var loan in loanList)
                {
                    //Console.WriteLine($"{count}. Konto: {account.AccountName} | Loan Amount: {loan.LoanedAmount} {account.AccountCurrency} {LoanDate}");
                    Console.WriteLine($"#{count}.");
                    Console.WriteLine($"Konto: {account.AccountName}");
                    Console.WriteLine($"Lånbelopp: {loan.LoanedAmount}{account.AccountCurrency} med räntan {loan.LoanRent}%.");
                    Console.WriteLine($"Återbetalningstid: {loan.MonthsToPayBack} månader");
                    Console.WriteLine($"Månatlig betalning: {CalculateMounthlyPayment():C}");
                    Console.WriteLine($"Återstående skuld: {loan.RemainingLoan:C}");
                    count++;
                }
            }
        }
        /// <summary>
        /// Calculate the mounthly payment.
        /// </summary>
        /// <returns></returns>
        public decimal CalculateMounthlyPayment()
        {
            decimal rent = LoanRent / 100; // Chance rent to better format to calculate %. 5,4 / 100 = 0,054. 
            decimal mounthlyRent = (LoanedAmount * rent) / MonthsToPayBack; //Calculate mounthly rent 100 000 * 0,054 = 5400
            decimal mounthlyPayment = (LoanedAmount + mounthlyRent) / 12; // calculate mounthly cost based on loan.

            return mounthlyPayment;
        }
        public void PayOfLoan(decimal amount)
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
