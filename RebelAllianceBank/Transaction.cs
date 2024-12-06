using RebelAllianceBank.Interfaces;

namespace RebelAllianceBank;
/// <summary>
/// A transaction class (copied from the work of Kim with the log) 
/// </summary>
public class Transaction
{
    public decimal Amount { get; set; }
    public IBankAccount AccountFrom { get; set; }
    public IBankAccount AccountTo { get; set; }
    public DateTime Timestamp{ get; set; }
    
    // a constructor for Deposits and money from a Loan when there is not accountFrom
    public Transaction(decimal amount, IBankAccount accountTo) 
    {
        Amount = amount; 
        AccountTo = accountTo;
    }
    //a constructor for transfers
    public Transaction(decimal amount, IBankAccount accountFrom, IBankAccount accountTo)
    {
        AccountFrom = accountFrom;
        AccountTo = accountTo; 
        Amount = amount;
    }
}