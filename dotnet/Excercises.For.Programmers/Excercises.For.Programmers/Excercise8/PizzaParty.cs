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

    public class PizzaSplitter
    {
        public SplitPizza Split(int numberOfPizzas, int numberOfPeople)
        {
            Debug.Assert(numberOfPeople > 1);
            Debug.Assert(numberOfPizzas > 1);

            var halfedPizzas = numberOfPizzas * 2;
            var slicesPerPerson = halfedPizzas / numberOfPeople;

            if (slicesPerPerson % 2 != 0 || slicesPerPerson == 0)
            {
                halfedPizzas += numberOfPizzas * 2;
                slicesPerPerson = halfedPizzas / numberOfPeople;

                if (slicesPerPerson % 2 != 0 || slicesPerPerson == 0)
                {
                    halfedPizzas += numberOfPizzas * 2;
                    slicesPerPerson = halfedPizzas / numberOfPeople;

                    if (slicesPerPerson % 2 != 0 || slicesPerPerson == 0)
                    {
                        halfedPizzas += numberOfPizzas * 2;
                        slicesPerPerson = halfedPizzas / numberOfPeople;

                        if (slicesPerPerson % 2 != 0 || slicesPerPerson == 0)
                        {
                            halfedPizzas += numberOfPizzas * 2;
                            slicesPerPerson = halfedPizzas / numberOfPeople;

                            if (slicesPerPerson % 2 != 0 || slicesPerPerson == 0)
                            {
                                halfedPizzas += numberOfPizzas * 2;
                                slicesPerPerson = halfedPizzas / numberOfPeople;

                            }
                        }
                    }
                }


            }

            var leftOvers = halfedPizzas % numberOfPeople;

            return new SplitPizza()
                   {
                       SlicesPerPerson = slicesPerPerson,
                       LeftOverSlices = leftOvers
                   };
        }

        public bool IsEven(int slicesOfPizza)
        {
            return slicesOfPizza % 2 == 0;
        }
    }

    public class SplitPizza
    {
        public int SlicesPerPerson { get; set; }
        
        public int LeftOverSlices { get; set; }
    }
}