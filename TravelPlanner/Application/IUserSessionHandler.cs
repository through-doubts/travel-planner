using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain;

namespace TravelPlanner.Application
{
    public interface IUserSessionHandler
    {
        void AddTravel(string travelName);
        void DeleteTravel(string travelName);
        void ChangeCurrentTravel(string travelName);
        List<string> GetTravelsNames();

        void AddEvent(ITravelEvent travelEvent);
        void ReplaceEvent(ITravelEvent oldTravelEvent, ITravelEvent newTravelEvent);
        List<ITravelEvent> GetTravelEvents();
    }
}
