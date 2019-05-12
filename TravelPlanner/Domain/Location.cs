using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Domain
{
    public class Location : ValueType<Location>
    {
        public GeoCoordinate Coordinate { get; }
        public string Name { get; }

        public Location(GeoCoordinate coordinate, string name)
        {
            Coordinate = coordinate;
            Name = name;
        }
    }
}
