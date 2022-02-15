using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace PolSl.UrbanHealthPath.PathData.DataLoaders
{
    public class JsonTestLoader : JsonDataLoader<Test>
    {
        public JsonTestLoader(JToken json, IParser<JObject, Test> waypointsParser) : base(json ,waypointsParser)
        {
        }

        public IList<Test> LoadWaypoints()
        {
            return LoadJsonData();
        }
    }
}
