using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal.Execution;
using TravelPlanner.Domain;

namespace TravelPlanner.Application
{
    interface IApplication
    {
        void AddUser();
        void AddTravel();
        void AddEvent(ITravelEvent travelEvent);
        List<string> GetEventsNames();
        ITravelEvent GetEvent(string name, params object[] parameters);
    }
}
