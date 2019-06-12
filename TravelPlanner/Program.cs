﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TravelPlanner.Application;
using TravelPlanner.Domain;
using TravelPlanner.Infrastructure;
using TravelPlanner.UserInterface;
using TravelPlanner.Properties;
using Ninject;
using TravelPlanner.Infrastructure.Countries;
using TravelPlanner.UserInterface.EventForms;

namespace TravelPlanner
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var application = GetApplication();
            var travelEventFormFactory = new TravelEventFormFactory(application);
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new ApplicationForm(application,
                () => new PathForm(application, travelEventFormFactory),
                s => new EnterForm(s)));
        }

        static IApplication GetApplication()
        {
            var container = new StandardKernel();
            container.Bind<IEventHandler>().To<TravelEventHandler>();
            container.Bind<IUserSessionHandler>().To<UserSessionHandler>();
            container.Bind<IFabric<ITravelEvent>>().To<EventFabric>();
            container.Bind<IFabric<Travel>>().To<TravelFabric>();
            container.Bind<ILocationHandler>().To<LocationHandler>();
            container.Bind<User>().ToConstant(new User(1));
            container.Bind<ITravelEvent>().To<Housing>();
            container.Bind<ITravelEvent>().To<Transfer>();
            return container.Get<MainApplication>();
        }
    }
}
