using System.Collections.Generic;
using Mapbox.Directions;
using Mapbox.Unity;
using Mapbox.Unity.Map;
using Mapbox.Unity.MeshGeneration.Data;
using Mapbox.Unity.MeshGeneration.Modifiers;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Navigation
{
    public class DirectionsFactory : MonoBehaviour
    {
        [SerializeField] private LineMeshModifier _lineMeshModifier;
        
        [SerializeField] private Material _material;

        [SerializeField] private Transform _navigatedObject;
        
        [SerializeField] private Transform _destination;
        
        private AbstractMap _map;
        
        private Directions _directions;

        private bool _initialized = false;
        
        public void Initialize(AbstractMap map)
        {
            _map = map;
            _directions = MapboxAccess.Instance.Directions;
            _lineMeshModifier.Initialize();
            _initialized = true;
        }
        
        private void Query()
        {
            Vector2d[] wp = new Vector2d[2];
            wp[0] = _navigatedObject.GetGeoPosition(_map.CenterMercator, _map.WorldRelativeScale);
            wp[1] = _destination.GetGeoPosition(_map.CenterMercator, _map.WorldRelativeScale);
            DirectionResource directionResource = new DirectionResource(wp, RoutingProfile.Walking)
            {
                Steps = true
            };
            _directions.Query(directionResource, HandleDirectionsResponse);
        }
        
        private void HandleDirectionsResponse(DirectionsResponse response)
        {
            if (response == null || null == response.Routes || response.Routes.Count < 1)
            {
                return;
            }
            var meshData = new MeshData();
            var dat = new List<Vector3>();
            
            foreach (Vector2d point in response.Routes[0].Geometry)
            {
                dat.Add(Conversions.GeoToWorldPosition(point.x, point.y, _map.CenterMercator, 
                    _map.WorldRelativeScale).ToVector3xz());
            }
            
            var feat = new VectorFeatureUnity();
            feat.Points.Add(dat);
            
            _lineMeshModifier.Run(feat, meshData, _map.WorldRelativeScale);

            CreateGameObject(meshData);
        }

        private GameObject CreateGameObject(MeshData data)
        {
            GameObject directionsGO = new GameObject("direction waypoint " + " entity");
            var mesh = directionsGO.AddComponent<MeshFilter>().mesh;
            mesh.subMeshCount = data.Triangles.Count;
            mesh.SetVertices(data.Vertices);
            for (int i = 0; i < data.Triangles.Count; i++)
            {
                var triangle = data.Triangles[i];
                mesh.SetTriangles(triangle, i);
            }
            for (int i = 0; i < data.UV.Count; i++)
            {
                var uv = data.UV[i];
                mesh.SetUVs(i, uv);
            }
            mesh.RecalculateNormals();
            directionsGO.AddComponent<MeshRenderer>().material = _material;
            return directionsGO;
        }

        public void CallQuery()
        {
            if (_initialized)
            {
                Query();
            }
        }
    }
}