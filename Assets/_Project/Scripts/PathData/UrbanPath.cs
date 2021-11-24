using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData
{
    public class UrbanPath
    {
        private int _id;
        private string _displayedName;
        private List<IWaypoint> _waypoints;
        private int _approximateDistance;
        private bool _isCyclic;
    }
}