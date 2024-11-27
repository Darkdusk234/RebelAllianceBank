namespace RebelAllianceBank.Interfaces
{
    public interface IUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool LoginLock { get; set; }
    }
}
