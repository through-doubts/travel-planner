using System;
using System.Linq;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Domain.TravelEvents
{
    public abstract class TravelEvent : ValueType<TravelEvent>,  ITravelEvent 
    {
        public string Name { get; }
    
        public FieldsInfo FieldsInfo { get; }

        public DateTime[] Dates { get; }
        public Money Cost { get; }
        public Location[] Locations { get; }
        public string Type { get; }


        public virtual string ToStringValue()
        {
            var lines = new string[]
            {
                $"{Type}. {string.Join(", ", FieldsInfo.LocationsHeaders.Zip(Locations, (h, l) => $"{h}: {l.Name}"))}",
                string.Join(", ", FieldsInfo.DatesHeaders.Zip(Dates, (h, d) => $"{h}: {d}")),
                $"Цена: {Cost.Amount} {Cost.Currency.Symbol}"
            };
            return string.Join("\n", lines);
        }

        public virtual string[] GetLocationsStrings()
        {
            return Locations.Select(l => l.Name).ToArray();
        }

        public virtual string[] GetDatesStrings()
        {
            return Dates.Select(d => d.ToString()).ToArray();
        }

        protected TravelEvent(string name, FieldsInfo fieldsInfo)
        {
            Name = name;
            FieldsInfo = fieldsInfo;
        }

        protected TravelEvent(DateTime[] dates, Location[] locations, Money cost, string type, string name, FieldsInfo fieldsInfo) : 
            this(name, fieldsInfo)
        {
            Dates = dates;
            Locations = locations;
            Cost = cost;
            if (!FieldsInfo.PossibleTypes.Contains(type))
                throw new ArgumentException($"Unknown type {type} for event {Name}");
            Type = type;
        }
    }
}
