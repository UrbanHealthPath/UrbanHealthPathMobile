using UnityEngine;

namespace PolSl.UrbanHealthPath.Tools.TextLogger
{
    public class UnityLogger : ITextLogger
    {
        private const string DEFAULT_CATEGORY = "default";
        
        public void Log(LogVerbosity verbosity, string message)
        {
            Log(verbosity, DEFAULT_CATEGORY, message);
        }

        public void Log(LogVerbosity verbosity, string category, string message)
        {
            string messageWithCategory = $"[{category}] {message}";

            if (verbosity >= LogVerbosity.Error)
            {
                LogError(messageWithCategory);
            }
            else if (verbosity >= LogVerbosity.Warning)
            {
                LogWarning(messageWithCategory);
            }
            else
            {
                LogDebug(messageWithCategory);
            }
        }

        private void LogDebug(string message)
        {
            Debug.Log(message);
        }

        private void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }

        private void LogError(string message)
        {
            Debug.LogError(message);
        }
    }
}