using TravelPlanner.Application.Fabrics;
using TravelPlanner.Application.Formats;
using TravelPlanner.Application.MetaInfoHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Application.Network;

using TravelPlanner.Domain;
using TravelPlanner.Domain.TravelEvents;

namespace TravelPlanner.Application
{
    public class MainApplication : IApplication
    {
        public static string SerializationFile = "users.json";

        public IEventHandler EventHandler { get; }   
        public IUserSessionHandler UserSessionHandler { get; }
        public IFabric<ITravelEvent> EventFabric { get; }
        public IFabric<Travel> TravelFabric { get; }
        public ILocationHandler LocationHandler { get; }
        public IFormatsHandler FormatsHandler { get; }
        public INetworkDataHandler NetworkDataHandler { get; }

        public MainApplication(IEventHandler eventHandler, IUserSessionHandler userSessionHandler, IFabric<ITravelEvent> eventFabric,
            IFabric<Travel> travelFabric, ILocationHandler locationHandler, INetworkDataHandler networkDataHandler, IFormatsHandler formatsHandler)

        {
            EventHandler = eventHandler;
            UserSessionHandler = userSessionHandler;
            EventFabric = eventFabric;
            TravelFabric = travelFabric;
            LocationHandler = locationHandler;
            FormatsHandler = formatsHandler;
            NetworkDataHandler = networkDataHandler;
        }

    }
}
