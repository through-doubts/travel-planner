using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain;

namespace TravelPlanner.Application
{
    public interface IEventHandler
    {
        List<string> GetEventsNames();
        Type GetEventType(string eventName);
        Type GetEventSubType(string eventName);
        ITravelEvent GetEvent(string name, params object[] parameters);
        ITravelEvent GetEvent(
            string name, 
            DateTime startDate, DateTime endDate,
            Location[] locations,
            decimal amountOfMoney, string currency,
            string eventSubType);


    }
}
