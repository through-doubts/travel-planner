using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanner.Infrastructure
{
    public class DateTimeInterval : ValueType<DateTimeInterval>
    {
        public DateTime Start { get; }
        public DateTime End { get; }

        public DateTimeInterval(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

    }
}
