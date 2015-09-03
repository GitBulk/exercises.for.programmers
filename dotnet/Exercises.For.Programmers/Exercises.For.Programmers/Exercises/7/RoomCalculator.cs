using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Exercises.For.Programmers.Exercises._7
{
    public class ConsoleRoomCalculator
    {
        public void Execute()
        {
            Console.Write("What is the length of the room in feet? ");
            var length = Console.ReadLine();

            Console.Write("What is the width of the room in feet? ");
            var width = Console.ReadLine();

            Console.WriteLine("You entered dimensions of {0} feet by {1} feet.", length, width);

            Console.WriteLine("The area is");

            var roomCalculator = new RoomCalculator();
            Area area = roomCalculator.Area(float.Parse(length), float.Parse(width));

            Console.WriteLine("{0} square feet.", area.Feet);
            Console.WriteLine("{0} square meters.", area.Meters);
        }
    }

    public class RoomCalculator
    {
        public Area Area(float length, float width)
        {
            Debug.Assert(length > 0);
            Debug.Assert(width > 0);

            var feet = Feet(length, width);

            return new Area(feet, Meters(feet));
        }

        private static float Meters(float feet)
        {
            return feet * ConversionFactorFromFeet();
        }

        private static float Feet(float length, float width)
        {
            return length * width;
        }

        private static float ConversionFactorFromFeet()
        {
            const float conversionFactorFromFeet = 0.09290304f;
            return conversionFactorFromFeet;
        }
    }

    public class Area
    {
        public Area(float feet, float meters)
        {
            Feet = feet;
            Meters = meters;
        }

        public float Feet { get; private set; }

        public float Meters { get; private set; }
    }

    [TestFixture]
    public class RoomCalculatorTests
    {
        [Test]
        public void Calculating_Feet()
        {
            var subject = new RoomCalculator();
            var area = subject.Area(length: 15.0f, width: 20.0f);
            Assert.That(area.Feet, Is.EqualTo(300.0f));
        }

        [Test]
        public void Calculating_Meters()
        {
            var subject = new RoomCalculator();
            var area = subject.Area(length: 15.0f, width: 20.0f);
            Assert.That(area.Meters, Is.EqualTo(27.871f).Within(0.01));
        }
    }
}