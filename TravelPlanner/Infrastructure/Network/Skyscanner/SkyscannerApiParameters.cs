using System.Collections.Generic;

namespace TravelPlanner.Infrastructure.Network.Skyscanner
{
    public class SkyscannerApiParameters
    {
        public static Dictionary<string, string> FromFlightParameters(TransportParameters parameters)
        {
            return new Dictionary<string, string>
            {
                {"country", "RU" },
                {"currency", parameters.Currency },
                {"locale", "ru-RU" },
                {"originPlace", parameters.OriginPlace }, //IATA
                {"destinationPlace", parameters.DestinationPlace }, //IATA
                {"outboundDate", parameters.OutboundDate.ToString("yyyy-MM-dd") },
                {"adults", parameters.AdultsCount.ToString() },
                {"children", parameters.ChildrenCount.ToString() },
                {"cabinClass", parameters.CabinClass }
            };
        }
    }
}