using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Domain
{
    public class Transfer : TravelEvent
    {
        private const string name = "Перемещение";
        private static readonly string[] possibleTypes = { "Поезд", "Самолет", "Автобус", "Машина" };
        private static readonly string[] locationsHeaders = { "Место отправления", "Место прибытия" };
        private static readonly string[] datesHeaders = { "Время отправления", "Время прибытия" };

        public Transfer() : base(name, possibleTypes, locationsHeaders, datesHeaders)
        {
        }

        public Transfer(DateTime[] dates, Location[] locations, Money cost, string type) :
            base(dates, locations, cost, type, name, possibleTypes, locationsHeaders, datesHeaders)
        {
        }
    }
}
