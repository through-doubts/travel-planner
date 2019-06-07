using System.Collections.Generic;
using RestSharp;

namespace TravelPlanner.Infrastructure.Network.Currency
{
    public class CurrencyApi : HttpApi, ICurrencyDataProvider
    {
        private const string Url = "https://api.ratesapi.io";
        private const string Resource = "api/latest";

        public string GetData(Infrastructure.Currency parameters)
        {
            var currencyParameters = new Dictionary<string, string>
            {
                {"base", parameters.ToString() }
            };
            var response = SendRequestAndReturnResponse(Method.GET, new Dictionary<string, string>(),
                currencyParameters, Resource, Url);
            return response.Content;
        }
    }
}