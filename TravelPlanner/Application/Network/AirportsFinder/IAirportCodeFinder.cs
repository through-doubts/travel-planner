using System.Collections.Generic;

namespace TravelPlanner.Application.Network.AirportsFinder
{
    public interface IAirportCodeFinder : INetworkDataProvider<string, List<string>>
    {
        
    }
}