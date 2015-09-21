using System;
using System.IO;
using NUnit.Framework;

namespace Exercises.For.Programmers.Exercises._16
{
    public class ConsoleDrivingAge
    {
        public void Execute()
        {
            var age = Input.ParseInt(message: "What is your age? ");
            DrivingAge.LegalToDrive(age, Console.Out);
        }
    }

    static class DrivingAge
    {
        public static void LegalToDrive(int age, TextWriter textWriter)
        {
            const int legalAgeToDrive = 16;

            textWriter.WriteLine(age >= legalAgeToDrive
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
            var stringWriter = new StringWriter();
            DrivingAge.LegalToDrive(legalAge, stringWriter);
            Assert.That(stringWriter.ToString(), Is.EqualTo("You are old enough to legally drive."));
        }

        [Test]
        [TestCase(15)]
        public void Illegal_To_Drive(int illegalAge)
        {
            var stringWriter = new StringWriter();
            DrivingAge.LegalToDrive(illegalAge, stringWriter);
            Assert.That(stringWriter.ToString(), Is.EqualTo("You are not old enough to legally drive."));
        }
    }
}