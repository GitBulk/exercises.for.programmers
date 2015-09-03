using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Exercises.For.Programmers.Exercises._9
{
    public class ConsolePaintCalculator
    {
        public void Execute()
        {
            var width = Input.Parse(message: "Width of ceiling: ");
            var length = Input.Parse(message: "Length of celing: ");

            var feet = width * length;

            var paintCalculator = new PaintCalculator();
            var gallonsOfPaint = paintCalculator.Calculate(feet);
            
            Console.WriteLine("You will need to purchase {0} gallon(s) of paint to cover {1} square feet.",
                gallonsOfPaint,
                feet);
        }
    }

    public class PaintCalculator
    {
        public int Calculate(float feet)
        {
            Debug.Assert(feet > 0);

            const float gallonPerCanOfPaint = 350.0f;
            return (int)Math.Ceiling(feet / gallonPerCanOfPaint);
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Exactly_A_Single_Gallon_Of_Paint()
        {
            var subject = new PaintCalculator();
            int gallonsOfPaint = subject.Calculate(feet: 350);
            Assert.That(gallonsOfPaint, Is.EqualTo(1));
        }

        [Test]
        public void Less_Than_A_Single_Gallon_Of_Paint()
        {
            var subject = new PaintCalculator();
            int gallonsOfPaint = subject.Calculate(feet: 349);
            Assert.That(gallonsOfPaint, Is.EqualTo(1));
        }

        [Test]
        public void Over_One_Gallon_Of_Paint_Requires_Two_Gallons_Of_Paint()
        {
            var subject = new PaintCalculator();
            int gallonsOfPaint = subject.Calculate(feet: 351);
            Assert.That(gallonsOfPaint, Is.EqualTo(2));
        }

        [Test]
        public void Less_Than_Two_Gallons_Of_Paint_Requires_Two_Gallons()
        {
            var subject = new PaintCalculator();
            int gallonsOfPaint = subject.Calculate(feet: 699);
            Assert.That(gallonsOfPaint, Is.EqualTo(2));
        }
    }
}