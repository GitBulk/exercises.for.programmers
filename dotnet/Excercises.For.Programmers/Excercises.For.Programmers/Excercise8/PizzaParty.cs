using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Excercises.For.Programmers.Excercise8
{
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
    }

    public class PizzaSplitter
    {
        public SplitPizza Split(int numberOfPizzas, int numberOfPeople)
        {
            Debug.Assert(numberOfPeople > 1);
            Debug.Assert(numberOfPizzas > 1);

            int splitPizza = SplitPizza(numberOfPizzas);
            int slicesPerPerson = SlicesPerPerson(numberOfPeople, splitPizza);
            int leftOvers = LeftOvers(numberOfPeople, splitPizza);

            while (IsNotEvenlyDivided(slicesPerPerson))
            {
                splitPizza += SplitPizza(numberOfPizzas);
                slicesPerPerson = SlicesPerPerson(numberOfPeople, splitPizza);
                leftOvers = LeftOvers(numberOfPeople, splitPizza);
            }

            return new SplitPizza(slicesPerPerson, leftOvers);
        }

        private static int LeftOvers(int numberOfPeople, int splitPizza)
        {
            return splitPizza % numberOfPeople;
        }

        private static int SlicesPerPerson(int numberOfPeople, int splitPizza)
        {
            return splitPizza / numberOfPeople;
        }

        private static int SplitPizza(int numberOfPizzas)
        {
            const int splitFactor = 2;
            return numberOfPizzas * splitFactor;
        }

        private static bool IsNotEvenlyDivided(int slicesPerPerson)
        {
            return slicesPerPerson % 2 != 0 || slicesPerPerson == 0;
        }
    }

    public class SplitPizza
    {
        public SplitPizza(int slicesPerPerson, int leftOvers)
        {
            SlicesPerPerson = slicesPerPerson;
            LeftOverSlices = leftOvers;
        }

        public int SlicesPerPerson { get; set; }
        
        public int LeftOverSlices { get; set; }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Eight_People_Two_Pizzas()
        {
            var subject = new PizzaSplitter();

            var split = subject.Split(numberOfPizzas:2, numberOfPeople:8);
            
            Assert.That(split.SlicesPerPerson, Is.EqualTo(2));
            Assert.That(split.LeftOverSlices, Is.EqualTo(0));
        }

        [Test]
        public void Two_People_Two_Pizzas()
        {
            var subject = new PizzaSplitter();

            var split = subject.Split(numberOfPizzas: 2, numberOfPeople: 2);

            Assert.That(split.SlicesPerPerson, Is.EqualTo(2));
            Assert.That(split.LeftOverSlices, Is.EqualTo(0));
        }

        [Test]
        public void Three_People_Two_Pizzas()
        {
            var subject = new PizzaSplitter();

            var split = subject.Split(numberOfPizzas: 2, numberOfPeople: 3);

            Assert.That(split.SlicesPerPerson, Is.EqualTo(2));
            Assert.That(split.LeftOverSlices, Is.EqualTo(2));
        }

        [Test]
        public void Four_People_Two_Pizzas()
        {
            var subject = new PizzaSplitter();

            var split = subject.Split(numberOfPizzas: 2, numberOfPeople: 4);

            Assert.That(split.SlicesPerPerson, Is.EqualTo(2));
            Assert.That(split.LeftOverSlices, Is.EqualTo(0));
        }

        [Test]
        public void Nine_People_Two_Pizzas()
        {
            var subject = new PizzaSplitter();
            
            var split = subject.Split(numberOfPizzas:2, numberOfPeople:9);
            
            Assert.That(split.SlicesPerPerson, Is.EqualTo(2));
            Assert.That(split.LeftOverSlices, Is.EqualTo(2));
        }
    }
}