using System;

namespace Excercises.For.Programmers.Excercise8
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