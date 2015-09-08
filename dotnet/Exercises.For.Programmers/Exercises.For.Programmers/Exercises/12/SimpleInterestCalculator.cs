using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using NUnit.Framework;

namespace Exercises.For.Programmers.Exercises._12
{
    public class ConsoleSimpleInterest
    {
        public void Execute()
        {
            decimal principal = Input.DecimalParse(message: "Enter the principal: ");
            decimal rateOfInterest = Input.DecimalParse(message: "Enter the rate of interest: ");
            int numberOfYears = Input.Parse(message: "Enter the number of years: ");

            var amount = SimpleInterest.Calculate(principal, numberOfYears, rateOfInterest);

            Console.WriteLine("After {0} years at {1}%, the investment will be worth {2}.",
                numberOfYears,
                rateOfInterest,
                amount.ToString("C", new CultureInfo("en-GB")));
        }
    }

    public class SimpleInterest
    {
        public static decimal Calculate(decimal principalAmount, int year, decimal interest)
        {
            Debug.Assert(year > -1);
            Debug.Assert(interest > -1);
            Debug.Assert(principalAmount > -1);

            var interestAsPercentage = (interest / 100);
            return principalAmount * (1 + (interestAsPercentage * year));
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Zero_Interest_Over_Zero_Years()
        {
            decimal amount = SimpleInterest.Calculate(0m, 0, 0m);
            Assert.That(amount, Is.EqualTo(0m));
        }

        [Test]
        public void Interest_Over_One_Year()
        {
            decimal amount = SimpleInterest.Calculate(principalAmount: 10m, year: 1, interest: 50m);
            Assert.That(amount, Is.EqualTo(15m));
        }

        [Test]
        public void Interest_Over_Multiple_Years()
        {
            decimal amount = SimpleInterest.Calculate(principalAmount: 1500m, year: 4, interest: 4.3m);
            Assert.That(amount, Is.EqualTo(1758m));
        }
    }
}