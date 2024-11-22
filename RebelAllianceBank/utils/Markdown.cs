using RebelAllianceBank.Interfaces;

namespace RebelAllianceBank.utils;

/// <summary>
/// This is a helper static class to generate markdown code.
/// </summary>
public static class Markdown
{

    /// <summary>
    /// This creates a markdown for a table and console out the result
    /// </summary>
    /// <param name="headers"></param>
    /// <param name="body"></param>
    public static void Table(string[] headers, List<string> body)
    {
        int maxHeaderWidth = headers.OrderByDescending(item => item.Length).First().Length;
        int maxBodyWith = body.OrderByDescending(item => item.Length).First().Length;
        int maxWith = maxHeaderWidth < maxBodyWith ? maxBodyWith : maxHeaderWidth;

        // Table header
        for (int i = 0; i < headers.Length; i++)
        {
            var header = headers[i];
            int amountToAddSpace = maxWith - header.Length;

            Console.Write("|");
            Console.Write(header);

            // Add remending space on a table cell
            Spacer(amountToAddSpace);

            // Add the last separator to the end
            if (headers.Length - 1 == i)
            {
                Console.Write("|");
            }
        }

        Console.WriteLine();

        for (int i = 0; i < headers.Length; i++)
        {
            int amountToAddDivider = maxWith < 3 ? 3 : maxWith;

            Console.Write("|");

            // console out the heder separator
            Spacer(amountToAddDivider, "-");

            // Add the last separator to the end
            if (headers.Length - 1 == i)
            {
                Console.Write("|");
            }
        }

        Console.WriteLine();

        // Table body
        for (int i = 0; i < body.Count; i++)
        {
            Console.Write("|");
            var currentBody = body[i];
            int amountToAddSpace = maxWith - currentBody.Length;

            Console.Write(currentBody);

            // Add remending space on a table cell
            Spacer(amountToAddSpace);

            // Add the last separator to the end and add a new row by checking how many item it´s in heder 
            if (((i + 1) % headers.Length) == 0)
            {
                Console.WriteLine("|");
            }
        }
    }
    
    /// <summary>
    /// Add a space withe given amount 
    /// </summary>
    /// <param name="amountToAddSpace"></param>
    /// <param name="spacerValue"></param>
    private static void Spacer(int amountToAddSpace, string spacerValue = " ")
    {
        for (int j = 0; j < amountToAddSpace; j++)
        {
            Console.Write(spacerValue);
        }
    }
}
