using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Exercises.For.Programmers.Exercises._19
{
    public class ConsoleBmiCalculator
    {
        public void Execute()
        {
            float height = Input.ParseFloat(message: "Height (inches): ");
            float weight = Input.ParseFloat(message: "Weight (pounds): ");

            var person = new Person(Height.Inches(height), Weight.Pounds(weight));
            var bmi = person.Bmi();
            
            Console.WriteLine(bmi);
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Bmi_For_Normal_Weight_Range()
        {
            var subject = new Person(Height.Inches(75), Weight.Pounds(170));

            var bmi = subject.Bmi();

            Assert.That(bmi, Is.EqualTo("You are in the correct weight range."));
        }

        [Test]
        public void Bmi_For_Under_Weight_Range()
        {
            var person = new Person(Height.Inches(75), Weight.Pounds(100));

            var bmi = person.Bmi();

            Assert.That(bmi, Is.EqualTo("You are under weight. You should see your doctor."));
        }

        [Test]
        public void Bmi_For_Over_Weight_Range()
        {
            var person = new Person(Height.Inches(75), Weight.Pounds(250));

            var bmi = person.Bmi();

            Assert.That(bmi, Is.EqualTo("You are over weight. You should see your doctor."));
        }
    }

    public class Weight
    {
        public float Value { get; private set; }

        private Weight(float pounds)
        {
            Debug.Assert(pounds > 0f);

            this.Value = pounds;
        }

        public static Weight Pounds(float pounds)
        {
            return new Weight(pounds);
        }

        public static float operator / (Weight weight, Height height)
        {
            return weight.Value / height.Value;
        }
    }

    public class Height
    {
        public float Value { get; private set; }

        private Height(float inches)
        {
            Debug.Assert(inches > 0f);

            this.Value = inches;
        }

        public static Height Inches(float inches)
        {
            return new Height(inches);
        }

        public Height Doubled()
        {
            return new Height(this.Value * this.Value);
        }
    }

    public class Person
    {
        private readonly Height height;
        private readonly Weight weight;

        public Person(Height height, Weight weight)
        {
            if (height == null) { throw new ArgumentNullException("height"); }
            if (weight == null) { throw new ArgumentNullException("weight"); }

            this.height = height;
            this.weight = weight;
        }

        public string Bmi()
        {
            var bmi = new BodyMassIndex(this.weight, this.height);
            
            if (bmi.UnderWeight())
            {
                return "You are under weight. You should see your doctor.";
            }
            else if (bmi.OverWeight())
            {
                return "You are over weight. You should see your doctor.";
            }

            return "You are in the correct weight range.";
        }

        private class BodyMassIndex
        {
            private readonly float bmi;

            public BodyMassIndex(Weight weight, Height height)
            {
                Debug.Assert(weight != null);
                Debug.Assert(height != null);

                this.bmi = weight / (height.Doubled()) * 703;
            }

            public bool UnderWeight()
            {
                return this.bmi < 18.5;
            }

            public bool OverWeight()
            {
                return this.bmi > 25;
            }
        }
    }
}