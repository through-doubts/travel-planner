using System;
using System.Collections.Generic;
using TravelPlanner.Domain;

namespace TravelPlanner.Application.Network
{
    public interface INetworkDataHandler
    {
        List<ITravelEvent> GetTransfers(string currency, string originPlace, string destinationPlace,
            DateTime outboundDate);
    }
}