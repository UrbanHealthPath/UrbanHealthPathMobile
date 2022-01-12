namespace PolSl.UrbanHealthPath.Tools.TextLogger
{
    public class DummyLogger : ITextLogger
    {
        public void Log(LogVerbosity verbosity, string message)
        {
        }

        public void Log(LogVerbosity verbosity, string category, string message)
        {
        }
    }
}