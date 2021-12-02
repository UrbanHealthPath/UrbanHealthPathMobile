using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData
{
    public class Exercise
    {
        private string _displayedName;
        private ExerciseCategory _category;
        private ExerciseSubcategory _subcategory;
        private List<ExerciseLevel> _levels;

        public string ExerciseId { get; }

        public Exercise(string exerciseId, string displayedName, ExerciseCategory category, ExerciseSubcategory subcategory, List<ExerciseLevel> levels)
        {
            ExerciseId = exerciseId;
            _displayedName = displayedName;
            _category = category;
            _subcategory = subcategory;
            _levels = levels;
        }
    }
}