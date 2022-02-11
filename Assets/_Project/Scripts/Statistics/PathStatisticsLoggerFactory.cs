using PolSl.UrbanHealthPath.Tools.TextLogger;

namespace PolSl.UrbanHealthPath.Statistics
{
    public class PathStatisticsLoggerFactory : IPathStatisticsLoggerFactory
    {
        private readonly ITextLogger _logger;
        private readonly string _logFileName;

        public PathStatisticsLoggerFactory(ITextLogger textLogger, string logFileName)
        {
            _logger = textLogger;
            _logFileName = logFileName;
        }

        public IPathStatisticsLogger GetLogger(bool debugMode = false)
        {
            if (debugMode)
            {
                return new DebugPathStatisticsLogger(_logger);
            }

            return new CsvFilePathStatisticsLogger(_logger, _logFileName);
        }
    }
}