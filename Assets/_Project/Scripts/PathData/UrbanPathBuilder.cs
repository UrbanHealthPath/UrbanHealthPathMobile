using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace PolSl.UrbanHealthPath.PathData
{
    public class UrbanPathBuilder
    {
        public UrbanPath Build()
        {
            TextAsset waypointsFile = Resources.Load<TextAsset>("ExampleData/waypoints");
            string waypointsJson = waypointsFile.text;

            JArray waypointsJArray = JsonConvert.DeserializeObject<JArray>(waypointsJson);

            List<IWaypoint> waypoints = new List<IWaypoint>();
            
            foreach (JObject waypoint in waypointsJArray)
            {
                waypoints.Add(new WaypointParser(waypoint).Parse());
            }
            
            TextAsset pathsFile = Resources.Load<TextAsset>("ExampleData/urbanpath");
            string pathsJson = pathsFile.text;
            
            JArray pathsJArray = JsonConvert.DeserializeObject<JArray>(pathsJson);
            
            List<UrbanPath> paths = new List<UrbanPath>();
            
            foreach (JObject path in pathsJArray)
            {
                paths.Add(new UrbanPathParser(path, waypoints).Parse());
            }

            return paths[0];
        }
    }
}