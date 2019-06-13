using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using RestSharp;
using TravelPlanner.Infrastructure.Network;

namespace TravelPlanner.Application.Network.Currency
{
    public class CurrencyApi : HttpDataProvider, ICurrencyDataProvider
    {
        private const string Url = "https://api.ratesapi.io";
        private const string Resource = "api/latest";

        public CurrencyApi(HttpApi httpApi) : base(httpApi) { }

        public Dictionary<Infrastructure.Currency, double> GetData(Infrastructure.Currency parameters)
        {
            var currencyParameters = new Dictionary<string, string>
            {
                {"base", parameters.ToString() }
            };
            var response = SendRequestAndGetResponse(Method.GET, new Dictionary<string, string>(),
                currencyParameters, Resource, Url);
            return ParseResponse(response.Content);
        }

        private Dictionary<Infrastructure.Currency, double> ParseResponse(string content)
        {
            var currencies = Enum.GetNames(typeof(Infrastructure.Currency));
            return SimpleJson.DeserializeObject<Dictionary<string, object>>(content)
                .Select(x => Tuple.Create(x.Key, x.Value))
                .Where(x => Array.Exists(currencies,
                    s => string.Equals(s, x.Item1, StringComparison.CurrentCultureIgnoreCase)))
                .Select(x =>
                    Tuple.Create((Infrastructure.Currency) Enum.Parse(typeof(Infrastructure.Currency), x.Item1),
                        Convert.ToDouble(x.Item2, CultureInfo.InvariantCulture)))
                .ToDictionary(x => x.Item1, x => x.Item2);
        }
    }
}