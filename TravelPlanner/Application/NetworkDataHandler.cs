using System;
using System.Collections.Generic;
using TravelPlanner.Application.Network;
using TravelPlanner.Application.Network.AirportsFinder;
using TravelPlanner.Domain;
using TravelPlanner.Infrastructure.Extensions;
using TravelPlanner.Infrastructure.Network;

namespace TravelPlanner.Application
{
    public class NetworkDataHandler : INetworkDataHandler
    {
        private readonly ITransportDataProvider transportDataProvider;
        private readonly IHousingDataProvider housingDataProvider;
        private readonly IAirportCodeFinder airportCodeFinder;
        private readonly ILocationHandler locationHandler;

        public NetworkDataHandler(ITransportDataProvider transportDataProvider,
            IHousingDataProvider housingDataProvider, IAirportCodeFinder airportCodeFinder,
            ILocationHandler locationHandler)
        {
            this.transportDataProvider = transportDataProvider;
            this.housingDataProvider = housingDataProvider;
            this.airportCodeFinder = airportCodeFinder;
            this.locationHandler = locationHandler;
        }

        public List<ITravelEvent> GetTransfers(string currency, string originPlace, string destinationPlace,
            DateTime outboundDate)
        {
            var travelEvents = new List<ITravelEvent>();
            var originAirports = airportCodeFinder.GetData(locationHandler.GetCityNameInEnglish(originPlace));
            var destinationAirports = airportCodeFinder.GetData(locationHandler.GetCityNameInEnglish(destinationPlace));
            foreach (var tuple in originAirports.Pairs(destinationAirports))
            {
                try
                {
                    travelEvents.AddRange(
                        transportDataProvider.GetData(new TransportParameters(currency, tuple.Item1,
                            tuple.Item2, outboundDate, 1, 0, "economy")));
                }
                catch (NetworkException)
                {
                    
                }
            }

            return travelEvents;
        }
    }
}