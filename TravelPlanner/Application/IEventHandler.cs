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
        string[] GetEventSubTypes(string eventName);
        string[] GetEventLocationsHeaders(string eventName);
        string[] GetEventDatesHeaders(string eventName);
    }
}
