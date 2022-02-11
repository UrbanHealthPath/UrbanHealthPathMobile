namespace PolSl.UrbanHealthPath.Statistics
{
    public interface IPathStatisticsLoggerFactory
    {
        IPathStatisticsLogger GetLogger(bool debugMode = false);
    }
}