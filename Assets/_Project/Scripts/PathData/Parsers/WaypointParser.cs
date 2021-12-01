using Newtonsoft.Json.Linq;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath
{
    public class WaypointParser : ValidatedJsonObjectParser<Waypoint>
    {
        private const string ID_KEY = "waypointId";
        private const string COORDINATES_KEY = "coordinates";
        private const string ZONE_NAME_KEY = "zoneName";
        private const string TYPE_KEY = "type";
        
        public WaypointParser() : base(new []{ID_KEY, COORDINATES_KEY, ZONE_NAME_KEY, TYPE_KEY})
        {
        }

        protected override Waypoint ParseJsonObject(JObject json)
        {
            return CreateWaypointBasedOnType(json[TYPE_KEY].Value<string>(), json);
        }
        
        private Waypoint CreateWaypointBasedOnType(string type, JObject json)
        {
            Coordinates coordinates = new Coordinates(json["coordinates"][0].Value<double>(), json["coordinates"][1].Value<double>());
            return new Station(json["waypointId"].Value<string>(), coordinates,
                json["zoneName"].Value<string>());
        }
    }
}