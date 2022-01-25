using Mapbox.Unity.Map;
using PolSl.UrbanHealthPath.PathData;
using UnityEngine;

namespace PolSl.UrbanHealthPath.Navigation
{
    public class NavigationPointProvider : MonoBehaviour
    {
        [SerializeField] private Transform _point;

        private Coordinates _coordinates;
        
        private AbstractMap _map;

        private bool _initialized = false;

        public void Initialize(AbstractMap map, Coordinates coordinates)
        {
            _map = map;
            _coordinates = coordinates;
            _initialized = true;
        }

        public void PutOnMap()
        {
            if (_initialized)
            {
                _point.position = _map.GeoToWorldPosition(_coordinates, false);
            }
        }
    }
}
