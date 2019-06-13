using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RestSharp;
using TravelPlanner.Domain;
using TravelPlanner.Domain.TravelEvents;
using TravelPlanner.Infrastructure;
using TravelPlanner.Infrastructure.Network;
using TravelPlanner.Properties;

namespace TravelPlanner.Application.Network.Skyscanner
{
    public class SkyscannerApi : HttpDataProvider, ITransportDataProvider
    {
        private const string RapidApiHost = "skyscanner-skyscanner-flight-search-v1.p.rapidapi.com";

        private static readonly string RapidApiKey = Resources.rapidapi_key;
        private const string PostResource = "apiservices/pricing/v1.0";
        private const string GetResource = "apiservices/pricing/uk2/v1.0/";
        private const string Url = "https://skyscanner-skyscanner-flight-search-v1.p.rapidapi.com";
        private readonly ILocationHandler locationHandler;

        public SkyscannerApi(HttpApi httpApi, ILocationHandler locationHandler) : base(httpApi)
        {
            this.locationHandler = locationHandler;
        }

        public List<Transfer> GetData(TransportParameters parameters)
        {
            var skyscannerParameters = SkyscannerApiParameters.FromFlightParameters(parameters);
            var data = SendRequestAndGetResponse(Method.POST, GetHeaders(), skyscannerParameters,
                PostResource, Url);
            var location = data.Headers
                .ToList()
                .Find(x => x.Name == "Location")
                .Value.ToString()
                .Split('/')
                .Last();
            var getParameters = new Dictionary<string, string>
            {
                { "sessionKey", location },
                {"sortType", "price" },
                {"sortOrder", "asc" },
                {"pageIndex", "0" },
                {"pageSize", "3" }
            };
            var response =
                SendRequestAndGetResponse(Method.GET, GetHeaders(), getParameters, GetResource + location, Url);
            return ParseResponse(response.Content);
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

        private List<Transfer> ParseResponse(string content)
        {
            var parsedResponse = SimpleJson.DeserializeObject<SkyscannerResponse>(content);
            var prices = parsedResponse.Itineraries
                .ToDictionary(x => x.OutboundLegId, x => x.PricingOptions.First().Price);
            var placesNames = parsedResponse.Places
                .ToDictionary(x => x.Id, x => x.Name);
            return parsedResponse.Legs.Where(x => prices.ContainsKey(x.Id))
                .Select(x => new Transfer(GetDates(x), GetLocations(x, placesNames),
                    GetMoney(x, prices, parsedResponse),"Самолет")).ToList();
        }

        private DateTime[] GetDates(Leg leg)
        {
            return new[] { leg.Departure, leg.Arrival}.Select(DateTime.Parse).ToArray();
        }

        private Location[] GetLocations(Leg leg, Dictionary<int, string> placesNames)
        {
            return new[] { leg.OriginStation, leg.DestinationStation}.Select(loc => placesNames[loc])
                .Select(loc => loc.Split(' ').First())
                .Select(loc => locationHandler.GetLocationByName(loc)).ToArray();
        }

        private Money GetMoney(Leg leg, Dictionary<string, decimal> prices, SkyscannerResponse response)
        {
            return new Money(Currencies.GetCurrency((string)response.Query["Currency"]),
                prices[leg.Id]);
        }
    }
}