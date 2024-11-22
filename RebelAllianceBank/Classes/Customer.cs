using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Classes
{
    public class Customer : IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Customer(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
