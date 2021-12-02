using System;
using System.Collections.Generic;
using UnityEngine;

namespace PolSl.UrbanHealthPath.PathData
{
    public class Station : Waypoint
    {
        private string _displayedName;
        private List<Exercise> _exercises;
        private List<HistoricalFact> _historicalFacts;
        private string _navigationAudio;

        public override void Trigger()
        {
            throw new NotImplementedException();
        }

        public Station(string waypointId, Coordinates coordinates, string zoneName, string displayedName, List<Exercise> exercises, List<HistoricalFact> historicalFacts, string navigationAudio) : base(waypointId, coordinates, zoneName)
        {
            _displayedName = displayedName;
            _exercises = exercises;
            _historicalFacts = historicalFacts;
            _navigationAudio = navigationAudio;
        }
    }
}