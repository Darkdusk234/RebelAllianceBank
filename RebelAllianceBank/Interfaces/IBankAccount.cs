namespace RebelAllianceBank.Interfaces
{
    public interface IBankAccount
    {
        public long ID { get; set; }
        public string UserId { get; set; }
        public int AccountType { get; set; }
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
        public string AccountCurrency { get; set; }
        public decimal IntrestRate { get; set; }
        
    }
}