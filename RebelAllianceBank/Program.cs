﻿using RebelAllianceBank.Classes;
using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();
            bank.Run();
        }
    }
}
