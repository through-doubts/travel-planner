using System;
using System.Collections.Generic;
using System.Linq;
using RestSharp;
using TravelPlanner.Infrastructure.Network;
using TravelPlanner.Properties;

namespace TravelPlanner.Application.Network.AirportsFinder
{
    public class AirportCodeFinder : HttpDataProvider, IAirportCodeFinder
    {
        private const string Url = "https://cometari-airportsfinder-v1.p.rapidapi.com";
        private const string Resource = "api/airports/by-text";
        private const string RapidApiHost = "cometari-airportsfinder-v1.p.rapidapi.com";

        private static readonly string RapidApiKey = Resources.rapidapi_key;

        public AirportCodeFinder(HttpApi httpApi) : base(httpApi) { }

        public List<string> GetData(string parameters)
        {
            var finderParameters = new Dictionary<string, string>
            {
                {"text", parameters }
            };
            var headers = GetHeaders();

            var response = SendRequestAndGetResponse(Method.GET, headers, finderParameters, Resource, Url);
            return ParseResponse(response.Content, parameters);
        }

        private List<string> ParseResponse(string content, string parameters)
        {
            return SimpleJson.DeserializeObject<List<Dictionary<string, object>>>(content)
                .Where(x => (string)x["city"] == parameters)
                .Select(x => x["code"]).OfType<string>().ToList();
        }

        private Dictionary<string, string> GetHeaders()
        {
            return new Dictionary<string, string>
            {
                {"X-RapidAPI-Host", RapidApiHost },
                {"X-RapidAPI-Key", RapidApiKey }
            };
        }
    }
}