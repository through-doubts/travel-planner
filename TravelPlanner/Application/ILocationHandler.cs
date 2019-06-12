using System.Collections.Generic;
using TravelPlanner.Domain;

namespace TravelPlanner.Application
{
    public interface ILocationHandler
    {
        Location GetLocationByName(string name);

        IEnumerable<string> GetLocationsNames();

        bool CityExists(string name);
    }
}
