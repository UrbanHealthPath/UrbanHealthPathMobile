using System.Collections.Generic;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using UnityEngine;
using PolSl.UrbanHealthPath.PathData;


namespace PolSl.UrbanHealthPath.Map
{
    public class StationFactory : MonoBehaviour
    {
        [SerializeField] private Transform _statation;

        [SerializeField] private Transform _halo;
        
        private List<Coordinates> _coordinatesList;
        
        private AbstractMap _map;

        private ILocationProvider _locationProvider;

        private bool _initialized = false;

        private Vector3 _offset = new Vector3(0, 1, 0);

        private int _currentStationIndex = 0;

        private Transform _currentStationTransform;

        public void Initialize(AbstractMap map, ILocationProvider locationProvider, List<Coordinates> coordinates)
        {
            _map = map;
            _locationProvider = locationProvider;
            if (coordinates != null && coordinates.Count != 0)
            {
                _coordinatesList = coordinates;
            }
            _locationProvider.LocationUpdated += LocationProviderFirstLocationUpdated;
            _initialized = true;
        }
        private void LocationProviderFirstLocationUpdated(Location location)
        {
            _locationProvider.LocationUpdated -= LocationProviderFirstLocationUpdated;
            if (_initialized&&_coordinatesList!=null&&_coordinatesList.Count!=0)
            {
                CreateStationHalo();
                foreach(Coordinates coordinates in _coordinatesList)
                {
                    Vector3 worldPosition = _map.GeoToWorldPosition(coordinates, false);
                    Transform newStation = Instantiate(_statation, worldPosition+_offset, _statation.rotation);
                    newStation.SetParent(this.transform);
                }
            }
        }
        
        private void CreateStationHalo()
        {
            Vector3 worldPosition = _map.GeoToWorldPosition(_coordinatesList[_currentStationIndex], false);
            _currentStationTransform =
                Instantiate(_halo, worldPosition + _offset, _halo.rotation);
            _currentStationTransform.SetParent(this.transform);
            _currentStationIndex++;
        }

        //ultimately I think this should just be an event listener of station finished event
        private void MoveStationHalo()
        {
            if (_currentStationTransform != null && _currentStationIndex == _coordinatesList.Count - 1)
            {
                Destroy(_currentStationTransform);
                return;
            }

            _currentStationTransform.position =
                _map.GeoToWorldPosition(_coordinatesList[_currentStationIndex], false) + _offset;
        }
    }
}