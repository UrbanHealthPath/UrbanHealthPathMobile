using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    public interface IApplicationData
    {
        IList<MediaFile> MediaFiles { get; }
        IList<Exercise> Exercises { get; }
        IList<Waypoint> Waypoints { get; }
        IList<UrbanPath> UrbanPaths { get; }
        IList<Test> Tests { get; }
    }
}