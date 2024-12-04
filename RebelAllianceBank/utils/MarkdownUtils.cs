using RebelAllianceBank.Interfaces;
using RebelAllianceBank.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RebelAllianceBank.utils
{
    public class MarkdownUtils
    {
        /// <summary>
        /// A dynamic generic method to navigate through rows using arrow keys.
        /// </summary>
        /// <typeparam name="T">Object type of the data list.</typeparam>
        /// <param name="cancel">By pressing ESC (true) will allow the user to cancel the selection.</param>
        /// <param name="columnHeaders">Array that represents the headers.</param>
        /// <param name="filterData">Filtered list of data items of the <typeparamref name="T"/>.</param>
        /// <param name="inData">Function that checks values for each item type <typeparamref name="T"/>.
        /// Then saves it to an array.</param>
        /// <returns>Index of the selected item if Enter is pressed.
        /// Returns -1 if ESC is pressed making <paramref name="cancel"/> true.
        /// </returns>
        public static int HighLightChoiceWithMarkdown<T>(bool cancel, string[] columnHeaders, List<T> filterData, Func<T, string[]> inData)
        {
            int userSelect = 0;
            ConsoleKey key;
            do
            {
                Console.Clear();
                Console.CursorVisible = false;
                Console.WriteLine("Använd piltangenter upp/ner. Enter för att välja. ESC för att avsluta.");
                if (typeof(T) == typeof(IUser))
                {
                    Console.WriteLine("Välj en användare.");
                }
                else if (typeof(T) == typeof(IBankAccount))
                {
                    Console.WriteLine("Välj ett konto.");
                }
                else
                {
                    Console.WriteLine("Välj ett alternativ.");
                }
                List<string> rows = new List<string>();
                for (int i = 0; i < filterData.Count; i++)
                {
                    string[] rowValues = inData(filterData[i]);
                    if (i == userSelect)
                    {
                        rowValues[0] = $"{TextColor.Yellow}> {rowValues[0]}{TextColor.NORMAL}";
                    }
                    rows.AddRange(rowValues);
                }
                Markdown.Table(columnHeaders, rows);
                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    userSelect = (userSelect == 0) ? filterData.Count - 1 : userSelect - 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    userSelect = (userSelect == filterData.Count - 1) ? 0 : userSelect + 1;
                }
            }
            while (key != ConsoleKey.Enter && key != ConsoleKey.Escape);

            return key == ConsoleKey.Enter ? userSelect : -1;
        }
        /// <summary>
        /// A dynamic generic method for the <see cref="HighLightChoiceWithMarkdown"/> method to filter out current user and to return only users from <see cref="Customer"/>.
        /// </summary>
        /// <typeparam name="T">Object type of the data list</typeparam>
        /// <param name="users"></param>
        /// <param name="currentUser">The user who is logged in</param>
        /// <returns></returns>
        public static List<T> FilterCustomerAndCurrentUser<T>(List<T> users, IUser currentUser)
        {
            return users
                .OfType<Customer>() // Behåll endast Customer
                .Where(user => user.PersonalNum != currentUser.PersonalNum) // Exkludera currentUser
                .Cast<T>() // Kasta tillbaka till IUser
                .ToList();
        }
        public void DictionaryForHighLight()
        {

        }
    }
}
