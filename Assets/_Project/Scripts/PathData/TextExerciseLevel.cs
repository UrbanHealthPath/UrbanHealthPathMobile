namespace PolSl.UrbanHealthPath.PathData
{
    public class TextExerciseLevel : ExerciseLevel
    {
        public string Description { get; }

        public TextExerciseLevel(DifficultyRange difficultyRange, string description) : base(difficultyRange)
        {
            Description = description;
        }
    }
}