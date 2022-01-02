namespace PolSl.UrbanHealthPath.Tools.TextLogger
{
    public interface ITextLogger
    {
        void Log(LogVerbosity verbosity, string message);
        void Log(LogVerbosity verbosity, string category, string message);
    }
}