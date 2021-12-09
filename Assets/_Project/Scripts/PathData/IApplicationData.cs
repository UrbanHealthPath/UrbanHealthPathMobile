using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    public interface IApplicationData
    {
        IList<MediaFile> MediaFiles { get; }
        IList<HistoricalFact> HistoricalFacts { get; }
        IList<Exercise> Exercises { get; }
        IList<Waypoint> Waypoints { get; }
        IList<UrbanPath> UrbanPaths { get; }
    }
}