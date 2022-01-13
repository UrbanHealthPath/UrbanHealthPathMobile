using System.Collections.Generic;
using System.Linq;
using PolSl.UrbanHealthPath.PathData.DataLoaders;

namespace PolSl.UrbanHealthPath.PathData
{
    public class ApplicationData : IApplicationData
    {
        public IList<MediaFile> MediaFiles { get; }
        public IList<HistoricalFact> HistoricalFacts { get; }
        public IList<Exercise> Exercises { get; }
        public IList<Waypoint> Waypoints { get; }
        public IList<UrbanPath> UrbanPaths { get; }

        public ApplicationData(IList<MediaFile> mediaFiles, IList<HistoricalFact> historicalFacts,
            IList<Exercise> exercises, IList<Waypoint> waypoints, IList<UrbanPath> urbanPaths)
        {
            MediaFiles = mediaFiles;
            HistoricalFacts = historicalFacts;
            Exercises = exercises;
            Waypoints = waypoints;
            UrbanPaths = urbanPaths;
        }

        public void SetLateBindings()
        {
            SetStationsLateBindings();
            SetUrbanPathsLateBindings();
            SetExercisesLateBindings();
        }

        private void SetStationsLateBindings()
        {
            List<Station> stations = Waypoints.OfType<Station>().ToList();

            foreach (Station station in stations)
            {
                foreach (LateBoundValue<Exercise> exercise in station.Exercises)
                {
                    exercise.InitializeValue(Exercises.FirstOrDefault(x => x.ExerciseId == exercise.Key));
                }

                foreach (LateBoundValue<HistoricalFact> historicalFact in station.HistoricalFacts)
                {
                    historicalFact.InitializeValue(HistoricalFacts.FirstOrDefault(x => x.HistoricalFactId == historicalFact.Key));
                }
                
                station.NavigationAudio.InitializeValue(MediaFiles.FirstOrDefault(x => x.MediaId == station.NavigationAudio.Key));
            }
        }

        private void SetUrbanPathsLateBindings()
        {
            foreach (UrbanPath urbanPath in UrbanPaths)
            {
                foreach (LateBoundValue<Waypoint> waypoint in urbanPath.Waypoints)
                {
                    waypoint.InitializeValue(Waypoints.FirstOrDefault(x => x.WaypointId == waypoint.Key));
                }
                
                urbanPath.PreviewImage.InitializeValue(MediaFiles.FirstOrDefault(x => x.MediaId == urbanPath.PreviewImage.Key));
            }
        }

        private void SetExercisesLateBindings()
        {
            foreach (Exercise exercise in Exercises)
            {
                List<VideoExerciseLevel> videoExerciseLevels = exercise.Levels.OfType<VideoExerciseLevel>().ToList();
                
                foreach (VideoExerciseLevel videoExerciseLevel in videoExerciseLevels)
                {
                    videoExerciseLevel.VideoFile.InitializeValue(MediaFiles.FirstOrDefault(x => x.MediaId == videoExerciseLevel.VideoFile.Key));
                }
            }
        }
    }
}