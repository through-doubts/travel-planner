using System;
using System.Collections.Generic;
using RestSharp;

namespace TravelPlanner.Infrastructure.Network.AirportsFinder
{
    public class AirportsFinder : HttpApi, INetworkDataProvider<string>
    {
        private const string Url = "https://cometari-airportsfinder-v1.p.rapidapi.com";
        private const string Resource = "api/airports/by-text";
        private const string RapidApiHost = "cometari-airportsfinder-v1.p.rapidapi.com";
        private static readonly string RapidApiKey = Environment.GetEnvironmentVariable("rapidapi-key");

        public string GetData(string parameters)
        {
            var finderParameters = new Dictionary<string, string>
            {
                {"text", parameters }
            };
            var headers = GetHeaders();

            var response = SendRequestAndReturnResponse(Method.GET, headers, finderParameters, Resource, Url);
            return response.Content;
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