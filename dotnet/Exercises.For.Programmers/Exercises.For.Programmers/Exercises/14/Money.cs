using System.Globalization;

namespace Exercises.For.Programmers.Exercises._14
{
    class Money
    {
        private readonly decimal value;

        public Money(decimal value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value.ToString("C", new CultureInfo("en-GB"));
        }
    }
}