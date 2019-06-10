using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Domain
{
    public class Transfer : ValueType<Transfer>, ITravelEvent
    {
        public DateTimeInterval DateTimeInterval { get; }
        public Money Cost { get; }
        public TransferType Type { get; }
        public string Name => "Перемещение";
        public Type SubTypesType => typeof(TransferType);
        public Checkpoints Checkpoints { get; }

        public string ToStringValue()
        {
            return $"{Name} {Checkpoints.From}--{Checkpoints.To}"
        }

        public Transfer()
        {
        }

        public Transfer(DateTimeInterval dateTimeInterval, Checkpoints checkpoints, Money cost, TransferType type)
        {
            DateTimeInterval = dateTimeInterval;
            Checkpoints = checkpoints;
            Cost = cost;
            Type = type;
        }
    }

    public enum TransferType
    {
        Train,
        Plane,
        Bus,
        Car
    }
}
