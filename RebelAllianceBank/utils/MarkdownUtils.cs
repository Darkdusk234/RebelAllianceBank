using RebelAllianceBank.Interfaces;
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
        /// <param name="listData">List of data items of the <typeparamref name="T"/>.</param>
        /// <param name="inData">Function that checks values for each item type <typeparamref name="T"/>.
        /// Then saves it to an array.</param>
        /// <returns>Index of the selected item if Enter is pressed.
        /// Returns -1 if ESC is pressed making <paramref name="cancel"/> true.
        /// </returns>
        public static int HighLightChoiceWithMarkdown<T>(bool cancel, string[] columnHeaders, List<T> listData, Func<T, string[]> inData)
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
                List<string> rows = new List<string>();
                for (int i = 0; i < listData.Count; i++)
                {
                    string[] rowValues = inData(listData[i]);
                    if (i == userSelect)
                    {
                        rowValues[0] = $"> {rowValues[0]}";
                    }
                    rows.AddRange(rowValues);
                }
                Markdown.Table(columnHeaders, rows);
                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    userSelect = (userSelect == 0) ? listData.Count - 1 : userSelect - 1;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    userSelect = (userSelect == listData.Count - 1) ? 0 : userSelect + 1;
                }
            }
            while (key != ConsoleKey.Enter && key != ConsoleKey.Escape);

            return key == ConsoleKey.Enter ? userSelect : -1;
        }
        public void DictionaryForHighLight()
        {

        }
    }
}
