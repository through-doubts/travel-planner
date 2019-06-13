using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Application.Formats;
using TravelPlanner.Domain;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Application
{
    public class MainApplication : IApplication
    {
        public IEventHandler EventHandler { get; }   
        public IUserSessionHandler UserSessionHandler { get; }
        public IFabric<ITravelEvent> EventFabric { get; }
        public IFabric<Travel> TravelFabric { get; }
        public ILocationHandler LocationHandler { get; }
        public IFormatsHandler FormatsHandler { get; }

        public MainApplication(IEventHandler eventHandler, IUserSessionHandler userSessionHandler, IFabric<ITravelEvent> eventFabric,
            IFabric<Travel> travelFabric, ILocationHandler locationHandler, IFormatsHandler formatsHandler)
        {
            EventHandler = eventHandler;
            UserSessionHandler = userSessionHandler;
            EventFabric = eventFabric;
            TravelFabric = travelFabric;
            LocationHandler = locationHandler;
            FormatsHandler = formatsHandler;
        }

    }
}
