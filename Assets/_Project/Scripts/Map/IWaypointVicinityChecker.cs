using System;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath.Map
{
    public interface IWaypointVicinityChecker
    {
        event Action EnteringVicinity;
        event Action LeavingVicinity;

        void SetReferenceWaypoint(Waypoint waypoint);
        bool CheckIfInWaypointVicinity(Coordinates coordinates);
    }
}