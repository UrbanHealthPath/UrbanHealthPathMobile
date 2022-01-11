using System.IO;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    public class JsonFilesLoadersFactory : ILoadersFactory
    {
        private readonly string _filesDirectoryPath;
        private readonly IJsonFileReader _fileReader;
        private readonly DataLoaderParsers _parsers;
        
        public JsonFilesLoadersFactory(string filesDirectoryPath, IJsonFileReader fileReader, DataLoaderParsers parsers)
        {
            _filesDirectoryPath = filesDirectoryPath;
            _fileReader = fileReader;
            _parsers = parsers;
        }
        
        public IMediaFilesLoader CreateMediaFilesLoader()
        {
            return new JsonMediaFilesLoader(_fileReader.ReadJsonFromFile(GetFullFilePath("media_files")), _parsers.MediaFileParser);
        }

        public IHistoricalFactsLoader CreateHistoricalFactsLoader()
        {
            return new JsonHistoricalFactsLoader(_fileReader.ReadJsonFromFile(GetFullFilePath("historical_facts")),
                _parsers.HistoricalFactParser);
        }

        public IExercisesLoader CreateExercisesLoader()
        {
            return new JsonExercisesLoader(_fileReader.ReadJsonFromFile(GetFullFilePath("exercises")),
                _parsers.ExerciseParser);
        }

        public IWaypointsLoader CreateWaypointsLoader()
        {
            return new JsonWaypointsLoader(_fileReader.ReadJsonFromFile(GetFullFilePath("waypoints")),
                _parsers.WaypointParser);
        }

        public IUrbanPathsLoader CreateUrbanPathsLoader()
        {
            return new JsonUrbanPathsLoader(_fileReader.ReadJsonFromFile(GetFullFilePath("urban_paths")),
                _parsers.UrbanPathParser);
        }
        
        private string GetFullFilePath(string fileName)
        {
            return Path.Combine(_filesDirectoryPath, fileName);
        }
    }
}