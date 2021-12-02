using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using PolSl.UrbanHealthPath.PathData;

namespace PolSl.UrbanHealthPath
{
    public class WaypointJsonParser : ValidatedJsonObjectParser<Waypoint>
    {
        protected const string ID_KEY = "waypointId";
        protected const string COORDINATES_KEY = "coordinates";
        protected const string ZONE_NAME_KEY = "zoneName";
        protected const string TYPE_KEY = "type";

        private readonly IDictionary<string, IParser<JObject, Waypoint>> _registeredTypesParsers;

        public WaypointJsonParser(WaypointJsonParser stationParser) : base(new[] {ID_KEY, COORDINATES_KEY, ZONE_NAME_KEY, TYPE_KEY})
        {
            _registeredTypesParsers = new Dictionary<string, IParser<JObject, Waypoint>>();
            _registeredTypesParsers.Add("station", stationParser);
        }
        
        protected WaypointJsonParser(string[] requiredKeys) : base(requiredKeys)
        {
        }

        protected override void ValidateJson(JObject json)
        {
            base.ValidateJson(json);

            // if (!_registeredTypesParsers.ContainsKey(json[TYPE_KEY].Value<string>()))
            // {
            //     throw new ParsingException();
            // }
            //
            // if (json[COORDINATES_KEY].Type != JTokenType.Array)
            // {
            //     throw new ParsingException();
            // }
        }

        protected override Waypoint ParseJsonObject(JObject json)
        {
            return _registeredTypesParsers[json[TYPE_KEY].Value<string>()].Parse(json);
        }
    }
}