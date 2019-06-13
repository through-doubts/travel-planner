using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain;

namespace TravelPlanner.Application.Formats
{
    public interface IFormat : INameable
    {
        void SaveTravel(string fileName, Travel travel);
    }
}
