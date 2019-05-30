using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Application
{
    public class User : Entity<int>
    {
        private readonly List<Travel> travels;
        private Travel currentTravel;
        private int currentTravelId;

        public User(int id) : base(id)
        {
            travels = new List<Travel>();
        }

        public void AddTravel(string travelName)
        {
            var travel = new Travel(currentTravelId, travelName);
            currentTravelId++;
            travels.Add(travel);
            currentTravel = travel;
        }

        public void ChangeCurrentTravel(string travelName)
        {
            var travel = travels.FirstOrDefault(t => t.Name == travelName);
            currentTravel = travel;
        }

        public List<ITravelEvent> GetTravelEvents()
        {
            return currentTravel.GetTravelEvents();
        }

        public void AddEvent(ITravelEvent travelEvent)
        {
            currentTravel.AddEvent(travelEvent);
        }

        public List<Travel> GetTravels()
        {
            return new List<Travel>(travels);
        }
    }
}
