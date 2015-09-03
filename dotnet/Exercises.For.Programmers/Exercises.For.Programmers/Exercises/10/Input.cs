using System;

namespace Exercises.For.Programmers.Exercises._10
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
    }
}