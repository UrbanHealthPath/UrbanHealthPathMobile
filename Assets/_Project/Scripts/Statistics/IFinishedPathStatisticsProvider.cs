using PolSl.UrbanHealthPath.PathData;
using PolSl.UrbanHealthPath.PathData.Progress;

namespace PolSl.UrbanHealthPath.Statistics
{
    public interface IFinishedPathStatisticsProvider
    {
        PathStatistics GetFinishedPathStatistics(IPathProgressManager progressManager, UrbanPath path);
    }
}