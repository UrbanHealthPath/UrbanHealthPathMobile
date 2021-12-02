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
            JsonObjectParser<HistoricalFact> historicalFactParser = new HistoricalFactJsonParser();
            TextAsset historicalFactsFile = Resources.Load<TextAsset>("ExampleData/historical_facts");
            string historicalFactsJson = historicalFactsFile.text;

            JArray historicalFactsJArray = JsonConvert.DeserializeObject<JArray>(historicalFactsJson);
            List<HistoricalFact> historicalFacts = new List<HistoricalFact>();
            
            foreach (JObject historicalFact in historicalFactsJArray)
            {
                historicalFacts.Add(historicalFactParser.Parse(historicalFact));
            }
            
            JsonObjectParser<TextExerciseLevel> textExerciseLevelParser = new TextExerciseLevelJsonParser();
            JsonObjectParser<ExerciseLevel> exerciseLevelParser = new ExerciseLevelJsonParser(textExerciseLevelParser);
            JsonObjectParser<Exercise> exerciseParser = new ExerciseJsonParser(exerciseLevelParser);
            
            TextAsset exercisesFile = Resources.Load<TextAsset>("ExampleData/exercises");
            string exercisesJson = exercisesFile.text;

            JArray exercisesJArray = JsonConvert.DeserializeObject<JArray>(exercisesJson);
            List<Exercise> exercises = new List<Exercise>();
            
            foreach (JObject exercise in exercisesJArray)
            {
                exercises.Add(exerciseParser.Parse(exercise));
            }
            
            WaypointJsonParser stationParser = new StationJsonParser(exercises, historicalFacts);
            JsonObjectParser<Waypoint> waypointParser = new WaypointJsonParser(stationParser);
                
            TextAsset waypointsFile = Resources.Load<TextAsset>("ExampleData/waypoints");
            string waypointsJson = waypointsFile.text;

            JArray waypointsJArray = JsonConvert.DeserializeObject<JArray>(waypointsJson);

            List<IWaypoint> waypoints = new List<IWaypoint>();
            
            foreach (JObject waypoint in waypointsJArray)
            {
                waypoints.Add(waypointParser.Parse(waypoint));
            }    
                
            JsonObjectParser<UrbanPath> urbanPathParser = new UrbanPathJsonParser(waypoints);
            
            TextAsset pathsFile = Resources.Load<TextAsset>("ExampleData/urbanpath");
            string pathsJson = pathsFile.text;
            
            JArray pathsJArray = JsonConvert.DeserializeObject<JArray>(pathsJson);
            
            List<UrbanPath> paths = new List<UrbanPath>();
            
            foreach (JObject path in pathsJArray)
            {
                paths.Add(urbanPathParser.Parse(path));
            }

            return paths[0];
        }
    }
}