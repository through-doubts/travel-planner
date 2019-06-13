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
        ListHandler<Travel> Travels { get; }
        ListHandler<ITravelEvent> CurrentTravelEvents { get; }

        void ChangeCurrentTravel(Travel travel);
        void FixateEventPrice(ITravelEvent travelEvent);
        bool EventPriceIsFixated(ITravelEvent travelEvent);
    }
}
