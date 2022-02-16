using Newtonsoft.Json.Linq;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    public class DataLoaderParsers<T>
    {
        public IParser<T, MediaFile> MediaFileParser { get; }
        public IParser<T, Exercise> ExerciseParser { get; }
        public IParser<T, Waypoint> WaypointParser { get; }
        public IParser<T, UrbanPath> UrbanPathParser { get; }
        public IParser<T, Test> TestParser { get; }

        public DataLoaderParsers(IParser<T, MediaFile> mediaFileParser, IParser<T, Exercise> exerciseParser, IParser<T, Waypoint> waypointParser, IParser<T, UrbanPath> urbanPathParser, IParser<T, Test> testParser)
        {
            MediaFileParser = mediaFileParser;
            ExerciseParser = exerciseParser;
            WaypointParser = waypointParser;
            UrbanPathParser = urbanPathParser;
            TestParser = testParser;
        }
    }
}