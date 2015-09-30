using System;
using NUnit.Framework;

namespace Exercises.For.Programmers.Exercises._18
{
    public class ConsoleTemperatureConverter
    {
        public void Execute()
        {
            Console.WriteLine("Press C to convert from Fahrenheit to Celsius.");
            Console.WriteLine("Press F to convert from Celsius to Fahrenheit.");

            var choice = Input.ParseStringMatching("Your choice: ", ("C|F"));

            if (choice.Equals("C", StringComparison.OrdinalIgnoreCase))
            {
                var fahrenheit = Input.FloatParse("Please enter the temperature in Fahrenheit: ");
                Console.WriteLine("The temperature in Celsius is " + new Fahrenheit(fahrenheit).ToCelsius());
            }
            else
            {
                var celsius = Input.FloatParse("Please enter the temperature in Celsius: ");
                Console.WriteLine("The temperature in Fahrenheit is " + new Celsius(celsius).ToFahrenheit());
            }
        }
    }

    class Fahrenheit
    {
        private readonly float fahrenheit;

        public Fahrenheit(float fahrenheit)
        {
            this.fahrenheit = fahrenheit;
        }

        public Celsius ToCelsius()
        {
            var c = (this.fahrenheit - 32f)* (5f / 9f);
            return new Celsius(c);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Fahrenheit;
            return other != null && this.fahrenheit.Equals(other.fahrenheit);
        }

        public override int GetHashCode()
        {
            return this.fahrenheit.GetHashCode();
        }

        public override string ToString()
        {
            return this.fahrenheit.ToString();
        }
    }

    class Celsius
    {
        private readonly float celsius;

        public Celsius(float celsius)
        {
            this.celsius = celsius;
        }

        public Fahrenheit ToFahrenheit()
        {
            return new Fahrenheit((this.celsius * 9f / 5f) + 32f);
        }

        public override bool Equals(object obj)
        {
            var other = obj as Celsius;
            return other != null && celsius.Equals(other.celsius);
        }

        public override int GetHashCode()
        {
            return this.celsius.GetHashCode();
        }

        public override string ToString()
        {
            return this.celsius.ToString();
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Fahrenheit_To_Celsius()
        {
            Assert.That(new Fahrenheit(32).ToCelsius(), Is.EqualTo(new Celsius(0f)));
        }

        [Test]
        public void Celsius_To_Fahrenheit()
        {
            Assert.That(new Celsius(0).ToFahrenheit(), Is.EqualTo(new Fahrenheit(32f)));
        }
    }
}