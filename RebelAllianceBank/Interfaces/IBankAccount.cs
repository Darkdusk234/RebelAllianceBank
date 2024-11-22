namespace RebelAllianceBank.Interfaces
{
    public interface IBankAccount
    {
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
        public string AccountCurrency { get; set; }
    }
}
