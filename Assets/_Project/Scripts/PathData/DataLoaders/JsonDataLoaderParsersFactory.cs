using Newtonsoft.Json.Linq;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    public class JsonDataLoaderParsersFactory
    {
        public DataLoaderParsers<JObject> Create()
        {
            JsonObjectParser<MediaFile> mediaFilesParser = new MediaFileJsonParser();
            JsonObjectParser<Exercise> exercisesParser = CreateExercisesParser();
            JsonObjectParser<Waypoint> waypointsParser = CreateWaypointsParser();
            JsonObjectParser<UrbanPath> urbanPathsParser = new UrbanPathJsonParser();

            return new DataLoaderParsers<JObject>(mediaFilesParser, exercisesParser,
                waypointsParser, urbanPathsParser);
        }

        private JsonObjectParser<Exercise> CreateExercisesParser()
        {
            JsonObjectParser<TextExerciseLevel> textExerciseLevelsParser = new TextExerciseLevelJsonParser();
            JsonObjectParser<VideoExerciseLevel> videoExerciseLevelParser = new VideoExerciseLevelJsonParser();
            JsonObjectParser<ImageExerciseLevel> imageExerciseLevelParser = new ImageExerciseLevelJsonParser();
            JsonObjectParser<ImageSelectionExerciseLevel> imageSelectionExerciseLevelParser =
                new ImageSelectionExerciseLevelJsonParser();
            JsonObjectParser<AnswerSelectionExerciseLevel> answerSelectionExerciseLevelParser =
                new AnswerSelectionExerciseLevelJsonParser();
            JsonObjectParser<HistoricalFactExerciseLevel> historicalFactExerciseLevelParser =
                new HistoricalFactExerciseLevelJsonParser();

            JsonObjectParser<ExerciseLevel>
                exerciseLevelsParser = new ExerciseLevelJsonParser(textExerciseLevelsParser, videoExerciseLevelParser,
                    imageExerciseLevelParser, imageSelectionExerciseLevelParser, answerSelectionExerciseLevelParser,
                    historicalFactExerciseLevelParser);

            return new ExerciseJsonParser(exerciseLevelsParser);
        }

        private JsonObjectParser<Waypoint> CreateWaypointsParser()
        {
            JsonObjectParser<Station> stationsParser = new StationJsonParser();

            return new WaypointJsonParser(stationsParser);
        }
    }
}