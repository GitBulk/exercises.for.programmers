using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Exercises.For.Programmers.Exercises._20
{
    public class ConsoleSalesTaxCalculator
    {
        public void Execute()
        {
            var orderAmount = Input.ParseDecimal(message: "What is the order amount? ");
            var state = Input.ParseString(message: "What state do you live in? ");

            var taxCalculator = new TaxCalculator();
            var order = taxCalculator.Calculate(new StateCounty(state), new Order(orderAmount));

            if (order.Tax != 0m)
            {
                Console.WriteLine("Tax: " + new Money(order.Tax));
            }
            
            Console.WriteLine(new Money(order.Total));
        }
    }

    class Money
    {
        private readonly decimal value;

        public Money(decimal value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return value.ToString("C", new CultureInfo("en-GB"));
        }
    }

    public class Order
    {
        public decimal Total { get; private set; }

        public decimal Tax { get; private set; }

        public Order(decimal order, decimal tax = 0m)
        {
            this.Total = order;
            this.Tax = tax;
        }

        public Order AddTax(decimal tax)
        {
            var orderTotal = this.Total + (this.Total * tax);
            return new Order(orderTotal, tax);
        }

        public override string ToString()
        {
            return this.Total.ToString(new CultureInfo("en-GB"));
        }
    }

    public class StateCounty
    {
        private readonly string state;

        public StateCounty(string state)
        {
            this.state = state;
        }

        public override int GetHashCode()
        {
            return this.state.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as StateCounty;
            return other != null && 
                this.state.Equals(other.state, StringComparison.OrdinalIgnoreCase);
        }

        public StateCounty County(string county)
        {
            return new StateCounty(string.Format("{0}-{1}", state, county));
        }
    }

    public class TaxCalculator
    {
        private readonly IDictionary<StateCounty, decimal> taxableStates
            = new Dictionary<StateCounty, decimal>()
              {
                  {new StateCounty("Illinois"), 0.08m},
                  {new StateCounty("Wisconsin-Eau Claire"), 0.05m},
                  {new StateCounty("Wisconsin-Dunn County"), 0.04m}
              };

        private readonly StateCounty[] taxableCounties = { new StateCounty("Wisconsin") };

        public Order Calculate(StateCounty stateCounty, Order order)
        {
            if (taxableStates.ContainsKey(stateCounty))
            {
                decimal tax = taxableStates[stateCounty];
                return order.AddTax(tax);
            }

            if (taxableCounties.Contains(stateCounty))
            {
                var county = Input.ParseString(message: "County: ");
                var taxableCounty = stateCounty.County(county);
                if (taxableStates.ContainsKey(taxableCounty))
                {
                    decimal tax = taxableStates[taxableCounty];
                    return order.AddTax(tax);
                }
            }

            return order;
        }
    }
}