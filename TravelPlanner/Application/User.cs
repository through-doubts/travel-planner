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

        public void AddTravel()
        {
            var travel = new Travel(currentTravelId);
            currentTravelId++;
            travels.Add(travel);
            currentTravel = travel;
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
