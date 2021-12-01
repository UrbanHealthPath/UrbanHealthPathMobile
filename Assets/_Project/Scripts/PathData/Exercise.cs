using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData
{
    public abstract class Exercise
    {
        private string _exerciseId;
        private string _displayedName;
        private ExerciseCategory _category;
        private ExerciseSubcategory _subcategory;
        private DifficultyRange _difficultyRange;
        private List<ExerciseLevel> _levels;

        protected Exercise(string exerciseId, string displayedName, ExerciseCategory category, ExerciseSubcategory subcategory, DifficultyRange difficultyRange, List<ExerciseLevel> levels)
        {
            _exerciseId = exerciseId;
            _displayedName = displayedName;
            _category = category;
            _subcategory = subcategory;
            _difficultyRange = difficultyRange;
            _levels = levels;
        }
    }
}