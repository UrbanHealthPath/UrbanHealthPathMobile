using System;

namespace PolSl.UrbanHealthPath.PathData
{
    /// <summary>
    /// Class that holds data for a waypoint - any point on path represented by coordinates.
    /// </summary>
    public abstract class Waypoint
    {
        public event EventHandler Triggering;
        public event EventHandler Triggered;
        
        public string WaypointId { get; }
        public Coordinates Coordinates { get; }
        public string ZoneName { get; }

        protected Waypoint(string waypointId, Coordinates coordinates, string zoneName)
        {
            WaypointId = waypointId;
            Coordinates = coordinates;
            ZoneName = zoneName;
        }
        
        public abstract void Trigger();

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