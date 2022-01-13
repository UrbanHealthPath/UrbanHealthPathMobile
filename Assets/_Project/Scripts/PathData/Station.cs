using System;
using System.Collections.Generic;
using UnityEngine;

namespace PolSl.UrbanHealthPath.PathData
{
    public class Station : Waypoint
    {
        public string DisplayedName { get; }
        public IList<LateBoundValue<Exercise>> Exercises { get; }
        public IList<LateBoundValue<HistoricalFact>> HistoricalFacts { get; }
        public LateBoundValue<MediaFile> NavigationAudio { get; }
        public LateBoundValue<MediaFile> Image { get; }

        public override void Trigger()
        {
        }

        public Station(string waypointId, Coordinates coordinates, string zoneName, string displayedName,
            IList<LateBoundValue<Exercise>> exercises, IList<LateBoundValue<HistoricalFact>> historicalFacts,
            LateBoundValue<MediaFile> navigationAudio, LateBoundValue<MediaFile> image) : base(waypointId, coordinates, zoneName)
        {
            DisplayedName = displayedName;
            Exercises = exercises;
            HistoricalFacts = historicalFacts;
            NavigationAudio = navigationAudio;
            Image = image;
        }
    }
}