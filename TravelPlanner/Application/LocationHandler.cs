using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain;

namespace TravelPlanner.Application
{
    public class LocationHandler : ILocationHandler
    {
        public Location GetLocationByName(string name)
        {
            return new Location(new GeoCoordinate(), name);
        }
    }
}
