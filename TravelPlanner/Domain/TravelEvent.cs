using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Domain
{
    public class TravelEvent : ValueType<TravelEvent>,  ITravelEvent 
    {
        public string Name { get; }
    
        public string[] PossibleTypes { get; }

        public DateTime[] Dates { get; }
        public Money Cost { get; }
        public Location[] Locations { get; }
        public string Type { get; }


        public virtual string ToStringValue()
        {
            return $"{Name}";
        }

        public TravelEvent(string name, string[] possibleTypes)
        {
            Name = name;
            PossibleTypes = possibleTypes;
        }

        public TravelEvent(DateTime[] dates, Location[] locations, Money cost, string type, string name, string[] possibleTypes) : this(name, possibleTypes)
        {
            Dates = dates;
            Locations = locations;
            Cost = cost;
            if (!PossibleTypes.Contains(type))
                throw new ArgumentException($"Unknown type {type} for event {Name}");
            Type = type;
        }
    }
}
