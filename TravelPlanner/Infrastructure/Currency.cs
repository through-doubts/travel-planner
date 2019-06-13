using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanner.Infrastructure
{
    public class Currencies
    {
        public static Currency EUR = new Currency("EUR", "€");
        public static Currency USD = new Currency("USD", "$");
        public static Currency RUB = new Currency("RUB", "₽");

        private static Currency[] currencies = {EUR, USD, RUB};

        public static Currency GetCurrency(string currencyName)
        {
            return currencies.FirstOrDefault(c => c.Name == currencyName);
        }

        public static string[] GetCurrenciesNames()
        {
            return currencies.Select(c => c.Name).ToArray();
        }
    }

    public class Currency : ValueType<Currency>
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
