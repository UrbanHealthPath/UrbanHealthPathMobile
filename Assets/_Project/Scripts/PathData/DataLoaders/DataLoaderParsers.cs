namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    public class DataLoaderParsers<T>
    {
        public IParser<T, MediaFile> MediaFileParser { get; }
        public IParser<T, Exercise> ExerciseParser { get; }
        public IParser<T, Waypoint> WaypointParser { get; }
        public IParser<T, UrbanPath> UrbanPathParser { get; }

        public DataLoaderParsers(IParser<T, MediaFile> mediaFileParser, IParser<T, Exercise> exerciseParser, IParser<T, Waypoint> waypointParser, IParser<T, UrbanPath> urbanPathParser)
        {
            MediaFileParser = mediaFileParser;
            ExerciseParser = exerciseParser;
            WaypointParser = waypointParser;
            UrbanPathParser = urbanPathParser;
        }
    }
}