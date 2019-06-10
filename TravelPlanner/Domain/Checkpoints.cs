using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelPlanner.Domain
{
    public class Checkpoints
    {
        private enum CheckpointType
        {
            Stop,
            Transfer
        }

        private CheckpointType checkpointType;
        private readonly Location from;
        private Location to;
        private Location stop;
        public Location From
        {
            get
            {
                if (checkpointType != CheckpointType.Transfer)
                    throw new InvalidOperationException("CheckpointType isn't Transfer");
                return from;
            }
        }

        public Location To
        {
            get
            {
                if (checkpointType != CheckpointType.Transfer)
                    throw new InvalidOperationException("CheckpointType isn't Transfer");
                return to;
            }
        }

        public Location Stop
        {
            get
            {
                if (checkpointType != CheckpointType.Stop)
                    throw new InvalidOperationException("CheckpointType isn't Stop");
                return stop;
            }
        }

        public Checkpoints(Location from, Location to)
        {
            checkpointType = CheckpointType.Transfer;
            this.from = from;
            this.to = to;
        }

        public Checkpoints(Location stop)
        {
            checkpointType = CheckpointType.Stop;
            this.stop = stop;
        }
    }

    
}
