using System.Collections.Generic;

namespace TravelPlanner.Application.Network.Skyscanner
{
    class SkyscannerResponse
    {
        public string SessionKey { get; set; }
        public Dictionary<string, object> Query { get; set; }
        public string Status { get; set; }
        public List<Itinerary> Itineraries { get; set; }
        public List<Leg> Legs { get; set; }
        public List<Dictionary<string, object>> Segments { get; set; }
        public List<Dictionary<string, object>> Carriers { get; set; }
        public List<Dictionary<string, object>> Agents { get; set; }
        public List<Place> Places { get; set; }
        public List<Dictionary<string, object>> Currencies { get; set; }
    }

    class Itinerary
    {
        public string OutboundLegId { get; set; }
        public List<PricingOption> PricingOptions { get; set; }
        public Dictionary<string, string> BookingDetailsLink { get; set; }
    }

    class PricingOption
    {
        public List<int> Agents { get; set; }
        public int QuoteAgeInMinutes { get; set; }
        public decimal Price { get; set; }
        public string DeeplinkUrl { get; set; }
    }

    class Leg
    {
        public string Id { get; set; }
        public List<int> SegmentIds { get; set; }
        public int OriginStation { get; set; }
        public int DestinationStation { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
        public int Duration { get; set; }
        public string JourneyMode { get; set; }
        public List<int> Stops { get; set; }
        public List<int> Carriers { get; set; }
        public List<int> OperatingCarriers { get; set; }
        public string Directionality { get; set; }
        public List<FlightNumbers> FlightNumbers { get; set; }
    }

    class FlightNumbers
    {
        public string FlightNumber { get; set; }
        public int CarrierId { get; set; }
    }

    class Place
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}