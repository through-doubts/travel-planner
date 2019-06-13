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
        public string[] LocationsHeaders { get; }
        public string[] DatesHeaders { get; }

        public DateTime[] Dates { get; }
        public Money Cost { get; }
        public Location[] Locations { get; }
        public string Type { get; }


        public virtual string ToStringValue()
        {
            var lines = new string[]
            {
                $"{Type}. {string.Join(", ", LocationsHeaders.Zip(Locations, (h, l) => $"{h}: {l.Name}"))}",
                string.Join(", ", DatesHeaders.Zip(Dates, (h, d) => $"{h}: {d}")),
                $"Цена: {Cost.Amount} {Cost.Currency}"
            };
            return string.Join("\n", lines);
        }

        public TravelEvent(string name, string[] possibleTypes, string[] locationsHeaders, string[] datesHeaders)
        {
            Name = name;
            PossibleTypes = possibleTypes;
            LocationsHeaders = locationsHeaders;
            DatesHeaders = datesHeaders;
        }

        public TravelEvent(DateTime[] dates, Location[] locations, Money cost, string type, string name,
            string[] possibleTypes, string[] locationsHeaders, string[] datesHeaders) :
            this(name, possibleTypes, locationsHeaders, datesHeaders)
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
