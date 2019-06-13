using System;
using System.Collections.Generic;
using System.Linq;
using TravelPlanner.Domain.TravelEvents;

namespace TravelPlanner.Application.MetaInfoHandlers
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

        public FieldsInfo GetFieldsInfo(string eventName)
        {
            return GetEventExemplar(eventName).FieldsInfo;
        }
    }
}
