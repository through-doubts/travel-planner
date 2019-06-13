using System.Collections.Generic;
using TravelPlanner.Domain;

namespace TravelPlanner.Application.Network
{
    public interface IHousingDataProvider : INetworkDataProvider<HousingParameters, List<Housing>>
    {
        
    }
}