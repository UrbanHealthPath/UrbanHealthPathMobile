using System;

namespace PolSl.UrbanHealthPath.PathData
{
    public abstract class Waypoint : IWaypoint
    {
        public event EventHandler Triggering;
        public event EventHandler Triggered;
        
        public abstract void Trigger();
        public string WaypointId { get; }
        public Coordinates Coordinates { get; }
        public string ZoneName { get; }

        protected Waypoint(string waypointId, Coordinates coordinates, string zoneName)
        {
            WaypointId = waypointId;
            Coordinates = coordinates;
            ZoneName = zoneName;
        }

        protected virtual void OnTriggered()
        {
            Triggered?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnTriggering()
        {
            Triggering?.Invoke(this, EventArgs.Empty);
        }
    }
}