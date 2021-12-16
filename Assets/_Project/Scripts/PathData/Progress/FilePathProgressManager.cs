
using System.IO;
using Newtonsoft.Json;

namespace PolSl.UrbanHealthPath.PathData.Progress
{
    public class FilePathProgressManager : IPathProgressManager
    {
        private readonly string _filePath;
        private readonly JsonSerializer _serializer;
        
        public PathProgress CurrentProgress { get; private set; }

        public FilePathProgressManager(string filePath, JsonSerializer serializer)
        {
            _filePath = filePath;
            _serializer = serializer;
        }
        
        public void LoadProgress()
        {
            using StreamReader file = File.OpenText(_filePath);
            CurrentProgress = LoadProgressFromFile(file);
        }

        public void SaveProgress(PathProgress progress)
        {
            CurrentProgress = progress;

            using StreamWriter file = File.CreateText(_filePath);
            SaveProgressToFile(file, progress);
        }

        private void SaveProgressToFile(TextWriter file, PathProgress progress)
        {
            _serializer.Serialize(file, progress);
        }

        private PathProgress LoadProgressFromFile(TextReader file)
        {
            return (PathProgress)_serializer.Deserialize(file, typeof(PathProgress));
        }
    }
}