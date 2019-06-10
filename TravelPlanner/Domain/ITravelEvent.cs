using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlanner.Infrastructure;

namespace TravelPlanner.Domain
{
    public interface ITravelEvent
    {
        DateTimeInterval DateTimeInterval { get; }
        Money Cost { get; }
        string Name { get; }
        Type SubTypesType { get; }
        Checkpoints Checkpoints { get; }
        CheckpointType CheckpointType { get; }
        string ToStringValue();
        


    }
}
