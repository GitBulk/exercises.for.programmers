using System;

namespace Exercises.For.Programmers.Exercises._11
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
                    return int.Parse(Console.ReadLine());
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
    }
}