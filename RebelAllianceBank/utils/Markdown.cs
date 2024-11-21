using System.Linq;
using RebelAllianceBank.Interfaces;

namespace RebelAllianceBank.utils;

public static class Markdown
{

    public static void Table(string[] headers, List<string> body)
    {
        int maxHeaderWidth = headers.OrderByDescending(header => header.Length).First().Length;
        int maxBodyWith = body.OrderByDescending(item => item.Length).First().Length;
        int maxWith = maxHeaderWidth < maxBodyWith ? maxBodyWith : maxHeaderWidth;

        // Hedder
        for (int i = 0; i < headers.Length; i++)
        {
            var header = headers[i];
            int amauntToAddSpce = maxWith - header.Length;

            Console.Write("|");
            Console.Write(header);

            for (int j = 0; j < amauntToAddSpce; j++)
            {
                Console.Write(" ");
            }

            if (headers.Length - 1 == i)
            {
                Console.Write("|");
            }
        }

        Console.WriteLine();

        for (int i = 0; i < headers.Length; i++)
        {
            int amauntToAddDevider = maxHeaderWidth < 3 ? 3 : maxHeaderWidth;

            Console.Write("|");

            for (int j = 0; j < maxWith; j++)
            {
                Console.Write("-");
            }

            if (headers.Length - 1 == i)
            {
                Console.Write("|");
            }
        }

        Console.WriteLine();

        for (int i = 0; i < body.Count; i++)
        {
            Console.Write("|");
            var currentBody = body[i];
            int count = currentBody.ToString().Length;
            int amauntToAddSpce = maxWith - count;

            Console.Write(currentBody);

            for (int j = 0; j < amauntToAddSpce; j++)
            {
                Console.Write(" ");
            }

            if (((i + 1) % headers.Length) == 0)
            {
                Console.WriteLine("|");
            }
        }
    }
}
