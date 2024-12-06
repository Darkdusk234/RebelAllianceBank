using RebelAllianceBank.Classes;
using RebelAllianceBank.Interfaces;
using RebelAllianceBank.Users;
using RebelAllianceBank.utils;

namespace RebelAllianceBank.Menu
{
    public class CustomerMenu : Menu
    {
        private Customer _currentCustomer;
        private List<IUser> _users;

        public CustomerMenu(IUser currentUser, List<IUser> users) : base(currentUser)
        {
            _currentCustomer = (Customer?)CurrentUser;
            _users = users;
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
                        _currentCustomer.DisplayLoan();
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
            string[] options = { "Insättning", "Ny överföring", "Överföring till externa konton", "Återgå till huvudmenyn" };

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
                        _currentCustomer.Deposit();
                        break;
                    case 1:
                        _currentCustomer.Transfer();
                        break;
                    case 2:
                        _currentCustomer.TransferUserToUser(_users);
                        break;
                    case 3:
                        runCustomerMenuTransaction = false;
                        break;
                }
            }
        }
    }
}
