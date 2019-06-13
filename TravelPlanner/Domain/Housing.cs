using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Domain
{
    public class Housing : TravelEvent
    {
        private const string name = "Проживание";
        private static readonly string[] possibleTypes = {"Отель", "Хостел", "Комната", "Апартаменты"};
        private static readonly string[] locationsHeaders = { "Место проживания" };
        private static readonly string[] datesHeaders = { "Дата заезда", "Дата выезда" };

        //public override string ToStringValue()
        //{
        //    return $"{Name} {Locations[0]}";
        //}

        public Housing() : base(name, possibleTypes, locationsHeaders, datesHeaders)
        {
        }

        public Housing(DateTime[] dates, Location[] locations, Money cost, string type) : 
            base(dates, locations, cost, type, name, possibleTypes, locationsHeaders, datesHeaders)
        {
        }
        
    }
}
