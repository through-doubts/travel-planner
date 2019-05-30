using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Domain
{
    public class Travel : Entity<int>
    {
        private readonly List<ITravelEvent> events;
        public string Name { get; }

        public Travel(int id, string name) : base(id)
        {
            events = new List<ITravelEvent>();
            Name = name;
        }

        public void AddEvent(ITravelEvent travelEvent)
        {
            events.Add(travelEvent);
        }

        public List<ITravelEvent> GetTravelEvents()
        {
            return new List<ITravelEvent>(events);
        }
    }
}
