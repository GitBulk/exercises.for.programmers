using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Excercises.For.Programmers.Excercise8
{
    // Todo: Handle pluralization.

    public class ConsolePizzaSplitter
    {
        public void Execute()
        {
            int numberOfPeople = Input.Parse(message: "How many people? ");
            int numberOfPizzas = Input.Parse(message: "How many pizzas do you have? ");

            Console.WriteLine("{0} people with {1} pizzas.", numberOfPeople, numberOfPizzas);

            var pizzaSplitter = new PizzaSplitter();
            var pizzaSlices = pizzaSplitter.Split(numberOfPizzas, numberOfPeople);

            Console.WriteLine("Each person gets {0} pieces of pizza.", pizzaSlices.SlicesPerPerson);
            Console.WriteLine("There are {0} leftover pieces.", pizzaSlices.LeftOverSlices);
        }

        static class Input
        {
            public static int Parse(string message)
            {
                while (true)
                {
                    Console.Write(message);

                    try
                    {
                        return int.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                    }
                }
            }
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase(2, 8, 2, 0)]
        public void Multiple_Pizzas_And_Multiple_People_With_No_Leftovers(
            int numberOfPizzas,
            int numberOfPeople,
            int slicesPerPerson,
            int leftOvers)
        {
            var subject = new PizzaSplitter();

            var split = subject.Split(numberOfPizzas, numberOfPeople);
            
            Assert.That(split.SlicesPerPerson, Is.EqualTo(slicesPerPerson));
            Assert.That(split.LeftOverSlices, Is.EqualTo(leftOvers));
        }

        [Test]
        [TestCase(2, 9, 2, 1)]
        public void Multiple_Pizzas_And_Multiple_People_With_Leftovers(
            int numberOfPizzas,
            int numberOfPeople,
            int slicesPerPerson,
            int leftOvers)
        {
            var subject = new PizzaSplitter();
            
            var split = subject.Split(numberOfPizzas, numberOfPeople);
            
            Assert.That(split.SlicesPerPerson, Is.EqualTo(slicesPerPerson));
            Assert.That(split.LeftOverSlices, Is.EqualTo(leftOvers));
        }
    }

    public class PizzaSplitter
    {
        public SplitPizza Split(int numberOfPizzas, int numberOfPeople)
        {
            Debug.Assert(numberOfPeople > 1);
            Debug.Assert(numberOfPizzas > 1);

            var slicesPerPerson = (numberOfPeople * numberOfPizzas) / numberOfPeople;
            return new SplitPizza() { SlicesPerPerson = slicesPerPerson, LeftOverSlices = numberOfPeople % numberOfPizzas };
        }
    }

    public class SplitPizza
    {
        public int SlicesPerPerson { get; set; }
        
        public int LeftOverSlices { get; set; }
    }
}