using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain;

namespace TravelPlanner.Application
{
    public interface ILocationHandler
    {
        Location GetLocationByName(string name);

        IEnumerable<string> GetLocationsNames();
    }
}
