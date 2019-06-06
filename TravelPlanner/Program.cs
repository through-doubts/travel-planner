using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TravelPlanner.Application;
using TravelPlanner.Domain;
using TravelPlanner.Infrastructure;
using TravelPlanner.UserInterface;
using TravelPlanner.Properties;
using Ninject;

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
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new ApplicationForm(application,
                new PathForm(application, new AddForm(application, new List<string>()))));
        }

        static IApplication GetApplication()
        {
            var container = new StandardKernel();
            container.Bind<IEventHandler>().To<TravelEventHandler>();
            container.Bind<ITravelEvent>().To<Housing>();
            container.Bind<ITravelEvent>().To<Transfer>();
            return container.Get<MainApplication>();
        }
    }
}
