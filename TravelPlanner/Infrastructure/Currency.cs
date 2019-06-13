using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanner.Infrastructure
{
    //public enum Currency
    //{
    //    EUR,
    //    RUB,
    //    USD
    //}

    public class Currencies
    {
        public static Currency EUR = new Currency("EUR", "€");
        public static Currency USD = new Currency("USD", "$");
        public static Currency RUB = new Currency("RUB", "₽");
    }

    public class Currency
    {
        public string Name { get; }
        public string Symbol { get; }

        public Currency(string name, string symbol)
        {
            Name = name;
            Symbol = symbol;
        }
    }
}
