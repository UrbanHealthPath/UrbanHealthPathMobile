using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using PolSl.UrbanHealthPath.PathData;
using UnityEngine;
using Random = System.Random;

namespace PolSl.UrbanHealthPath
{
    public class StationJsonParser : WaypointJsonParser
    {
        private const string EXERCISES_KEY = "exercises";
        private const string DISPLAYED_NAME_KEY = "displayedName";
        private const string HISTORICAL_FACTS_KEY = "historicalFacts";
        private const string NAVIGATION_AUDIO_KEY = "navigationAudio";

        private readonly List<Exercise> _exercises;
        private readonly List<HistoricalFact> _historicalFacts;

        public StationJsonParser(List<Exercise> exercises, List<HistoricalFact> historicalFacts) : base(new[]
            {EXERCISES_KEY, DISPLAYED_NAME_KEY, HISTORICAL_FACTS_KEY, NAVIGATION_AUDIO_KEY})
        {
            _exercises = exercises;
            _historicalFacts = historicalFacts;
        }

        protected override Waypoint ParseJsonObject(JObject json)
        {
            JArray coordinatesArray = (JArray) json[COORDINATES_KEY];
            Coordinates coordinates =
                new Coordinates(coordinatesArray[0].Value<double>(), coordinatesArray[1].Value<double>());

            List<Exercise> exercises = new List<Exercise>();

            JArray jsonExercises = (JArray) json[EXERCISES_KEY];

            if (jsonExercises.HasValues)
            {
                bool hasExerciseGroups = false;

                foreach (JToken jToken in jsonExercises)
                {
                    if (jToken.Type == JTokenType.Array)
                    {
                        hasExerciseGroups = true;
                    }
                }

                JToken jsonExercisesToAdd;

                if (hasExerciseGroups)
                {
                    int chosenGroup = new Random().Next(0, jsonExercises.Count - 1);
                    jsonExercisesToAdd = jsonExercises[chosenGroup];
                }
                else
                {
                    jsonExercisesToAdd = jsonExercises;
                }

                foreach (JToken exercise in jsonExercisesToAdd)
                {
                    exercises.Add(_exercises.Find(x => x.ExerciseId == (string)exercise));
                }
            }

            List<HistoricalFact> historicalFacts = new List<HistoricalFact>();
            JArray jsonHistoricalFacts = (JArray) json[HISTORICAL_FACTS_KEY];
            
            foreach (JToken historicalFact in jsonHistoricalFacts)
            {
                historicalFacts.Add(_historicalFacts.Find(x =>
                    x.HistoricalFactId == (string)historicalFact));
            }

            return new Station(json[ID_KEY].Value<string>(), coordinates, json[ZONE_NAME_KEY].Value<string>(),
                json[DISPLAYED_NAME_KEY].Value<string>(), exercises, historicalFacts,
                json[NAVIGATION_AUDIO_KEY].Value<string>());
        }
    }
}