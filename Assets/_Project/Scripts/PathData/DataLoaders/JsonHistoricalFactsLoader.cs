using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    public class JsonHistoricalFactsLoader : JsonDataLoader<HistoricalFact>, IHistoricalFactsLoader
    {
        public JsonHistoricalFactsLoader(JToken json, IParser<JObject, HistoricalFact> historicalFactParser) : base(
            json, historicalFactParser)
        {
        }

        public IList<HistoricalFact> LoadHistoricalFacts()
        {
            return LoadJsonData();
        }
    }
}