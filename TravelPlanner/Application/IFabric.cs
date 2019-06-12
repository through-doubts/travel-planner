using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain;

namespace TravelPlanner.Application
{
    public interface IFabric<T>
    {
        T Get(string name, params object[] parameters);
    }
}
