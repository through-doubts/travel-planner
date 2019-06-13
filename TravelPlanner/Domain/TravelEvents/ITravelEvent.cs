using System;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Domain.TravelEvents
{
    public interface ITravelEvent : INameable
    {
        FieldsInfo FieldsInfo { get; }

        DateTime[] Dates { get; }
        Money Cost { get; }
        Location[] Locations { get; }
        string Type { get; }

        string ToStringValue();
        string[] GetLocationsStrings();
        string[] GetDatesStrings();
    }
}
