using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Application 
{
    public class TravelEventHandler : IEventHandler
    {
        private readonly ITravelEvent[] travelEvents;

        public TravelEventHandler(ITravelEvent[] travelEvents)
        {
            this.travelEvents = travelEvents;
        }

        public List<string> GetEventsNames()
        {
            return travelEvents.Select(e => e.Name).OrderBy(n => n).ToList();
        }

        private ITravelEvent GetEventExemplar(string eventName)
        {
            return travelEvents.FirstOrDefault(e => e.Name == eventName);
        }

        public Type GetEventType(string eventName)
        {
            var foundEvent = GetEventExemplar(eventName);
            if (foundEvent == null)
                throw new ArgumentException($"Unknown event name: {eventName}");
            return foundEvent.GetType();
        }

        public string[] GetEventSubTypes(string eventName)
        {
            return GetEventExemplar(eventName).PossibleTypes;
        }

        public string[] GetEventLocationsHeaders(string eventName)
        {
            return GetEventExemplar(eventName).LocationsHeaders;
        }

        public string[] GetEventDatesHeaders(string eventName)
        {
            return GetEventExemplar(eventName).DatesHeaders;
        }
    }
}
