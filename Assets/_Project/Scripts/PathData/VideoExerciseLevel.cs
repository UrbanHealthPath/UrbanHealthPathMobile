namespace PolSl.UrbanHealthPath.PathData
{
    public class VideoExerciseLevel : ExerciseLevel
    {
        public string Description { get; }
        public LateBoundValue<MediaFile> VideoFile { get; }

        public VideoExerciseLevel(DifficultyRange difficultyRange, string description, LateBoundValue<MediaFile> videoFile) : base(difficultyRange)
        {
            Description = description;
            VideoFile = videoFile;
        }
    }
}