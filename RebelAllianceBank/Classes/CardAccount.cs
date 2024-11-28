﻿using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Classes
{
    public class CardAccount : IBankAccount
    {
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
        public string AccountCurrency { get; set; }
        public decimal IntrestRate { get; set; } = 13.95m;

        public CardAccount(string accountName, decimal balance, string accountCurrency, decimal intrestRate)
        {
            AccountName = accountName;
            Balance = balance;
            AccountCurrency = accountCurrency;
            IntrestRate = intrestRate;
        }
    }
}
