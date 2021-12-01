using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData
{
    public class UrbanPath
    {
        private string _pathId;
        private string _displayedName;
        private int _approximateDistanceInMeters;
        private bool _isCyclic;

        public List<IWaypoint> Waypoints { get; }
        public string MapUrl { get; }

        public UrbanPath(string pathId, string displayedName, int approximateDistanceInMeters, bool isCyclic, string mapUrl, List<IWaypoint> waypoints)
        {
            _pathId = pathId;
            _displayedName = displayedName;
            _approximateDistanceInMeters = approximateDistanceInMeters;
            _isCyclic = isCyclic;
            MapUrl = mapUrl;
            Waypoints = waypoints;
        }
    }
}