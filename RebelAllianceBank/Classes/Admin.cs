using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Classes
{
    public class Admin : IUser
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
