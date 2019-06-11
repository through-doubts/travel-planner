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
        public  List<Travel> Travels { get; }

        public User(int id) : base(id)
        {
            Travels = new List<Travel>();
        }
    }
}
