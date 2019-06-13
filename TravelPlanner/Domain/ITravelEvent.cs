using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Domain
{
    public interface ITravelEvent : INameable
    {
        string[] PossibleTypes { get; }
        string[] LocationsHeaders { get; }
        string[] DatesHeaders { get; }

        DateTime[] Dates { get; }
        Money Cost { get; }
        Location[] Locations { get; }
        string Type { get; }

        string ToStringValue();
    }
}
