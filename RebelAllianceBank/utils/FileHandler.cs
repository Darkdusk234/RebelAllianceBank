using RebelAllianceBank.Accounts;
using RebelAllianceBank.Interfaces;
using RebelAllianceBank.Users;
using System.Text;
using RebelAllianceBank.Other;

namespace RebelAllianceBank.utils
{
    public class FileHandler
    {
        // Stores the filepath used for users, accounts and loans.
        private readonly string _usersFilePath = "users.txt";
        private readonly string _accountsFilePath = "accounts.txt";
        private readonly string _loansFilePath = "loans.txt";
        /// <summary>
        /// Reads a list of users from the file. And store accounts and loans related to an user
        /// </summary>
        /// <returns>A list of <see cref="IUser"/> objects read from the file.</returns>
        public List<IUser> LoadUsersWithAccountAndLoans()
        {
            // Call method ReadFromFile and stores in correct variable type
            var userlist = ReadFromFile(_usersFilePath, ParseUserFromData);
            var accountlist = ReadFromFile(_accountsFilePath, ParseAccountFromData);
            var loanlist = ReadFromFile(_loansFilePath, ParseLoanFromData);
            // go through every Customer to find associated bank accounts & loan
            // where PersonalNum matches the UserId add them to current user
            foreach (var user in userlist.OfType<Customer>())
            {   
                user.GetListBankAccount().AddRange(accountlist.Where(account => account.UserId == user.PersonalNum));
                user.GetListLoan().AddRange(loanlist.Where(loan => loan.UserId == user.PersonalNum));
            }
            return userlist;
        }
        /// <summary>
        /// Reads data from a specified file and converts each line into an object of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of objects to be created from the file data.</typeparam>
        /// <param name="filePath">The path to the file that contains the data to be read.</param>
        /// <param name="aMethod">A delegate method that takes a string array (representing a line split into parts)
        /// and returns an object of type <typeparamref name="T"/>.</param>
        /// <returns>A list of objects of type <typeparamref name="T"/> created from the file data.</returns>
        public static List<T> ReadFromFile<T>(string filePath, Func<string[], T> aMethod)
        {
            // creates empty list to store objects created from the stored data.
            List<T> savedList = new List<T>();
            if (File.Exists(filePath))
            {
                // use StreamReader and open file and read with UTF8 encoding
                using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
                {
                    // empty string to store the current line
                    string currentLine;
                    // while loop to go through one line at a time
                    while ((currentLine = sr.ReadLine()) != null)
                    {
                        // the stored data in .txt is separated with an "-"
                        // for example; Gustav-Svensson. So split the strings where "-" appears
                        // and store it to an string[].
                        string[] dataParts = currentLine.Split('-');
                        // Use the provided method function to convert the parts into an object of type T (IUsers, IBankAccount or Loan).
                        // "aMethod" is a delegate, meaning a method passed as an argument.
                        // It takes a "string[]" as input (using the one stored from above).
                        T parsedObjects = aMethod(dataParts);
                        // If the parsed objects is not null
                        if (parsedObjects != null)
                        {
                            // add objects to list
                            savedList.Add(parsedObjects);
                        }
                    }
                }
            }
            return savedList;
        }
        /// <summary>
        /// Converts a string array to an <see cref="IUser"/> object.
        /// </summary>
        /// <param name="dataParts">A string array representing a user row from the file.</param>
        /// <returns>An <see cref="IUser"/> object or null if the row is invalid.</returns>
        public IUser ParseUserFromData(string[] dataParts)
        {
            // ensure that the length is equal 6
            if (dataParts.Length == 6)
            {
                // depending on if the user is admin or not
                switch (dataParts[4])
                {
                    case "true":
                        return new Admin
                        {
                            PersonalNum = dataParts[0], // 8802252525
                            Password = dataParts[1],
                            Surname = dataParts[2],
                            Forename = dataParts[3],
                            LoginLock = bool.Parse(dataParts[5])
                        };
                    case "false":
                        return new Customer
                        {
                            PersonalNum = dataParts[0],
                            Password = dataParts[1],
                            Surname = dataParts[2],
                            Forename = dataParts[3],
                            LoginLock = bool.Parse(dataParts[5])
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
        /// <param name="dataParts">A string array representing the accounts from each row.</param>
        /// <returns>An object of <see cref="IBankAccount"/> or null if row is invalid.</returns>
        public IBankAccount ParseAccountFromData(string[] dataParts)
        {
            // ensure that input data contains 6 parts
            if (dataParts.Length == 6)
            {
                // Check accountType value and create an object of it
                switch (dataParts[0])
                {
                    case "0":
                        return new CardAccount
                        {
                            AccountType = Convert.ToInt32(dataParts[0]),
                            UserId = dataParts[1],
                            AccountName = dataParts[2],
                            Balance = Convert.ToDecimal(dataParts[3]),
                            AccountCurrency = dataParts[4],
                            IntrestRate = Convert.ToDecimal(dataParts[5])
                        };
                    case "1":
                        return new SavingsAccount
                        {
                            AccountType = Convert.ToInt32(dataParts[0]),
                            UserId = dataParts[1],
                            AccountName = dataParts[2],
                            Balance = Convert.ToDecimal(dataParts[3]),
                            AccountCurrency = dataParts[4],
                            IntrestRate = Convert.ToDecimal(dataParts[5])
                        };
                    case "2":
                        return new ISK
                        {
                            AccountType = Convert.ToInt32(dataParts[0]),
                            UserId = dataParts[1],
                            AccountName = dataParts[2],
                            Balance = Convert.ToDecimal(dataParts[3]),
                            AccountCurrency = dataParts[4],
                            IntrestRate = Convert.ToDecimal(dataParts[5])
                        };
                    default:
                        return null;
                }
            }
            return null;
        }
        public Loan ParseLoanFromData(string[] dataParts)
        {
            if (dataParts.Length == 3)
            {
                return new Loan
                {
                    UserId = dataParts[0],
                    LoanedAmount = Convert.ToDecimal(dataParts[1]),
                    LoanRent = Convert.ToDecimal(dataParts[2])
                };
            }
            return null;
        }
        /// <summary>
        /// Saves user data, including accounts and loan to respective files.
        /// </summary>
        /// <param name="userlist"></param>
        public void WriteUsersAndAssociatedData(List<IUser> userlist)
        {
            // Writes user to file
            using (StreamWriter sw = new StreamWriter(_usersFilePath, false, Encoding.UTF8))
            {
                // Go through each user in userlist
                foreach (var user in userlist)
                {
                    // writes user information to one line
                    sw.WriteLine($"{user.PersonalNum}-{user.Password}-{user.Surname}-{user.Forename}-{(user is Admin).ToString().ToLower()}-{user.LoginLock.ToString().ToLower()}");
                }
            }
            // wrties account to file
            using (StreamWriter sw = new StreamWriter(_accountsFilePath, false, Encoding.UTF8))
            {
                // go through each user in userlist of the type customer
                foreach (var customer in userlist.OfType<Customer>())
                {
                    foreach (var account in customer.GetListBankAccount())
                    {
                        sw.WriteLine($"{account.AccountType}-{account.UserId}-{account.AccountName}-{account.Balance}-{account.AccountCurrency}-{account.IntrestRate}");
                    }
                }
            }
            // writes loan to file
            using (StreamWriter sw = new StreamWriter(_loansFilePath, false, Encoding.UTF8))
            {
                foreach (var customer in userlist.OfType<Customer>())
                {
                    foreach (var loan in customer.GetListLoan())
                    {
                        sw.WriteLine($"{loan.UserId}-{loan.LoanedAmount}-{loan.LoanRent}");
                    }
                }
            }
        }
    }
}
