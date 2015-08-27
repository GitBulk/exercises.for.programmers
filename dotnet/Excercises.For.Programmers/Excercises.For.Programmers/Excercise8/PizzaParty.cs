using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Excercises.For.Programmers.Excercise8
{
    // TODO: only accept numeric values, retry otherwise. Keep this in the UI layer, do not
    // allow it to leak into the domain as I did in the other excercise.

    // Todo: Handle pluralization.

    public class ConsolePizzaSplitter
    {
        public void Execute()
        {
            Console.Write("How many people? ");
            var numberOfPeople = Console.ReadLine();

            Console.Write("How many pizzas do you have? ");
            var numberOfPizzas = Console.ReadLine();

            Console.WriteLine("{0} people with {1} pizzas.", numberOfPeople, numberOfPizzas);

            var pizzaSplitter = new PizzaSplitter();
            var pizzaSlices = pizzaSplitter.Split(int.Parse(numberOfPizzas), int.Parse(numberOfPeople));

            Console.WriteLine("Each person gets {0} pieces of pizza.", pizzaSlices.SlicesPerPerson);
            Console.WriteLine("There are {0} leftover pieces.", pizzaSlices.LeftOverSlices);
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        [TestCase(2, 4, 1, 0)]
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
        [TestCase(2, 5, 1, 1)]
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

            var slicesPerPerson = (numberOfPeople / numberOfPizzas) / numberOfPizzas;
            return new SplitPizza() { SlicesPerPerson = slicesPerPerson, LeftOverSlices = numberOfPeople % numberOfPizzas};
        }
    }

    public class SplitPizza
    {
        public int SlicesPerPerson { get; set; }
        
        public int LeftOverSlices { get; set; }
    }
}