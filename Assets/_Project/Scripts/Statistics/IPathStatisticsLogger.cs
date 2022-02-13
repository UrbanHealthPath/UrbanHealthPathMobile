namespace PolSl.UrbanHealthPath.Statistics
{
    public interface IPathStatisticsLogger
    {
        void LogCompletedPathStatistics(PathStatistics pathStatistics);
        void LogCancelledPathStatistics(PathStatistics pathStatistics);
    }
}