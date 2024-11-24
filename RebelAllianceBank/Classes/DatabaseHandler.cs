using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RebelAllianceBank.Interfaces;
namespace RebelAllianceBank.Classes
{
    internal class DatabaseHandler
    {
        private string _connect = @"Data Source=Database\RebelAllianceBankDB.db;Version=3;";
        public List<IUser> GetUsersFromDatabase()
        {
            List<IUser> users = new List<IUser>();
            using (SQLiteConnection connection = new SQLiteConnection(_connect))
            {
                string query = "SELECT * FROM Users";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string username = reader.GetString(1);
                        string password = reader.GetString(2);
                        bool isAdmin = reader.GetBoolean(3);

                        if (isAdmin)
                        {
                            users.Add(new Admin
                            {
                                ID = id,
                                Username = username,
                                Password = password,
                                IsAdmin = isAdmin
                            });
                        }
                        else
                        {
                            users.Add(new Customer
                            {
                                ID = id,
                                Username = username,
                                Password = password,
                                IsAdmin = isAdmin
                            });
                        }
                    }
                }
            }
            return users;
        }
        public IUser WriteUserToDatabase(int id, string un, string pw, bool isAdmin)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connect))
            {
                string query = "INSERT INTO Users (Id, Username, Password, IsAdmin) VALUES (@id, @un, @pw, @isAdmin)";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@un", un);
                command.Parameters.AddWithValue("@pw", pw);
                command.Parameters.AddWithValue("@isAdmin", isAdmin);
                connection.Open();
                command.ExecuteNonQuery();
            }
            return isAdmin ?
                new Admin { ID = id, Username = un, Password = pw, IsAdmin = isAdmin }
                : new Customer { ID = id, Username = un, Password = pw, IsAdmin = isAdmin };
        }

        public List<IBankAccount> GetAccountsFromDatabase()
        {
            List<IBankAccount> accounts = new List<IBankAccount>();
            using (SQLiteConnection connection = new SQLiteConnection(_connect))
            {
                string query = "SELECT * FROM Accounts";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                connection.Open();
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        int accountType = reader.GetInt32(1);
                        string accountName = reader.GetString(2);
                        decimal balance = reader.GetDecimal(3);
                        string currency = reader.GetString(4);

                        IBankAccount account = accountType switch
                        {
                            0 => new CardAccount
                            {
                                ID = id,
                                AccountType = accountType,
                                AccountName = accountName,
                                Balance = balance,
                                AccountCurrency = currency
                            },
                            1 => new SavingsAccount
                            {
                                ID = id,
                                AccountType = accountType,
                                AccountName = accountName,
                                Balance = balance,
                                AccountCurrency = currency
                            },
                            2 => new ISK
                            {
                                ID = id,
                                AccountType = accountType,
                                AccountName = accountName,
                                Balance = balance,
                                AccountCurrency = currency
                            },
                            _ => throw new ArgumentException("Felaktigt bankkonto")
                        };
                        accounts.Add(account);
                    }
                }
            }
            return accounts;
        }
        public IBankAccount WriteAccountToDatabase(int id, int type, string name, decimal balance, string currency)
        {
            using (SQLiteConnection connection = new SQLiteConnection(_connect))
            {
                string query = "INSERT INTO Accounts (Id, AccountType, AccountName, Balance, Currency) VALUES (@id, @type, @name, @balance, @currency)";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                return type switch
                {
                    0 => new CardAccount
                    {
                        ID = id,
                        AccountType = type,
                        AccountName = name,
                        Balance = balance,
                        AccountCurrency = currency
                    },
                    1 => new SavingsAccount
                    {
                        ID = id,
                        AccountType = type,
                        AccountName = name,
                        Balance = balance,
                        AccountCurrency = currency
                    },
                    2 => new ISK
                    {
                        ID = id,
                        AccountType = type,
                        AccountName = name,
                        Balance = balance,
                        AccountCurrency = currency
                    },
                    _ => throw new ArgumentException("felaktigt konto")
                };
            }
        }
    }
}
