using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    public interface IExercisesLoader
    {
        IList<Exercise> LoadExercises();
    }
}