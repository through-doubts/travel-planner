using System;
using System.Linq;
using TravelPlanner.Application.MetaInfoHandlers;
using TravelPlanner.Domain;
using TravelPlanner.Domain.TravelEvents;

namespace TravelPlanner.Application.Fabrics
{
    public class EventFabric : IFabric<ITravelEvent>
    {
        private readonly IEventHandler eventHandler;
        public EventFabric(IEventHandler eventHandler)
        {
            this.eventHandler = eventHandler;
        }

        public ITravelEvent Get(string name, params object[] parameters)
        {
            var eventType = eventHandler.GetEventType(name);

            var types = parameters.Select(p => p.GetType()).ToArray();
            var constructor = eventType.GetConstructor(types);
            if (constructor == null)
                throw new ArgumentException(
                    $"{eventType} doesn't have constructor with arguments " +
                    $"of types: {string.Join(", ", types.Select(t => t.Name))}");

            return (ITravelEvent)constructor.Invoke(parameters);
        }
    }
}
