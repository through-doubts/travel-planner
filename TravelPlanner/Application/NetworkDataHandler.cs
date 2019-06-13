using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IUserSessionHandler userSessionHandler;

        public NetworkDataHandler(ITransportDataProvider transportDataProvider,
            IHousingDataProvider housingDataProvider, IAirportCodeFinder airportCodeFinder,
            ILocationHandler locationHandler, IUserSessionHandler userSessionHandler)
        {
            this.transportDataProvider = transportDataProvider;
            this.housingDataProvider = housingDataProvider;
            this.airportCodeFinder = airportCodeFinder;
            this.locationHandler = locationHandler;
            this.userSessionHandler = userSessionHandler;
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

        public void UpdatePrices(List<ITravelEvent> travelEvents)
        {
            foreach (var travelEvent in travelEvents.Where(t => !userSessionHandler.EventPriceIsFixated(t))
                .Where(t => t.Name == "Перемещение"))
            {
                var others = GetTransfers(travelEvent.Cost.Currency.ToString(), travelEvent.Locations[0].Name,
                    travelEvent.Locations[1].Name, travelEvent.Dates[0]);
                foreach (var other in others)
                {
                    if (other.Type == travelEvent.Type &&
                        other.Dates.SequenceEqual(travelEvent.Dates) &&
                        other.Locations.SequenceEqual(travelEvent.Locations))
                    {
                        userSessionHandler.CurrentTravelEvents.Replace(travelEvent, other);
                        return;
                    }
                }
            }
        }
    }
}