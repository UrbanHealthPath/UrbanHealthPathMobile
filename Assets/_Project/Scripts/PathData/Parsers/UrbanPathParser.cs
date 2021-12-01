using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath
{
    public class UrbanPathParser : IParser<UrbanPath>
    {
        private readonly JObject _json;
        private List<IWaypoint> _waypoints;

        public UrbanPathParser(JObject json, List<IWaypoint> waypoints)
        {
            _json = json;
            _waypoints = waypoints;
        }

        public UrbanPath Parse()
        {
            List<IWaypoint> waypoints = new List<IWaypoint>();

            foreach (JToken waypoint in _json["waypoints"])
            {
                waypoints.Add(_waypoints.Find(x => x.WaypointId == waypoint.Value<string>()));
            }

            return new UrbanPath(_json["pathId"].Value<string>(), _json["displayedName"].Value<string>(),
                _json["approximateDistanceInMeters"].Value<int>(), _json["isCyclic"].Value<bool>(),
                _json["mapUrl"].Value<string>(), waypoints);
        }
    }
}