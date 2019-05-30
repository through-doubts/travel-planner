using System;
using System.Collections.Generic;

namespace TravelPlanner.Infrastructure.Network
{
    public class SkyscannerApiParameters
    {
        public static Dictionary<string, string> FromFlightParameters(FlightParameters parameters)
        {
            return new Dictionary<string, string>
            {
                {"country", "RU" },
                {"currency", parameters.Currency },
                {"locale", "ru-RU" },
                {"originPlace", parameters.OriginPlace },
                {"destinationPlace", parameters.DestinationPlace },
                {"outboundDate", parameters.OutboundDate.ToString("yyyy-mm-dd") },
                {"adults", parameters.AdultsCount.ToString() },
                {"children", parameters.ChildrenCount.ToString() },
                {"cabinClass", parameters.CabinClass }
            };
        }
    }
}