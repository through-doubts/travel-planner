using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace TravelPlanner.Infrastructure.Network.Skyscanner
{
    public class SkyscannerApi : HttpApi, ITransportDataProvider
    {
        private const string RapidApiHost = "skyscanner-skyscanner-flight-search-v1.p.rapidapi.com";
        private static readonly string RapidApiKey = Environment.GetEnvironmentVariable("rapidapi-key");
        private const string PostResource = "apiservices/pricing/v1.0";
        private const string GetResource = "apiservices/pricing/uk2/v1.0/";
        private const string Url = "https://skyscanner-skyscanner-flight-search-v1.p.rapidapi.com";

        public string GetData(TransportParameters parameters)
        {
            var skyscannerParameters = SkyscannerApiParameters.FromFlightParameters(parameters);
            var data = SendRequestAndReturnResponse(Method.POST, GetHeaders(), skyscannerParameters, PostResource, Url);
            var location = data.Headers
                .ToList()
                .Find(x => x.Name == "Location")
                .Value.ToString()
                .Split('/')
                .Last();
            var getParameters = new Dictionary<string, string>
            {
                { "sessionKey", location },
                {"pageIndex", "0" },
                {"pageSize", "10" }
            };
            var response = SendRequestAndReturnResponse(Method.GET, GetHeaders(), getParameters, GetResource, Url);
            return response.Content;
        }

        private Dictionary<string, string> GetHeaders()
        {
            return new Dictionary<string, string>
            {
                {"X-RapidAPI-Host", RapidApiHost },
                {"X-RapidAPI-Key", RapidApiKey },
                {"Content-Type", "application/x-www-form-urlencoded" }
            };
        }
    }
}