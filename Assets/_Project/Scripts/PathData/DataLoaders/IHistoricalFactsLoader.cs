using System.Collections.Generic;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    public interface IHistoricalFactsLoader
    {
        IList<HistoricalFact> LoadHistoricalFacts();
    }
}