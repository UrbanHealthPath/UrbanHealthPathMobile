using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using Mapbox.Utils;
using UnityEngine;


namespace PolSl.UrbanHealthPath.Map
{
    public class StationFactory : MonoBehaviour
    {
        [SerializeField] private Transform _statation;

        [SerializeField] private AbstractMap _map;

        [SerializeField] private List<Vector2d> _coordinatesList;

        private ILocationProvider _locationProvider;

        private bool _initialized = false;

        private Vector3 _offset = new Vector3(0, 1, 0);

        public void Initialize(ILocationProvider locationProvider, List<Vector2d> coordinates)
        {
            _locationProvider = locationProvider;
            if (coordinates != null && coordinates.Count != 0)
            {
                _coordinatesList.AddRange(coordinates);
            }

            _locationProvider.LocationUpdated += LocationProviderFirstLocationUpdated;
            _initialized = true;
        }
        private void LocationProviderFirstLocationUpdated(Location location)
        {
            _locationProvider.LocationUpdated -= LocationProviderFirstLocationUpdated;
            if (_initialized)
            {
                foreach(Vector2d coordinates in _coordinatesList)
                {
                    Vector3 worldPosition = _map.GeoToWorldPosition(coordinates, false);
                    Transform newStation = Instantiate(_statation, worldPosition+_offset, _statation.rotation);
                    newStation.SetParent(this.transform);
                }
            }
        }
    }
}