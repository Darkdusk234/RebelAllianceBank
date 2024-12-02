using RebelAllianceBank.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RebelAllianceBank.Users;
using RebelAllianceBank.utils;

namespace RebelAllianceBank.Menu
{
    public class CustomerMenu : Menu
    {
        private Customer _currentCustomer;

        public CustomerMenu(IUser currentUser) : base(currentUser)
        {
            _currentCustomer = (Customer?)CurrentUser; 
        }

        public override void Show()
        {
            string[] options = { "Konton", "Betala/Överföra", "Lån", "Logga ut" };
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
            string[] options = { "Mina lån", "Ansök om nytt lån", "Återgå till huvudmenyn" };
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
                        Console.WriteLine("Mina lån");
                        Console.ReadKey(); //Ta ev bort sen när det finns en metod
                        break;
                    case 1:
                        Console.WriteLine("Ansök om nytt lån");
                        Console.ReadKey(); //Ta ev bort sen när det finns en metod
                        break;
                    case 2:
                        runCustomerMenuLoan = false;
                        break;
                }
            }
        }
        public void CustomerMenuAccounts()
        {
            string[] options = { "Visa konton", "Öppna nytt konto", "Återgå till huvudmenyn" };
            bool runCustomerMenuAccounts = true;

            while (runCustomerMenuAccounts)
            {
                int choice = MarkdownUtils.HighLightChoiceWithMarkdown(
                    cancel: false,
                    columnHeaders: new[] { $"Lån - {CurrentUser.Surname}" },
                    filterData: new List<string>(options),
                    inData: option => new[] { option });

                switch (choice)
                {
                    case 0:
                        Console.Clear();
                        _currentCustomer.ShowBankAccounts();
                        Console.ReadKey(); //Ta ev bort sen när det finns en metod
                        break;
                    case 1:
                        Console.Clear();
                        _currentCustomer.CreateAccount();
                        Console.ReadKey();
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
            string[] options = { "Ny överföring", "Överföring till externa konton", "Återgå till huvudmenyn" };

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
                        Console.WriteLine("Ny överföring");
                        Console.ReadKey(); //Ta ev bort sen när det finns en metod
                        break;
                    case 1:
                        Console.WriteLine("Överföring till externa konton");
                        Console.ReadKey(); //Ta ev bort sen när det finns en metod
                        break;
                    case 2:
                        runCustomerMenuTransaction = false;
                        break;
                }
            }
        }
    }
}
