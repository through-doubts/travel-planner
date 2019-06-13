using System.Collections.Generic;

namespace TravelPlanner.Application.Network.Currency
{
    public interface ICurrencyDataProvider :
        INetworkDataProvider<Infrastructure.Currency, Dictionary<Infrastructure.Currency, double>>
    {
        
    }
}