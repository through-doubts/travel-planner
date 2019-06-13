using System;

namespace TravelPlanner.Application.Network
{
    public class TransportParameters
    {
        public readonly string Currency;
        public readonly string OriginPlace;
        public readonly string DestinationPlace;
        public readonly DateTime OutboundDate;
        public readonly int AdultsCount;
        public readonly int ChildrenCount;
        public readonly string CabinClass;
        public TransportParameters(string currency, string originPlace, string destinationPlace, DateTime outboundDate,
            int adultsCount, int childrenCount, string cabinClass)
        {
            Currency = currency;
            OriginPlace = originPlace;
            DestinationPlace = destinationPlace;
            OutboundDate = outboundDate;
            AdultsCount = adultsCount;
            ChildrenCount = childrenCount;
            CabinClass = cabinClass;
        }
    }
}