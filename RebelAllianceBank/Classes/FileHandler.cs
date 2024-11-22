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
                        if (row[2] == "true")
                        {
                            IUser adminUser = new Admin
                            {
                                Username = row[0],
                                Password = row[1],
                                IsAdmin = bool.Parse(row[2])
                            };
                            savedList.Add(adminUser);
                        }
                        else
                        {
                            IUser user = new Customer
                            {
                                Username = row[0],
                                Password = row[1],
                                IsAdmin = bool.Parse(row[2])
                            };
                            savedList.Add(user);
                        }
                    }
                }
            }
            return savedList;
        }
        public void WriteUserToFile(string un, string pw, bool isAdmin)
        {
            List<IUser> savedList = new List<IUser>();
            string filePath = "..\\..\\..\\users.txt";
            if (File.Exists(filePath))
            {
                using (StreamWriter sw = new StreamWriter(filePath, append: true))
                {
                    string write = un + "-" + pw + "-" + isAdmin;
                    sw.WriteLine(write, Environment.NewLine);
                }
            }
        }
    }
}
