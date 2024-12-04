using RebelAllianceBank.Interfaces;
using RebelAllianceBank.utils;
using RebelAllianceBank.Users;

namespace RebelAllianceBank.Menu
{
    public class AdminMenu : Menu
    {
        private List<IUser> _users;
        private Admin _currentAdmin;

        public AdminMenu(IUser currentUser, List<IUser> users) : base(currentUser)
        {
            this._users = users;
            _currentAdmin = (Admin?)currentUser; 
        }
        public override void Show()
        {
            string[] options = { "Skapa användare", "Ändra växelkurs", "Lås upp användarkonto???", "Logga ut" };
            bool runAdminMenu = true;

            while (runAdminMenu)
            {
                int choice = MarkdownUtils.HighLightChoiceWithMarkdown(
                    cancel: false,
                    columnHeaders: new[] { "MENY - ADMIN" },
                    filterData: new List<string>(options),
                    inData: option => new[] { option });

                switch (choice)
                {
                    case 0:
                        _currentAdmin.CreateUser(_users);
                        break;
                    case 1:
                        _currentAdmin.UpDateCurrency();
                        break;
                    case 2:
                        _currentAdmin.UnlockUser(_users);
                        break;
                    case 3:
                        runAdminMenu = false;
                        break;
                }
            }
        }
    }
}
