using RebelAllianceBank.Interfaces;

namespace RebelAllianceBank.Classes
{
    public class FileHandler
    {
        /// <summary>
        /// Reads a list of users from the file.
        /// </summary>
        /// <returns>A list of <see cref="IUser"/> objects read from the file.</returns>
        public List<IUser> ReadUser()
        {
            string filePath = $"..\\..\\..\\users.txt";
            return ReadFromFile(filePath, StoredUser);
        }
        /// <summary>
        /// Reads a list of accounts from the file.
        /// </summary>
        /// <returns>A list of <see cref="IBankAccount"/> objects read from the file.</returns>
        public List<IBankAccount> ReadAccount()
        {
            string filePath = $"..\\..\\..\\accounts.txt";
            return ReadFromFile(filePath, StoredBankAccount);
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
                using (StreamReader sr = new StreamReader(filePath))
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
        /// <summary>
        /// Writes a user to the file and returns the created <see cref="IUser"/> object.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <param name="un">The username of the user.</param>
        /// <param name="pw">The password of the user.</param>
        /// <param name="isAdmin">Specifies whether the user is an admin or not.</param>
        /// <returns>The created <see cref="IUser"/> object or null if writing failed.</returns>
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
        /// <summary>
        /// Converts a string array to an <see cref="IBankAccount"/> object.
        /// </summary>
        /// <param name="row">A string array representing the accounts from each row.</param>
        /// <returns>An object of <see cref="IBankAccount"/> or null if row is invalid.</returns>
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
        /// <summary>
        /// Writes a bank account to the file and returns the created <see cref="IBankAccount"/> object.
        /// </summary>
        /// <param name="id">The ID of the account.</param>
        /// <param name="accType">The type of the account.</param>
        /// <param name="name">The name of the account.</param>
        /// <param name="balance">The balance of the account.</param>
        /// <param name="curr">The currency of the account.</param>
        /// <returns>The created <see cref="IBankAccount"/> object or null if writing fails.</returns>
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
