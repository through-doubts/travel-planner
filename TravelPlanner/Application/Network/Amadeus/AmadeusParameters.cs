using System.Collections.Generic;
using TravelPlanner.Infrastructure.Network;

namespace TravelPlanner.Application.Network.Amadeus
{
    public class AmadeusParameters
    {
        public static Dictionary<string, string> FromHousingParameters(HousingParameters parameters)
        {
            return new Dictionary<string, string>
            {
                {"cityCode", parameters.City }, //IATA
                {"checkInDate", parameters.CheckInDate.ToString("YYYY-MM-DD") },
                {"checkOutDate", parameters.CheckOutDate.ToString("YYYY-MM-DD") },
                {"adults", parameters.AdultsCount.ToString() },
                {"radius", "4" },
                {"radiusUnit", "KM" },
                {"currency", parameters.Currency },
                {"includeClosed", "false" },
                {"bestRateOnly", "true" },
                {"view", "NONE" },
                {"sort", "PRICE" },
                {"lang", "ru-RU" }
            };
        }
    }
}