namespace RebelAllianceBank.Interfaces
{
    public interface IUser
    {
        public int ID { get; set; }
        public string PersonalNum {get; set;}
        public string Password { get; set; }
        public string Surname { get; set; }
        public string Forename { get; set; }
        public bool LoginLock { get; set; }
    }
}
