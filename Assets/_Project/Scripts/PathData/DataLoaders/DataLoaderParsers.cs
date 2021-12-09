using Newtonsoft.Json.Linq;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    public class DataLoaderParsers
    {
        public IParser<JObject, MediaFile> MediaFileParser { get; }
        public IParser<JObject, HistoricalFact> HistoricalFactParser { get; }
        public IParser<JObject, Exercise> ExerciseParser { get; }
        public IParser<JObject, Waypoint> WaypointParser { get; }
        public IParser<JObject, UrbanPath> UrbanPathParser { get; }

        public DataLoaderParsers(IParser<JObject, MediaFile> mediaFileParser, IParser<JObject, HistoricalFact> historicalFactParser, IParser<JObject, Exercise> exerciseParser, IParser<JObject, Waypoint> waypointParser, IParser<JObject, UrbanPath> urbanPathParser)
        {
            MediaFileParser = mediaFileParser;
            HistoricalFactParser = historicalFactParser;
            ExerciseParser = exerciseParser;
            WaypointParser = waypointParser;
            UrbanPathParser = urbanPathParser;
        }
    }
}