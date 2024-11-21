using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Classes
{
    public class Admin : IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
