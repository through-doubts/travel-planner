using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain;

namespace TravelPlanner.Application
{
    public class UserSessionHandler : IUserSessionHandler
    {
        private readonly User user;
        private Travel currentTravel;
        private int currentTravelId;

        public UserSessionHandler(User user)
        {
            this.user = user;
        }

        public void AddTravel(string travelName)
        {
            var travel = new Travel(currentTravelId, travelName);
            currentTravelId++;
            currentTravel = travel;
            user.AddTravel(travel);
        }

        public void DeleteTravel(string travelName)
        {
            user.DeleteTravel(travelName);
        }

        public void ChangeCurrentTravel(string travelName)
        {
            var travel = user.GetTravel(travelName);
            currentTravel = travel;
        }

        public List<string> GetTravelsNames()
        {
            return user.GetTravelsNames();
        }

        public void AddEvent(ITravelEvent travelEvent)
        {
            currentTravel.AddEvent(travelEvent);
        }

        public void DeleteEvent(ITravelEvent travelEvent)
        {
            currentTravel.DeleteEvent(travelEvent);
        }

        public void ReplaceEvent(ITravelEvent oldTravelEvent, ITravelEvent newTravelEvent)
        {
            currentTravel.ReplaceEvent(oldTravelEvent, newTravelEvent);
        }

        public List<ITravelEvent> GetTravelEvents()
        {
            return currentTravel?.GetTravelEvents();
        }
    }
}

