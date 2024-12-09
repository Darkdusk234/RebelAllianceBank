﻿using RebelAllianceBank.Classes;
using RebelAllianceBank.Interfaces;
using RebelAllianceBank.Users;
using RebelAllianceBank.utils;

namespace RebelAllianceBank.Menu
{
    public class CustomerMenu : Menu
    {
        private List<IUser> _users;
        private Customer _currentCustomer;
        private List<IUser> _listUsers;

        public CustomerMenu(IUser currentUser, List<IUser> users) : base(currentUser)
        {
            _currentCustomer = (Customer?)CurrentUser;
            _users = users;
        }

        public override void Show()
        {
            List<string> options = ["Konton", "Betala/Överföra", "Lån", "Logga ut"];
            bool runCustomerMenu = true;
            while (runCustomerMenu)
            {
                int choice = MarkdownUtils.HighLightChoiceWithMarkdown(
                    cancel: false,
                    columnHeaders: new[] { $"MENY - {CurrentUser.Surname}" },
                    filterData: new List<string>(options),
                    inData: option => new[] { option });


                switch (choice)
                {
                    case 0:
                        CustomerMenuAccounts();
                        break;
                    case 1:
                        CustomerMenuTransaction();
                        break;
                    case 2:
                        CustomerMenuLoan();
                        break;
                    case 3:
                        runCustomerMenu = false;
                        break;
                }
            }
        }
        public void CustomerMenuLoan()
        {
            List<string> options = ["Mina lån", "Ansök om nytt lån", "Återgå till huvudmenyn"];
            bool runCustomerMenuLoan = true;
            while (runCustomerMenuLoan)
            {
                int choice = MarkdownUtils.HighLightChoiceWithMarkdown(
                    cancel: false,
                    columnHeaders: new[] { $"Lån - {CurrentUser.Surname}" },
                    filterData: new List<string>(options),
                    inData: option => new[] { option });

                switch (choice)
                {
                    case 0:
                        _currentCustomer.DisplayLoans();
                        break;
                    case 1:
                        _currentCustomer.TakeLoan();
                        break;
                    case 2:
                        runCustomerMenuLoan = false;
                        break;
                }
            }
        }
        public void CustomerMenuAccounts()
        {
            List<string> options = ["Visa konton", "Öppna nytt konto", "Återgå till huvudmenyn"];
            bool runCustomerMenuAccounts = true;

            while (runCustomerMenuAccounts)
            {
                int choice = MarkdownUtils.HighLightChoiceWithMarkdown(
                    cancel: false,
                    columnHeaders: new[] { $"Konton meny - {CurrentUser.Surname}" },
                    filterData: new List<string>(options),
                    inData: option => new[] { option });


                switch (choice)
                {
                    case 0:
                        Console.Clear();
                        _currentCustomer.ShowBankAccounts();
                        break;
                    case 1:
                        Console.Clear();
                        _currentCustomer.CreateAccount();
                        break;
                    case 2:
                        runCustomerMenuAccounts = false;
                        break;
                }
            }
        }
        private void CustomerMenuTransaction()
        {
            bool runCustomerMenuTransaction = true;
            List<String> options = ["Ny överföring", "Överföring till externa konton", "Återgå till huvudmenyn"];

            while (runCustomerMenuTransaction)
            {
                int choice = MarkdownUtils.HighLightChoiceWithMarkdown(
                    cancel: false,
                    columnHeaders: new[] { $"Lån - {CurrentUser.Surname}" },
                    filterData: new List<string>(options),
                    inData: option => new[] { option });

                switch (choice)
                {
                    case 0:
                        _currentCustomer.Transfer();
                        break;
                    case 1:
                        _currentCustomer.TransferUserToUser(_users);
                        break;
                    case 2:
                        runCustomerMenuTransaction = false;
                        break;
                }
            }
        }
    }
}
