using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;

namespace Exercises.For.Programmers.Exercises._16
{
    public class ConsoleDrivingAge
    {
        public void Execute()
        {
            var age = Input.ParseMinimumInt(
                message: "What is your age? ",
                minimum: 0,
                minimumErrorMessage: "Enter a valid age.");

            DrivingAge.LegalToDrive(age);
        }
    }

    static class DrivingAge
    {
        public static TextWriter Output = Console.Out;

        public static void LegalToDrive(int age)
        {
            Debug.Assert(age >= 0);
            
            const int legalAgeToDrive = 16;

            Output.WriteLine(age >= legalAgeToDrive
                ? "You are old enough to legally drive."
                : "You are not old enough to legally drive.");
        }
    }

    [TestFixture]
    class Tests
    {
        [Test]
        [TestCase(16)]
        [TestCase(17)]
        [TestCase(35)]
        public void Legal_To_Drive(int legalAge)
        {
            DrivingAge.Output = new StringWriter();

            DrivingAge.LegalToDrive(legalAge);

            Assert.That(DrivingAge.Output.ToString(), Is.StringContaining("You are old enough to legally drive."));
        }

        [Test]
        [TestCase(15)]
        public void Illegal_To_Drive(int illegalAge)
        {
            DrivingAge.Output = new StringWriter();
            
            DrivingAge.LegalToDrive(illegalAge);

            Assert.That(DrivingAge.Output.ToString(), Is.StringContaining("You are not old enough to legally drive."));
        }
    }
}