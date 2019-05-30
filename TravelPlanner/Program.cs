using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TravelPlanner.Application;
using TravelPlanner.Domain;
using TravelPlanner.Infrastructure;
using TravelPlanner.UserInterface;
using TravelPlanner.Properties;

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
            var application = new MainApplication(new ITravelEvent[]
            {
                new Housing(),
                new Transfer(),
            });
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new ApplicationForm(application,
                new PathForm(new AddForm(application, new List<string>()))));
            //ExcelReader.TableToListOfRows(new MemoryStream(Resources.countries),
            //Encoding.GetEncoding("windows-1251"))))));
        }
    }
}
