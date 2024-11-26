using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Classes
{
    public class Admin : IUser
    {
        public int ID { get; set; }
        public string PersonalNum { get; set; }
        public string Password { get; set; }
        public string Surname { get; set; }
        public string Lastname { get; set; }
        public Admin() { }
        public Admin(string pNum, string password)
        {
            PersonalNum = pNum;
            Password = password;
        }
    }
}
