using System;
using System.Collections.Generic;
using RestSharp;

namespace TravelPlanner.Infrastructure.Network.Amadeus
{
    public class AmadeusApi : HttpApi, IHousingDataProvider, INetworkDataUpdater
    {
        private const string Url = "https://amadeus-developers-test.apigee.net";
        private const string AuthResource = "v1/security/oauth2/token";

        private const string SearchResource = "v1/shopping/hotel-offers";

        private static readonly string ClientId = Environment.GetEnvironmentVariable("amadeus-id");
        private static readonly string ClientSecret = Environment.GetEnvironmentVariable("amadeus-secret");
        private const string GrantType = "client_credentials";

        public string GetData(string parameters)
        {
            var headers = GetHeaders();
            var resource = $"v1/shopping/hotels/{parameters}/hotel-offers";
            var response =
                SendRequestAndReturnResponse(Method.GET, headers, new Dictionary<string, string>(), resource, Url);
            return response.Content;
        }

        public string GetData(HousingParameters parameters)
        {
            var headers = GetHeaders();
            var amadeusParameters = AmadeusParameters.FromHousingParameters(parameters);
            var response = SendRequestAndReturnResponse(
                Method.GET, headers, amadeusParameters, SearchResource, Url);
            return response.Content;
        }

        private Dictionary<string, string> GetHeaders()
        {
            var authParameters = new Dictionary<string, string>
            {
                {"client_id", ClientId },
                {"client_secret", ClientSecret },
                {"grant_type", GrantType }
            };
            var response = SendRequestAndReturnResponse(Method.POST, new Dictionary<string, string>(), authParameters,
                AuthResource, Url);
            var token = SimpleJson.DeserializeObject<Dictionary<string, object>>(response.Content)["access_token"];
            return new Dictionary<string, string>
            {
                { "Authorization", $"Bearer {token}"}
            };
        }
    }
}