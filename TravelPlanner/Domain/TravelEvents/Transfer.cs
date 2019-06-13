using System;
using System.Linq;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Domain.TravelEvents
{
    public class Transfer : TravelEvent
    {
        private const string name = "Перемещение";
        private static readonly string[] possibleTypes = { "Поезд", "Самолет", "Автобус", "Машина" };
        private static readonly string[] locationsHeaders = { "Место отправления", "Место прибытия" };
        private static readonly string[] datesHeaders = { "Время отправления", "Время прибытия" };
        private static readonly FieldsInfo fieldsInfo = new FieldsInfo(possibleTypes, locationsHeaders, datesHeaders);

        public Transfer() : base(name, fieldsInfo)
        {
        }

        public Transfer(DateTime[] dates, Location[] locations, Money cost, string type) :
            base(dates, locations, cost, type, name, fieldsInfo)
        {
        }

        public override string[] GetDatesStrings()
        {
            return Dates.Select(d => $"{d.ToShortDateString()} {d.ToShortTimeString()}").ToArray();
        }
    }
}
