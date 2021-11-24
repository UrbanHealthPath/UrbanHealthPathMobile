using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData
{
    public class UrbanPath
    {
        private string _pathId;
        private string _displayedName;
        private List<IWaypoint> _waypoints;
        private int _approximateDistanceInMeters;
        private bool _isCyclic;
        private string _mapUrl;
    }
}