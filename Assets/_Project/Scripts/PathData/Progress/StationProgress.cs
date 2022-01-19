using System;
using System.Collections.Generic;
using System.Linq;

namespace PolSl.UrbanHealthPath.PathData.Progress
{
    public class StationProgress
    {
        private readonly Station _station;

        private readonly Dictionary<ExerciseCategory, IReadOnlyList<Exercise>> _categoriesCache;
        private readonly Dictionary<ExerciseCategory, int> _categoriesProgress;

        public StationProgress(Station station)
        {
            _station = station;
            _categoriesCache = new Dictionary<ExerciseCategory, IReadOnlyList<Exercise>>();
            _categoriesProgress = new Dictionary<ExerciseCategory, int>();

            InitializeCategories(Enum.GetValues(typeof(ExerciseCategory)).Cast<ExerciseCategory>());
        }

        public Exercise GetCurrentExercise(ExerciseCategory category)
        {
            return IsCategoryFinished(category) ? null : _categoriesCache[category][_categoriesProgress[category]];
        }

        public void CompleteCurrentExercise(ExerciseCategory category)
        {
            _categoriesProgress[category]++;
        }

        public bool IsFinished()
        {
            return _categoriesProgress.All(x => IsCategoryFinished(x.Key));
        }

        public bool IsCategoryFinished(ExerciseCategory category)
        {
            return _categoriesProgress[category] >= _categoriesCache[category].Count;
        }

        private void InitializeCategories(IEnumerable<ExerciseCategory> categories)
        {
            foreach (ExerciseCategory category in categories)
            {
                CacheCategory(category);
                InitializeProgress(category);
            }
        }

        private void CacheCategory(ExerciseCategory category)
        {
            _categoriesCache.Add(category, _station.GetExercisesOfCategory(category));
        }

        private void InitializeProgress(ExerciseCategory category)
        {
            _categoriesProgress.Add(category, 0);
        }
    }
}