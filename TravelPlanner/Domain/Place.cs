using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Device.Location;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Domain
{
    public class Place : ValueType<Place>
    {
        public GeoCoordinate Coordinate { get; }
        public string Name { get; }

        public Place(GeoCoordinate coordinate, string name)
        {
            Coordinate = coordinate;
            Name = name;
        }
    }
}
