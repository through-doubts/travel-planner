using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain;

namespace TravelPlanner.Application
{
    public class TravelFabric : IFabric<Travel>
    {
        private int currentId;

        public TravelFabric()
        {

        }

        public Travel Get(string name, params object[] parameters)
        {
            currentId++;
            return new Travel(currentId, name);
        }
    }
}
