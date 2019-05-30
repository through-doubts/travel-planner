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

        public Type GetEventType(string eventName)
        {
            var foundEvent = travelEvents.FirstOrDefault(e => e.Name == eventName);
            if (foundEvent == null)
                throw new ArgumentException($"Unknown event name: {eventName}");
            return foundEvent.GetType();
        }

        public Type GetEventSubType(string eventName)
        {
            var eventType = GetEventType(eventName);
            var constructor = eventType.GetConstructor(new Type[0]);
            var travelEvent = (ITravelEvent)constructor.Invoke(new object[0]);
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
            string name, DateTime startDate, DateTime endDate, 
            decimal amountOfMoney, string currency, string eventSubType)
        {
            var interval = new DateTimeInterval(startDate, endDate);
            var parsedCurrency = (Currency) Enum.Parse(typeof(Currency), currency);
            var money = new Money(parsedCurrency, amountOfMoney);
            var parsedEventSubType = Enum.Parse(GetEventSubType(name), eventSubType);
            return GetEvent(name, interval, money, parsedEventSubType);
        }
    }
}
