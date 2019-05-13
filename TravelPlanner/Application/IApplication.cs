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
        void AddUser();
        void AddTravel();
        void AddEvent(ITravelEvent travelEvent);
        List<string> GetEventsNames();

        ITravelEvent GetEvent(string name, DateTime startDate, DateTime endDate, decimal amountOfMoney,
            string currency, string eventSubType);
        List<Travel> GetTravels();

        Dictionary<string, Type> EventTypes { get; }
    }
}
