using RebelAllianceBank.Accounts;
using RebelAllianceBank.Classes;
using RebelAllianceBank.Interfaces;
using RebelAllianceBank.Users;
using System.Text;

namespace RebelAllianceBank.Classes
{
    public class FileHandler
    {
        private string _filePathUsers = "users.txt";
        private string _filePathAccounts = "accounts.txt";
        /// <summary>
        /// Reads a list of users from the file.
        /// </summary>
        /// <returns>A list of <see cref="IUser"/> objects read from the file.</returns>
        public List<IUser> ReadUserAndAccounts()
        {
            var users = ReadFromFile(_filePathUsers, StoredUser);
            var accounts = ReadFromFile(_filePathAccounts, StoredBankAccount);
            foreach (var user in users.OfType<Customer>())
            {
                user.GetListBankAccount().AddRange(accounts.Where(acc => acc.UserId == user.PersonalNum));
            }
            return users;
        }
        /// <summary>
        /// Reads data from a specified file and converts each line into an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of objects to be created from the file data.</typeparam>
        /// <param name="filePath">The path to the file that contains the data to be read.</param>
        /// <param name="StoredData">A delegate method that takes a string array (representing a line split into parts)
        /// and returns an object of type <typeparamref name="T"/>.</param>
        /// <returns>A list of objects of type <typeparamref name="T"/> created from the file data.</returns>
        public static List<T> ReadFromFile<T>(string filePath, Func<string[], T> StoredData)
        {
            List<T> savedList = new List<T>();
            if (File.Exists(filePath))
            {
                using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    string read;
                    while ((read = sr.ReadLine()) != null)
                    {
                        string[] row = read.Split('-');
                        T instance = StoredData(row);
                        if (instance != null)
                        {
                            savedList.Add(instance);
                        }
                    }
                }
            }
            return savedList;
        }
        /// <summary>
        /// Converts a string array to an <see cref="IUser"/> object.
        /// </summary>
        /// <param name="row">A string array representing a user row from the file.</param>
        /// <returns>An <see cref="IUser"/> object or null if the row is invalid.</returns>
        public IUser StoredUser(string[] row)
        {
            if (row.Length == 6)
            {
                switch (row[4])
                {
                    case "true":
                        return new Admin
                        {
                            PersonalNum = row[0], // 8802252525
                            Password = row[1],
                            Surname = row[2],
                            Forename = row[3],
                            LoginLock = bool.Parse(row[5])
                        };
                    case "false":
                        return new Customer
                        {
                            PersonalNum = row[0],
                            Password = row[1],
                            Surname = row[2],
                            Forename = row[3],
                            LoginLock = bool.Parse(row[5])
                        };
                    default:
                        return null;
                }
            }
            return null;
        }
        /// <summary>
        /// Converts a string array to an <see cref="IBankAccount"/> object.
        /// </summary>
        /// <param name="row">A string array representing the accounts from each row.</param>
        /// <returns>An object of <see cref="IBankAccount"/> or null if row is invalid.</returns>
        public IBankAccount StoredBankAccount(string[] row)
        {
            if (row.Length == 6)
            {
                switch (row[0])
                {
                    case "0":
                        return new CardAccount
                        {
                            AccountType = Convert.ToInt32(row[0]),
                            UserId = row[1],
                            AccountName = row[2],
                            Balance = Convert.ToDecimal(row[3]),
                            AccountCurrency = row[4],
                            IntrestRate = Convert.ToDecimal(row[5])
                        };
                    case "1":
                        return new SavingsAccount
                        {
                            AccountType = Convert.ToInt32(row[0]),
                            UserId = row[1],
                            AccountName = row[2],
                            Balance = Convert.ToDecimal(row[3]),
                            AccountCurrency = row[4],
                            IntrestRate = Convert.ToDecimal(row[5])
                        };
                    case "2":
                        return new ISK
                        {
                            AccountType = Convert.ToInt32(row[0]),
                            UserId = row[1],
                            AccountName = row[2],
                            Balance = Convert.ToDecimal(row[3]),
                            AccountCurrency = row[4],
                            IntrestRate = Convert.ToDecimal(row[5])
                        };
                    default:
                        return null;
                }
            }
            return null;
        }

        public void WriteUsersAndAccounts(List<IUser> users)
        {
            using (StreamWriter sw = new StreamWriter(_filePathUsers, false))
            {
                foreach (var user in users)
                {
                    sw.WriteLine($"{user.PersonalNum}-{user.Password}-{user.Surname}-{user.Forename}-{(user is Admin).ToString().ToLower()}-{user.LoginLock.ToString().ToLower()}");
                }
            }
            using (StreamWriter sw = new StreamWriter(_filePathAccounts, false, Encoding.UTF8))
            {
                foreach (var user in users.OfType<Customer>())
                {
                    foreach (var account in user.GetListBankAccount())
                    {
                        sw.WriteLine($"{account.AccountType}-{account.UserId}-{account.AccountName}-{account.Balance}-{account.AccountCurrency}-{account.IntrestRate}");
                    }
                }
            }
        }
    }
}
