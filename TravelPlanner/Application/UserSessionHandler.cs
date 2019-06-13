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

        public ListHandler<Travel> Travels => new ListHandler<Travel>(user.Travels);
        public ListHandler<ITravelEvent> CurrentTravelEvents => new ListHandler<ITravelEvent>(currentTravel.Events);

        public UserSessionHandler(User user)
        {
            this.user = user;
        }

        public void ChangeCurrentTravel(Travel travel)
        {
            currentTravel = travel;
        }

        public void FixateEventPrice(ITravelEvent travelEvent)
        {
            currentTravel.FixateEventPrice(travelEvent);
        }

        public bool EventPriceIsFixated(ITravelEvent travelEvent)
        {
            return currentTravel.EventPriceIsFixated(travelEvent);
        }
    }
}

