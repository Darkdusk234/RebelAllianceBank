using RebelAllianceBank.Interfaces;
using RebelAllianceBank.utils;

namespace RebelAllianceBank.Menu
{
    public abstract class Menu
    {
        protected IUser CurrentUser { get; }
        protected Menu(IUser currentUser)
        {
            CurrentUser = currentUser;
        }
        public abstract void Show();
    }
}
