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

        private ITravelEvent GetTravelEventExemplar(string eventName)
        {
            var eventType = GetEventType(eventName);
            var constructor = eventType.GetConstructor(new Type[0]);
            return (ITravelEvent)constructor.Invoke(new object[0]);
        }

        public Type GetEventType(string eventName)
        {
            var foundEvent = travelEvents.FirstOrDefault(e => e.Name == eventName);
            if (foundEvent == null)
                throw new ArgumentException($"Unknown event name: {eventName}");
            return foundEvent.GetType();
        }

        public Type GetEventSubType(string eventName)
        {
            var travelEvent = GetTravelEventExemplar(eventName);
            return travelEvent.SubTypesType;
        }

        public ITravelEvent GetEvent(string name, params object[] parameters)
        {
            var eventType = GetEventType(name);

            var types = parameters.Select(p => p.GetType()).ToArray();
            var constructor = eventType.GetConstructor(types);
            if (constructor == null)
                throw new ArgumentException(
                    $"{eventType} doesn't have constructor with arguments " +
                    $"of types: {string.Join(", ", types.Select(t => t.Name))}");

            return (ITravelEvent)constructor.Invoke(parameters);
        }

        public ITravelEvent GetEvent(
            string name, DateTime startDate, DateTime endDate, Location[] locations,
            decimal amountOfMoney, string currency, string eventSubType)
        {
            var interval = new DateTimeInterval(startDate, endDate);
            var parsedCurrency = (Currency) Enum.Parse(typeof(Currency), currency);
            var money = new Money(parsedCurrency, amountOfMoney);
            var parsedEventSubType = Enum.Parse(GetEventSubType(name), eventSubType);
            var checkpoints = GetCheckpoints(locations, name);
            return GetEvent(name, interval, checkpoints, money, parsedEventSubType);
        }

        private Checkpoints GetCheckpoints(Location[] locations, string eventName)
        {
            var travelEvent = GetTravelEventExemplar(eventName);
            Checkpoints checkpoint = null;
            switch (travelEvent.CheckpointType)
            {
                case CheckpointType.Stop:
                    if (locations.Length != 1)
                        throw new ArgumentException($"Should be one location for TravelEvent {eventName}");
                    checkpoint = new Checkpoints(locations[0]);
                    break;
                case CheckpointType.Transfer:
                    if (locations.Length != 2)
                        throw new ArgumentException($"Should be two locations for TravelEvent {eventName}");
                    checkpoint = new Checkpoints(locations[0], locations[1]);
                    break;
            }
            return checkpoint;
        }
    }
}
