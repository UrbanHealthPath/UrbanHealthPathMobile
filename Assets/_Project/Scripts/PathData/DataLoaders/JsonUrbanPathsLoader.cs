using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    public class JsonUrbanPathsLoader : JsonDataLoader<UrbanPath>, IUrbanPathsLoader
    {
        public JsonUrbanPathsLoader(JToken json, IParser<JObject, UrbanPath> urbanPathParser) : base(json, urbanPathParser)
        {
        }

        public IList<UrbanPath> LoadUrbanPaths()
        {
            return LoadJsonData();
        }
    }
}