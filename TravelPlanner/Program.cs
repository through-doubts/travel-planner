using System;
using System.Windows.Forms;
using TravelPlanner.Application;
using TravelPlanner.Domain;
using TravelPlanner.UserInterface;

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
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new ApplicationForm(new MainApplication(new ITravelEvent[]
            {
                new Housing(),
                new Transfer(),
            })));
        }
    }
}
