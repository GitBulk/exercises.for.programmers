using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using NUnit.Framework;

namespace Exercises.For.Programmers.Exercises._10
{
    public class ConsoleSelfCheckout
    {
        public void Execute()
        {
            var selfCheckout = new SelfCheckout();

            foreach (var index in Enumerable.Range(1, 3))
            {
                selfCheckout = EnterItem(selfCheckout, index);
            }

            var formattedSelfCheckout = new FormattedSelfCheckout(selfCheckout);

            Console.WriteLine("Subtotal: {0}", formattedSelfCheckout.SubTotal());
            Console.WriteLine("Tax: {0}", formattedSelfCheckout.Tax());
            Console.WriteLine("Total: {0}", formattedSelfCheckout.Total());
        }

        private static SelfCheckout EnterItem(SelfCheckout selfCheckout, int index)
        {
            var price = Input.Parse(message: string.Format("Price of item {0}: ", index));
            var quantity = Input.Parse(message: string.Format("Quantity of item {0}: ", index));

            return selfCheckout.Scan(price, quantity);
        }
    }

    [TestFixture]
    public class Tests
    {
        [Test]
        public void SubTotal_Of_No_Items_Scanned_Is_Zero()
        {
            var subject = new SelfCheckout();

            decimal total = subject.SubTotal();

            Assert.That(total, Is.EqualTo(0m));
        }

        [Test]
        public void Tax_Of_No_Items_Scanned_Is_Zero()
        {
            var subject = new SelfCheckout();

            decimal tax = subject.Tax();

            Assert.That(tax, Is.EqualTo(0m));
        }

        [Test]
        public void Total_Of_No_Items_Scanned_Is_Zero()
        {
            var subject = new SelfCheckout();

            decimal total = subject.Total();

            Assert.That(total, Is.EqualTo(0m));
        }

        [Test]
        public void SubTotal_Of_One_Item_Scanned()
        {
            var subject = new SelfCheckout();

            decimal total = subject
                .Scan(price: 25, quantity: 1)
                .SubTotal();

            Assert.That(total, Is.EqualTo(25.00m));
        }

        [Test]
        public void Total_Of_One_Item_Scanned()
        {
            var subject = new SelfCheckout();

            decimal total = subject
                .Scan(price: 25, quantity: 1)
                .Total();

            Assert.That(total, Is.EqualTo(26.375m));
        }

        [Test]
        public void Tax_Of_One_Item_Scanned()
        {
            var subject = new SelfCheckout();

            decimal tax = subject
                .Scan(price: 25, quantity: 1)
                .Tax();

            Assert.That(tax, Is.EqualTo(1.375m));
        }

        [Test]
        public void SubTotal_Of_One_Item_Scanned_With_Multiple_Quantities()
        {
            var subject = new SelfCheckout();

            decimal total = subject
                .Scan(price: 25, quantity: 2)
                .SubTotal();

            Assert.That(total, Is.EqualTo(50m));
        }

        [Test]
        public void Tax_Of_One_Item_Scanned_With_Multiple_Quantities()
        {
            var subject = new SelfCheckout();

            decimal total = subject
                .Scan(price: 25, quantity: 2)
                .Tax();

            Assert.That(total, Is.EqualTo(2.75m));
        }

        [Test]
        public void SubTotal_Of_Multiple_Items_Scanned_With_Multiple_Quantities()
        {
            var subject = new SelfCheckout();

            decimal total = subject
                .Scan(price: 25, quantity: 2)
                .Scan(price: 10, quantity: 1)
                .SubTotal();

            Assert.That(total, Is.EqualTo(60m));
        }

        [Test]
        public void Scanning_Multiple_Items()
        {
            var subject = new SelfCheckout();

            subject = subject
                .Scan(price: 25, quantity: 2)
                .Scan(price: 10, quantity: 1)
                .Scan(price: 4, quantity: 1);

            Assert.That(subject.SubTotal(), Is.EqualTo(64.00m));
            Assert.That(subject.Tax(), Is.EqualTo(3.52m));
            Assert.That(subject.Total(), Is.EqualTo(67.52m));
        }
    }

    public class FormattedSelfCheckout
    {
        private readonly SelfCheckout selfCheckout;

        public FormattedSelfCheckout(SelfCheckout selfCheckout)
        {
            if (selfCheckout == null)
            {
                throw new ArgumentNullException("selfCheckout");
            }

            this.selfCheckout = selfCheckout;
        }

        public string Total()
        {
            return FormatCurrency(selfCheckout.Total());
        }

        public string SubTotal()
        {
            return FormatCurrency(selfCheckout.SubTotal());
        }

        public string Tax()
        {
            return FormatCurrency(selfCheckout.Tax());
        }

        private static string FormatCurrency(decimal amount)
        {
            return amount.ToString("C", new CultureInfo("en-GB"));
        }
    }

    public class SelfCheckout
    {
        private readonly decimal subTotal;

        public SelfCheckout(decimal subTotal = 0m)
        {
            this.subTotal = subTotal;
        }

        public decimal SubTotal()
        {
            return this.subTotal;
        }

        public SelfCheckout Scan(decimal price, int quantity)
        {
            Debug.Assert(quantity > 0);
            Debug.Assert(price > 0m);

            var itemTotal = price * quantity;

            return new SelfCheckout(subTotal: itemTotal + SubTotal());
        }

        public decimal Tax()
        {
            const decimal taxRate = 0.055m;
            return this.SubTotal() * taxRate;
        }

        public decimal Total()
        {
            return this.SubTotal() + this.Tax();
        }
    }
}