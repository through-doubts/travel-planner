using System.Collections.Generic;
using System.Linq;
using TravelPlanner.Domain;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Application
{
    public class User : Entity<int>
    {
        public  List<Travel> Travels { get; }

        public User(int id) : base(id)
        {
            Travels = new List<Travel>();
        }

        public Travel FindTravelById(int travelId)
        {
            return Travels.FirstOrDefault(t => t.Id == travelId);
        }
    }
}
