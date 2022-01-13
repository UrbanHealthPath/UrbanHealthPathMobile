using System;

namespace PolSl.UrbanHealthPath.PathData.Progress
{
    public class PathProgressCheckpoint
    {
        public string WaypointId { get; }
        public DateTime ReachedAt { get; }

        public PathProgressCheckpoint(string waypointId, DateTime reachedAt)
        {
            WaypointId = waypointId;
            ReachedAt = reachedAt;
        }
    }
}