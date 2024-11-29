namespace RebelAllianceBank.Interfaces
{
    public interface IBankAccount
    {
        public int ID { get; set; }
        public int AccountType { get; set; }
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
        public string AccountCurrency { get; set; }
        public decimal IntrestRate { get; set; }
        
        private List

    }
}
