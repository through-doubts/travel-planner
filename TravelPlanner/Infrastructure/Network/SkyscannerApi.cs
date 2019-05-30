using System.Collections.Generic;
using System.Linq;
using RestSharp;

namespace TravelPlanner.Infrastructure.Network
{
    public class SkyscannerApi : HttpApi, IFlightDataProvider
    {
        private static readonly string RapidApiHost = "skyscanner-skyscanner-flight-search-v1.p.rapidapi.com";
        private static readonly string RapidApiKey = "5e1b6e0ce2mshf8d7bf48452442dp1fda98jsn9ef336f6b389";
        private static readonly string PostResource = "apiservices/pricing/v1.0";
        private static readonly string GetResource = "apiservices/pricing/uk2/v1.0/";
        private static readonly string Url = "https://skyscanner-skyscanner-flight-search-v1.p.rapidapi.com";

        public string GetData(FlightParameters parameters)
        {
            var skyscannerParameters = SkyscannerApiParameters.FromFlightParameters(parameters);
            var data = SendRequestAndReturnResponse(Method.POST, GetHeaders(), skyscannerParameters, PostResource, Url);
            var location = data.Headers
                .ToList()
                .Find(x => x.Name == "location")
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