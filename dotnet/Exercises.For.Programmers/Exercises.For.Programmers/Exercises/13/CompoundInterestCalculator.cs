using System;
using System.Diagnostics;
using System.Globalization;
using NUnit.Framework;

namespace Exercises.For.Programmers.Exercises._13
{
    public class ConsoleCompoundInterest
    {
        public void Execute()
        {
            var principalAmount = Input.DecimalParse(message: "What is the principal amount? ");
            var rate = Input.DecimalParse(message: "What is the rate: ");
            var numberOfYears = Input.Parse(message: "What is the number of years: ");
            var compoundYears = Input.Parse(message: "What is the number of times the interest is compounded per year: ");

            var amount = CompoundInterest.Calculate(principalAmount, (double)rate, compoundYears, numberOfYears);

            Console.WriteLine("{0} invested at {1}% for {2} years compounded {3} times per year is {4}",
                FormatCurrency(principalAmount),
                rate,
                numberOfYears,
                compoundYears,
                FormatCurrency(amount));
        }

        private static string FormatCurrency(decimal amount)
        {
            return amount.ToString("C", new CultureInfo("en-GB"));
        }
    }

    class CompoundInterest
    {
        public static decimal Calculate(decimal principalAmount, double rateOfInterest, int timesCompoundedPerYear, int yearsOfInvestment)
        {
            Debug.Assert(principalAmount > 0m);
            Debug.Assert(rateOfInterest > -1);
            Debug.Assert(timesCompoundedPerYear > -1);
            Debug.Assert(yearsOfInvestment > -1);

            var rateOverCompoundedYears = (rateOfInterest / 100) / timesCompoundedPerYear;
            return principalAmount * (decimal)Math.Pow(1 + rateOverCompoundedYears, timesCompoundedPerYear*yearsOfInvestment);
        }
    }

    [TestFixture]
    class Tests
    {
        [Test]
        public void Compounded_Interest_Calculation()
        {
            var amount = CompoundInterest.Calculate(
                principalAmount: 1500,
                rateOfInterest: 4.3,
                timesCompoundedPerYear: 4,
                yearsOfInvestment: 6);

            Assert.That(amount, Is.EqualTo(1938.84).Within(0.01m));
        }
    }
}