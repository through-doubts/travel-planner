
using TravelPlanner.Application.Fabrics;
using TravelPlanner.Application.Formats;
using TravelPlanner.Application.MetaInfoHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal.Execution;
using TravelPlanner.Application.Network;
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
        INetworkDataHandler NetworkDataHandler { get; }

    }
}
