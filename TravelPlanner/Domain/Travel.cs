using System.Collections.Generic;
using TravelPlanner.Domain.TravelEvents;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Domain
{
    public class Travel : Entity<int>, INameable
    {
        public List<ITravelEvent> Events { get; }
        public string Name { get; }
        private readonly List<ITravelEvent> TravelEventsWithFixatedPrice;

        public Travel(int id, string name) : base(id)
        {
            Events = new List<ITravelEvent>();
            TravelEventsWithFixatedPrice = new List<ITravelEvent>();
            Name = name;
        }

        public void FixateEventPrice(ITravelEvent travelEvent)
        {
            TravelEventsWithFixatedPrice.Add(travelEvent);
        }

        public bool EventPriceIsFixated(ITravelEvent travelEvent)
        {
            return TravelEventsWithFixatedPrice.Contains(travelEvent);
        }
    }
}
