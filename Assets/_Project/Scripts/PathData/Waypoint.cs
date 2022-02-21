using System;

namespace PolSl.UrbanHealthPath.PathData
{
    public abstract class Waypoint
    {
        public string WaypointId { get; }
        public Coordinates Coordinates { get; }
        public string ZoneName { get; }

        protected Waypoint(string waypointId, Coordinates coordinates, string zoneName)
        {
            WaypointId = waypointId;
            Coordinates = coordinates;
            ZoneName = zoneName;
        }
    }
}