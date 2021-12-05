using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace PolSl.UrbanHealthPath.PathData
{
    public class UrbanPathBuilder
    {
        public UrbanPath Build()
        {
            List<MediaFile> mediaFiles = BuildMediaFiles("ExampleData/media_files");
            List<HistoricalFact> historicalFacts = BuildHistoricalFacts("ExampleData/historical_facts");
            List<Exercise> exercises = BuildExercises("ExampleData/exercises");
            List<IWaypoint> waypoints = BuildWaypoints("ExampleData/waypoints", exercises, historicalFacts, mediaFiles);
            List<UrbanPath> paths = BuildUrbanPaths("ExampleData/urbanpath", waypoints);

            return paths[0];
        }

        private List<MediaFile> BuildMediaFiles(string filePath)
        {
            JsonObjectParser<MediaFile> mediaFileParser = new MediaFileJsonParser();
            TextAsset mediaFilesFile = Resources.Load<TextAsset>(filePath);
            string mediaFilesJson = mediaFilesFile.text;

            JArray mediaFilesJArray = JsonConvert.DeserializeObject<JArray>(mediaFilesJson);
            List<MediaFile> mediaFiles = new List<MediaFile>();

            foreach (JObject mediaFile in mediaFilesJArray)
            {
                mediaFiles.Add(mediaFileParser.Parse(mediaFile));
            }

            return mediaFiles;
        }

        private List<UrbanPath> BuildUrbanPaths(string filePath, List<IWaypoint> waypoints)
        {
            JsonObjectParser<UrbanPath> urbanPathParser = new UrbanPathJsonParser(waypoints);

            TextAsset pathsFile = Resources.Load<TextAsset>(filePath);
            string pathsJson = pathsFile.text;

            JArray pathsJArray = JsonConvert.DeserializeObject<JArray>(pathsJson);

            List<UrbanPath> paths = new List<UrbanPath>();

            foreach (JObject path in pathsJArray)
            {
                paths.Add(urbanPathParser.Parse(path));
            }

            return paths;
        }

        private List<IWaypoint> BuildWaypoints(string filePath, List<Exercise> exercises, List<HistoricalFact> historicalFacts, List<MediaFile> mediaFiles)
        {
            JsonObjectParser<Station> stationParser = new StationJsonParser(exercises, historicalFacts, mediaFiles);
            JsonObjectParser<Waypoint> waypointParser = new WaypointJsonParser(stationParser);

            TextAsset waypointsFile = Resources.Load<TextAsset>(filePath);
            string waypointsJson = waypointsFile.text;

            JArray waypointsJArray = JsonConvert.DeserializeObject<JArray>(waypointsJson);

            List<IWaypoint> waypoints = new List<IWaypoint>();

            foreach (JObject waypoint in waypointsJArray)
            {
                waypoints.Add(waypointParser.Parse(waypoint));
            }

            return waypoints;
        }

        private List<Exercise> BuildExercises(string filePath)
        {
            JsonObjectParser<TextExerciseLevel> textExerciseLevelParser = new TextExerciseLevelJsonParser();
            JsonObjectParser<ExerciseLevel> exerciseLevelParser = new ExerciseLevelJsonParser(textExerciseLevelParser);
            JsonObjectParser<Exercise> exerciseParser = new ExerciseJsonParser(exerciseLevelParser);

            TextAsset exercisesFile = Resources.Load<TextAsset>(filePath);
            string exercisesJson = exercisesFile.text;

            JArray exercisesJArray = JsonConvert.DeserializeObject<JArray>(exercisesJson);
            List<Exercise> exercises = new List<Exercise>();

            foreach (JObject exercise in exercisesJArray)
            {
                exercises.Add(exerciseParser.Parse(exercise));
            }

            return exercises;
        }

        private List<HistoricalFact> BuildHistoricalFacts(string filePath)
        {
            JsonObjectParser<HistoricalFact> historicalFactParser = new HistoricalFactJsonParser();
            TextAsset historicalFactsFile = Resources.Load<TextAsset>(filePath);
            string historicalFactsJson = historicalFactsFile.text;

            JArray historicalFactsJArray = JsonConvert.DeserializeObject<JArray>(historicalFactsJson);
            List<HistoricalFact> historicalFacts = new List<HistoricalFact>();

            foreach (JObject historicalFact in historicalFactsJArray)
            {
                historicalFacts.Add(historicalFactParser.Parse(historicalFact));
            }

            return historicalFacts;
        }
    }
}