namespace PolSl.UrbanHealthPath.PathData
{
    public abstract class ExerciseLevel
    {
        private DifficultyRange _difficultyRange;

        protected ExerciseLevel(DifficultyRange difficultyRange)
        {
            _difficultyRange = difficultyRange;
        }
    }
}