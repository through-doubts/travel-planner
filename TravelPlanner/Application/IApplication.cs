using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal.Execution;
using TravelPlanner.Domain;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Application
{
    interface IApplication
    {
        IEventHandler EventHandler { get; }

        void AddUser();
        void AddTravel(string travelName);
        void AddEvent(ITravelEvent travelEvent);
        List<Travel> GetTravels();

        void ChangeCurrentTravel(string travelName);
        List<ITravelEvent> GetTravelEvents();
    }
}
