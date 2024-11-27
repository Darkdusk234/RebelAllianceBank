using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Classes
{
    public class Admin : IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool LoginLock { get; set; } = false;

        public Admin(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
