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
        private AudioClip _navigationAudio;
        
        public override void Trigger()
        {
            throw new NotImplementedException();
        }

        public Station(string waypointTag, Coordinates coordinates, string zoneName) : base(waypointTag, coordinates, zoneName)
        {
        }
    }
}