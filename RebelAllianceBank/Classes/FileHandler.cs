using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RebelAllianceBank.Interfaces;

namespace RebelAllianceBank.Classes
{
    internal class FileHandler
    {
        public List<IUser> ReadUsersFromFile()
        {
            List<IUser> savedList = new List<IUser>();
            string filePath = "..\\..\\..\\users.txt";
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string read;
                    while ((read = sr.ReadLine()) != null)
                    {
                        string[] row = read.Split('-');
                        IUser user = StoredUser(row);
                        if (user != null)
                        {
                            savedList.Add(user);
                        }
                    }
                }
            }
            return savedList;
        }
        public IUser StoredUser(string[] row)
        {
            switch (row[3])
            {
                case "true":
                    return new Admin
                    {
                        ID = Convert.ToInt16(row[0]),
                        Username = row[1],
                        Password = row[2],
                        IsAdmin = bool.Parse(row[3])
                    };
                case "false":
                    return new Customer
                    {
                        ID = Convert.ToInt16(row[0]),
                        Username = row[1],
                        Password = row[2],
                        IsAdmin = bool.Parse(row[3])
                    };
                default:
                    return null;
            }
        }

        public IUser WriteUserToFile(int id, string un, string pw, bool isAdmin)
        {
            string filePath = "..\\..\\..\\users.txt";
            if (File.Exists(filePath))
            {
                using (StreamWriter sw = new StreamWriter(filePath, append: true))
                {
                    string write = $"{id}-{un}-{pw}-{isAdmin}";
                    sw.WriteLine(write, Environment.NewLine);
                }
            }
            IUser user = StoredUser(new string[] { id.ToString(), un, pw, isAdmin.ToString().ToLower() });
            if (user != null)
            {
                return user;
            }
            return null;
        }
        public List<IBankAccount> ReadAccountsFromFile()
        {
            List<IBankAccount> savedList = new List<IBankAccount>();
            string filePath = "..\\..\\..\\accounts.txt";
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string read;
                    while ((read = sr.ReadLine()) != null)
                    {
                        string[] row = read.Split('-');
                        IBankAccount account = StoredBankAccount(row);
                        if (account != null)
                        {
                            savedList.Add(account);
                        }
                    }
                }
            }
            return savedList;
        }
        public IBankAccount StoredBankAccount(string[] row)
        {
            switch (row[0])
            {
                case "0":
                    return new CardAccount
                    {
                        ID = Convert.ToInt32(row[0]),
                        AccountType = Convert.ToInt32(row[1]),
                        AccountName = row[2],
                        Balance = Convert.ToDecimal(row[3]),
                        AccountCurrency = row[4]
                    };
                case "1":
                    return new SavingsAccount
                    {
                        ID = Convert.ToInt32(row[0]),
                        AccountType = Convert.ToInt32(row[1]),
                        AccountName = row[2],
                        Balance = Convert.ToDecimal(row[3]),
                        AccountCurrency = row[4]
                    };
                case "2":
                    return new ISK
                    {
                        ID = Convert.ToInt32(row[0]),
                        AccountType = Convert.ToInt32(row[1]),
                        AccountName = row[2],
                        Balance = Convert.ToDecimal(row[3]),
                        AccountCurrency = row[4]
                    };
                default:
                    return null;
            }
        }
        public IBankAccount WriteAccountToFile(int id, int accType, string name, decimal balance, string curr)
        {
            string filePath = "..\\..\\..\\accounts.txt";
            if (File.Exists(filePath))
            {
                using (StreamWriter sw = new StreamWriter(filePath, append: true))
                {
                    string write = $"{id}-{accType}-{name}-{balance}-{curr}";
                    sw.WriteLine(write, Environment.NewLine);
                }
            }
            IBankAccount account = StoredBankAccount(new string[] { id.ToString(), accType.ToString(), name, balance.ToString(), curr });
            if (account != null)
            {
                return account;
            }
            return null;
        }
    }
}
