using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData
{
    public class UrbanPath
    {
        public string PathId { get; }
        public string DisplayedName { get; }
        public int ApproximateDistanceInMeters { get; }
        public bool IsCyclic { get; }
        public IList<LateBoundValue<Waypoint>> Waypoints { get; }
        public string MapUrl { get; }

        public UrbanPath(string pathId, string displayedName, int approximateDistanceInMeters, bool isCyclic, string mapUrl, IList<LateBoundValue<Waypoint>> waypoints)
        {
            PathId = pathId;
            DisplayedName = displayedName;
            ApproximateDistanceInMeters = approximateDistanceInMeters;
            IsCyclic = isCyclic;
            MapUrl = mapUrl;
            Waypoints = waypoints;
        }
    }
}