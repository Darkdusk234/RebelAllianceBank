namespace RebelAllianceBank.Interfaces
{
    public interface IBankAccount
    {
        public int ID { get; set; }
        public string UserId { get; set; }
        //public long AccountNr { get; set; }
        public int AccountType { get; set; }
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
        public string AccountCurrency { get; set; }
        public decimal IntrestRate { get; set; }

        public void AddToTransactionLog(Transaction newTransaction);

        public void ShowTransactionLog(); 

    }
}