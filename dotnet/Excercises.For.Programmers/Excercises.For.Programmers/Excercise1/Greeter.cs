using System;
using NUnit.Framework;

namespace Excercises.For.Programmers.Excercise1
{
    public class Greeter
    {
        public void SayHello()
        {
            Console.Write("{0} ", Question());
            Console.WriteLine(Greeting(AskForName()));
        }

        internal string Greeting(string name)
        {
            string formattedName = string.IsNullOrWhiteSpace(name) ? "" : string.Format(" {0},", name);
            return string.Format("Hello,{0} nice to meet you!", formattedName);
        }

        internal string Question()
        {
            return "What is your name?";
        }

        protected virtual string AskForName()
        {
            return Console.ReadLine();
        }
    }

    [TestFixture]
    public class Excercise1Tests
    {
        [Test]
        public void AskQuestion()
        {
            string question = new TestGreeter().Question();
            Assert.That(question, Is.EqualTo("What is your name?"));
        }

        [Test]
        public void Greeting()
        {
            string greeting = new TestGreeter().Greeting("Shaun");
            Assert.That(greeting, Is.EqualTo("Hello, Shaun, nice to meet you!"));
        }

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void GreetingWithNoName(string name)
        {
            string greeting = new TestGreeter().Greeting(name);
            Assert.That(greeting, Is.EqualTo("Hello, nice to meet you!"));
        }

        class TestGreeter : Greeter
        {
            protected override string AskForName()
            {
                return "Shaun";
            }
        }
    }
}
