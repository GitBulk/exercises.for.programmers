using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;

namespace Exercises.For.Programmers.Exercises._17
{
    public class ConsoleBloodAlcoholCalculator
    {
        public void Execute()
        {
            var alcohol = Input.FloatParse(message: "Alcohol (ounces): ");
            var weight = Input.FloatParse(message: "Weight (pounds): ");
            var genderInput = Input.ParseStringMatching(message: "Gender Male/Female: ", expression: "(Male|Female)");
            var hoursSinceDrink = Input.ParseInt(message: "Hours Since Drink: ");

            var calculator = new BloodAlcoholCalculator();

            calculator.Bac(
                Ounces.FromOunces(alcohol),
                Pounds.FromPounds(weight),
                (Gender)Enum.Parse(typeof(Gender), genderInput, ignoreCase: true),
                TimeSpan.FromHours(hoursSinceDrink)
            );
        }
    }

    class BloodAlcoholCalculator
    {
        private const int MinBac = 0;
        private const float MaleFactor = 0.73f;
        private const float FemaleFactor = 0.66f;

        public TextWriter Output = Console.Out;

        public void Bac(Ounces alcohol, Pounds weight, Gender gender, TimeSpan sinceDrink)
        {
            Debug.Assert(alcohol != null && alcohol.Total >= 0f);
            Debug.Assert(weight != null && weight.Total >= 0f);
            Debug.Assert(sinceDrink.Hours >= 0f);

            BacResults(CalculateBac(alcohol.Total, weight.Total, gender, sinceDrink));
        }

        private static float CalculateBac(float alcoholInOunces, float weight, Gender gender, TimeSpan sinceDrink)
        {
            var bac = Math.Max(
                ((alcoholInOunces * 5.14f) / weight * GenderFactor(gender))
                - (0.015f * sinceDrink.Hours), MinBac);

            Debug.Assert(bac >= 0f);
            return bac;
        }

        private static float GenderFactor(Gender gender)
        {
            return gender == Gender.Male ? MaleFactor : FemaleFactor;
        }

        public void BacResults(float bac)
        {
            Output.WriteLine(bac <= 0.08f ? "You are legal to drive." : "It is not legal for you to drive.");
        }
    }

    class Ounces
    {
        private Ounces(float total)
        {
            this.Total = total;
        }

        public float Total { get; private set; }

        public static Ounces FromOunces(float total)
        {
            return new Ounces(total);
        }
    }

    class Pounds
    {
        private Pounds(float total)
        {
            this.Total = total;
        }

        public float Total { get; private set; }

        public static Pounds FromPounds(float pounds)
        {
            return new Pounds(pounds);
        }

        public static Pounds FromKilograms(float kilograms)
        {
            const float poundsInKilogram = 2.205f;
            var total = kilograms * poundsInKilogram;
            return new Pounds((float)Math.Round(value: total, digits: 2));
        }

        public override int GetHashCode()
        {
            return Total.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as Pounds;
            
            if (other == null)
            {
                return false;
            }

            return other.Total.Equals(this.Total);
        }

        public override string ToString()
        {
            return this.Total.ToString();
        }
    }

    enum Gender
    {
        Male,
        Female
    }

    [TestFixture]
    class Tests
    {
        private class MaleTests
        {
            [Test]
            public void Legal_To_Drive()
            {
                var subject = new BloodAlcoholCalculator { Output = new StringWriter() };

                subject.Bac(
                    alcohol: Ounces.FromOunces(1.4f),
                    weight: Pounds.FromPounds(180.0f),
                    gender: Gender.Male,
                    sinceDrink: TimeSpan.FromHours(3));

                Assert.That(subject.Output.ToString(), Is.StringContaining("You are legal to drive."));
            }

            [Test]
            public void Illegal_To_Drive()
            {
                var subject = new BloodAlcoholCalculator { Output = new StringWriter() };

                subject.Bac(
                    alcohol: Ounces.FromOunces(25.0f),
                    weight: Pounds.FromPounds(100.0f),
                    gender: Gender.Male,
                    sinceDrink: TimeSpan.FromHours(1));

                Assert.That(subject.Output.ToString(),
                    Is.StringContaining("It is not legal for you to drive."));
            }
        }

        class FemaleTests
        {
            [Test]
            public void Legal_To_Drive()
            {
                var subject = new BloodAlcoholCalculator { Output = new StringWriter() };

                subject.Bac(
                    alcohol: Ounces.FromOunces(4.4f),
                    weight: Pounds.FromPounds(180.0f),
                    gender: Gender.Female,
                    sinceDrink: TimeSpan.FromHours(3));

                Assert.That(subject.Output.ToString(), Is.StringContaining("You are legal to drive."));
            }

            [Test]
            public void Illegal_To_Drive()
            {
                var subject = new BloodAlcoholCalculator { Output = new StringWriter() };

                subject.Bac(
                    alcohol: Ounces.FromOunces(25f),
                    weight: Pounds.FromPounds(150.0f),
                    gender: Gender.Female,
                    sinceDrink: TimeSpan.FromHours(1));

                Assert.That(subject.Output.ToString(),
                    Is.StringContaining("It is not legal for you to drive."));
            }
        }

        class BacResultTests
        {
            [Test]
            public void Invalid_Bac()
            {
                var subject = new BloodAlcoholCalculator { Output = new StringWriter() };

                subject.BacResults(0.09f);

                Assert.That(subject.Output.ToString(),
                    Is.StringContaining("It is not legal for you to drive."));
            }

            [Test]
            [TestCase(0.08f)]
            [TestCase(0.07f)]
            public void Valid_Bac(float validBac)
            {
                var subject = new BloodAlcoholCalculator { Output = new StringWriter() };

                subject.BacResults(validBac);

                Assert.That(subject.Output.ToString(),
                    Is.StringContaining("You are legal to drive."));
            }
        }

        [TestFixture]
        class WeightTests
        {
            [Test]
            public void Ounce_To_Ounce()
            {
                Assert.That(Pounds.FromPounds(1), Is.EqualTo(Pounds.FromPounds(1)));
            }

            [Test]
            [TestCase(1f, 2.2f)]
            [TestCase(12f, 26.46f)]
            [TestCase(12.5f, 27.56f)]
            public void Kilograms_To_Pounds(float kilograms, float pounds)
            {
                Assert.That(
                    Pounds.FromKilograms(kilograms),
                    Is.EqualTo(Pounds.FromPounds(pounds)));
            }
        }
    }
}