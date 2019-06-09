using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Domain
{
    public class Housing : ValueType<Housing>, ITravelEvent
    {
        public DateTimeInterval DateTimeInterval { get; }
        public Money Cost { get; }
        public HousingType Type { get; }
        public string Name => "Жилье";
        public Type SubTypesType => typeof(HousingType);
        public string ToStringValue()
        {
            return Name; 
        }

        public Housing()
        {
        }

        public Housing(DateTimeInterval dateTimeInterval, Money cost, HousingType type)
        {
            DateTimeInterval = dateTimeInterval;
            Cost = cost;
            Type = type;
        }
    }

    public enum HousingType
    {
        Hotel,
        Hostel, 
        Room, 
        Apartment
    }
}
