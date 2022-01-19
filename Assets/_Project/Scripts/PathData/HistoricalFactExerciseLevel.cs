namespace PolSl.UrbanHealthPath.PathData
{
    public class HistoricalFactExerciseLevel : TextExerciseLevel
    {
        public string Fact => Description;
        
        public HistoricalFactExerciseLevel(DifficultyRange difficultyRange, string fact) : base(difficultyRange, fact)
        {
        }
    }
}