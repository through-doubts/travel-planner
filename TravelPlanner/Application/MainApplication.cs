using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Domain;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Application
{
    public class MainApplication : IApplication
    {
        public IEventHandler EventHandler { get; }   
        public IUserSessionHandler UserSessionHandler { get; }

        public MainApplication(IEventHandler eventHandler, IUserSessionHandler userSessionHandler)
        {
            EventHandler = eventHandler;
            UserSessionHandler = userSessionHandler;
        }

    }
}
