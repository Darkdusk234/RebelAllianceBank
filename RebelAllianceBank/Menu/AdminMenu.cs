using RebelAllianceBank.Interfaces;
using RebelAllianceBank.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RebelAllianceBank.Menu
{
    public class AdminMenu : Menu
    {
        private List<IUser> users;

        public AdminMenu(IUser currentUser, List<IUser> users) : base(currentUser)
        {
            this.users = users;
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
                        Console.WriteLine("Skapa användare");
                        break;
                    case 1:
                        Console.WriteLine("Ändra växelkurs");
                        break;
                    case 2:
                        Console.WriteLine("Lås upp användarkonto???");
                        break;
                    case 3:
                        runAdminMenu = false;
                        break;
                }
                Console.ReadKey();
            }
        }
    }
}
