using System.Collections.Generic;
using TravelPlanner.Domain;

namespace TravelPlanner.Application.Network
{
    public interface ITransportDataProvider : INetworkDataProvider<TransportParameters, List<Transfer>>
    {
        
    }
}