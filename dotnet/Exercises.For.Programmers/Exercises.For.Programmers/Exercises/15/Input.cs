using System;
using System.Text;

namespace Exercises.For.Programmers.Exercises._15
{
    static class Input
    {
        public static int Parse(string message)
        {
            while (true)
            {
                Console.Write(message);

                try
                {
                    return Int32.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                }
            }
        }

        public static decimal DecimalParse(string message)
        {
            while (true)
            {
                Console.Write(message);

                try
                {
                    return decimal.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                }
            }
        }

        public static string ParseString(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        public static string ParsePassword(string message)
        {
            Console.Write(message);

            var password = new StringBuilder();

            while (true)
            {
                var consoleKeyInfo = Console.ReadKey(true);

                if (consoleKeyInfo.Key == ConsoleKey.Enter)
                {
                    break;
                }

                password.Append(consoleKeyInfo.KeyChar);
            }

            Console.WriteLine();

            return password.ToString();
        }
    }
}