using TravelPlanner.Application.Fabrics;
using TravelPlanner.Application.Formats;
using TravelPlanner.Application.MetaInfoHandlers;
using TravelPlanner.Domain;
using TravelPlanner.Domain.TravelEvents;

namespace TravelPlanner.Application
{
    interface IApplication
    {
        IEventHandler EventHandler { get; }
        IUserSessionHandler UserSessionHandler { get; }
        IFabric<ITravelEvent> EventFabric { get; }
        IFabric<Travel> TravelFabric { get; }
        ILocationHandler LocationHandler { get; }
        IFormatsHandler FormatsHandler { get; }
    }
}
