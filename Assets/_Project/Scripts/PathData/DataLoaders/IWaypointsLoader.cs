using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    public interface IWaypointsLoader
    {
        IList<Waypoint> LoadWaypoints();
    }
}