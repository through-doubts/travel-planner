using System.Collections.Generic;
using System.Linq;
using TravelPlanner.Application.Serialization;
using TravelPlanner.Domain;
using TravelPlanner.Domain.TravelEvents;

namespace TravelPlanner.Application
{
    public class UserSessionHandler : IUserSessionHandler
    {
        private readonly List<User> users;
        private readonly User currentUser;
        private Travel currentTravel;
        private readonly ISerialization serialization;

        public ListHandler<Travel> Travels => new ListHandler<Travel>(currentUser.Travels);
        public ListHandler<ITravelEvent> CurrentTravelEvents => new ListHandler<ITravelEvent>(currentTravel.Events);

        public UserSessionHandler(ISerialization serialization)
        {
            users = serialization.LoadUsers();
            currentUser = users.Any() ? users[0] : new User(1);
            this.serialization = serialization;
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

        public void SaveUsers()
        {
            serialization.SaveUsers(users);
        }
    }
}

