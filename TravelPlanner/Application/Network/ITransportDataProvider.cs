using System.Collections.Generic;
using TravelPlanner.Domain;
using TravelPlanner.Domain.TravelEvents;

namespace TravelPlanner.Application.Network
{
    public interface ITransportDataProvider : INetworkDataProvider<TransportParameters, List<Transfer>>
    {
        
    }
}