namespace PolSl.UrbanHealthPath.PathData
{
    public class TextExerciseLevel : ExerciseLevel
    {
        private string _description;

        public TextExerciseLevel(DifficultyRange difficultyRange, string description) : base(difficultyRange)
        {
            _description = description;
        }
    }
}