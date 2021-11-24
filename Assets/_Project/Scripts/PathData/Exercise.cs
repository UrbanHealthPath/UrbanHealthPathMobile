namespace PolSl.UrbanHealthPath.PathData
{
    public abstract class Exercise
    {
        private string _exerciseId;
        private string _displayedName;
        private ExerciseCategory _category;
        private ExerciseSubcategory _subcategory;
        private string _description;
        private DifficultyRange _difficultyRange;
    }
}