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
        public string Name => "Проживание";
        public Type SubTypesType => typeof(HousingType);
        public Checkpoints Checkpoints { get; }
        public CheckpointType CheckpointType => CheckpointType.Stop;

        public string ToStringValue()
        {
            return $"{Name} {Checkpoints.Stop}";
        }

        public Housing()
        {
        }

        public Housing(DateTimeInterval dateTimeInterval, Checkpoints checkpoints, Money cost, HousingType type)
        {
            DateTimeInterval = dateTimeInterval;
            Cost = cost;
            Type = type;
            Checkpoints = checkpoints;
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
