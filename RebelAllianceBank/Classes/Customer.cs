﻿using RebelAllianceBank.Interfaces;
using RebelAllianceBank.utils;
namespace RebelAllianceBank.Classes
{
    public class Customer : IUser
    {
        private List<IBankAccount> BankAccounts = [];

        public void ShowBankAccounts()
        {
            Console.WriteLine("Konton");

            if (BankAccounts.Count == 0)
            {
                Console.WriteLine("Du har inga konton att visa");
                return;
            }


            List<string> bodyKeys = [];
            foreach (var BankAccount in BankAccounts)
            {
                bodyKeys.Add(BankAccount.AccountName);
                bodyKeys.Add(BankAccount.Balance.ToString("N2"));
            }
            Markdown.Table(["Konto Namn", "Saldo"], bodyKeys);
        }
    }
}
