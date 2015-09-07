using System;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;

namespace Exercises.For.Programmers.Exercises._11
{
    public class ConsoleCurrencyConversion
    {
        public void Execute()
        {
            decimal euros = Input.DecimalParse(message: "How many Euros are you exchanging? ");

            var currencyConverter = new ConsoleCurrencyConverter();
            var dollars = currencyConverter.Convert(euros);

            Console.WriteLine("{0} Euros at an exchange rate of {1} is {2} dollars.",
                euros,
                Rates.RateFor("EUR"),
                dollars);
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Exchanging_Into_Difference_Currency_Yields_The_Converted_Amount()
        {
            var currencyConverter = new CurrencyConverter();

            decimal convertedAmount = currencyConverter.Convert(fromAmount: 81m, currencyFrom: "EUR");

            Assert.That(convertedAmount, Is.EqualTo(90.44m).Within(0.01m));
        }
    }

    public class ConsoleCurrencyConverter
    {
        public string Convert(decimal fromAmount)
        {
            var currencyConverter = new CurrencyConverter();
            var dollars = currencyConverter.Convert(fromAmount, "EUR");
            return dollars.ToString("##.##");
        }
    }

    public class CurrencyConverter
    {
        public decimal Convert(decimal fromAmount, string currencyFrom)
        {
            Debug.Assert(fromAmount > 0m);
            Debug.Assert(!string.IsNullOrWhiteSpace(currencyFrom));

            return (fromAmount * Rates.RateFrom(currencyFrom)) / 1m;
        }
    }

    static class Rates
    {
        private static readonly Dictionary<string, decimal> CurrencyRates;

        static Rates()
        {
             CurrencyRates = new Dictionary<string, decimal>() { { "EUR", 1.11665m } };
        }

        public static decimal RateFrom(string currencyFrom)
        {
            return CurrencyRates[currencyFrom];
        }

        public static decimal RateFor(string currency)
        {
            return CurrencyRates[currency];
        }
    }
}