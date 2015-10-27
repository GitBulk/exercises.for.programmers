using System.IO;
using NUnit.Framework;

namespace Exercises.For.Programmers.Exercises._20
{
    public class ConsoleSalesTaxCalculator
    {
        public void Execute()
        {
            var orderAmount = Input.ParseDecimal(message: "What is the order amount? ");
            var state = Input.ParseString(message: "What state do you live in? ");
        }
    }

    // ask for user order amount
    // ask for the state

    // if wisconsin
        // ask for county
        // if eau claire
            // add 0.05 tax
        // else if dunn county
            // add 0.04 tax
        // display tax
        // display total
    // else if illinois
        // add 0.08 tax
        // display tax
        // display total
    // else
        // display total

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Order_For_Non_Taxable_State()
        {
            var orderAmount = new TaxCalculator().Calculate(state: "New York", order:10m);
            Assert.That(orderAmount, Is.EqualTo(10m));
        }

        [Test]
        public void Order_For_Taxable_State()
        {
            var orderAmount = new TaxCalculator().Calculate(state: "Illinois", order: 10m);
            Assert.That(orderAmount, Is.EqualTo(10.80m));
        }

        private interface ITaxCalculator
        {
            decimal Calculate(string state, decimal order);
        }

        class TaxCalculator : ITaxCalculator
        {
            public decimal Calculate(string state, decimal order)
            {
                if (state == "Illinois")
                {
                    const decimal tax = 0.08m;
                    return order + (order * tax);
                }

                return order;
            }
        }


    }
}