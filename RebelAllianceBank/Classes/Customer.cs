using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Classes
{
    public class Customer : IUser
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
