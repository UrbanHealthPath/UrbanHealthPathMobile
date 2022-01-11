using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    public interface IUrbanPathsLoader
    {
        IList<UrbanPath> LoadUrbanPaths();
    }
}