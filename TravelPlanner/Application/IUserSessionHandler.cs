using TravelPlanner.Domain;
using TravelPlanner.Domain.TravelEvents;

namespace TravelPlanner.Application
{
    public interface IUserSessionHandler
    {
        ListHandler<Travel> Travels { get; }
        ListHandler<ITravelEvent> CurrentTravelEvents { get; }

        void ChangeCurrentTravel(Travel travel);
        void FixateEventPrice(ITravelEvent travelEvent);
        bool EventPriceIsFixated(ITravelEvent travelEvent);

        void SaveUsers();
    }
}
