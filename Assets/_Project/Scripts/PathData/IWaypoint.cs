using System;

namespace PolSl.UrbanHealthPath.PathData
{
    public interface IWaypoint
    {
        event EventHandler Triggering;
        event EventHandler Triggered;

        string WaypointId { get; }
        Coordinates Coordinates { get; }
        string ZoneName { get; }
        
        void Trigger();
    }
}