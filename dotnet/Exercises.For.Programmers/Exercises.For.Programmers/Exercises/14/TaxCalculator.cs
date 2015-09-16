using System;
using System.Diagnostics;
using System.IO;
using NUnit.Framework;

namespace Exercises.For.Programmers.Exercises._14
{
    public class ConsoleTaxCalculator
    {
        public void Execute()
        {
            var orderAmount = Input.DecimalParse(message: "What is the order amount? ");
            var state = Input.ParseString(message: "What is the state? ");

            var taxCalculator = new TaxCalculator();
            ITaxResult taxResult = taxCalculator.Calculate(orderAmount, state);
            taxResult.FormatTo(Console.Out);
        }
    }

    class TaxCalculator
    {
        private const string TaxableState = "WI";

        public ITaxResult Calculate(decimal orderAmount, string state)
        {
            Debug.Assert(orderAmount > 0m);
            Debug.Assert(!string.IsNullOrWhiteSpace(state));

            if (state == TaxableState)
            {
                return new TaxResult(orderAmount);
            }

            return new TaxFreeResult(orderAmount);
        }
    }

    class TaxFreeResult : ITaxResult
    {
        private readonly decimal orderAmount;

        public TaxFreeResult(decimal orderAmount)
        {
            this.orderAmount = orderAmount;
        }

        public decimal Total()
        {
            return this.orderAmount;
        }

        public decimal Tax()
        {
            return 0m;
        }

        public void FormatTo(TextWriter output)
        {
            output.WriteLine("The total is {0}", new Money(Total()));
        }
    }

    class TaxResult : ITaxResult
    {
        private readonly decimal orderAmount;

        public TaxResult(decimal orderAmount)
        {
            this.orderAmount = orderAmount;
        }

        public decimal Total()
        {
            return this.orderAmount + Tax();
        }

        public decimal Tax()
        {
            const decimal taxRate = 5.5m;
            const int percent = 100;
            return (this.orderAmount * taxRate) / percent;
        }

        public void FormatTo(TextWriter output)
        {
            output.WriteLine("The subtotal is {0}", new Money(orderAmount));
            output.WriteLine("The tax is {0}", new Money(Tax()));
            output.WriteLine("The total is {0}", new Money(Total()));
        }
    }

    public interface IFormattedTaxResult
    {
        void FormatTo(TextWriter output);
    }

    public interface ITaxResult : IFormattedTaxResult
    {
        decimal Total();

        decimal Tax();
    }

    [TestFixture]
    class Tests
    {
        class Tax_Free_State_Tests
        {
            [Test]
            public void Tax_Free_State()
            {
                var subject = new TaxCalculator();

                var result = subject.Calculate(orderAmount: 10m, state: "NY");

                Assert.That(result.Tax(), Is.EqualTo(0m));
                Assert.That(result.Total(), Is.EqualTo(10m));
            }

            [Test]
            public void Formatting()
            {
                var subject = new TaxCalculator();

                var result = subject.Calculate(orderAmount: 10m, state: "NY");

                var output = new StringWriter();
                result.FormatTo(output);
                Assert.That(output.ToString(), Is.StringContaining("The total is £10.00"));
            }
        }

        class Taxable_State_Tests
        {
            [Test]
            public void Taxable_State()
            {
                var subject = new TaxCalculator();

                var result = subject.Calculate(orderAmount: 10m, state: "WI");

                Assert.That(result.Tax(), Is.EqualTo(0.55m));
                Assert.That(result.Total(), Is.EqualTo(10.55m));
            }

            [Test]
            public void Formatting()
            {
                var subject = new TaxCalculator();

                var result = subject.Calculate(orderAmount: 10m, state: "WI");

                var output = new StringWriter();
                result.FormatTo(output);
                Assert.That(output.ToString(), Is.StringContaining("The subtotal is £10.00"));
                Assert.That(output.ToString(), Is.StringContaining("The tax is £0.55"));
                Assert.That(output.ToString(), Is.StringContaining("The total is £10.55"));
            }
        }
    }
}