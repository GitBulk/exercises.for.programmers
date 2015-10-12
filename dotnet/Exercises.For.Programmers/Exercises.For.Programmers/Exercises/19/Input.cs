using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Exercises.For.Programmers.Exercises._19
{
    static class Input
    {
        public static int ParseInt(string message)
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

        public static int ParseMinimumInt(string message, int minimum, string minimumErrorMessage)
        {
            while (true)
            {
                Console.Write(message);

                try
                {
                    var parsedInt = int.Parse(Console.ReadLine());
                    if (parsedInt >= minimum)
                    {
                        return parsedInt;
                    }

                    Console.WriteLine(minimumErrorMessage);
                }
                catch (FormatException)
                {
                }
            }
        }

        public static decimal ParseDecimal(string message)
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

        public static float ParseFloat(string message)
        {
            while (true)
            {
                Console.Write(message);

                try
                {
                    return float.Parse(Console.ReadLine());
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

        public static string ParseStringMatching(string message, string expression)
        {
            while (true)
            {
                Console.Write(message);

                try
                {
                    var input = Console.ReadLine();
                    if (Regex.IsMatch(input, expression, RegexOptions.IgnoreCase))
                    {
                        return input;
                    }
                }
                catch (FormatException)
                {
                }
            }
        }
    }
}