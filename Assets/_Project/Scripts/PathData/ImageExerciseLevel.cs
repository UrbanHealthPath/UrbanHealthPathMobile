namespace PolSl.UrbanHealthPath.PathData
{
    public class ImageExerciseLevel : ExerciseLevel
    {
        public string Description { get; }
        public LateBoundValue<MediaFile> ImageFile { get; }

        public ImageExerciseLevel(DifficultyRange difficultyRange, string description, LateBoundValue<MediaFile> imageFile) : base(difficultyRange)
        {
            Description = description;
            ImageFile = imageFile;
        }
    }
}