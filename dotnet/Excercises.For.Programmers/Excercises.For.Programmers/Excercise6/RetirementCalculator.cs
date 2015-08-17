using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Excercises.For.Programmers.Excercise6
{
    public class ConsoleRetirementCalculator
    {
        public void Execute()
        {
            IRetirementCalculator calculator = new RetirementCalculator();

            Console.Write("What is your current age? ");
            var currentAge = Console.ReadLine();

            Console.Write("At what age would you like to retire? ");
            var retirementAge = Console.ReadLine();

            var results = calculator.RetirementYear(int.Parse(currentAge), int.Parse(retirementAge));

            if (results.YearsToRetirement == 0)
            {
                Console.WriteLine("You can retire already.");
            }
            else
            {
                Console.WriteLine("You have {0} years left until you can retire.", results.YearsToRetirement);
                Console.WriteLine("It's {0}, so you can retire in {1}.", DateTime.Now.Year, results.RetirementYear);
            }
        }
    }

    public interface IRetirementCalculator
    {
        RetirementResults RetirementYear(int currentAge, int ageToRetire);
    }

    public class RetirementCalculator : IRetirementCalculator
    {
        public RetirementResults RetirementYear(int currentAge, int ageToRetire)
        {
            return RetirementYear(DateTime.Now, currentAge, ageToRetire);
        }

        public RetirementResults RetirementYear(DateTime currentDateTime, int currentAge, int ageToRetire)
        {
            var yearsToRetirement = ageToRetire - currentAge;
            
            if (yearsToRetirement < 0)
            {
                yearsToRetirement = 0;
            }

            var retirementYear = currentDateTime.AddYears(yearsToRetirement).Year;
            return new RetirementResults(yearsToRetirement, retirementYear);
        }
    }

    public class RetirementResults
    {
        public RetirementResults(int yearsToRetirement, int retirementYear)
        {
            YearsToRetirement = yearsToRetirement;
            RetirementYear = retirementYear;
        }

        public int YearsToRetirement { get; private set; }

        public int RetirementYear { get; private set; }
    }

    [TestFixture]
    public class RetirementCalculatorTests
    {
        private RetirementCalculator subject;

        [SetUp]
        public void SetUp()
        {
            subject = new RetirementCalculator();
        }

        [Test]
        public void Calculate_Years_To_Retirement()
        {
            var retirementResults = subject.RetirementYear(new DateTime(2015, 01, 01), 25, 65);
            Assert.That(retirementResults.YearsToRetirement, Is.EqualTo(40));
        }

        [Test]
        public void Calculate_Retirement_Year()
        {
            var retirementResults = subject.RetirementYear(new DateTime(2015, 01, 01), 25, 65);
            Assert.That(retirementResults.RetirementYear, Is.EqualTo(2055));
        }

        [Test]
        [TestCase(65)]
        [TestCase(66)]
        public void Calculate_Years_To_Retirement_For_User_Who_Can_Retire_Already(int currentAge)
        {
            var retirementResults = subject.RetirementYear(DateTime.Now, ageToRetire: 65, currentAge: currentAge);
            Assert.That(retirementResults.YearsToRetirement, Is.EqualTo(0));
        }
    }
}