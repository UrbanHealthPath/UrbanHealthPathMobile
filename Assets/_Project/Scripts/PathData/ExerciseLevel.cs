namespace PolSl.UrbanHealthPath.PathData
{
    public abstract class ExerciseLevel
    {
        public DifficultyRange DifficultyRange { get; }

        protected ExerciseLevel(DifficultyRange difficultyRange)
        {
            DifficultyRange = difficultyRange;
        }
    }
}