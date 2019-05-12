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
        private List<ITravelEvent> events;

        public Travel(int id) : base(id)
        {
            events = new List<ITravelEvent>();
        }

        public void AddEvent(ITravelEvent travelEvent)
        {
            events.Add(travelEvent);
        }
    }
}
