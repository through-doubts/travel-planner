using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Domain
{
    public class Travel : Entity<int>, INameable
    {
        public List<ITravelEvent> Events { get; }
        public string Name { get; }

        public Travel(int id, string name) : base(id)
        {
            Events = new List<ITravelEvent>();
            Name = name;
        }
    }
}
