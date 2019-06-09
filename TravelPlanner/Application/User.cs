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

        public User(int id) : base(id)
        {
            travels = new List<Travel>();
        }

        public void AddTravel(Travel travel)
        {
            travels.Add(travel);
        }

        public Travel GetTravel(string travelName)
        {
            return travels.FirstOrDefault(t => t.Name == travelName);
        }

        public void DeleteTravel(string travelName)
        {
            var travel = GetTravel(travelName);
            travels.Remove(travel);
        }

        public List<string> GetTravelsNames()
        {
            return travels.Select(t => t.Name).ToList();
        }
    }
}
