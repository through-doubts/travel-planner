using System;
using System.Linq;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Domain.TravelEvents
{
    public class Housing : TravelEvent
    {
        private const string name = "Проживание";
        private static readonly string[] possibleTypes = { "Отель", "Хостел", "Комната", "Апартаменты" };
        private static readonly string[] locationsHeaders = { "Место проживания" };
        private static readonly string[] datesHeaders = { "Дата заезда", "Дата выезда" };
        private static readonly FieldsInfo fieldsInfo = new FieldsInfo(possibleTypes, locationsHeaders, datesHeaders);

        //public override string ToStringValue()
        //{
        //    return $"{Name} {Locations[0]}";
        //}

        public Housing(DateTime[] dates, Location[] locations, Money cost, string type) :
            base(dates, locations, cost, type, name, fieldsInfo)
        {
        }

        public Housing() : base(name, fieldsInfo)
        {
        }

        public override string[] GetLocationsStrings()
        {
            return new[] {Locations[0].Name, ""};
        }

        public override string[] GetDatesStrings()
        {
            return Dates.Select(d => d.ToShortDateString()).ToArray();
        }



    }
}
