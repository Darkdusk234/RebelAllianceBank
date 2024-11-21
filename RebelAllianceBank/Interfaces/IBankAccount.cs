namespace RebelAllianceBank.Interfaces
{
    public interface IBankAccount
    {
        public string accountName { get; set; }
        public decimal balance { get; set; }
        public string accountCurrency { get; set; }
    }
}
