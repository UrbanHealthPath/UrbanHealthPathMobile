using Newtonsoft.Json.Linq;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath
{
    public class HistoricalFactJsonParser : ValidatedJsonObjectParser<HistoricalFact>
    {
        private const string ID_KEY = "historicalFactId";
        private const string NAME_KEY = "name";
        private const string DESCRIPTION_KEY = "description";
        
        public HistoricalFactJsonParser() : base(new[]{ID_KEY, NAME_KEY, DESCRIPTION_KEY})
        {
        }

        protected override HistoricalFact ParseJsonObject(JObject json)
        {
            return new HistoricalFact(json[ID_KEY].Value<string>(), json[NAME_KEY].Value<string>(), json[DESCRIPTION_KEY].Value<string>());
        }
    }
}