using System;
using System.Collections.Generic;
using System.Linq;

namespace PolSl.UrbanHealthPath.PathData
{
    public class Station : Waypoint
    {
        public string DisplayedName { get; }
        public IList<LateBoundValue<Exercise>> Exercises { get; }
        public LateBoundValue<MediaFile> NavigationAudio { get; }
        public LateBoundValue<MediaFile> Image { get; }
        public LateBoundValue<MediaFile> IntroductionAudio { get; }
        public string Introduction { get; }

        public override void Trigger()
        {
            throw new NotImplementedException();
        }

        public Station(string waypointId, Coordinates coordinates, string zoneName, string displayedName,
            IList<LateBoundValue<Exercise>> exercises,
            LateBoundValue<MediaFile> navigationAudio, LateBoundValue<MediaFile> image, LateBoundValue<MediaFile> introductionAudio, string introduction) : base(waypointId, coordinates, zoneName)
        {
            DisplayedName = displayedName;
            Exercises = exercises;
            NavigationAudio = navigationAudio;
            Image = image;
            IntroductionAudio = introductionAudio;
            Introduction = introduction;
        }

        public IReadOnlyList<Exercise> GetExercisesOfCategory(ExerciseCategory category)
        {
            return Exercises.Select(x => x.Value).Where(x => x != null && x.Category == category).ToArray();
        }
    }
}