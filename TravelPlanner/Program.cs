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
using TravelPlanner.Infrastructure.Countries;

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
                new PathFormFactory(application,
                    new TravelEventFormFactory(application,
                        new GeographicDatabase(ExcelReader.TableToListOfRows(new MemoryStream(Resources.countries),
                            Encoding.GetEncoding("windows-1251"))).GetAllCities()))));
        }

        static IApplication GetApplication()
        {
            var container = new StandardKernel();
            container.Bind<IEventHandler>().To<TravelEventHandler>();
            container.Bind<IUserSessionHandler>().To<UserSessionHandler>();
            container.Bind<User>().ToConstant(new User(1));
            container.Bind<ITravelEvent>().To<Housing>();
            container.Bind<ITravelEvent>().To<Transfer>();
            return container.Get<MainApplication>();
        }
    }
}
