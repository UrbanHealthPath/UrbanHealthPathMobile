using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath
{
    public class UrbanPathJsonParser : ValidatedJsonObjectParser<UrbanPath>
    {
        private const string ID_KEY = "pathId";
        private const string DISPLAYED_NAME_KEY = "displayedName";
        private const string APPROXIMATE_DISTANCE_KEY = "approximateDistanceInMeters";
        private const string IS_CYCLIC_KEY = "isCyclic";
        private const string MAP_URL_KEY = "mapUrl";
        private const string WAYPOINTS_KEY = "waypoints";

        public UrbanPathJsonParser() : base(new []{ID_KEY, DISPLAYED_NAME_KEY, APPROXIMATE_DISTANCE_KEY,
            IS_CYCLIC_KEY, MAP_URL_KEY, WAYPOINTS_KEY})
        {
        }

        protected override UrbanPath ParseJsonObject(JObject json)
        {
            List<LateBoundValue<Waypoint>> waypoints = new List<LateBoundValue<Waypoint>>();

            foreach (JToken waypoint in json[WAYPOINTS_KEY])
            {
                waypoints.Add(new LateBoundValue<Waypoint>(waypoint.Value<string>()));
            }

            return new UrbanPath(json[ID_KEY].Value<string>(), json[DISPLAYED_NAME_KEY].Value<string>(),
                json[APPROXIMATE_DISTANCE_KEY].Value<int>(), json[IS_CYCLIC_KEY].Value<bool>(),
                json[MAP_URL_KEY].Value<string>(), waypoints);
        }
    }
}