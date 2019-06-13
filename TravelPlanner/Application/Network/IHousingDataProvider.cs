using System.Collections.Generic;
using TravelPlanner.Domain;
using TravelPlanner.Domain.TravelEvents;

namespace TravelPlanner.Application.Network
{
    public interface IHousingDataProvider : INetworkDataProvider<HousingParameters, List<Housing>>
    {
        
    }
}