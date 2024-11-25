using RebelAllianceBank.Classes;
using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var rebelAllianceBank = new Bank();
            rebelAllianceBank.Run();
        }
    }
}
