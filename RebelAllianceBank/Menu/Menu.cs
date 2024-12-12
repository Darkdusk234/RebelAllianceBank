using RebelAllianceBank.Interfaces;

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
